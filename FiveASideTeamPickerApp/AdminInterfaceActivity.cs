using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "AdminInterfaceActivity")]
    public class AdminInterfaceActivity : Activity
    {
        private SQLiteFantasyTeamRepository fantasyTeamRepository;
        private SQLitePlayerRepository playerRepository;

        public AdminInterfaceActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
            playerRepository = new SQLitePlayerRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
                StartActivity(typeof(AdminPlayersActivity));
            };

            adminPremierTeamButton.Click += (sender, args) =>
            {
                StartActivity(typeof(AdminPremierTeamListActivity));
            };
        }

        void ResetApp()
        {
            List<FantasyTeam> allFantasyTeams = fantasyTeamRepository.GetAllFantasyTeams();
            foreach (FantasyTeam team in allFantasyTeams) {
                fantasyTeamRepository.RemoveFantasyTeam(team);
            }

            playerRepository.ResetFantasyTeamSelection();

            // Alert user that app reset has taken place
            Toast.MakeText(this, Resource.String.admin_reset_app_toast_message, ToastLength.Short).Show();
        }
    }
}
