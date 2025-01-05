﻿
using Server.DAL;

namespace Server.Models
{
    public class User
    {
        int id;
        string name;
        string email;
        string password;

        public User() { }
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        //public static List<User> usersList = new List<User>();

        //public bool Insert()
        //{
        //    for (int i = 0; i < usersList.Count; i++)
        //    {
        //        if (this.id == usersList[i].id)
        //            return false;
        //    }
        //    usersList.Add(this);
        //    return true;
        //}

        public User updateUserDet(User user)
        {
            DBservices db = new DBservices();
            return db.UpdateUser(user);
        }


        //public List<User> Read()
        //{
        //    return usersList;
        //}

        public int Register()
        {
            DBservices db = new DBservices();
            return db.InsertUser(this);
        }
        public User isValidUser(string email, string password)
        {
            DBservices dBservices = new DBservices();
            return dBservices.ReadUserToLogin(email, password);

            throw new Exception($"User not found.");
        }
    }
}
