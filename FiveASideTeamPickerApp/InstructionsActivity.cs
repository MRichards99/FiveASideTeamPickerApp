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