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
using static FantasyTeamsDBSharedCode.Player;

namespace FiveASideTeamPickerApp
{
    public class PlayerAdapter : BaseAdapter<Player>
    {
        // Defining delegates to be used with this adapter
        public delegate List<Player> NoParameterPlayerDelegate();
        public delegate List<Player> PositionPlayerDelegate(Position position, int fantasyTeamID);

        // TODO - Should I change all my private variables to private readonly and start with underscores?
        private readonly Activity _context;
        private List<Player> _players;
        private readonly int _listViewLayout;

        // TODO - Check if these variable should be private or not and make it consistent across the files
        private SQLitePremierTeamRepository premierTeamRepository;
        private SQLitePositionRepository positionRepository;

        public PlayerAdapter(Activity context, IEnumerable<Player> players, int listViewLayout)
        {
            this._context = context;
            this._players = players.OrderBy(s => s.Surname).ToList();
            this._listViewLayout = listViewLayout;

            this.premierTeamRepository = new SQLitePremierTeamRepository();
            this.positionRepository = new SQLitePositionRepository();
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
                view = _context.LayoutInflater.Inflate(this._listViewLayout, null);
            }

            // Get player and the two text fields that'll display player data
            Player player = _players[index];
            TextView playerNameAndTeam = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            TextView playerAdditionalDetails = view.FindViewById<TextView>(Android.Resource.Id.Text2);

            // TODO - decide if I wish to change any other properties of the text fields

            // Change formatting of TextView depending on if firstname is empty or not
            // An empty first name means the TextView doesn't need a space to separate first and last names
            string premierTeamName = premierTeamRepository.GetPremierTeamNameFromID(player.PremierTeamID);
            playerNameAndTeam.Text = NameTeamTextFormatting.FormatNameAndTeam(player.Firstname, player.Surname, premierTeamName);

            // Add position and player's price onto second line of text
            string playerPositionName = positionRepository.GetPositionNameByID(player.PositionID);
            // TODO - Change all of these to formatted strings
            playerAdditionalDetails.Text = playerPositionName + ", £" + player.Price + " million";
             
            return view;
        }

        public void UpdatePlayer(int index, Player player)
        {
            // Cannot add a setter to `this[int index]` as this would invalid the method from BaseAdapter
            _players[index] = player;

            // Automatically refresh the data view
            this.NotifyDataSetChanged();
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            _players.OrderBy(s => s.Surname).ToList();
        }

        public void RemoveAllPlayers()
        {
            _players.Clear();       
        }

        public void AppendToPlayerList(NoParameterPlayerDelegate databaseQuery)
        {
            List<Player> queryResult = databaseQuery().OrderBy(s => s.Surname).ToList();
            foreach (Player player in queryResult)
            {
                AddPlayer(player);
            }
        }

        public void AppendToPlayerList(PositionPlayerDelegate databaseQuery, Position position, int fantasyTeamID)
        {
            // TODO - Can we do away with RebuildPlayerList?
            List<Player> queryResult = databaseQuery(position, fantasyTeamID).OrderBy(s => s.Surname).ToList();

            foreach (Player player in queryResult)
            {
                // Appending to the list means the data can be rebuilt from scratch (used with RemoveAllPlayers()) or added to when needed
                AddPlayer(player);
            }
        }
    }
}