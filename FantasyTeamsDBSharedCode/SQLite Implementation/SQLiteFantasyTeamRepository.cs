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

        public List<FantasyTeam> GetAllFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().ToList<FantasyTeam>();
        }

        public FantasyTeam GetFantasyTeamByID(int teamID)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().Count();
        }

        public int RemoveFantasyTeam(FantasyTeam team)
        {
            return dbConnection.Delete(team);
        }
    }
}