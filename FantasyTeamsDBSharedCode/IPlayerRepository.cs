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
    interface IPlayerRepository
    {
        // TODO - Refactor the order and spacing of all these interfaces
        
        Player GetPlayerByID(int playerID);
        List<Player> GetAllPlayersExceptGoalkeepers();
        List<Player> GetAllGoalkeepers();
        List<Player> GetAllDefenders();
        List<Player> GetAllPlayers();
        List<Player> GetAllPlayersAssignedToFantasyTeams();
        List<Player> GetAllPlayersOfAFantasyTeam(int fantasyTeamID);

        List<Player> GetSelectablePlayersForFantasyTeam(Position position, int fantasyTeamID);
        int GetNumberOfPlayersAssignedToFantasyTeamInPremierTeam(int premierTeamID, int fantasyTeamID);
        int ResetFantasyTeamSelection();

        int InsertNewPlayer(Player player);
        int UpdatePlayer(Player player);
        int DeletePlayer(Player player);
    }
}