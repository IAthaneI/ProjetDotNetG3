using System.Xml.Serialization;

namespace BankLib.Models
{
    /// <summary>
    /// Modele contenant une opération
    /// </summary>
    [Serializable]
    public class OperationModel
    {
        public int Id { get; set; }

        public string NumCompte { get; set; }

        public string NumCarte { get; set; }

        public string NomTitulaire { get; set; }

        public DateTime DateOuverture { get; set; }

        public double Solde { get; set; }

        public DateTime DateExpiration { get; set; }

        public double Montant { get; set; }

        public Type Type { get; set; }

        public DateTime DateOp { get; set; }
    }
}
