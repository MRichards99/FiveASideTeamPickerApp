﻿using System;
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
    public class SQLitePlayerRepository : IPlayerRepository
    {
        private SQLiteConnection dbConnection;

        public SQLitePlayerRepository()
        {
            dbConnection = SQLiteConnector.Connection;
        }

        public int DeletePlayer(Player player)
        {
            return dbConnection.Delete(player);
        }

        public List<Player> GetAllDefenders()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllGoalkeepers()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllPlayers()
        {
            return dbConnection.Table<Player>().ToList<Player>();
        }

        public List<Player> GetAllPlayersAssignedToFantasyTeams()
        {
            return dbConnection.Table<Player>().Where(p => p.FantasyTeamID != 0).ToList<Player>();
        }

        public List<Player> GetAllPlayersExceptGoalkeepers()
        {
            // TODO - Check which of these methods have been used in the app
            Position position = dbConnection.Table<Position>().SingleOrDefault(p => p.PositionName == "Goalkeeper");
            return dbConnection.Table<Player>().Where(p => p.PositionID != position.PositionID).ToList<Player>();
        }

        public Player GetPlayerByID(int playerID)
        {
            // TODO - Make all these methods consistent to just return the first line, don't bother with the variable
            Player player = dbConnection.Table<Player>().SingleOrDefault(c => c.PlayerID == playerID);
            return player;
        }

        public List<Player> GetSelectablePlayersOfAPositionType(Position position)
        {
            return dbConnection.Table<Player>().Where(p => p.PositionID == position.PositionID && p.FantasyTeamID == 0).ToList<Player>();
        }

        public int InsertNewPlayer(Player player)
        {
            return dbConnection.Insert(player);
        }

        public int UpdatePlayer(Player player)
        {
            return dbConnection.Update(player);
        }
    }
}