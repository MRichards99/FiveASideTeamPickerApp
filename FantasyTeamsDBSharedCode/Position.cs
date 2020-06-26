using System;

using SQLite;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FantasyTeamsDBSharedCode
{
    [Table("Positions")]
    public class Position
    {
        private string positionName;

        public Position()
        {
        }

        public Position(int positionID)
        {
            this.PositionID = positionID;
            this.PositionName = new SQLitePositionRepository().GetPositionNameByID(this.PositionID);
        }

        [PrimaryKey, AutoIncrement]
        public int PositionID { get; set; }
        public string PositionName
        {
            get { return positionName; }
            set
            {
                if (value.Length < 1 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Position cannot be more than 20 characters");
                }

                positionName = value;
            }
        }
    }
}
