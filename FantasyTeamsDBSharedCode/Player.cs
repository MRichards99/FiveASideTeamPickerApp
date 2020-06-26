using System;

using SQLite;

namespace FantasyTeamsDBSharedCode
{
    [Table("Players")]
    public class Player
    {
        private string firstName;
        private string surname;

        public Player()
        {
        }

        public Player(string firstName, string surname, int premierTeamID, int fantasyTeamID, int positionID, double price)
        {
            this.Firstname = firstName;
            this.Surname = surname;
            this.FantasyTeamID = fantasyTeamID;
            this.PositionID = positionID;
            this.Price = price;
        }

        [PrimaryKey, AutoIncrement]
        public int PlayerID { get; set; }
        public string Firstname
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (value.Length < 1 || value.Length > 30)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Player's surname can't be longer than 30 characters");
                }
                surname = value;
            }
        }

        public int PremierTeamID { get; set; }
        public int FantasyTeamID { get; set; }
        public int PositionID { get; set; }

        public double Price { get; set; }
    }
}
