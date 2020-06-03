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
        public string PremierTeamName { get; set; }
    }
}