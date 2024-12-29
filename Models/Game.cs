using System.Text.Json.Serialization;
using Server.DAL;

namespace Server.Models
{
    public class Game
    {
        // the right one

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
        public static List<Game> gamesList = new List<Game>();
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

        public bool Insert()
        {
            for (int i = 0; i < gamesList.Count; i++)
            {
                if (this.appID == gamesList[i].appID)
                    return false;
            }
            gamesList.Add(this);
            return true;
        }
        public List<Game> Read()
        {
            return gamesList;
        }

        public List<Game> GetGameByPrice(double price)
        {
            List<Game> selectedList = new List<Game>();
            foreach (Game g in gamesList)
            {
                if (g.price >= price)
                    selectedList.Add(g);
            }
            return selectedList;
        }

        public List<Game> GetGamesByRankScore(int rank)
        {
            List<Game> selectedList = new List<Game>();
            foreach (Game g in gamesList)
            {
                if (g.scoreRank >= rank)
                    selectedList.Add(g);
            }
            return selectedList;
        }

        public void DeleteFromGamesList(int id)
        {
            if (gamesList.Count == 0)
            {
                throw new Exception("No Games in the list");
            }
            Game gameToRemove = gamesList.Find(game => game.appID == id);
            if (gameToRemove == null)
            {
                throw new Exception($"Game with ID {id} not found.");
            }
            gamesList.Remove(gameToRemove);
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
