//Connexion avec la base do
using MySql.Data.MySqlClient;

namespace test_app.Data
{
    public static class DatabaseConfig
    {
        private const string host = "127.0.0.1";
        private const string database = "QuizDatabase";
        private const string user = "root";
        private const string password = "root";
        private const string port = "8889"; 

        public static MySqlConnection GetConnection()
{
    var connectionString = $"Server={host};Database={database};Uid={user};Pwd={password};Port={port};SslMode=None;CharSet=utf8;Connection Timeout=30;default command timeout=120;Pooling=true;";
    return new MySqlConnection(connectionString);
}

    }
}
