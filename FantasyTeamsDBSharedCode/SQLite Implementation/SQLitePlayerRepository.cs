using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    public class SQLitePlayerRepository : IPlayerRepository
    {
        private SQLiteConnection dbConnection;

        public SQLitePlayerRepository()
        {
            dbConnection = SQLiteConnector.Connection;
        }

        public int DeletePlayer(Player player)
        {
            return dbConnection.Delete(player);
        }

        public List<Player> GetAllPlayers()
        {
            return dbConnection.Table<Player>().ToList<Player>();
        }

        public List<Player> GetAllPlayersAssignedToFantasyTeams()
        {
            return dbConnection.Table<Player>().Where(p => p.FantasyTeamID != 0).ToList<Player>();
        }

        public List<Player> GetAllPlayersOfAFantasyTeam(int fantasyTeamID)
        {
            return dbConnection.Table<Player>().Where(p => p.FantasyTeamID == fantasyTeamID).ToList();
        }

        public int GetNumberOfPlayersAssignedToFantasyTeamInPremierTeam(int premierTeamID, int fantasyTeamID)
        {
            return dbConnection.Table<Player>().Where(p => p.FantasyTeamID == fantasyTeamID && p.PremierTeamID == premierTeamID).Count();
        }

        public List<Player> GetSelectablePlayersForFantasyTeam(Position position, int fantasyTeamID)
        {
            SQLitePremierTeamRepository premierTeamRepository = new SQLitePremierTeamRepository();
            List<int> eligiblePremierTeamIDs = premierTeamRepository.GetEligiblePremierTeams(fantasyTeamID);
            
            return dbConnection.Table<Player>().Where(p => p.PositionID == position.PositionID && p.FantasyTeamID == 0 && eligiblePremierTeamIDs.Contains(p.PremierTeamID)).ToList<Player>();
        }

        public int InsertNewPlayer(Player player)
        {
            return dbConnection.Insert(player);
        }

        public int ResetFantasyTeamSelection()
        {
            int rowsUpdated = 0;
            List<Player> fantasyTeamPlayers = GetAllPlayersAssignedToFantasyTeams();
            foreach (Player player in fantasyTeamPlayers)
            {
                player.FantasyTeamID = 0;
                rowsUpdated += UpdatePlayer(player);
            }

            return rowsUpdated;
        }

        public int UpdatePlayer(Player player)
        {
            return dbConnection.Update(player);
        }
    }
}
