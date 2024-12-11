using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Serveur.Entities
{
    /// <summary>
    /// Modele Carte bancaire
    /// </summary>
    internal class CarteBancaire
    {
        private readonly string numCartePrefixe = "4974 0185 0223 ";

        [Required]
        public int Id { get; set; }

        [NotMapped]
        [Required]
        [Range(0, 9999)]
        public int NumCarteSuffixe { get; set; }

        [StringLength(19)]
        public string NumCarte { get => numCartePrefixe + $"{NumCarteSuffixe:D4}"; }

        [Required]
        public DateOnly DateExpiration { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string? NomTitulaire { get; set; }
    }
}
