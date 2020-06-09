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
    [Table("PremierTeams")]
    public class PremierTeam
    {
        public PremierTeam()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int PremierTeamID { get; set; }
        public string PremierTeamName
        {
            get { return PremierTeamName; }
            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Premier team's  name can't be longer than 20 characters");
                }
                PremierTeamName = value;
            }
        }
        }
    }
}