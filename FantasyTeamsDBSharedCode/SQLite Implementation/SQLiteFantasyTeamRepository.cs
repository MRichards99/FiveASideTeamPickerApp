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
    public class SQLiteFantasyTeamRepository : IFantasyTeamRepository
    {
        private SQLiteConnection dbConnection;

        public SQLiteFantasyTeamRepository()
        {
            dbConnection = SQLiteConnector.Connection;
        }

        public int AddFantasyTeam(FantasyTeam team)
        {
            int rowsAdded = dbConnection.Insert(team);
            return rowsAdded;
        }

        public FantasyTeam GetFantasyTeamByID(int teamID)
        {
            throw new NotImplementedException();
        }
    }
}