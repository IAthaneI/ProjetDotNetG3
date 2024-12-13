namespace BankLib.Utilities
{
    /// <summary>
    /// Classe utilitaire contenant des methodes de validation
    /// </summary>
    public static class ValidationTool
    {
        /// <summary>
        /// Execute la fonction task jusqu'a ce qu'elle retourne true ou temps écoulé
        /// </summary>
        /// <param name="task">Fonction a executer avec un type de retour booléen</param>
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

        /// <summary>
        /// Permet de vérifier la validité d'un numéro de carte
        /// </summary>
        /// <param name="numCarte">4974 0185 0223 XXXX</param>
        /// <returns>True/False</returns>
        public static bool AlgoLuhn(string numCarte)
        {
            int somme = 0;
            bool doitDoubler = false;

            for (int i = numCarte.Length - 1; i >= 0; i--)
            {
                char c = numCarte[i];
                if (!char.IsDigit(c)) return false;

                int chiffre = c - '0';
                if (doitDoubler)
                {
                    chiffre *= 2;
                    if (chiffre > 9)
                    {
                        chiffre -= 9;
                    }
                }

                somme += chiffre;
                doitDoubler = !doitDoubler;
            }
            return (somme % 10 == 0);
        }
    }
}
