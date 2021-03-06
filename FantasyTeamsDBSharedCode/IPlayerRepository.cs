﻿using System.Collections.Generic;

namespace FantasyTeamsDBSharedCode
{
    interface IPlayerRepository
    {        
        List<Player> GetAllPlayers();
        List<Player> GetAllPlayersAssignedToFantasyTeams();
        List<Player> GetAllPlayersOfAFantasyTeam(int fantasyTeamID);
        List<Player> GetSelectablePlayersForFantasyTeam(Position position, double remainingTeamBalance, int fantasyTeamID);
        double GetAverageCostOfAllPlayers();
        int GetNumberOfPlayersAssignedToFantasyTeamInPremierTeam(int premierTeamID, int fantasyTeamID);

        int ResetFantasyTeamSelection();

        int InsertNewPlayer(Player player);
        int UpdatePlayer(Player player);
        int DeletePlayer(Player player);
    }
}
