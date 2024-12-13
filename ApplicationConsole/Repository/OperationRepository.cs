using ApplicationConsole.Utilities;
using BankLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsole.Repository
{
    /// <summary>
    /// Classe d'accès en BDD aux Entregistrements
    /// </summary>
    public class OperationRepository
    {
        private DbConnection? connection;

        public OperationRepository()
        {
            connection = DBUtilities.GetConnection();
        }

        /// <summary>
        /// Récupere les enregistrements
        /// Sur une période donnée
        /// Pour un compte client
        /// </summary>
        /// <returns>Liste de carte bancaires</returns>
        public List<OperationModel> GetOperations()
        {
            List<OperationModel> opModels = new List<OperationModel>();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT cpt.NumCompte, cb.NumCarte, cb.NomTitulaire, cpt.DateOuverture, cpt.Solde, cb.DateExpiration, enr.Montant, enr.Type, enr.DateOp " +
                    "FROM dbo.CompteBancaire cpt " +
                    "LEFT JOIN dbo.CarteBancaire cb ON cpt.Id = cb.Id " +
                    "JOIN dbo.Enregistrement enr ON enr.IdCarteBancaire = cb.Id " +
                    "WHERE enr.DateOp BETWEEN '2024-09-12 00:00:00' AND '2024-30-12 00:00:00' " +
                    "ORDER BY cpt.NumCompte, enr.DateOp";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DbDataReader dbDataReader = command.ExecuteReader();
                    table.Load(dbDataReader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return DataConvert.ToOperationModel(table);
            }
            return opModels;
        }
    }
}
