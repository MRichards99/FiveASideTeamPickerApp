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
    [Activity(Label = "AdminPremierTeamListActivity")]
    public class AdminPremierTeamListActivity : Activity
    {
        SQLitePremierTeamRepository premierTeamRepository;
        List<PremierTeam> premierTeams;
        PremierTeamAdapter premierTeamAdapter;

        public AdminPremierTeamListActivity()
        {
            premierTeamRepository = new SQLitePremierTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminPremierTeamListLayout);

            // Create your application here
            ListView premierTeamList = FindViewById<ListView>(Resource.Id.premierTeamsListView);
            Button addPremierTeamButton = FindViewById<Button>(Resource.Id.addPremierTeamButton);

            addPremierTeamButton.Click += (sender, args) =>
            {

            };

            premierTeams = premierTeamRepository.GetAllPremierTeams();
            premierTeamAdapter = new PremierTeamAdapter(this, premierTeams);
            premierTeamList.Adapter = premierTeamAdapter;
            premierTeamList.ItemClick += ListViewClickEvent;


        }

        void ListViewClickEvent(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

    }
}