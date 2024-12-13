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
        [StringLength(Constantes.COMPTE_BANCAIRE_NUM_LEN)]
        public string NumCompte { get; set; }

        [Required]
        public DateTime DateOuverture { get; set; } = DateTime.Today;

        [Required]
        public double Solde { get; set; } = Constantes.COMPTE_BANCAIRE_SOLDE_INITIAL;

        public List<CarteBancaire>? CarteBancaireList { get; set; }

        public override string ToString()
        {
            return $"Compte {Id} : {NumCompte} {DateOuverture:dd/MM/yyyy} {Solde:00000.00} ";
        }

    }
}
