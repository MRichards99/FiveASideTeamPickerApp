using System.Collections.Generic;

using FantasyTeamsDBSharedCode;

namespace FiveASideTeamPickerApp
{
    public static class Stages
    {
        public static List<Stage> GetStagesOfTeamSelection()
        {
            List<Stage> stages = new List<Stage>();

            // Defining all the positions so they can be added to the relevant stage(s)
            Position goalkeeper = new Position(1);
            Position defender = new Position(2);
            Position midfielder = new Position(3);
            Position forward = new Position(4);

            // Add each stage of the team picking process
            stages.Add(new Stage(new List<Position> { goalkeeper }, 0));
            stages.Add(new Stage(new List<Position> { defender }, 1));
            stages.Add(new Stage(new List<Position> { midfielder }, 0));
            stages.Add(new Stage(new List<Position> { forward }, 1));
            stages.Add(new Stage(new List<Position> { defender, midfielder, forward }, 0));

            return stages;
        }
    }
}
