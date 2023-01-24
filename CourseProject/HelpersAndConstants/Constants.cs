namespace CourseProject.HelpersAndConstants
{
    internal class Constants
    {
        public static string folderName = "Json";

        public static string visitorsFileName = "visitors.json";
        public static string coachesFileName = "coaches.json";

        public static string visitorsCredentialsFileName = "visitorsCredentials.json";
        public static string coachesCredentialsFileName = "coachesCredentials.json";
        public static string equipmentFileName = "equipment.json";

        public static string equipmentJsonPath = GetFilePath(folderName, equipmentFileName);
        public static string visitorsJsonPath = GetFilePath(folderName, visitorsFileName);
        public static string coachesJsonPath = GetFilePath(folderName, coachesFileName);
        public static string visitorsCredentialsJsonPath = GetFilePath(folderName, visitorsCredentialsFileName);
        public static string coachesCredentialsJsonPath = GetFilePath(folderName, coachesCredentialsFileName);

        public static string[] columnNames = { "Id", "Category", "Name", "Quantity" };

        public static string GetFilePath(string folderName, string fileName) => Path.Combine($"..\\..\\..\\{folderName}", fileName);
    }
}
