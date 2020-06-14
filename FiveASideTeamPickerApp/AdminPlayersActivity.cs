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
using Newtonsoft.Json;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "AdminPlayersActivity")]
    public class AdminPlayersActivity : Activity
    {
        SQLitePlayerRepository playerRepository;
        List<Player> players;
        PlayerAdapter playerListAdapter;

        public AdminPlayersActivity()
        {
            playerRepository = new SQLitePlayerRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminPlayersLayout);

            ListView playerList = FindViewById<ListView>(Resource.Id.playersListView);
            players = playerRepository.GetAllPlayers();
            playerListAdapter = new PlayerAdapter(this, players);
            playerList.Adapter = playerListAdapter;

            playerList.ItemClick += ClickEventOnItemInListView;
        }

        void ClickEventOnItemInListView(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Convert Player object into JSON ready to be sent to next activity
            Player selectedPlayer = playerListAdapter[e.Position];
            string jsonPlayer = JsonConvert.SerializeObject(selectedPlayer);

            Intent changePlayerDetailsIntent = new Intent(this, typeof(AdminChangePlayerDetailsActivity));
            changePlayerDetailsIntent.PutExtra("selectedPlayer", jsonPlayer);
            StartActivity(changePlayerDetailsIntent);
        }
    }
}