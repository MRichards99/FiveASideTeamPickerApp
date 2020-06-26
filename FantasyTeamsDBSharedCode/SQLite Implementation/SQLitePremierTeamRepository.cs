using System;
using System.Collections.Generic;
using System.Linq;

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
            return dbConnection.Insert(team);
        }

        public int DeletePremierTeam(PremierTeam team)
        {
            return dbConnection.Delete(team);
        }

        public List<PremierTeam> GetAllPremierTeams()
        {
            return dbConnection.Table<PremierTeam>().ToList<PremierTeam>();
        }

        public List<int> GetEligiblePremierTeams(int fantasyTeamID)
        {
            List<int> eligiblePremierTeamIDs = new List<int>();

            List<PremierTeam> allPremierTeams = GetAllPremierTeams();
            foreach (PremierTeam premierTeam in allPremierTeams)
            {
                SQLitePlayerRepository playerRepository = new SQLitePlayerRepository();
                int premierTeamPlayerAssignedCount = playerRepository.GetNumberOfPlayersAssignedToFantasyTeamInPremierTeam(premierTeam.PremierTeamID, fantasyTeamID);

                if (premierTeamPlayerAssignedCount < 2)
                {
                    eligiblePremierTeamIDs.Add(premierTeam.PremierTeamID);
                }
            }

            return eligiblePremierTeamIDs;
        }

        public PremierTeam GetPremierTeamFromID(int premierTeamID)
        {
            return dbConnection.Table<PremierTeam>().SingleOrDefault(c => c.PremierTeamID == premierTeamID);
        }

        public int GetPremierTeamIDFromName(string premierTeamName)
        {
            return dbConnection.Table<PremierTeam>().SingleOrDefault(p => p.PremierTeamName == premierTeamName).PremierTeamID;
        }

        public string GetPremierTeamNameFromID(int premierTeamID)
        {
            PremierTeam premierTeamFromDB = GetPremierTeamFromID(premierTeamID);
            return premierTeamFromDB.PremierTeamName;
        }

        public int UpdatePremierTeam(PremierTeam team)
        {
            return dbConnection.Update(team);
        }
    }
}
