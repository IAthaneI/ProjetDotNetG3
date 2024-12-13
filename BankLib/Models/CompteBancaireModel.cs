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
        [StringLength(10)]
        public string NumCompte
        {
            get; set;
        }
        
        [Required]
        public DateTime DateOuverture { get; set; }

        [Required]
        public double Solde { get; set; } = 1000;

        public List<CarteBancaireModel>? CarteBancaireList { get; set; }

        public override string ToString()
        {
            return $"Compte {Id} : {NumCompte} {DateOuverture:dd/MM/yyyy} {Solde:00000.00} ";
        }
    }
}
