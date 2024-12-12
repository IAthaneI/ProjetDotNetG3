using System.ComponentModel.DataAnnotations;

namespace BankLib.Entities
{
    /// <summary>
    /// Entite compte bancaire
    /// </summary>
    public class CompteBancaire
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string NumCompte { get; set; }

        [Required]
        public DateTime DateOuverture { get; set; } = DateTime.Today;

        [Required]
        public double Solde { get; set; } = 1000;

        public List<CarteBancaire>? CarteBancaireList { get; set; }

    }
}
