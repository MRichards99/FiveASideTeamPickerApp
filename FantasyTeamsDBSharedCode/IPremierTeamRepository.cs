using System.Collections.Generic;

namespace FantasyTeamsDBSharedCode
{
    interface IPremierTeamRepository
    {
        PremierTeam GetPremierTeamFromID(int premierTeamID);
        string GetPremierTeamNameFromID(int premierTeamID);
        List<int> GetEligiblePremierTeams(int fantasyTeamID);
        List<PremierTeam> GetAllPremierTeams();
        int GetPremierTeamIDFromName(string premierTeamName);

        int DeletePremierTeam(PremierTeam team);
        int AddPremierTeam(PremierTeam team);
        int UpdatePremierTeam(PremierTeam team);
    }
}
