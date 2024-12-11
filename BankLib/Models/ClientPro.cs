using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib.Model
{
    public class ClientPro : Client
    {
        private int ident;
        private string? siret;
        private StatutJuridique statutJuridique;
        private Adresse siege;

        public ClientPro(int identifiant, string? nom, Adresse? adresse, string? mail,int ident, string? siret, StatutJuridique statutJuridique, Adresse siege) 
            : base(identifiant, nom, adresse, mail)
        {
            Ident = ident;
            Siret = siret;
            StatutJuridique = statutJuridique;
            Siege = siege;
        }

        public int Ident { get => ident; set => ident = value; }
        public string? Siret { get => siret; set => siret = value; }
        public StatutJuridique StatutJuridique { get => statutJuridique; set => statutJuridique = value; }
        public Adresse Siege { get => siege; set => siege = value; }

        public override String toString()
        {
            return $"{Identifiant} {Nom} {Adresse.toString()} {Mail} {Ident} {Siret} {Siege.toString()} {StatutJuridique} ";
        }
    }
}
