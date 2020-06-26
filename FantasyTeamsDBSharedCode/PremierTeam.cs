using System;

using SQLite;

namespace FantasyTeamsDBSharedCode
{
    [Table("PremierTeams")]
    public class PremierTeam
    {
        private string premierTeamName;

        public PremierTeam()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int PremierTeamID { get; set; }
        public string PremierTeamName
        {
            get { return premierTeamName; }
            set
            {
                if (value.Length < 1 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Premier team's name can't be longer than 20 characters");
                }
                premierTeamName = value;
            }
        }
    }
}
