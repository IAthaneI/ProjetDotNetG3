using BankLib.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BankLib.Entities
{
    /// <summary>
    /// Entite Carte bancaire
    /// A utiliser dans les applications
    /// </summary>
    [JsonSerializable(typeof(CarteBancaire))]
    public class CarteBancaire
    {

        [Key]
        [JsonPropertyName("id")]
        [XmlElement(Order = 1)]
        public int Id { get; set; }

        [Required]
        [Range(0, Constantes.CARTE_BANCAIRE_NUM_MAX_VAL)]
        public int NumCarte { get; set; }

        [NotMapped]
        [JsonPropertyName("numCarte")]
        [XmlAttribute("numCarte")]
        public string NumCarteDisplay { get => Constantes.CARTE_BANCAIRE_NUM_PREFIXE + $"{NumCarte:D4}"; }

        [Required]
        public DateOnly DateExpiration { get; set; }

        [Required]
        [StringLength(Constantes.CARTE_BANCAIRE_NOM_TITULAIRE_MAX_LEN, MinimumLength = 2)]
        public string NomTitulaire { get; set; }

        [Required]
        public int CompteBancaireId { get; set; }

        public CompteBancaire CompteBancaire { get; set; }
    }
}
