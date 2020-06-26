using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    public class SQLitePositionRepository : IPositionRepository
    {
        private SQLiteConnection dbConnection;

        public SQLitePositionRepository()
        {
            dbConnection = SQLiteConnector.Connection;
        }

        public List<Position> GetAllPositions()
        {
            return dbConnection.Table<Position>().ToList<Position>();
        }

        public Position GetPositionByID(int positionID)
        {
            return dbConnection.Table<Position>().SingleOrDefault(c => c.PositionID == positionID);
        }

        public int GetPositionIDFromName(string positionName)
        {
            return dbConnection.Table<Position>().SingleOrDefault(p => p.PositionName == positionName).PositionID;
        }

        public string GetPositionNameByID(int positionID)
        {
            Position positionFromDB = GetPositionByID(positionID);
            return positionFromDB.PositionName;
        }
    }
}
