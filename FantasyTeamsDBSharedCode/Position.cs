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
        //private int positionID;
        private string positionName;

        public Position()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int PositionID { get; set; }
        public string PositionName
        {
            get { return positionName; }
            set
            {
                if (value.Length < 1 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Position cannot be more than 20 characters");
                }
                List<string> validPositions = new List<string> { "Goalkeeper", "Defender", "Midfielder", "Forward" };
                if (validPositions.Contains(value) != true)
                {
                    throw new ArgumentException(nameof(value), "Not a valid position");
                }

                positionName = value;
            }
        }
    }
}