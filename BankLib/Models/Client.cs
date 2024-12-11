using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib.Model
{
    public abstract class Client
    {
        private int identifiant;
        private string? nom;
        private Adresse? adresse;
        private string? mail;

        protected Client(int identifiant, string? nom, Adresse? adresse, string? mail)
        {
            Identifiant = identifiant;
            Nom = nom;
            Adresse = adresse;
            Mail = mail;
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string? Nom { get => nom; set => nom = value; }
        public string? Mail { get => mail; set => mail = value; }
        public Adresse? Adresse { get => adresse; set => adresse = value; }

        public abstract string toString();
    }
}
