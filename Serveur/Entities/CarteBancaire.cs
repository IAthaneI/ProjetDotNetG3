using Serveur.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveur.Entities
{
    /// <summary>
    /// Entite Carte bancaire
    /// </summary>
    public class CarteBancaire
    {
        // prefixe d'un numéro de carte
        private const string numCartePrefixe = "4974 0185 0223 ";

        private int numCarteSuffixe;

        [Key]
        public int Id { get; set; }

        [NotMapped]
        [Range(0, 9999)]
        public int NumCarteSuffixe
        {
            get => numCarteSuffixe;
            set
            {
                numCarteSuffixe = Tool.RandomInt(9999);
            }
        }

        [Required]
        [StringLength(19)]
        public string NumCarte { get => numCartePrefixe + $"{NumCarteSuffixe:D4}"; }

        [Required]
        public DateOnly DateExpiration { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string? NomTitulaire { get; set; }
    }
}
