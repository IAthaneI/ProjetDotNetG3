using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Utilities
{
    /// <summary>
    /// classe Outils
    /// </summary>
    internal class Tool
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Permet de générer un nombre aléatoire entre 0 et length
        /// </summary>
        /// <param name="length">valeur max</param>
        /// <returns>un nombre aléatoire entre 0 et length</returns>
        public static int RandomInt(int length)
        {
            return random.Next(0, length);
        }
       
    }
}
