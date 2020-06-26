namespace FiveASideTeamPickerApp
{
    static class NameTeamTextFormatting
    {
        public static string FormatNameAndTeam(string firstName, string surname, string teamName)
        {
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

            return formattedText;
        }
    }
}
