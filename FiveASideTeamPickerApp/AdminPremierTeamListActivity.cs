using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "AdminPremierTeamListActivity")]
    public class AdminPremierTeamListActivity : Activity
    {
        private SQLitePremierTeamRepository premierTeamRepository;

        private List<PremierTeam> premierTeams;
        private PremierTeamAdapter premierTeamAdapter;

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
                Intent addNewPremierTeamIntent = new Intent(this, typeof(AdminChangePremierTeamDetailsActivity));

                addNewPremierTeamIntent.PutExtra("type", "new");
                StartActivityForResult(addNewPremierTeamIntent, 1);
            };

            premierTeams = premierTeamRepository.GetAllPremierTeams();
            premierTeamAdapter = new PremierTeamAdapter(this, premierTeams);
            premierTeamList.Adapter = premierTeamAdapter;
            premierTeamList.ItemClick += ListViewClickEvent;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            premierTeamAdapter.RemoveAllPremierTeams();
            premierTeamAdapter.AppendToPremierTeamList(new PremierTeamAdapter.GetAllPremierTeamsDelegate(premierTeamRepository.GetAllPremierTeams));
            premierTeamAdapter.NotifyDataSetChanged();
        }

        void ListViewClickEvent(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent changePremierTeamDetailsIntent = new Intent(this, typeof(AdminChangePremierTeamDetailsActivity));
            changePremierTeamDetailsIntent.PutExtra("type", "existing");

            PremierTeam selectedPremierTeam = premierTeamAdapter[e.Position];

            string jsonPremierTeam = JsonConvert.SerializeObject(selectedPremierTeam);
            changePremierTeamDetailsIntent.PutExtra("selectedPremierTeam", jsonPremierTeam);

            StartActivityForResult(changePremierTeamDetailsIntent, 1);

        }
    }
}
