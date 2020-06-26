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
                premierTeam.PremierTeamName = premierTeamName.Text;

                if (pageType == "existing")
                {
                    premierTeamRepository.UpdatePremierTeam(premierTeam);
                }
                else if (pageType == "new")
                {
                    premierTeamRepository.AddPremierTeam(premierTeam);
                }

                SetResult(Result.Ok);
                Finish();
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
