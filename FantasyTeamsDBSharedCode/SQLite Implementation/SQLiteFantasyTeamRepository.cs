using System;
using System.Collections.Generic;
using System.Linq;

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
            return dbConnection.Insert(team);
        }

        public List<FantasyTeam> GetAllFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().ToList<FantasyTeam>();
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
