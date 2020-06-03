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
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public int PremierTeamID { get; set; }
        public int FantasyTeamID { get; set; }
        public int PositionID { get; set; }

        public decimal Price { get; set; }
    }
}