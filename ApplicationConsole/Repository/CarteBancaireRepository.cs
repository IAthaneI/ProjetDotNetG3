using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Models;

namespace ApplicationConsole.Repository
{
    internal class CarteBancaireRepository
    {
        private DbConnection? connection;
        public CarteBancaireRepository()
        {

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
