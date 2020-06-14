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
using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;


namespace FiveASideTeamPickerApp
{
    public class PlayerAdapter : BaseAdapter<Player>
    {
        private readonly Activity _context;
        private readonly List<Player> _players;
        private SQLitePremierTeamRepository premierTeamRepository;

        public PlayerAdapter(Activity context, IEnumerable<Player> players)
        {
            this._context = context;
            this._players = players.OrderBy(s => s.Surname).ToList();
            this.premierTeamRepository = new SQLitePremierTeamRepository();
        }

        public override Player this[int index]
        {
            get
            {
                return _players[index];
            }
        } 

        public override int Count
        {
            get
            {
                return _players.Count;
            }
        }

        public override long GetItemId(int index)
        {
            return index;
        }

        public override View GetView(int index, View convertView, ViewGroup parent)
        {
            View view = convertView;
            
            // Create a view if one doesn't already exist
            if (view == null)
            { 
                view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            // Get player and the two text fields that'll display player data
            Player player = _players[index];
            TextView playerName = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            TextView playerPremierTeam = view.FindViewById<TextView>(Android.Resource.Id.Text2);

            // TODO - decide if I wish to change any other properties of the text fields

            // Change formatting of TextView depending on if firstname is empty or not
            // An empty first name means the TextView doesn't need a space to separate first and last names
            if (string.IsNullOrEmpty(player.Firstname))
            {
                // Blank firstname
                playerName.Text = player.Surname;
            }
            else
            {
                playerName.Text = player.Firstname + " " + player.Surname;
            }
            string premierTeamName = premierTeamRepository.GetPremierTeamNameFromID(player.PremierTeamID);
            playerPremierTeam.Text = premierTeamName;
             
            return view;
        }
    }
}