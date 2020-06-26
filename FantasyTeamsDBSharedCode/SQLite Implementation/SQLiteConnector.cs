using System.IO;

using SQLite;

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    public class SQLiteConnector
    {
        /* Static property so it can be retrieved without having to create an instance of SQLiteConnector,
         * Ideal for a container type class such as this
         */

        public static SQLiteConnection Connection
        {
            get
            {
                return new SQLiteConnection(DatabaseFileLocation);
            }
        }

        private static string DatabaseFileLocation
        {
            get
            {
                string fileLocation = "FiveASide.sqlite";
                return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileLocation);
            }
        }
    }
}
