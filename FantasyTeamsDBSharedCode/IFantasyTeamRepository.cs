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

namespace FantasyTeamsDBSharedCode
{
    interface IFantasyTeamRepository
    {
        FantasyTeam GetFantasyTeamByID(int teamID);

        int AddFantasyTeam(FantasyTeam team);
        // TODO - Would this is better to remove via ID?
        int RemoveFantasyTeam(FantasyTeam team);
        int GetNumberOfFantasyTeams();
        List<FantasyTeam> GetAllFantasyTeams();
    }
}