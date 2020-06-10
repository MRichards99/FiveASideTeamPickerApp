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
    [Activity(Label = "AdminInterfaceActivity")]
    public class AdminInterfaceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Setting view from Resources
            SetContentView(Resource.Layout.AdminInterface);

            // Get UI resources on this view
            Button resetButton = FindViewById<Button>(Resource.Id.appResetButton);
            Button adminPlayerButton = FindViewById<Button>(Resource.Id.adminPlayerButton);
            Button adminPremierTeamButton = FindViewById<Button>(Resource.Id.adminPremierTeamButton);



        }
    }
}