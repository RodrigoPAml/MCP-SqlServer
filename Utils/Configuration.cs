namespace Server.Utils
{
    public static class Configuration
    {
        public static string GetConnectionString()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "env.database");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Database configuration file not found at {filePath}");

            return File.ReadAllText(filePath);
        }
    }
}
