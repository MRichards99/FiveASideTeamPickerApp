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
        Player GetPlayerByID(int playerID);
        List<Player> GetAllPlayersExceptGoalkeepers();
        List<Player> GetAllGoalkeepers();
        List<Player> GetAllDefenders();
        

        int InsertNewPlayer(Player player);
        int UpdatePlayer(Player player);
        int DeletePlayer(Player player);
    }
}