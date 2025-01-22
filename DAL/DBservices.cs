using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Data.Common;

namespace Server.DAL
{
    public class DBservices
    {
        public DBservices()
        {
        
        }
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //public int InitInsert(List<Game> listOfGames)
        //{

        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    int sumOfNumEff = 0;
        //    for (int i = 0; i < listOfGames.Count; i++)
        //    {
        //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        //        Game game = listOfGames[i];
        //        //paramDic.Add("@AppID", game.AppID);
        //        paramDic.Add("@Name", game.Name);
        //        paramDic.Add("@IsMac", game.Mac);
        //        paramDic.Add("@Price", game.Price);
        //        paramDic.Add("@Description", game.Description);
        //        paramDic.Add("@IsWindows", game.Windows);
        //        paramDic.Add("@Website", game.Website);
        //        paramDic.Add("@HeaderImage", game.HeaderImage);
        //        paramDic.Add("@IsLinux", game.Linux);
        //        paramDic.Add("@ScoreRank", game.ScoreRank);
        //        paramDic.Add("@Recommendations", game.Recommendations);
        //        paramDic.Add("@Publisher", game.Publisher);
        //        paramDic.Add("@ReleaseDate", game.ReleaseDate);
        //        paramDic.Add("@NumberOfPurchases", 0);
        //        cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertGame", con, paramDic);        // create the command
        //        try
        //        {
        //            int numEffected = cmd.ExecuteNonQuery(); // execute the command
        //            sumOfNumEff += numEffected;
        //        }
        //        catch (Exception ex)
        //        {
        //            // write to log
        //            if (con != null)
        //            {
        //                // close the db connection
        //                con.Close();
        //            }
        //            throw (ex);
        //        }
        //    }
        //    if (con != null)
        //    {
        //        // close the db connection
        //        con.Close();
        //    }
        //    return sumOfNumEff;
        //}
        
        //Create a new user
        public int InsertUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Name", user.Name);
            paramDic.Add("@Email", user.Email);
            paramDic.Add("@Password", user.Password);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertUser", con, paramDic);        // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int ChangeActiveStatus(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@isActive", user.IsActive);
            paramDic.Add("@id", user.Id);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_ChangeActiveStatus", con, paramDic);        // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //Update users details
        public User UpdateUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", user.Id);
            paramDic.Add("@name", user.Name);
            paramDic.Add("@email", user.Email);
            paramDic.Add("@password", user.Password);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUserDetails", con, paramDic);        // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                User u = new User();

                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                u.Name = dataReader["name"].ToString();
                u.Id = Convert.ToInt32(dataReader["id"]);
                return u;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Read User By Email and Password on Login
        public User ReadUserToLogin(string email, string password)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@email", email);
            paramDic.Add("@password", password);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetUserToLogin1", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                User u = new User();
                
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                u.Name = dataReader["name"].ToString();
                u.Id = Convert.ToInt32(dataReader["id"]);
                u.IsActive=Convert.ToBoolean(dataReader["isActive"]);
                return u;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public List<Object> getGamesAd()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            List<Object> games = new List<Object>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GamesListWithRevenue", con, paramDic); 

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //Object g = new Object();
                    games.Add(new
                    {
                        AppID = Convert.ToInt16(dataReader["AppID"]),
                        AppName = dataReader["AppName"].ToString(),
                        numOfPurcheses = Convert.ToInt16(dataReader["numOfPurcheses"]),
                        revenue = Convert.ToDouble(dataReader["revenue"])
                    });
                }
                return games;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //public List<User> UsersList()
        //{
        //    SqlConnection con;
        //    SqlCommand cmd;

        //    try
        //    {
        //        con = connect("myProjDB"); // create the connection
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    Dictionary<string, object> paramDic = new Dictionary<string, object>();

        //    List<User> users = new List<User>();

        //    cmd = CreateCommandWithStoredProcedureGeneral("SP_GetUsersDetails", con, paramDic);

        //    try
        //    {
        //        SqlDataReader dataReader = cmd.ExecuteReader();

        //        while (dataReader.Read())
        //        {
        //            User u = new User();
        //            u.Id = Convert.ToInt16(dataReader["UsersID"]);
        //            u.Name = dataReader["UsersName"].ToString();
        //            u.Email= dataReader["UsersEmail"].ToString();
        //            u.Password = dataReader["UsersPassword"].ToString();
        //            u.IsActive = Convert.ToBoolean(dataReader["ActiveStatus"]);
        //            users.Add(u);
        //        }
        //        return users;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            // close the db connection
        //            con.Close();
        //        }
        //    }
        //}

        public List<Object> UsersAList()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            List<Object> users = new List<Object>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UserInformation", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    users.Add(new
                    {
                        id = Convert.ToInt16(dataReader["id"]),
                        name = dataReader["name"].ToString(),
                        isActive = Convert.ToBoolean(dataReader["isActive"]),
                        numOfPurcheses = Convert.ToInt16(dataReader["NumOfGames"]),
                        amountSpent = Convert.ToDouble(dataReader["AmountSpent"])
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Read all games that dont exist in the users wish list
        public List<Game> ReadAllGames(int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);

            List<Game> games = new List<Game>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadAllGames", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Game g = new Game();
                    g.AppID = Convert.ToInt32(dataReader["appID"]);
                    g.Name = dataReader["name"].ToString();
                    g.ReleaseDate = Convert.ToDateTime(dataReader["releaseDate"]);
                    g.Price = Convert.ToDouble(dataReader["price"]);
                    g.Description = dataReader["description"].ToString();
                    g.HeaderImage = dataReader["headerImage"].ToString();
                    g.Website = dataReader["website"].ToString();
                    g.Windows = Convert.ToBoolean(dataReader["IsWindows"]);
                    g.Mac = Convert.ToBoolean(dataReader["IsMac"]);
                    g.Linux = Convert.ToBoolean(dataReader["IsLinux"]);
                    g.ScoreRank = Convert.ToInt32(dataReader["scoreRank"]);
                    g.Recommendations = dataReader["recommendations"].ToString();
                    g.Publisher = dataReader["publisher"].ToString();
                    g.NumberOfPurchases = Convert.ToInt32(dataReader["numberOfPurchases"]);
                    games.Add(g);
                }
                return games;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Read all games that exist in the users wish list
        public List<Game> ReadUsersWishList(int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);

            List<Game> wishList = new List<Game>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_ReadUsersWishList", con, paramDic);

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Game g = new Game();
                    g.AppID = Convert.ToInt32(dataReader["appID"]);
                    g.Name = dataReader["name"].ToString();
                    g.ReleaseDate = Convert.ToDateTime(dataReader["releaseDate"]);
                    g.Price = Convert.ToDouble(dataReader["price"]);
                    g.Description = dataReader["description"].ToString();
                    g.HeaderImage = dataReader["headerImage"].ToString();
                    g.Website = dataReader["website"].ToString();
                    g.Windows = Convert.ToBoolean(dataReader["IsWindows"]);
                    g.Mac = Convert.ToBoolean(dataReader["IsMac"]);
                    g.Linux = Convert.ToBoolean(dataReader["IsLinux"]);
                    g.ScoreRank = Convert.ToInt32(dataReader["scoreRank"]);
                    g.Recommendations = dataReader["recommendations"].ToString();
                    g.Publisher = dataReader["publisher"].ToString();
                    g.NumberOfPurchases = Convert.ToInt32(dataReader["numberOfPurchases"]);
                    wishList.Add(g);
                }
                return wishList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Filter games by price
        public List<Game> ReadGamesByMinPrice(double price, int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@price", price);
            paramDic.Add("@id", id);

            List<Game> filterdList = new List<Game>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetGamesFromWishListByMinPrice", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Game g = new Game();
                    g.AppID = Convert.ToInt32(dataReader["appID"]);
                    g.Name = dataReader["name"].ToString();
                    g.ReleaseDate = Convert.ToDateTime(dataReader["releaseDate"]);
                    g.Price = Convert.ToDouble(dataReader["price"]);
                    g.Description = dataReader["description"].ToString();
                    g.HeaderImage = dataReader["headerImage"].ToString();
                    g.Website = dataReader["website"].ToString();
                    g.Windows = Convert.ToBoolean(dataReader["IsWindows"]);
                    g.Mac = Convert.ToBoolean(dataReader["IsMac"]);
                    g.Linux = Convert.ToBoolean(dataReader["IsLinux"]);
                    g.ScoreRank = Convert.ToInt32(dataReader["scoreRank"]);
                    g.Recommendations = dataReader["recommendations"].ToString();
                    g.Publisher = dataReader["publisher"].ToString();
                    g.NumberOfPurchases = Convert.ToInt32(dataReader["numberOfPurchases"]);
                    filterdList.Add(g);
                }
                return filterdList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Read games by ScoreRank
        public List<Game> ReadGamesByMinRank(int scoreRank, int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@ScoreRank", scoreRank);
            paramDic.Add("@id", id);

            List<Game> filterdList = new List<Game>();

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetGamesFromWishListByMinRank", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Game g = new Game();
                    g.AppID = Convert.ToInt32(dataReader["appID"]);
                    g.Name = dataReader["name"].ToString();
                    g.ReleaseDate = Convert.ToDateTime(dataReader["releaseDate"]);
                    g.Price = Convert.ToDouble(dataReader["price"]);
                    g.Description = dataReader["description"].ToString();
                    g.HeaderImage = dataReader["headerImage"].ToString();
                    g.Website = dataReader["website"].ToString();
                    g.Windows = Convert.ToBoolean(dataReader["IsWindows"]);
                    g.Mac = Convert.ToBoolean(dataReader["IsMac"]);
                    g.Linux = Convert.ToBoolean(dataReader["IsLinux"]);
                    g.ScoreRank = Convert.ToInt32(dataReader["scoreRank"]);
                    g.Recommendations = dataReader["recommendations"].ToString();
                    g.Publisher = dataReader["publisher"].ToString();
                    g.NumberOfPurchases = Convert.ToInt32(dataReader["numberOfPurchases"]);
                    filterdList.Add(g);
                }
                return filterdList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Insert game into a users wish list
        public int AddGameToFavorites(int id, int appID)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);
            paramDic.Add("@appID", appID);


            cmd = CreateCommandWithStoredProcedureGeneral("SP_UserBuyGame", con, paramDic);          // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Delete a game from a users wish list
        public int DeleteFromWishList(int id, int appID)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@id", id);
            paramDic.Add("@appID", appID);


            cmd = CreateCommandWithStoredProcedureGeneral("SP_UserDeleteGame", con, paramDic);          // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }

            return cmd;
        }

    }
}
