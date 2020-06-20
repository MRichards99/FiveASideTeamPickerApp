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
using FantasyTeamsDBSharedCode;

namespace FiveASideTeamPickerApp
{
    public class Stage
    {
        public Stage(List<Position> positions, int startingManager, string description)
        {
            this.SelectablePositions = positions;
            this.StartingManager = startingManager;
            this.Description = description;
        }

        public List<Position> SelectablePositions
        {
            get;
            set;
        }

        // TODO - Implement as an enum
        public int StartingManager
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

    }
}