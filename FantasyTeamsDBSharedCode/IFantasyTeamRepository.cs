using System.Collections.Generic;

namespace FantasyTeamsDBSharedCode
{
    interface IFantasyTeamRepository
    {
        int GetNumberOfFantasyTeams();
        List<FantasyTeam> GetAllFantasyTeams();
        double GetPriceLimitForFantasyTeams();
        double GetRemainingFantasyTeamCost(int fantasyTeamID);

        int AddFantasyTeam(FantasyTeam team);
        int RemoveFantasyTeam(FantasyTeam team);
    }
}
