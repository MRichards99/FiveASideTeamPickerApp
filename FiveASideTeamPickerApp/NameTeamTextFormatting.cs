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

namespace FiveASideTeamPickerApp
{
    static class NameTeamTextFormatting
    {
        public static string FormatNameAndTeam(string firstName, string surname, string teamName)
        {
            //Console.WriteLine($"Firstname: {firstName}, Surname: {surname}, Team Name: {teamName}");

            string formattedText = "";

            if (string.IsNullOrEmpty(firstName))
            {
                // Blank firstname
                 formattedText = surname;
            }
            else
            {
                formattedText = firstName + " " + surname;
            }
            formattedText += ", " + teamName;

            //Console.WriteLine(formattedText);

            return formattedText;
        }

    }
}