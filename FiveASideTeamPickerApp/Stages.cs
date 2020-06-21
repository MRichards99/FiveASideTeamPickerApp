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
            stages.Add(new Stage(new List<Position> { goalkeeper }, 0, "Pick your goalkeeper!"));
            stages.Add(new Stage(new List<Position> { defender }, 1, "Pick your defender!"));
            stages.Add(new Stage(new List<Position> { midfielder }, 0, "Pick your midfielder!"));
            stages.Add(new Stage(new List<Position> { forward }, 1, "Pick your forward!"));
            stages.Add(new Stage(new List<Position> { defender, midfielder, forward }, 0, "Pick your final player!"));

            return stages;
        }
    }
}