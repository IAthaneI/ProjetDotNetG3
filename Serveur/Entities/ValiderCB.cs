namespace Serveur.Entities
{
    public static class ValiderCB
    {
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
