using System.Collections.Generic;

namespace FantasyTeamsDBSharedCode
{
    interface IPositionRepository
    {
        Position GetPositionByID(int positionID);
        string GetPositionNameByID(int positionID);
        List<Position> GetAllPositions();
        int GetPositionIDFromName(string positionName);
    }
}
