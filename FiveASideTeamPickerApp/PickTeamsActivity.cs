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
    [Activity(Label = "PickTeamsActivity")]
    public class PickTeamsActivity : Activity
    {
        TextView currentManagerTurn;
        ListView selectablePlayers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PickTeams);

            currentManagerTurn = FindViewById<TextView>(Resource.Id.currentManagerTurn);
            selectablePlayers = FindViewById<ListView>(Resource.Id.selectablePlayers);
            Button nextTurnButton = FindViewById<Button>(Resource.Id.nextTurnButton);
            

            //
            
        }

        void SelectManagerToPickPlayer()
    }
}