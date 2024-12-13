using System.ComponentModel.DataAnnotations.Schema;

namespace Serveur.Entities
{
    public enum TypeOperation
    {
        Retrait,
        FactureCarteBleue,
        DepotGuichet
    }
    public class Enregistrement
    {
        private const string BaseCardNumber = "497401850223"; // Base fixe pour générer des numéros
        private static readonly Random Random = new Random();

        public int Id { get; set; }
        public string NumCarte {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Montant {  get; set; }
        public TypeOperation TypeOperation { get; set; }
        public DateTime DateOperation { get; set; }
        public string Devise {  get; set; }
        [NotMapped]
        public bool EstValide { get; set; }



        // Constructeur pour générer automatiquement un numéro de carte
        public Enregistrement()
        {
            NumCarte = GenerateCardNumber(); // Génère un numéro de carte par défaut
        }

        // Surcharge : Constructeur avec un numéro de carte spécifique
        public Enregistrement(string numCarte)
        {
            NumCarte = numCarte ?? GenerateCardNumber(); // Utilise le numéro fourni ou en génère un
        }

        // Méthode statique pour générer des numéros de carte
        public static string GenerateCardNumber()
        {
            var suffix = Random.Next(0, 10000).ToString("D4"); // Suffixe aléatoire de 4 chiffres
            var fullCardNumber = BaseCardNumber + suffix;

            // Formater le numéro de carte avec des espaces tous les 4 chiffres
            //return FormatCardNumber(fullCardNumber);
            return fullCardNumber;
        }

        // Méthode pour formater le numéro de carte avec des espaces tous les 4 chiffres
       /* private static string FormatCardNumber(string cardNumber)
        {
            return string.Join(" ", Enumerable.Range(0, cardNumber.Length / 4)
                .Select(i => cardNumber.Substring(i * 4, 4)));
        }*/



    }
}
