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
using Java.Util;
using Random = System.Random;
using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "PickTeamsActivity")]
    public class PickTeamsActivity : Activity
    {
        SQLiteFantasyTeamRepository fantasyTeamRepository;
        SQLitePlayerRepository playerRepository;

        TextView currentManagerTurnTextView;
        ListView selectablePlayersList;

        Player selectedPlayer;

        int currentManagerTurnPointer;
        int turnCounter;
        List<Position> currentSelectablePositions;
        int stageManagerXOR;

        public PickTeamsActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
            playerRepository = new SQLitePlayerRepository();
            turnCounter = 0;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // TODO - Check this are formatted like this in all files
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PickTeams);

            // Getting UI elements from layout
            currentManagerTurnTextView = FindViewById<TextView>(Resource.Id.currentManagerTurn);
            selectablePlayersList = FindViewById<ListView>(Resource.Id.selectablePlayers);
            Button nextTurnButton = FindViewById<Button>(Resource.Id.nextTurnButton);

            // Disable next turn button until a player has been selected
            nextTurnButton.Enabled = false;

            // Create list view to show which players the user can select on their turn
            selectablePlayersList.ChoiceMode = ChoiceMode.Single;
            List<Player> selectablePlayers = new List<Player>();
            PlayerAdapter selectablePlayersAdapter = new PlayerAdapter(this, selectablePlayers, Android.Resource.Layout.SimpleListItemActivated2);
            selectablePlayersList.Adapter = selectablePlayersAdapter;

            // Get a list of stages
            List<Stage> stages = Stages.GetStagesOfTeamSelection();

            // Randomly select which manager should go first
            currentManagerTurnPointer = SelectStartingManager();

            // Get fantasy teams
            List<FantasyTeam> allFantasyTeams = fantasyTeamRepository.GetAllFantasyTeams();

            StageManagementWrapper(stages, selectablePlayersAdapter, allFantasyTeams);
            ArrangeNewTurn(selectablePlayersAdapter, allFantasyTeams);

            string currentManagerFirstName = allFantasyTeams[currentManagerTurnPointer].ManagerFirstname;
            string currentManagerSurname = allFantasyTeams[currentManagerTurnPointer].ManagerSurname;
            string currentFantasyTeamName = allFantasyTeams[currentManagerTurnPointer].FantasyTeamName;
            currentManagerTurnTextView.Text = NameTeamTextFormatting.FormatNameAndTeam(currentManagerFirstName, currentManagerSurname, currentFantasyTeamName);

            selectablePlayersList.ItemSelected += (sender, args) =>
            {
                selectedPlayer = selectablePlayersAdapter[args.Position];

            };

            nextTurnButton.Click += (sender, args) =>
            {
                // TODO - Deselect item in list view and disable next turn button each time
                // Deselect item when turn is finished
                /*
                selectablePlayersList.Selected = false;
                selectablePlayersList.SetSelection(-1);
                */

                // Assign selected player to the fantasy team
                //int selectedPlayerPosition = selectablePlayersList.SelectedItemPosition;

                

                //GetSelectedPlayer(selectedPlayerPosition, selectablePlayersAdapter);
                selectedPlayer.FantasyTeamID = allFantasyTeams[currentManagerTurnPointer].FantasyTeamID;
                playerRepository.UpdatePlayer(selectedPlayer);

                if (turnCounter == 2)
                {
                    // Checking if there are uncompleted stages left
                    if (stages.Count != 0)
                    {
                        StageManagementWrapper(stages, selectablePlayersAdapter, allFantasyTeams);
                        turnCounter += 1;
                    }
                    else
                    {
                        StartActivity(typeof(DisplayTeamsActivity));
                    }
                }
                else
                {
                    currentManagerTurnPointer = currentManagerTurnPointer ^ 1;

                    // Go to next turn
                    ArrangeNewTurn(selectablePlayersAdapter, allFantasyTeams);
                    
                }

                currentManagerFirstName = allFantasyTeams[currentManagerTurnPointer].ManagerFirstname;
                currentManagerSurname = allFantasyTeams[currentManagerTurnPointer].ManagerSurname;
                currentFantasyTeamName = allFantasyTeams[currentManagerTurnPointer].FantasyTeamName;
                currentManagerTurnTextView.Text = NameTeamTextFormatting.FormatNameAndTeam(currentManagerFirstName, currentManagerSurname, currentFantasyTeamName);

                // Data will have changed so list view needs refreshing - player selected or new stage, one or both
                selectablePlayersAdapter.NotifyDataSetChanged();
            };


            selectablePlayersList.ItemClick += (sender, args) =>
            {
                // Enable next turn button now a player has been selected
                nextTurnButton.Enabled = true;
                GetSelectedPlayer(args, selectablePlayersAdapter);
            };
        }

        void GetSelectedPlayer(AdapterView.ItemClickEventArgs args, PlayerAdapter playerAdapter)
        {
            selectedPlayer = playerAdapter[args.Position];
        }

        int SelectStartingManager()
        {
            /*
            By randomly selecting an integer between 0 and 1 inclusive, the result can be used as
            an index to grab the correct manager from a list of fantasy teams, of which there'll
            only ever be two (this activity cannot be started unless there are two teams)
            */

            Random random = new Random();
            return random.Next(0, 2);

        }

        // TODO - Test that selectablePlayers isn't required to mess about with the data
        void AssembleNewStage(Stage stage, PlayerAdapter playerAdapter, List<FantasyTeam> fantasyTeams)
        {
            // Reset turn counter and correct manager having the starting turn
            turnCounter = 0;
            // TODO - Comment my thoughts behind XOR of 0 and 1
            currentManagerTurnPointer ^= stageManagerXOR;

            UpdateListOfPlayers(stage.SelectablePositions, playerAdapter, fantasyTeams[currentManagerTurnPointer].FantasyTeamID);            
        }

        void ArrangeNewTurn(PlayerAdapter playerAdapter, List<FantasyTeam> fantasyTeams)
        {
            turnCounter += 1;
            

            UpdateListOfPlayers(currentSelectablePositions, playerAdapter, fantasyTeams[currentManagerTurnPointer].FantasyTeamID);
        }

        void UpdateListOfPlayers(List<Position> selectablePositions, PlayerAdapter playerAdapter, int currentFantasyTeamTurnID)
        {
            // Remove all players so the data from this list view can be rebuilt
            playerAdapter.RemoveAllPlayers();

            foreach (Position position in selectablePositions)
            {
                playerAdapter.AppendToPlayerList(new PlayerAdapter.PositionPlayerDelegate(playerRepository.GetSelectablePlayersForFantasyTeam), position, currentFantasyTeamTurnID);
            }
        }

        void StageManagementWrapper(List<Stage> stages, PlayerAdapter playerAdapter, List<FantasyTeam> fantasyTeams)
        {
            /*
            Wrapper method over the method calls that are used when preparing for a new stage
            */

            stageManagerXOR = stages[0].StartingManager;
            // stages[0] will always contain the next stage
            AssembleNewStage(stages[0], playerAdapter, fantasyTeams);
            // Set which positions can be selected, just before the stage is removed - data required for the next turn in the same stage
            currentSelectablePositions = stages[0].SelectablePositions;
            // Remove stage from the list now it's been assembled for the user
            stages.RemoveAt(0);
        }
    }
}