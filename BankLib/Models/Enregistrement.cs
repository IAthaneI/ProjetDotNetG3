using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLib.Entities;

namespace BankLib.Models
{
    /// <summary>
    /// Reprensation d'une operation bancaire
    /// </summary>
    public class Enregistrement
    {
        private int id;
        private string numCarte;
        private double montant;
        private TypeOperation type;
        private DateTime date;
        private int carteBancaireId;

        public int Id { get => id; set => id = value; }
        public string NumCarte { get => numCarte; set => numCarte = value; }
        public double Montant { get => montant; set => montant = value; }
        public TypeOperation Type { get => type; set => type = value; }
        public DateTime Date { get => date; set => date = value; }
        public int CarteBancaireId { get => carteBancaireId; set => carteBancaireId = value; }

        public Enregistrement(int id, string numCarte, double montant, TypeOperation type, DateTime date, int carteBancaireId)
        {
            Id = id;
            NumCarte = numCarte;
            Montant = montant;
            Type = type;
            Date = date;
            CarteBancaireId = carteBancaireId;
        }
    }
}
