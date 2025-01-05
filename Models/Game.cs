using System.Text.Json.Serialization;
using Server.DAL;

namespace Server.Models
{
    public class Game
    {
        private int appID;
        private string name;
        private DateTime releaseDate;
        private double price;
        private string description;
        private string headerImage;
        private string website;
        private bool windows;
        private bool mac;
        private bool linux;
        private int scoreRank;
        private string recommendations;
        private string publisher;
        private int numberOfPurchases;
       // public static List<Game> gamesList = new List<Game>();
        public Game() { }

        public Game(int appID, string name, DateTime releaseDate, double price, string description, string headerImage, string website, bool windows, bool mac, bool linux, int scoreRank, string recommendations, string publisher, int numberOfPurchases)
        {
            AppID = appID;
            Name = name;
            ReleaseDate = releaseDate;
            Price = price;
            Description = description;
            HeaderImage = headerImage;
            Website = website;
            Windows = windows;
            Mac = mac;
            Linux = linux;
            ScoreRank = scoreRank;
            Recommendations = recommendations;
            Publisher = publisher;
            NumberOfPurchases = numberOfPurchases;
        }

        public int AppID { get => appID; set => appID = value; }
        public string Name { get => name; set => name = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public double Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string HeaderImage { get => headerImage; set => headerImage = value; }
        public string Website { get => website; set => website = value; }
        public bool Windows { get => windows; set => windows = value; }
        public bool Linux { get => linux; set => linux = value; }
        public int ScoreRank { get => scoreRank; set => scoreRank = value; }
        public string Recommendations { get => recommendations; set => recommendations = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public int NumberOfPurchases { get => numberOfPurchases; set => numberOfPurchases = value; }
        public bool Mac { get => mac; set => mac = value; }

        public int InsertToFavorite(int id, int appID)
        {
            DBservices db = new DBservices();
            return db.AddGameToFavorites(id,appID);
        }
        public List<Game> Read(int id)
        {
            DBservices db = new DBservices();
            return db.ReadAllGames(id);
        }

        public List<Game> ReadWishList(int id)
        {
            DBservices db = new DBservices();
            return db.ReadUsersWishList(id);
        }

        public List<Game> GetGameByPrice(double price,int id)
        {
            DBservices db = new DBservices();
            return db.ReadGamesByMinPrice(price, id);
        }

        public List<Game> GetGamesByRankScore(int rank, int id)
        {
            DBservices db = new DBservices();
            return db.ReadGamesByMinRank(rank, id);
        }

        public int DeleteFromGamesList(int id, int appID)
        {
            DBservices db = new DBservices();
            return db.DeleteFromWishList(id,appID);
        }

        //static public bool InsertAllGamesOnce(List<Game> AllGames)
        //{
        //    DBservices db = new DBservices();
        //    int sumOfNumEff = db.InitInsert(AllGames);
        //    if (sumOfNumEff < 99)
        //    {
        //        throw new Exception("not all Games was Inserted");
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
