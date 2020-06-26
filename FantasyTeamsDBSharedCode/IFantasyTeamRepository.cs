using System.Collections.Generic;

namespace FantasyTeamsDBSharedCode
{
    interface IFantasyTeamRepository
    {
        int GetNumberOfFantasyTeams();
        List<FantasyTeam> GetAllFantasyTeams();

        int AddFantasyTeam(FantasyTeam team);
        int RemoveFantasyTeam(FantasyTeam team);
    }
}
