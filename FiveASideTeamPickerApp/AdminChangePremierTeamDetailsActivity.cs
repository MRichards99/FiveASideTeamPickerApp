using System;
using Newtonsoft.Json;

using Android.App;
using Android.OS;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "AdminChangePremierTeamDetailsActivity")]
    public class AdminChangePremierTeamDetailsActivity : Activity
    {
        private SQLitePremierTeamRepository premierTeamRepository;

        private PremierTeam premierTeam;

        public AdminChangePremierTeamDetailsActivity()
        {
            premierTeamRepository = new SQLitePremierTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminChangePremierTeamsDetailsLayout);

            EditText premierTeamName = FindViewById<EditText>(Resource.Id.premierTeamDetailsPremierTeamNameEditText);
            Button saveButton = FindViewById<Button>(Resource.Id.premierTeamDetailsSaveButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.premierTeamDetailsCancelButton);
            Button deleteButton = FindViewById<Button>(Resource.Id.premierTeamDetailsDeleteButton);

            string pageType = this.Intent.GetStringExtra("type");
            if (pageType == "existing")
            {
                string jsonPremierTeam = this.Intent.GetStringExtra("selectedPremierTeam");
                premierTeam = JsonConvert.DeserializeObject<PremierTeam>(jsonPremierTeam);

                premierTeamName.Text = premierTeam.PremierTeamName;
            }
            else
            {
                premierTeam = new PremierTeam();
                deleteButton.Enabled = false;
            }

            saveButton.Click += (sender, args) =>
            {
                Boolean validUserInput = true;

                try
                {
                    premierTeam.PremierTeamName = premierTeamName.Text;
                }
                catch(ArgumentOutOfRangeException)
                {
                    Toast.MakeText(this, Resource.String.invalid_add_premier_team_input, ToastLength.Long).Show();
                    validUserInput = false;
                }

                if (pageType == "existing" && validUserInput is true)
                {
                    premierTeamRepository.UpdatePremierTeam(premierTeam);
                    SetResult(Result.Ok);
                    Finish();
                }
                else if (pageType == "new" && validUserInput is true)
                {
                    premierTeamRepository.AddPremierTeam(premierTeam);
                    SetResult(Result.Ok);
                    Finish();
                }
            };

            deleteButton.Click += (sender, args) =>
            {
                premierTeamRepository.DeletePremierTeam(premierTeam);
                SetResult(Result.Ok);
                Finish();
            };

            cancelButton.Click += (sender, args) =>
            {
                Finish();
            };
        }
    }
}
