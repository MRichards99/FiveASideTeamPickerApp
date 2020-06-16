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
        private Player player;

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

            // TODO - Make premier team be able to be edited, perhaps select from a drop down?

            // Get view's resources
            EditText playerFirstName = FindViewById<EditText>(Resource.Id.playerDetailsFirstNameEditText);
            EditText playerSurname = FindViewById<EditText>(Resource.Id.playerDetailsSurnameEditText);
            EditText playerPosition = FindViewById<EditText>(Resource.Id.playerDetailsPositionEditText);
            EditText playerPrice = FindViewById<EditText>(Resource.Id.playerDetailsPriceEditText);
            Button saveButton = FindViewById<Button>(Resource.Id.playerDetailsSaveButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.playerDetailsCancelButton);
            Button deleteButton = FindViewById<Button>(Resource.Id.playerDetailsDeleteButton);

            string pageType = this.Intent.GetStringExtra("type");
            if (pageType == "existing")
            {
                // Get JSON serialised player and convert it back to a Player object
                string jsonPlayer = this.Intent.GetStringExtra("selectedPlayer");
                player = JsonConvert.DeserializeObject<Player>(jsonPlayer);

                // Assign player's properties to the relevant editable text fields
                playerFirstName.Text = player.Firstname;
                playerSurname.Text = player.Surname;
                playerPrice.Text = player.Price.ToString();
                // Get position name from database
                string selectedPlayerPosition = positionRepository.GetPositionNameByID(player.PositionID);
                playerPosition.Text = selectedPlayerPosition;
            }
            // If type is not existing, assume its "new" as the app won't have player details to assign to the text fields
            else
            {
                player = new Player();
                // If a player doesn't exist, then they cannot be deleted
                deleteButton.Enabled = false;
            }

            saveButton.Click += (sender, args) =>
            {
                // Update player object with user's inputs from text fields
                player.Firstname = playerFirstName.Text;
                player.Surname = playerSurname.Text;
                player.Price = Convert.ToDouble(playerPrice.Text);

                Intent updatedPlayerIntent = new Intent();

                if (pageType == "existing")
                {
                    // TODO - Fix trying to compare the two objects before going off to DB, or remove it

                    /*
                    // Check if the user has updated any aspects of the player to avoid unneeded DB traffic
                  
                    string originalPlayer = this.Intent.GetStringExtra("selectedPlayer");
                    string editedPlayer = JsonConvert.SerializeObject(player);

                    if (originalPlayer == editedPlayer)
                    {
                        Console.WriteLine("THEYRE THE SAME");
                    }
                    else
                    {
                        Console.WriteLine("DIFFERENT");
                    }
                    */

                    playerRepository.UpdatePlayer(player);
                    updatedPlayerIntent.PutExtra("type", "existing");

                }
                else if (pageType == "new")
                {
                    // TODO - Make inserting new players actually work

                    playerRepository.InsertNewPlayer(player);
                    updatedPlayerIntent.PutExtra("type", "new");
                }

                // Allow the updated player object to be sent back to AdminPlayersActivity so list view can be updated

                string updatedPlayerJson = JsonConvert.SerializeObject(player);
                updatedPlayerIntent.PutExtra("updatedPlayer", updatedPlayerJson);

                SetResult(Result.Ok, updatedPlayerIntent);
                Finish();
            };

            deleteButton.Click += (sender, args) =>
            {
                // Remove player from the database
                playerRepository.DeletePlayer(player);
            };

            cancelButton.Click += (sender, args) =>
            {
                // Return user to previous activity from the stack
                Finish();
            };
        }
    }
}