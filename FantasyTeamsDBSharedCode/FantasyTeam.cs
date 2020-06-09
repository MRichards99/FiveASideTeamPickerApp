using SQLite;
using System;

namespace FantasyTeamsDBSharedCode
{
    
    [Table("FantasyTeams")]
    public class FantasyTeam
    {
        public FantasyTeam()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int FantasyTeamID { get; set; }
        public string FantasyTeamName {
            get { return FantasyTeamName; }
            set
            {
                if (value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Fantasy team name can't be longer than 30 characters");
                }
                FantasyTeamName = value;
            }
        }

        public string ManagerFirstname
        {
            get { return ManagerFirstname; }
            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Manager's first name can't be longer than 20 characters");
                }
                ManagerFirstname = value;
            }
        }

        public string ManagerSurname
        {
            get { return ManagerSurname; }
            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Manager's surname can't be longer than 20 characters");
                }
                ManagerSurname = value;
            }
        }
    }
}
