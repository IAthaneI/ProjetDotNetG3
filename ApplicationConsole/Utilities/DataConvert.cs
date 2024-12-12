using BankLib.Entities;
using BankLib.Model;
using BankLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Id = (byte)row.Field<int>("Id"),
                    NumCompte = row.Field<string>("NumCompte") ?? string.Empty,
                    DateOuverture = row.Field<DateTime>("DateOuverture"),
                    Solde = (double)row.Field<decimal>("Solde"),
                });
            }
            return cbModel;
        }
    }
}
