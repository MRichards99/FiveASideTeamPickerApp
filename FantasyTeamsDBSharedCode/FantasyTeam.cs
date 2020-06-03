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
        public string FantasyTeamName { get; set; }
        public string ManagerFirstname { get; set; }
        public string ManagerSurname { get; set; }
    }
}
