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

        public double GetRemainingFantasyTeamCost(int fantasyTeamID)
        {
            List<Player> fantasyTeamPlayerList = new SQLitePlayerRepository().GetAllPlayersOfAFantasyTeam(fantasyTeamID);
            double fantasyTeamCost = 0;

            foreach (Player player in fantasyTeamPlayerList)
            {
                fantasyTeamCost += player.Price;
            }

            // Stay at 1 decimal place, following the standard set in the database data
            double fantasyTeamPriceLimit = Math.Round(GetPriceLimitForFantasyTeams(), 1);

            return Math.Round(fantasyTeamPriceLimit - fantasyTeamCost, 1);
        }

        public int GetNumberOfFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().Count();
        }

        public double GetPriceLimitForFantasyTeams()
        {
            double averagePlayerCost = new SQLitePlayerRepository().GetAverageCostOfAllPlayers();
            return (averagePlayerCost * 5) + 1;
        }

        public int RemoveFantasyTeam(FantasyTeam team)
        {
            return dbConnection.Delete(team);
        }
    }
}
