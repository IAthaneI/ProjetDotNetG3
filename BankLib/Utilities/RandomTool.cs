namespace BankLib.Utilities
{
    /// <summary>
    /// classe Utilitaire permettant de générer des valeurs aléatoires
    /// </summary>
    public static class RandomTool
    {
        private static Random random = new Random();

        /// <summary>
        /// Permet de générer une chaine de caractère aléatoire de taille "length"
        /// </summary>
        /// <param name="length">taille de la chaine souhaitée</param>
        /// <returns> chaine de caractère de taille length</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Permet de générer un nombre aléatoire entre 0 et "length"
        /// </summary>
        /// <param name="length">valeur max</param>
        /// <returns>un nombre aléatoire entre 0 et length</returns>
        public static int RandomInt(int length)
        {
            return random.Next(0, length);
        }

       
    }
}
