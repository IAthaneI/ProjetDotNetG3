using System.Xml.Serialization;

namespace BankLib.Models
{
    /// <summary>
    /// Modele contenant une opération
    /// </summary>
    [Serializable]
    public class OperationModel
    {
        public string NumCompte { get; set; }

        [XmlElement(ElementName = "nomTitulaire")]
        public string NomTitulaire { get; set; }

        [XmlElement(ElementName = "dateOuvertureCompte")]
        public DateTime DateOuverture { get; set; }

        [XmlElement(ElementName = "soldeCompte")]
        public double Solde { get; set; }

        [XmlElement(ElementName = "numeroCarte")]
        public string NumCarte { get; set; }

        [XmlElement(ElementName = "dateExpirationCarte")]
        public DateTime DateExpiration { get; set; }

        [XmlElement(ElementName = "dateOperation")]
        public DateTime DateOp { get; set; }
        
        [XmlElement(ElementName = "typeOperation")]
        public TypeOperation TypeOperation { get; set; }

        [XmlElement(ElementName = "montantOperation")]
        public double Montant { get; set; }
    }
}
