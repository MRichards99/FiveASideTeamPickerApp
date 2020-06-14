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
    [Activity(Label = "AdminChangePlayerDetailsActivity")]
    public class AdminChangePlayerDetailsActivity : Activity
    {
        private SQLitePositionRepository positionRepository;
        private SQLitePlayerRepository playerRepository;

        public AdminChangePlayerDetailsActivity()
        {
            // TODO - Change all constructors which create a new repo to use this keyword
            positionRepository = new SQLitePositionRepository();
            playerRepository = new SQLitePlayerRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminChangePlayerDetailsLayout);

            // Get JSON serialised player and convert it back to a Player object
            string jsonPlayer = this.Intent.GetStringExtra("selectedPlayer");
            Player selectedPlayer = JsonConvert.DeserializeObject<Player>(jsonPlayer);

            // Get view's resources
            EditText playerFirstName = FindViewById<EditText>(Resource.Id.playerDetailsFirstNameEditText);
            EditText playerSurname = FindViewById<EditText>(Resource.Id.playerDetailsSurnameEditText);
            EditText playerPosition = FindViewById<EditText>(Resource.Id.playerDetailsPositionEditText);
            EditText playerPrice = FindViewById<EditText>(Resource.Id.playerDetailsPriceEditText);
            Button saveButton = FindViewById<Button>(Resource.Id.playerDetailsSaveButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.playerDetailsCancelButton);

            // Assign player's properties to the relevant editable text fields
            playerFirstName.Text = selectedPlayer.Firstname;
            playerSurname.Text = selectedPlayer.Surname;
            playerPrice.Text = selectedPlayer.Price.ToString();
            // Get position name from database
            string selectedPlayerPosition = positionRepository.GetPositionNameByID(selectedPlayer.PositionID);
            playerPosition.Text = selectedPlayerPosition;

            saveButton.Click += (sender, args) =>
            {
                // TODO - Test that updating a player's details actually works
                playerRepository.UpdatePlayer(selectedPlayer);

            };

            cancelButton.Click += (sender, args) =>
            {
                // Return user to previous activity from the stack
                Finish();
            };
        }
    }
}