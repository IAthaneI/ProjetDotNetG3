using BankLib.Utilities;
using System.ComponentModel.DataAnnotations;

namespace BankLib.Entities
{
    /// <summary>
    /// Entite compte bancaire
    /// </summary>
    public class CompteBancaire
    {
        // parametre : nombre de caractères d'un numéro de compte
        private const int tailleNumCompte = 10;

        private string numCompte;

        [Key]
        public int Id { get; set; }

        [Required]
        public string NumCompte
        {
            get => numCompte;
            private set
            {
                numCompte = RandomTool.RandomString(tailleNumCompte);
            }
        }

        [Required]
        public DateOnly DateOuverture { get; set; }

        [Required]
        public double Solde { get; set; } = 1000;

        public List<CarteBancaire>? CarteBancaireList { get; set; }
    }
}
