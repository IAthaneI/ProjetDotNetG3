namespace BankLib.Utilities
{
    /// <summary>
    /// classe Outils
    /// </summary>
    public class RandomTool
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

        /// <summary>
        /// Execute la fonction task jusqu'a ce qu'elle retourne true ou temps écoulé
        /// </summary>
        /// <param name="task">Fonction a executer</param>
        /// <param name="timeSpan">temps d'execution</param>
        /// <returns>True/False</returns>
        public static bool RetryUntilSuccessOrTimeout(Func<bool> task, TimeSpan timeSpan)
        {
            bool success = false;
            int elapsed = 0;
            while ((!success) && (elapsed < timeSpan.TotalMilliseconds))
            {
                Thread.Sleep(1000);
                elapsed += 1000;
                success = task();
            }
            return success;
        }

    }
}
