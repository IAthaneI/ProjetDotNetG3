using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLib.Exceptions;

namespace BankLib.Model
{
    /// <summary>
    /// Representation d'un client particulier (personne)
    /// </summary>
    public class ClientPart : Client
    {
        private int ident;
        private DateTime dateNaissance;
        private string? prenom;
        private Sexe sexe;

        public ClientPart(int identifiant, string? nom, Adresse? adresse, string? mail,int idCompte,int ident, DateTime dateNaissance, string? prenom, Sexe sexe) 
            : base(identifiant, nom, adresse, mail, idCompte)
        {
            Ident = ident;
            DateNaissance = dateNaissance;
            Prenom = prenom; 
            Sexe = sexe;
        }

        public int Ident { get => ident; set => ident = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string? Prenom { get => prenom; set
            {
                if (value != null && value.Length > 50) throw new ClientException(2);
                prenom = value;
            }
        }
        public Sexe Sexe { get => sexe; set => sexe = value; }

        public override String toString() 
        {
            return $"{Identifiant} {Nom} {Adresse.toString()} {Mail} {Ident} {dateNaissance:dd/MM/yyyy} {prenom} {sexe} ";
        }
    }
}
