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
    interface IPremierTeamRepository
    {
        int DeleteAllPremierTeams();
        int DeletePremierTeam(PremierTeam team);
        int AddPremierTeam(PremierTeam team);
        int UpdatePremierTeam(PremierTeam team);

        string GetPremierTeamNameFromID(int premierTeamID);
    }
}