using System.ComponentModel.DataAnnotations;

namespace BankLib.Models
{
    /// <summary>
    /// Modele Carte bancaire
    /// Sert de mappage avec la BDD
    /// </summary>
    public class CarteBancaireModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(Constantes.CARTE_BANCAIRE_NUM_LEN)]
        [Range(0, Constantes.CARTE_BANCAIRE_NUM_MAX_VAL)]
        public string NumCarte { get; set; }

        [Required]
        public DateTime DateExpiration { get; set; } = DateTime.Today.AddYears(Constantes.CARTE_BANCAIRE_NUM_VALIDITE);

        [Required]
        [StringLength(Constantes.CARTE_BANCAIRE_NOM_TITULAIRE_MAX_LEN, MinimumLength = 2)]
        public string NomTitulaire { get; set; }

        [Required]
        public int CompteBancaireId { get; set; }

        public CompteBancaireModel CompteBancaire { get; set; }
    }
}
