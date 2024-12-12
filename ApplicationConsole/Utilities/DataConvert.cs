﻿using BankLib.Models;
using System.Data;

namespace ApplicationConsole.Utilities
{
    /// <summary>
    /// Convertit le retour d'une table de la BDD en Modèle
    /// </summary>
    public class DataConvert
    {
        /// <summary>
        /// Convertit la table CompteBancaire en liste de CompteBancaireModel
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>Liste de compte bancaires</returns>
        public static List<CompteBancaireModel> ToCompteBancaireModel(DataTable dataTable)
        {
            var cbModel = new List<CompteBancaireModel>();
            if (dataTable == null || dataTable.Rows.Count == 0) return cbModel;
            foreach (DataRow row in dataTable.AsEnumerable())
            {
                cbModel.Add(new CompteBancaireModel
                {
                    Id = row.Field<int>("Id"),
                    NumCompte = row.Field<string>("NumCompte") ?? string.Empty,
                    DateOuverture = row.Field<DateTime>("DateOuverture"),
                    Solde = (double)row.Field<decimal>("Solde")
                });
            }
            return cbModel;
        }

        /// <summary>
        /// Convertit la table CarteBancaire en liste de CarteBancaireModel
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>Liste de compte bancaires</returns>
        public static List<CarteBancaireModel> ToCarteBancaireModel(DataTable dataTable)
        {
            var cbModel = new List<CarteBancaireModel>();
            if (dataTable == null || dataTable.Rows.Count == 0) return cbModel;
            foreach (DataRow row in dataTable.AsEnumerable())
            {
                cbModel.Add(new CarteBancaireModel
                {
                    Id = row.Field<int>("Id"),
                    NumCarte = row.Field<string>("NumCarte") ?? string.Empty,
                    DateExpiration = row.Field<DateTime>("DateExpiration"),
                    NomTitulaire = row.Field<string>("NomTitulaire") ?? string.Empty,
                    CompteBancaireId = row.Field<int>("CompteBancaireId"),
                });
            }
            return cbModel;
        }
    }

}
