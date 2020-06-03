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
    [Table("Positions")]
    public class Position
    {
        public Position()
        {

        }


        [PrimaryKey, AutoIncrement]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
    }
}