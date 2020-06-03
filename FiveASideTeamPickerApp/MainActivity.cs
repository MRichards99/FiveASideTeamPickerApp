using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Getting buttons on the main activity
            Button instructionsButton = FindViewById<Button>(Resource.Id.instructionsButton);
            Button registerTeamButton = FindViewById<Button>(Resource.Id.registerTeamButton);
            Button pickTeamsButton = FindViewById<Button>(Resource.Id.pickTeamsButton);
            Button adminInterfaceButton = FindViewById<Button>(Resource.Id.adminInterfaceButton);

            // Open instructions page when relevant button clicked
            instructionsButton.Click += (sender, args) =>
            {
                StartActivity(typeof(InstructionsActivity));
            };

            // Open page to register a team when relevant button clicked
            registerTeamButton.Click += (sender, args) =>
            {
                // Open register teams page
                StartActivity(typeof(RegisterTeamActivity));
            };

            // Open page to select players for teams when relevant button clicked
            pickTeamsButton.Click += (sender, args) =>
            {
                StartActivity(typeof(PickTeamsActivity));
            };

            // Open admin interface when relevant button clicked
            adminInterfaceButton.Click += (sender, args) =>
            {
                StartActivity(typeof(AdminInterfaceActivity));
            };
        }
    }
}