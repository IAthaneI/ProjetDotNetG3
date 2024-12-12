﻿using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Models;
using BankLib.Utilities;
using System.Data;
using System.Data.Common;

namespace ApplicationConsole.Repository
{
    /// <summary>
    /// Classe d'accès en BDD pour Compte Bancaire
    /// </summary>
    public class CompteBancaireRepository
    {
        private DbConnection? connection;
        public CompteBancaireRepository()
        {
            connection = DBUtilities.GetConnection();
        }

        /// <summary>
        /// Récupere tous les comptes en BDD
        /// </summary>
        /// <returns>Liste de compte bancaires</returns>
        public List<CompteBancaireModel> GetCompteBancaires()
        {
            List<CompteBancaireModel> cModels = new List<CompteBancaireModel>();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT * FROM CompteBancaire";
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
                return DataConvert.ToCompteBancaireModel(table);
            }
            return cModels;
        }

        /// <summary>
        /// Récupère un compte en DBB
        /// </summary>
        /// <param name="id">id du CompteBancaire recherché</param>
        /// <returns>CompteBancaireModel</returns>
        public CompteBancaireModel GetCompteBancaire(int id)
        {
            CompteBancaireModel cModel = new CompteBancaireModel();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT * FROM CompteBancaire where Id = @pId";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "pId", id, "Id");
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
                return DataConvert.ToCompteBancaireModel(table).FirstOrDefault();
            }
            return cModel;
        }

        /// <summary>
        ///  Récupère un compte en DBB dont le numero de compte est passé en parametre
        /// </summary>
        /// <param name="numCompte"></param>
        /// <returns></returns>
        public CompteBancaireModel GetCompteBancaire(string numCompte)
        {
            CompteBancaireModel cModel = new CompteBancaireModel();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT * FROM CompteBancaire where NumCompte = @pNumCompte";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "pNumCompte", numCompte, "NumCompte");
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
                return DataConvert.ToCompteBancaireModel(table).FirstOrDefault();
            }
            return cModel;
        }

        /// <summary>
        /// Ajoute un compte bancaire en BDD
        /// </summary>
        /// <param name="compteBancaire"></param>
        /// <returns>True si l'ajout s'est bien passé sinon False</returns>
        public bool InsertCompteBancaire(CompteBancaire compteBancaire)
        {
            int res = 0;
            if (connection != null)
            {
                if(compteBancaire == null)
                    Console.WriteLine("Valeurs incorrectes");
                else
                    compteBancaire.NumCompte = SetUniqueNumCompte();
                if(string.IsNullOrWhiteSpace(compteBancaire.NumCompte))
                {
                    Console.WriteLine("Echec d'affectation de numéro de compte unique");
                    return false;
                }    
                string query = "INSERT INTO CompteBancaire Values (@pNumCompte, @pDateOuverture, @pSolde)";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "pNumCompte", compteBancaire.NumCompte, "NumCompte");
                    DBUtilities.AddParameter(command, "pDateOuverture", compteBancaire.DateOuverture, "DateOuverture");
                    DBUtilities.AddParameter(command, "pSolde", compteBancaire.Solde, "Solde");
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        /// <summary>
        /// Affecte un numéro de compte unique
        /// La fonction GetCompteBancaire s'execute jusqu'au Timeout de 30 secondes au maximum
        /// Elle s'arrete si elle retourne null => pas de correspondance
        /// </summary>
        /// <returns></returns>
        private string SetUniqueNumCompte()
        {
            string res = RandomTool.RandomString(10);
            if (RandomTool.RetryUntilSuccessOrTimeout(() => GetCompteBancaire(res) == null, TimeSpan.FromSeconds(30)))
            {
                return res;
            }
            return string.Empty;
        }
    }
}