using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Models;
using BankLib;

namespace ApplicationConsole.Repository
{
    internal class CarteBancaireRepository
    {
        private DbConnection? connection;
        public CarteBancaireRepository()
        {
            connection = DBUtilities.GetConnection();
        }

        /// <summary>
        /// Récupere tous les cartes en BDD
        /// </summary>
        /// <returns>Liste de carte bancaires</returns>
        public List<CarteBancaireModel> GetCarteBancaires()
        {
            List<CarteBancaireModel> cModels = new List<CarteBancaireModel>();
            if (connection != null)
            {
                DataTable table = new DataTable();
                // TODO : préciser les champs au lieu de *
                string query = "SELECT * FROM CarteBancaire";
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
                return DataConvert.ToCarteBancaireModel(table);
            }
            return cModels;
        }

        /// <summary>
        /// Récupère un compte en DBB
        /// </summary>
        /// <param name="id">id du CarteBancaire recherché</param>
        /// <returns>CarteBancaireModel</returns>
        public CarteBancaireModel GetCarteBancaire(int id)
        {
            CarteBancaireModel cModel = new CarteBancaireModel();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT * FROM CarteBancaire where Id = @pId";
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
                return DataConvert.ToCarteBancaireModel(table).FirstOrDefault();
            }
            return cModel;
        }

        /// <summary>
        ///  Récupère une carte en DBB dont le numeroSuffixe est passé en parametre
        /// </summary>
        /// <param name="numCarte"></param>
        /// <returns></returns>
        public CarteBancaireModel GetCarteBancaire(string numCarte)
        {
            CarteBancaireModel cModel = new CarteBancaireModel();
            if (connection != null)
            {
                DataTable table = new DataTable();
                string query = "SELECT * FROM CarteBancaire where NumCarte = @pNumCarte";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "pNumCarte", numCarte, "NumCarte");
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
                return DataConvert.ToCarteBancaireModel(table).FirstOrDefault();
            }
            return cModel;
        }

        /// <summary>
        /// Ajoute une carte bancaire en BDD
        /// </summary>
        /// <param name="carteBancaire"></param>
        /// <returns>True si l'ajout s'est bien passé sinon False</returns>
        public bool InsertCarteBancaire(CarteBancaireModel carteBancaire)
        {
            int res = 0;
            if (connection != null)
            {
                if (carteBancaire == null)
                    Console.WriteLine("Valeurs incorrectes");
                else
                    carteBancaire.NumCarte = SetUniqueNumCarte();
                if (string.IsNullOrWhiteSpace(carteBancaire.NumCarte))
                {
                    Console.WriteLine("Echec d'affectation de numéro de carte unique");
                    return false;
                }
                string query = "INSERT INTO CarteBancaire Values (@pNumCarte, @pDateExpiration, @pNomTitulaire, @pCompteBancaireId)";
                try
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "pNumCarte", carteBancaire.NumCarte, "NumCarte");
                    DBUtilities.AddParameter(command, "pDateExpiration", carteBancaire.DateExpiration, "DateExpiration");
                    DBUtilities.AddParameter(command, "pNomTitulaire", carteBancaire.NomTitulaire, "NomTitulaire");
                    DBUtilities.AddParameter(command, "pCompteBancaireId", carteBancaire.CompteBancaireId, "CompteBancaireId");
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
        /// Affecte un numéro de carte unique
        /// La fonction GetCarteBancaire s'execute jusqu'au Timeout de 30 secondes au maximum
        /// Elle s'arrete si elle retourne null => pas de correspondance
        /// </summary>
        /// <returns></returns>
        private string SetUniqueNumCarte()
        {
            int rand = RandomTool.RandomInt(Constantes.CARTE_BANCAIRE_NUM_MAX_VAL);
            string numCarteLong = Constantes.CARTE_BANCAIRE_NUM_PREFIXE + rand;
            if (ValidationTool.AlgoLuhn(numCarteLong))
            {
                if (ValidationTool.RetryUntilSuccessOrTimeout(() => GetCarteBancaire(numCarteLong) == null, TimeSpan.FromSeconds(Constantes.RANDOM_WAIT_TIMEOUT)))
                {
                    return new string($"{rand:D4}");
                }
            }
            return string.Empty;
        }
      
        public List<CarteBancaire> GetCarteBancaireOfCompte(int? idCompte)
        {

            List<CarteBancaire> result = new List<CarteBancaire>();
            if (idCompte == null) return result;
            connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT ca.Id as Id, ca.NumCarte as NumCarte, ca.DateExpiration as DateExp, ca.NomTitulaire as NomTitulaire " +
                    "FROM CarteBancaire ca JOIN CompteBancaire co ON ca.CompteBancaireId = co.Id " +
                    "WHERE ca.CompteBancaireId = @IdCompte";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "IdCompte", idCompte, "Id");

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarteBancaire carteBancaire = new CarteBancaire();
                    carteBancaire.Id = reader.GetInt32(0);
                    carteBancaire.NumCarteSuffixe = Int32.Parse(reader.GetString(1));
                    carteBancaire.DateExpiration = new DateOnly(reader.GetDateTime(2).Year, reader.GetDateTime(2).Month, reader.GetDateTime(2).Day);
                    carteBancaire.NomTitulaire = reader.GetString(3);
                    result.Add(carteBancaire);
                }
                connection.Close();
            }
            return result;
        }

        public int GetIdCarteBancaireByNumCarte(string numCarte)
        {
            int result = -1;
            connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT Id FROM CarteBancaire WHERE NumCarte = @NumCarte ";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "NumCarte", numCarte.Substring(12), "NumCarte");

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
                connection.Close();
            }
            return result;
        }

        public int GetNewMaxId()
        {
            int id = -1;
            connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT MAX(Id) as idMax FROM CarteBancaire ";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                connection.Close();
            }
            return ++id;
        }
    }
}
