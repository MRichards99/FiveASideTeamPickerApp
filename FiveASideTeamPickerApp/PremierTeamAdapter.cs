using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;

using FantasyTeamsDBSharedCode;

namespace FiveASideTeamPickerApp
{
    class PremierTeamAdapter : BaseAdapter<PremierTeam>
    {
        public delegate List<PremierTeam> GetAllPremierTeamsDelegate();

        private readonly Activity _context;
        private List<PremierTeam> _premierTeams;

        public PremierTeamAdapter(Activity context, IEnumerable<PremierTeam> premierTeams)
        {
            this._context = context;
            this._premierTeams = premierTeams.OrderBy(p => p.PremierTeamName).ToList();
        }

        public override PremierTeam this[int index]
        {
            get
            {
                return _premierTeams[index];
            }
        }

        public override int Count
        {
            get
            {
                return _premierTeams.Count;
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
                view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            PremierTeam premierTeam = _premierTeams[index];
            TextView premierTeamNameTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            premierTeamNameTextView.Text = premierTeam.PremierTeamName;

            return view;
        }

        public void AddPremierTeam(PremierTeam premierTeam)
        {
            _premierTeams.Add(premierTeam);
            _premierTeams.OrderBy(p => p.PremierTeamName).ToList();
        }

        public void RemoveAllPremierTeams()
        {
            _premierTeams.Clear();
        }

        public void AppendToPremierTeamList(GetAllPremierTeamsDelegate databaseQuery)
        {
            List<PremierTeam> queryResult = databaseQuery().OrderBy(p => p.PremierTeamName).ToList();

            foreach (PremierTeam premierTeam in queryResult)
            {
                AddPremierTeam(premierTeam);
            }
        }
    }
}
