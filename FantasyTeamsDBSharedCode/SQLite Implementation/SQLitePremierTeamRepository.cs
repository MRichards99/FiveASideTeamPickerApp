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

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    class SQLitePremierTeamRepository : IPremierTeamRepository
    {
        public SQLitePremierTeamRepository()
        {
            SQLiteConnection dbConnection = SQLiteConnector.Connection;
        }

        public int AddPremierTeam(PremierTeam team)
        {
            throw new NotImplementedException();
        }

        public int DeleteAllPremierTeams()
        {
            throw new NotImplementedException();
        }

        public int DeletePremierTeam(PremierTeam team)
        {
            throw new NotImplementedException();
        }

        public int UpdatePremierTeam(PremierTeam team)
        {
            throw new NotImplementedException();
        }
    }
}