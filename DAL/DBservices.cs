using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;
using System.Xml.Linq;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data.SqlClient;
//using System.Data;
//using System.Text;
//using RuppinProj.Models;
//using CCEC___DR.BL;
using System.Net.NetworkInformation;
using System.Data.Common;

namespace Server.DAL
{
    public class DBservices
    {
        public DBservices()
        {
            //this.email = email;
            //this.password = password;
        }
        // yes we can

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

            //paramDic.Add("@AppID", game.AppID);
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
                //if (con != null)
                //{
                //    // close the db connection
                //    con.Close();
                //}
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


        //Read User By Email and Password on Login
        public bool ReadUserToLogin(string email, string password)
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

            // List<Flight> flights = new List<Flight>();
            //User user = new User();

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@email", email);
            paramDic.Add("@password", password);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GetUserToLogin", con, paramDic);

            SqlDataReader reader = cmd.ExecuteReader();

            //if (!reader.HasRows) // בדיקה אם אין שורות בתוצאה
            //{
            //    // אם אין תוצאות
            //    Console.WriteLine("No user found.");
            //    return null; // או פעולת שגיאה אחרת
            //}

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //while (dataReader.Read())
                //{
                dataReader.Read();
                User u = new User();
                // f.Id = Convert.ToInt32(dataReader["Id"]);
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                //f.Price = Convert.ToDouble(dataReader["Price"]);
                //flights.Add(f);
                // }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                //throw (ex);
                return false;
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

       
    }

}
