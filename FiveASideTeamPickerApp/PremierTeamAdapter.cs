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

namespace FiveASideTeamPickerApp
{
    class PremierTeamAdapter : BaseAdapter<PremierTeam>
    {
        private readonly Activity _context;
        private List<PremierTeam> _premierTeams;
        private readonly int _listViewLayout;

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
            // TODO - Check this doesn't mess up a text 1 in admin player bit or when picking a team
            TextView premierTeamNameTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            premierTeamNameTextView.Text = premierTeam.PremierTeamName;

            return view;
        }
    }
}