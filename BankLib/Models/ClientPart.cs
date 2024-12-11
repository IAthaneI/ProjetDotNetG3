using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib.Model
{
    public class ClientPart : Client
    {
        private int ident;
        private DateTime dateNaissance;
        private string? prenom;
        private Sexe sexe;

        public ClientPart(int identifiant, string? nom, Adresse? adresse, string? mail,int ident, DateTime dateNaissance, string? prenom, Sexe sexe) 
            : base(identifiant, nom, adresse, mail)
        {
            Ident = ident;
            DateNaissance = dateNaissance;
            Prenom = prenom; 
            Sexe = sexe;
        }

        public int Ident { get => ident; set => ident = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string? Prenom { get => prenom; set => prenom = value; }
        public Sexe Sexe { get => sexe; set => sexe = value; }

        public override String toString() 
        {
            return $"{Identifiant} {Nom} {Adresse.toString()} {Mail} {Ident} {dateNaissance:dd/MM/yyyy} {prenom} {sexe} ";
        }
    }
}
