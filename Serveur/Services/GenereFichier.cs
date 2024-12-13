namespace Serveur.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Serveur.Entities;

    public class GenereFichier
    {
        public static void GenererFichierEnregistrement(List<Enregistrement> enregistrements, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("NumCarte,Montant,TypeOperation,DateOperation,Devise");

                foreach (var enregistrement in enregistrements)
                {
                    writer.WriteLine($"{enregistrement.NumCarte},{enregistrement.Montant},{enregistrement.TypeOperation},{enregistrement.DateOperation},{enregistrement.Devise}");
                }
            }
        }
    }

}
