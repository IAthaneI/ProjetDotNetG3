using System.Configuration;
using System.Data.Common;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;

namespace ApplicationConsole.Utilities
{
    /// <summary>
    /// Cette classe permet l'Authentification d'un utilisateur a l'application
    /// </summary>
    internal class Authent
    {
        /// <summary>
        /// Cette methode va permettre la connection d'un utilisateur
        /// </summary>
        /// <param name="login"></param> 
        /// <param name="password"></param> 
        /// <returns>Renvoi true si la paire existe en base false sinon</returns>
        public static bool Login(string? login, string? password) 
        {
            // On verifie si les champs ne sont pas vide car ça sert a rien d'aller plus loin sinon
            if (String.IsNullOrEmpty(login) || login.Length > 50
                || String.IsNullOrEmpty(password) || password.Length > 50)
            {
                Console.WriteLine("ERROR - Login or Password not valid");
                return false;
            }

            DbConnection? connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT * FROM Authent WHERE login = @login";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "login", login, "login");

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    // Verification du mots de passe
                    if (reader["password"].Equals(password)) 
                    {
                        return true;
                    }
                }
                connection.Close();
            }
            return false;

        }

        /// <summary>
        /// Cette methode va permettre l'inscription d'un utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Renvoi true si l'inscription a reussie false sinon</returns>
        public static bool Register(string? login, string? password) 
        {
            // On verifie si les champs ne sont pas vide car ça sert a rien d'aller plus loin sinon
            if (String.IsNullOrEmpty(login) || login.Length > 50
                || String.IsNullOrEmpty(password) || password.Length > 50) 
            { 
                Console.WriteLine("ERROR - Login or Password not valid");
                return false;
            }

            DbConnection? connection = DBUtilities.GetConnection();
            if (connection != null) 
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Authent Values (@login, @password)";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "login", login, "login");
                    DBUtilities.AddParameter(command, "password", password, "password");

                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result > 0;

                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR - Error during register, User already exist ");
                }
            }

            return false;
        }

    }
}
