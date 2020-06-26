using Android.App;
using Android.OS;
using Android.Widget;

namespace FiveASideTeamPickerApp
{
    [Activity(Label = "InstructionsActivity")]
    public class InstructionsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InstructionsLayout);

            Button returnToMainMenuButton = FindViewById<Button>(Resource.Id.instructionsReturnToMainMenuButton);

            returnToMainMenuButton.Click += (sender, args) =>
            {
                Finish();
            };
        }
    }
}
