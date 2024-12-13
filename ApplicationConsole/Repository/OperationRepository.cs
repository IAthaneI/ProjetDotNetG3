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
        /// Récupere les enregistrements sur une période donnée
        /// Pour un compte client si numCompte renseigné
        /// Sinon pour tous les clients
        /// </summary>
        /// <param name="dateDebut"></param>
        /// <param name="dateFin"></param>
        /// <param name="numCompte"></param>
        /// <returns>Liste de carte bancaires</returns>
        public List<OperationModel> GetOperations(DateTime dateDebut, DateTime dateFin, string numCompte = null)
        {
            List<OperationModel> opModels = new List<OperationModel>();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query1 = "SELECT cpt.NumCompte, cb.NumCarte, cb.NomTitulaire, cpt.DateOuverture, cpt.Solde, cb.DateExpiration, enr.Montant, enr.Type, enr.DateOp " +
                    "FROM dbo.CompteBancaire cpt " +
                    "LEFT JOIN dbo.CarteBancaire cb ON cpt.Id = cb.Id " +
                    "JOIN dbo.Enregistrement enr ON enr.IdCarteBancaire = cb.Id " +
                    "WHERE enr.DateOp BETWEEN @pDateDebut AND @pDateFin " +
                    "ORDER BY cpt.NumCompte, enr.DateOp";
                string query2 = "SELECT cpt.NumCompte, cb.NumCarte, cb.NomTitulaire, cpt.DateOuverture, cpt.Solde, cb.DateExpiration, enr.Montant, enr.Type, enr.DateOp " +
                    "FROM dbo.CompteBancaire cpt " +
                    "LEFT JOIN dbo.CarteBancaire cb ON cpt.Id = cb.Id " +
                    "JOIN dbo.Enregistrement enr ON enr.IdCarteBancaire = cb.Id " +
                    "WHERE enr.DateOp BETWEEN @pDateDebut AND @pDateFin AND cpt.NumCompte = @pNumCompte " +
                    "ORDER BY enr.DateOp";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = string.IsNullOrWhiteSpace(numCompte) ? query1 : query2;
                    DBUtilities.AddParameter(command, "pDateDebut", dateDebut, "DateOp");
                    DBUtilities.AddParameter(command, "pDateFin", dateFin, "DateOp");
                    if(!string.IsNullOrWhiteSpace(numCompte))
                    {
                        DBUtilities.AddParameter(command, "pNumCompte", numCompte, "NumCompte");
                    }
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
