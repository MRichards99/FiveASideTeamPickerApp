using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace FantasyTeamsDBSharedCode
{
    [Table("Players")]
    public class Player
    {

        //private int playerID;
        private string firstName;
        private string surname;
        //private int premierTeamID;
        //private int fantasyTeamID;
        //private int positionID;
        //private decimal price;

        public Player()
        {
            // TODO - Implement constructors on all these classes
        }

        public Player(string firstName, string surname, int premierTeamID, int fantasyTeamID, int positionID, double price)
        {
            this.Firstname = firstName;
            this.Surname = surname;
            this.FantasyTeamID = fantasyTeamID;
            this.PositionID = positionID;
            this.Price = price;
        }

        [PrimaryKey, AutoIncrement]
        public int PlayerID { get; set; }
        public string Firstname
        {
            get { return firstName; }
            set
            {
                // TODO - Reimplement error handling on this setter
                
                /*
                 // No minimum length as existing records contain blank first names
                if (value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player's first name can't be longer than 30 characters");
                }
                */
                firstName = value;
            }
        }


        public string Surname
        {
            get { return surname; }
            set
            {
                if (value.Length < 1 || value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player's surname can't be longer than 30 characters");
                }
                surname = value;
            }
        }

        public int PremierTeamID { get; set; }
        public int FantasyTeamID { get; set; }
        public int PositionID { get; set; }

        public double Price { get; set; }
    }
}