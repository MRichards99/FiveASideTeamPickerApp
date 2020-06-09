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
    class SQLitePlayerRepository : IPlayerRepository
    {
        public SQLitePlayerRepository()
        {
            SQLiteConnection dbConnection = SQLiteConnector.Connection;
        }

        public int DeletePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllDefenders()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllGoalkeepers()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllPlayersExceptGoalkeepers()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerByID(int playerID)
        {
            throw new NotImplementedException();
        }

        public int InsertNewPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public int UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}