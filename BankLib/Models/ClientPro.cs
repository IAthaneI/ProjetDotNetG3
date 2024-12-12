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
    /// Representation d'un client professionnel (Entreprise)
    /// </summary>
    public class ClientPro : Client
    {
        private int ident;
        private string? siret;
        private StatutJuridique statutJuridique;
        private Adresse siege;

        public ClientPro(int identifiant, string? nom, Adresse? adresse, string? mail, int idCompte,int ident, string? siret, StatutJuridique statutJuridique, Adresse siege) 
            : base(identifiant, nom, adresse, mail, idCompte)
        {
            Ident = ident;
            Siret = siret;
            StatutJuridique = statutJuridique;
            Siege = siege;
        }

        public int Ident { get => ident; set => ident = value; }
        public string? Siret { get => siret; set
            {
                if (value != null && value.Length != 14) throw new ClientException(3);
                siret = value;
            }
        }
        public StatutJuridique StatutJuridique { get => statutJuridique; set => statutJuridique = value; }
        public Adresse Siege { get => siege; set => siege = value; }

        public override String toString()
        {
            return $"{Identifiant} {Nom} {Adresse.toString()} {Mail} {Ident} {Siret} {Siege.toString()} {StatutJuridique} ";
        }
    }
}
