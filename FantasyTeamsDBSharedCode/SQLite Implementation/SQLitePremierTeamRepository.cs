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
    public class SQLitePremierTeamRepository : IPremierTeamRepository
    {
        private SQLiteConnection dbConnection;

        public SQLitePremierTeamRepository()
        {
            dbConnection = SQLiteConnector.Connection;
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

        public string GetPremierTeamNameFromID(int premierTeamID)
        {
            // TODO - Refactor so premier team is grabbed from GetPremierTeam()
            PremierTeam premierTeamFromDB = dbConnection.Table<PremierTeam>().SingleOrDefault(c => c.PremierTeamID == premierTeamID);
            return premierTeamFromDB.PremierTeamName;
        }

        public int UpdatePremierTeam(PremierTeam team)
        {
            throw new NotImplementedException();
        }
    }
}