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
    public class CarteBancaireModel
    {
        [JsonPropertyName("id")]
        [XmlElement(Order = 1)]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("numCarte")]
        [XmlAttribute("numCarte")]
        [StringLength(19)]
        [Required]
        public string NumCarte { get; }

        [Required]
        public DateOnly DateExpiration { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string? NomTitulaire { get; set; }
    }
}
