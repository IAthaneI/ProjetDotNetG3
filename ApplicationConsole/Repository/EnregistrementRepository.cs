using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationConsole.Utilities;
using BankLib.Model;
using BankLib.Models;
using Microsoft.Identity.Client;

namespace ApplicationConsole.Repository
{
    /// <summary>
    /// Cette classe va avoir toute les methodes pour acceder au enregistrement en base 
    /// </summary>
    internal class EnregistrementRepository
    {
        private DbConnection? connection;

        public EnregistrementRepository() { }

        /// <summary>
        /// Va récuperer tout les Enregistrements en bases
        /// </summary>
        /// <returns>
        /// Une liste d'enregistrement
        /// </returns>
        public List<Enregistrement> GetEnregistrements() 
        {
            List<Enregistrement> result = new List<Enregistrement>();
            connection = DBUtilities.GetConnection();
            if (connection != null) {
                connection.Open();
                string query = "SELECT * FROM Enregistrement";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enum.TryParse(reader.GetString(3), out TypeOperation op);
                    result.Add(new Enregistrement(reader.GetInt32(0), reader.GetString(1), Double.Parse(reader.GetDecimal(2).ToString()), op, reader.GetDateTime(4), reader.GetInt32(5)));
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Recupere un enregistrement celon l'ID précisé
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// L'enregistrement si il existe, null sinon 
        /// </returns>
        public Enregistrement? GetEnregistrement(int id) 
        {
            Enregistrement? result = null;
            connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT * FROM Enregistrement WHERE Id = @Id";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "Id", id, "Id");

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enum.TryParse(reader.GetString(3), out TypeOperation op);
                    result = new Enregistrement(reader.GetInt32(0), reader.GetString(1), Double.Parse(reader.GetDecimal(2).ToString()), op, reader.GetDateTime(4), reader.GetInt32(5));
                }

                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Permet d'inserer un enregistrement en base
        /// </summary>
        /// <param name="toInsert"></param>
        /// <returns>
        /// true si il a bien été inserer , false si ça a échouer
        /// </returns>
        public bool InsertEnregistrement(Enregistrement toInsert) 
        {
            connection = DBUtilities.GetConnection();
            if (connection != null) 
            {
                connection.Open();
                string query = "INSERT INTO Enregistrement Values (@Id, @NumCarte, @Montant, @TypeOp, @Date, @IdCarte)";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "Id", toInsert.Id, "Id");
                DBUtilities.AddParameter(command, "NumCarte", toInsert.NumCarte, "NumCarte");
                DBUtilities.AddParameter(command, "Montant", toInsert.Montant, "Montant");
                DBUtilities.AddParameter(command, "TypeOp", toInsert.Type, "Type");
                DBUtilities.AddParameter(command, "Date", toInsert.Date, "Date");
                DBUtilities.AddParameter(command, "IdCarte", toInsert.CarteBancaireId, "IdCarteBancaire");

                int nbRow = command.ExecuteNonQuery();

                connection.Close();
                
                return nbRow > 0;
            }
            return false;
        }
    }
}
