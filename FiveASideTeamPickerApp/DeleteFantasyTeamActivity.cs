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
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "DeleteFantasyTeamActivity")]
    public class DeleteFantasyTeamActivity : Activity
    {
        SQLiteFantasyTeamRepository fantasyTeamRepository;

        public DeleteFantasyTeamActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteFantasyTeam);

            Button deleteFantasyTeam1Button = FindViewById<Button>(Resource.Id.deleteFantasyTeam1Button);
            Button deleteFantasyTeam2Button = FindViewById<Button>(Resource.Id.deleteFantasyTeam2Button);

            List<FantasyTeam> allFantasyTeams = fantasyTeamRepository.GetAllFantasyTeams();

            deleteFantasyTeam1Button.Text = allFantasyTeams[0].FantasyTeamName;
            deleteFantasyTeam2Button.Text = allFantasyTeams[1].FantasyTeamName;

            deleteFantasyTeam1Button.Click += (sender, args) =>
            {
                DeleteSelectedFantasyTeam(allFantasyTeams[0]);
                Finish();
            };

            deleteFantasyTeam2Button.Click += (sender, args) =>
            {
                DeleteSelectedFantasyTeam(allFantasyTeams[1]);
                // Finishing activity call not going into `DeleteSelectedFantasyTeam()` because a method should have a single purpose
                Finish();
            };
        }

        void DeleteSelectedFantasyTeam(FantasyTeam fantasyTeam)
        {
            fantasyTeamRepository.RemoveFantasyTeam(fantasyTeam);
        }
    }
}