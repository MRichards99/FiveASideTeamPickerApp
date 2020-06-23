﻿using System;
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
        private SQLitePremierTeamRepository premierTeamRepository;
        private Player player;

        string selectedPositionName;

        public AdminChangePlayerDetailsActivity()
        {
            // TODO - Change all constructors which create a new repo to use this keyword
            positionRepository = new SQLitePositionRepository();
            playerRepository = new SQLitePlayerRepository();
            premierTeamRepository = new SQLitePremierTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminChangePlayerDetailsLayout);

            // TODO - Make premier team be able to be edited, perhaps select from a drop down?

            // Get view's resources
            EditText playerFirstName = FindViewById<EditText>(Resource.Id.playerDetailsFirstNameEditText);
            EditText playerSurname = FindViewById<EditText>(Resource.Id.playerDetailsSurnameEditText);
            Spinner playerPosition = FindViewById<Spinner>(Resource.Id.playerDetailsPositionSpinner);
            Spinner playerPremierTeam = FindViewById<Spinner>(Resource.Id.playerDetailsPremierTeamSpinner);
            EditText playerPrice = FindViewById<EditText>(Resource.Id.playerDetailsPriceEditText);
            Button saveButton = FindViewById<Button>(Resource.Id.playerDetailsSaveButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.playerDetailsCancelButton);
            Button deleteButton = FindViewById<Button>(Resource.Id.playerDetailsDeleteButton);

            // Get selectable positions for spinner and populate the spinner with the position names
            List<Position> positions = positionRepository.GetAllPositions();
            List<string> positionNamesArray = new List<string>();
            foreach (Position position in positions)
            {
                positionNamesArray.Add(position.PositionName);
            }
            ArrayAdapter<string> positionSpinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, positionNamesArray);
            positionSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            playerPosition.Adapter = positionSpinnerAdapter;

            // Get selectable premier teams for spinner and populate spinner with them
            List<PremierTeam> premierTeams = premierTeamRepository.GetAllPremierTeams();
            List<string> premierTeamNamesArray = new List<string>();
            foreach (PremierTeam premierTeam in premierTeams)
            {
                premierTeamNamesArray.Add(premierTeam.PremierTeamName);
            }
            ArrayAdapter<string> premierTeamSpinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, premierTeamNamesArray);
            premierTeamSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            playerPremierTeam.Adapter = premierTeamSpinnerAdapter;


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
                playerPosition.SetSelection(positionSpinnerAdapter.GetPosition(selectedPlayerPosition));

                string selectedPlayerPremierTeam = premierTeamRepository.GetPremierTeamNameFromID(player.PremierTeamID);
                playerPremierTeam.SetSelection(premierTeamSpinnerAdapter.GetPosition(selectedPlayerPremierTeam));
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
                player.PositionID = positionRepository.GetPositionIDFromName(playerPosition.SelectedItem.ToString());
                player.PremierTeamID = premierTeamRepository.GetPremierTeamIDFromName(playerPremierTeam.SelectedItem.ToString());

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
                }
                else if (pageType == "new")
                {
                    player.FantasyTeamID = 0;
                    playerRepository.InsertNewPlayer(player);
                }
                
                // TODO - Delete intent code
                SetResult(Result.Ok);
                Finish();
            };

            deleteButton.Click += (sender, args) =>
            {
                // Remove player from the database
                playerRepository.DeletePlayer(player);
                SetResult(Result.Ok);
                Finish();
            };

            cancelButton.Click += (sender, args) =>
            {
                // Return user to previous activity from the stack
                Finish();
            };
        }
    }
}