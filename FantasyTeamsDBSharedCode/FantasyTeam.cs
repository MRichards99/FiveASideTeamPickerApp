using System;

using SQLite;

namespace FantasyTeamsDBSharedCode
{
    
    [Table("FantasyTeams")]
    public class FantasyTeam
    {
        private string fantasyTeamName;
        private string managerFirstname;
        private string managerSurname;

        public FantasyTeam()
        {

        }

        public FantasyTeam(string firstname, string surname, string teamName)
        {
            FantasyTeamName = teamName;
            ManagerFirstname = firstname;
            ManagerSurname = surname;
        }

        [PrimaryKey, AutoIncrement]
        public int FantasyTeamID { get; set; }

        [Unique]
        public string FantasyTeamName {
            get { return fantasyTeamName; }
            set
            {
                if (value.Length < 1 || value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Fantasy team name must be between 1 and 30 characters");
                }
                fantasyTeamName = value;
            }
        }

        public string ManagerFirstname
        {
            get { return managerFirstname; }
            set
            {
                if (value.Length < 1 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Manager's first name can't be longer than 20 characters");
                }
                managerFirstname = value;
            }
        }

        public string ManagerSurname
        {
            get { return this.managerSurname; }
            set
            {
                if (value.Length < 1 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Manager's surname can't be longer than 20 characters");
                }
                managerSurname = value;
            }
        }
    }
}
