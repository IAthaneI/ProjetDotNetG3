using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ApplicationConsole.Model
{
    /// <summary>
    /// Modele Carte bancaire
    /// </summary>
    [JsonSerializable(typeof(CarteBancaireModel))]
    internal class CarteBancaireModel
    {
        private readonly string numCartePrefixe = "4974 0185 0223 ";

        [JsonPropertyName("id")]
        [XmlElement(Order = 1)]
        [Required]
        public int Id { get; set; }

        //[NotMapped]
        [Required]
        [Range(0, 9999)]
        public int NumCarteSuffixe { get; set; }

        [JsonPropertyName("numCarte")]
        [XmlAttribute("numCarte")]
        [StringLength(19)]
        public string NumCarte { get => numCartePrefixe + $"{NumCarteSuffixe:D4}"; }

        [Required]
        public DateOnly DateExpiration { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string? NomTitulaire { get; set; }
    }
}
