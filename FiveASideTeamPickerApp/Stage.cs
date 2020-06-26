using System.Collections.Generic;

using FantasyTeamsDBSharedCode;

namespace FiveASideTeamPickerApp
{
    public class Stage
    {
        /* A stage is a concept for each type of player that can be selected. So first a goalkeeper is selected by both
         * managers (first stage), then a defender (second stage) and so on. In the 4th stage, the 'second' manager 
         * get to select their player first, so the concept of a 'starting manager' is created in this class. This 
         * integer is used as a pointer to the list of fantasy teams to reference who's 'manager' turn it is. These 
         * values are manipulated by the XOR operator to swap the manager at the correct time. This is much more 
         * efficient than a large switch statement.
         * 
         * The list of stages are constructed in Stages.cs, with that list implemented in PickTeamsActivty.cs
         */

        public Stage(List<Position> positions, int startingManager)
        {
            this.SelectablePositions = positions;
            this.StartingManager = startingManager;
        }

        public List<Position> SelectablePositions
        {
            get;
            set;
        }

        public int StartingManager
        {
            get;
            set;
        }
    }
}
