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
using SQLite;

namespace FantasyTeamsDBSharedCode.SQLite_Implementation
{
    class SQLitePlayerRepository : IPlayerRepository
    {
        public SQLitePlayerRepository()
        {
            SQLiteConnection dbConnection = SQLiteConnector.Connection;
        }
    }
}