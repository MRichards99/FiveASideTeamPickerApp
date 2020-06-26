using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;

using FantasyTeamsDBSharedCode;
using FantasyTeamsDBSharedCode.SQLite_Implementation;

namespace FiveASideTeamPickerApp
{
    public class PlayerAdapter : BaseAdapter<Player>
    {
        // Defining delegates to be used with this adapter
        public delegate List<Player> NoParameterPlayerDelegate();
        public delegate List<Player> PositionPlayerDelegate(Position position, int fantasyTeamID);

        private readonly Activity _context;
        private List<Player> _players;
        private readonly int _listViewLayout;

        private SQLitePremierTeamRepository premierTeamRepository;
        private SQLitePositionRepository positionRepository;

        public PlayerAdapter(Activity context, IEnumerable<Player> players, int listViewLayout)
        {
            this._context = context;
            this._players = players.OrderBy(p => p.Surname).ToList();
            this._listViewLayout = listViewLayout;

            premierTeamRepository = new SQLitePremierTeamRepository();
            positionRepository = new SQLitePositionRepository();
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

            // Change formatting of TextView depending on if firstname is empty or not
            // An empty first name means the TextView doesn't need a space to separate first and last names
            string premierTeamName = premierTeamRepository.GetPremierTeamNameFromID(player.PremierTeamID);
            playerNameAndTeam.Text = NameTeamTextFormatting.FormatNameAndTeam(player.Firstname, player.Surname, premierTeamName);

            // Add position and player's price onto second line of text
            string playerPositionName = positionRepository.GetPositionNameByID(player.PositionID);
            playerAdditionalDetails.Text = playerPositionName + ", £" + player.Price + " million";
             
            return view;
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
            List<Player> queryResult = databaseQuery(position, fantasyTeamID).OrderBy(s => s.Surname).ToList();

            foreach (Player player in queryResult)
            {
                // Appending to the list means the data can be rebuilt from scratch (used with RemoveAllPlayers()) or added to when needed
                AddPlayer(player);
            }
        }
    }
}
