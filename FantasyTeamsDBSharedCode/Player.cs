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
        public Player()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int PlayerID { get; set; }
        public string Firstname
        {
            get { return Firstname; }
            set
            {
                if (value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player's first name can't be longer than 30 characters");
                }
                Firstname = value;
            }
        }


        public string Surname
        {
            get { return Surname; }
            set
            {
                if (value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player's surname can't be longer than 30 characters");
                }
                Surname = value;
            }
        }

        public int PremierTeamID { get; set; }
        public int FantasyTeamID { get; set; }
        public int PositionID { get; set; }

        public decimal Price { get; set; }
    }
}