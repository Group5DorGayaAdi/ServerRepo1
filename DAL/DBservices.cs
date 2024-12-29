using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;
using System.Xml.Linq;

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

        public int InitInsert(List<Game> listOfGames)
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
            int sumOfNumEff = 0;
            for (int i = 0; i < listOfGames.Count; i++)
            {
                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                Game game = listOfGames[i];
                paramDic.Add("@AppID", game.AppID);
                paramDic.Add("@Name", game.Name);
                //paramDic.Add("@IsMac", game.Mac);
                paramDic.Add("@Price", game.Price);
                paramDic.Add("@Description", game.Description);
                paramDic.Add("@IsWindows", game.Windows);
                paramDic.Add("@Website", game.Website);
                paramDic.Add("@HeaderImage", game.HeaderImage);
                paramDic.Add("@IsLinux", game.Linux);
                paramDic.Add("@ScoreRank", game.ScoreRank);
                paramDic.Add("@Recommendations", game.Recommendations);
                paramDic.Add("@Publisher", game.Publisher);
                paramDic.Add("@ReleaseDate", game.ReleaseDate);



                cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertGame", con, paramDic);        // create the command

                try
                {
                    int numEffected = cmd.ExecuteNonQuery(); // execute the command
                    sumOfNumEff += numEffected;
                }
                catch (Exception ex)
                {
                    // write to log
                    if (con != null)
                    {
                        // close the db connection
                        con.Close();
                    }
                    throw (ex);
                }


            }
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            return sumOfNumEff;
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
