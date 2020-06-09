using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.IO;

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

            // Open a stream to database file in Resources/Raw to be written to app's personal folder
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // File name to use when copied
            var dbFile = Path.Combine(docFolder, "FiveASide.sqlite");
            // Don't repeat these steps if the database is already part of the app's data
            if (!System.IO.File.Exists(dbFile))
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