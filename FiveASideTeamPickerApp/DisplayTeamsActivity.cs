using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "DisplayTeamsActivity")]
    public class DisplayTeamsActivity : Activity
    {
        private SQLiteFantasyTeamRepository fantasyTeamRepository;
        private SQLitePlayerRepository playerRepository;

        public DisplayTeamsActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
            playerRepository = new SQLitePlayerRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DisplayTeamsLayout);

            TextView displayTeamsTeam1DetailsTextView = FindViewById<TextView>(Resource.Id.displayTeamsTeam1DetailsTextView);
            TextView displayTeamsTeam1TeamPriceText = FindViewById<TextView>(Resource.Id.displayTeamsTeam1TeamPriceText);
            ListView displayTeamsTeam1PlayerListView = FindViewById<ListView>(Resource.Id.displayTeamsTeam1PlayerListView);
            displayTeamsTeam1PlayerListView.SetMinimumHeight(1000);

            TextView displayTeamsTeam2DetailsTextView = FindViewById<TextView>(Resource.Id.displayTeamsTeam2DetailsTextView);
            TextView displayTeamsTeam2TeamPriceText = FindViewById<TextView>(Resource.Id.displayTeamsTeam2TeamPriceText);
            ListView displayTeamsTeam2PlayerListView = FindViewById<ListView>(Resource.Id.displayTeamsTeam2PlayerListView);

            Button returnToMainMenuButton = FindViewById<Button>(Resource.Id.displayTeamsReturnToMainMenuButton);
            List<FantasyTeam> fantasyTeams = fantasyTeamRepository.GetAllFantasyTeams();

            InsertDataIntoUIComponents(fantasyTeams[0], displayTeamsTeam1DetailsTextView, displayTeamsTeam1TeamPriceText, displayTeamsTeam1PlayerListView);
            InsertDataIntoUIComponents(fantasyTeams[1], displayTeamsTeam2DetailsTextView, displayTeamsTeam2TeamPriceText, displayTeamsTeam2PlayerListView);

            returnToMainMenuButton.Click += (sender, args) =>
            {
                Finish();
            };
        }

        void InsertDataIntoUIComponents(FantasyTeam fantasyTeam, TextView teamDetailsTextView, TextView teamPriceTextView, ListView teamPlayersListView)
        {
            teamDetailsTextView.Text = NameTeamTextFormatting.FormatNameAndTeam(fantasyTeam.ManagerFirstname, fantasyTeam.ManagerSurname, fantasyTeam.FantasyTeamName);
            List<Player> fantasyTeamPlayers = playerRepository.GetAllPlayersOfAFantasyTeam(fantasyTeam.FantasyTeamID);
            teamPriceTextView.Text = "£" + CalculateTotalFantasyTeamCost(fantasyTeamPlayers).ToString() + " million";

            teamPlayersListView.Adapter = new PlayerAdapter(this, fantasyTeamPlayers, Android.Resource.Layout.SimpleListItem2);
        }

        double CalculateTotalFantasyTeamCost(List<Player> players)
        {
            double teamPrice = 0.0;
            foreach (Player player in players)
            {
                teamPrice += player.Price;
            }

            return teamPrice;
        }
    }
}
