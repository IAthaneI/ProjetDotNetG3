using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib.Model
{
    /// <summary>
    /// Representation d'une adresse postale 
    /// </summary>
    public class Adresse
    {
        private string? libelle;
        private string? complement;
        private string? cp;
        private string? ville;

        public Adresse(string? libelle, string? complement, string? cp, string? ville)
        {
            Libelle = libelle;
            Complement = complement;
            Cp = cp;
            Ville = ville;
        }
        public string? Libelle { get => libelle; set => libelle = value; }
        public string? Complement { get => complement; set => complement = value; }
        public string? Cp { get => cp; set => cp = value; }
        public string? Ville { get => ville; set => ville = value; }

        public string toString() 
        {
            return $"{Libelle} {Complement} {Cp} {Ville}";
        }
    }
}
