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
                // TODO - Do a find on all Console statements and remove them
                SQLitePlayerRepository playerRepository = new SQLitePlayerRepository();
                int premierTeamPlayerAssignedCount = playerRepository.GetNumberOfPlayersAssignedToFantasyTeamInPremierTeam(premierTeam.PremierTeamID, fantasyTeamID);

                Console.WriteLine($"Fantasy Team ID: {fantasyTeamID}, Number of players in {premierTeam.PremierTeamName}: {premierTeamPlayerAssignedCount}");
                if (premierTeamPlayerAssignedCount < 2)
                {
                    eligiblePremierTeamIDs.Add(premierTeam.PremierTeamID);
                }
            }

            return eligiblePremierTeamIDs;
        }

        public string GetPremierTeamNameFromID(int premierTeamID)
        {
            // TODO - Refactor so premier team is grabbed from GetPremierTeam()
            // TODO - Check if all the methods which don't return lists use the singleordefault bit
            PremierTeam premierTeamFromDB = dbConnection.Table<PremierTeam>().SingleOrDefault(c => c.PremierTeamID == premierTeamID);
            return premierTeamFromDB.PremierTeamName;
        }

        public int UpdatePremierTeam(PremierTeam team)
        {
            throw new NotImplementedException();
        }
    }
}