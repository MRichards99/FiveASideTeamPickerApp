using System.IO;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private SQLiteFantasyTeamRepository fantasyTeamRepository;

        public MainActivity()
        {
            fantasyTeamRepository = new SQLiteFantasyTeamRepository();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
                // Open register teams page provided there aren't already 2 teams registered
                int numberOfFantasyTeams = fantasyTeamRepository.GetNumberOfFantasyTeams();
                if (numberOfFantasyTeams < 2)
                {
                    StartActivity(typeof(RegisterTeamActivity));
                }
                else
                {
                    // User can delete 1 or more teams if two already exist
                    StartActivity(typeof(DeleteFantasyTeamActivity));
                }
            };

            // Open page to select players for teams when relevant button clicked
            pickTeamsButton.Click += (sender, args) =>
            {
                int numberOfFantasyTeams = fantasyTeamRepository.GetNumberOfFantasyTeams();
                if (numberOfFantasyTeams != 2)
                {
                    Toast.MakeText(this, $"Please ensure you have 2 teams registered before picking teams. There are currently {numberOfFantasyTeams} team(s) registered", ToastLength.Short).Show();
                }
                else
                {
                    StartActivity(typeof(PickTeamsActivity));
                }
            };

            // Open admin interface when relevant button clicked
            adminInterfaceButton.Click += (sender, args) =>
            {
                StartActivity(typeof(AdminInterfaceActivity));
            };

            // Open a stream to database file in Resources/Raw to be written to app's personal folder
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // File name to use when copied
            var dbFile = Path.Combine(docFolder, "FiveASide.sqlite");
            // Don't repeat these steps if the database is already part of the app's data
            if (!File.Exists(dbFile))
            {
                // Get DB file from Resources and stream the data
                var s = Resources.OpenRawResource(Resource.Raw.FantasyTeams);
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamExistingDatabase(s, writeStream);
            }
        }

        private void StreamExistingDatabase(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            byte[] buffer = new byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // Write data when it's been ingested
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}
