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
        Player selectedPlayer;
        int selectedPlayerIndex;

        public AdminPlayersActivity()
        {
            playerRepository = new SQLitePlayerRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // TODO - Change name of this layout to be 'AdminPlayersList'

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminPlayersLayout);

            ListView playerList = FindViewById<ListView>(Resource.Id.playersListView);
            Button addPlayerButton = FindViewById<Button>(Resource.Id.addPlayerButton);

            addPlayerButton.Click += (sender, args) =>
            {
                Intent addNewPlayerIntent = new Intent(this, typeof(AdminChangePlayerDetailsActivity));

                // Define that a new player needs to be created, so no existing player to send to the following activity
                addNewPlayerIntent.PutExtra("type", "new");

                StartActivityForResult(addNewPlayerIntent, 1);
            };

            players = playerRepository.GetAllPlayers();
            playerListAdapter = new PlayerAdapter(this, players, Android.Resource.Layout.SimpleListItem2);
            playerList.Adapter = playerListAdapter;
            playerList.ItemClick += ClickEventOnItemInListView;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Rebuild data in list view
            playerListAdapter.RemoveAllPlayers();
            playerListAdapter.AppendToPlayerList(new PlayerAdapter.NoParameterPlayerDelegate(playerRepository.GetAllPlayers));

            // This makes the adapter aware that data has changed and the view needs to be refreshed as a result
            playerListAdapter.NotifyDataSetChanged();
        }

        void ClickEventOnItemInListView(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent changePlayerDetailsIntent = new Intent(this, typeof(AdminChangePlayerDetailsActivity));

            // Define that an existing player is going to be edited
            changePlayerDetailsIntent.PutExtra("type", "existing");

            // Detect which player has been selected by the user to pass to the details activity
            // This data will also be used when updating the list view once player details have been updated
            selectedPlayer = playerListAdapter[e.Position];
            selectedPlayerIndex = e.Position;

            // Convert Player object into JSON ready to be sent to next activity
            string jsonPlayer = JsonConvert.SerializeObject(selectedPlayer);
            changePlayerDetailsIntent.PutExtra("selectedPlayer", jsonPlayer);

            // Need the player object to be called back so the list view can be updated
            StartActivityForResult(changePlayerDetailsIntent, 1);
        }
    }
}