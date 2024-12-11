using System.ComponentModel.DataAnnotations;

namespace BankLib.Models
{
    /// <summary>
    /// Modele de compte bancaire
    /// </summary>
    public class CompteBancaireModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string NumCompte
        {
            get;
        }
        
        [Required]
        public DateOnly DateOuverture { get; set; }

        [Required]
        public double Solde { get; set; } = 1000;

        public List<CarteBancaireModel>? CarteBancaireList { get; set; }
    }
}
