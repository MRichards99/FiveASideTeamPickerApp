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
    [Activity(Label = "AdminInterfaceActivity")]
    public class AdminInterfaceActivity : Activity
    {
        SQLiteFantasyTeamRepository fantasyTeamRepository;

        public AdminInterfaceActivity()
        {
            // TODO - Add comment about creating relevant repos across the different activities
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Setting view from Resources
            SetContentView(Resource.Layout.AdminInterface);
            
            // Get UI resources on this view
            Button resetButton = FindViewById<Button>(Resource.Id.appResetButton);
            Button adminPlayerButton = FindViewById<Button>(Resource.Id.adminPlayerButton);
            Button adminPremierTeamButton = FindViewById<Button>(Resource.Id.adminPremierTeamButton);


            resetButton.Click += (sender, args) =>
            {
                ResetApp();
            };

            adminPlayerButton.Click += (sender, args) =>
            {

            };

            adminPremierTeamButton.Click += (sender, args) =>
            {

            };
        }

        void ResetApp()
        {
            List<FantasyTeam> allFantasyTeams = fantasyTeamRepository.GetAllFantasyTeams();
            foreach (FantasyTeam team in allFantasyTeams) {
                fantasyTeamRepository.RemoveFantasyTeam(team);
            }

            // TODO - Implement making all players selectable again

            // Alert user that app reset has taken place
            Toast.MakeText(this, "All fantasy teams have been removed and all players are now selectable", ToastLength.Short).Show();
        }
    }
}