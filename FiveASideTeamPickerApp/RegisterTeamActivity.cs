using System;

using Android.App;
using Android.OS;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "RegisterTeamActivity")]
    public class RegisterTeamActivity : Activity
    {
        private SQLiteFantasyTeamRepository fantasyTeamRepository;

        public RegisterTeamActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterTeam);

            // Get UI resources on this view
            EditText managerFirstNameEditText = FindViewById<EditText>(Resource.Id.managerFirstNameEditText);
            EditText managerSurnameEditText = FindViewById<EditText>(Resource.Id.managerSurnameEditText);
            EditText fantasyTeamEditText = FindViewById<EditText>(Resource.Id.fantasyTeamEditText);
            Button saveButton = FindViewById<Button>(Resource.Id.registerTeamSaveButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.registerTeamCancelButton);

            // Assuming valid data from user, when save button is clicked, add fantasy team to DB and return user to main menu
            saveButton.Click += (sender, args) =>
            {
                // Validate user input
                Boolean validUserInput = true;
                FantasyTeam newTeam = null;

                string managerFirstName = managerFirstNameEditText.Text;
                string managerSurname = managerSurnameEditText.Text;
                string fantasyTeamName = fantasyTeamEditText.Text;

                try
                {
                    newTeam = new FantasyTeam(managerFirstName, managerSurname, fantasyTeamName);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    // TODO - Deal with invalid data, highlight relevant UI element causing issue and display message of exception on screen
                }

                if (validUserInput is true && newTeam != null)
                {
                    // Insert team to DB
                    fantasyTeamRepository.AddFantasyTeam(newTeam);

                    Finish();
                }
            };


            // When cancel button is clicked, return to the main menu
            cancelButton.Click += (sender, args) =>
            {
                Finish();
            };
        }
    }
}
