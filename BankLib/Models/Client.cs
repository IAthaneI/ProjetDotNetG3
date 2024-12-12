using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLib.Exceptions;

namespace BankLib.Model
{
    public abstract class Client
    {
        private int identifiant;
        private string? nom;
        private Adresse? adresse;
        private string? mail;
        private int idCompte;

        protected Client(int identifiant, string? nom, Adresse? adresse, string? mail, int idCompte)
        {
            Identifiant = identifiant;
            Nom = nom;
            Adresse = adresse;
            Mail = mail;
            IdCompte = idCompte;
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string? Nom { get => nom; set {
                if (value != null && value.Length > 50) throw new ClientException(0); 
                nom = value;
            } }
        public string? Mail { get => mail; set {
                if (value != null && !value.Contains('@')) throw new ClientException(1);
                mail = value;
            } }
        public Adresse? Adresse { get => adresse; set => adresse = value; }
        public int IdCompte { get => idCompte; set => idCompte = value; }

        public abstract string toString();
    }
}
