using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    public class SQLiteConnector
    {

        // Static property so it can be retrieved without having to create an instance of SQLiteConnector
        // Ideal for a container type class such as this
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
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileLocation);

                return path;
            }
        }
    }
}