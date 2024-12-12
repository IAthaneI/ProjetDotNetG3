namespace BankLib
{
    /// <summary>
    /// Classe contenant des constantes
    /// qui servent de contraintes au projet
    /// </summary>
    public static class Constantes
    {
        #region Compte bancaire
        public const int    COMPTE_BANCAIRE_NUM_LEN =                   10;
        public const double COMPTE_BANCAIRE_SOLDE_INITIAL =             1000;
        #endregion

        #region Carte bancaire
        public const int    CARTE_BANCAIRE_NUM_MAX_VAL =                9999;
        public const int    CARTE_BANCAIRE_NUM_LEN =                    4;
        public const string CARTE_BANCAIRE_NUM_PREFIXE =                "4974 0185 0223 ";
        public const int    CARTE_BANCAIRE_NUM_VALIDITE =               2;
        public const int    CARTE_BANCAIRE_NOM_TITULAIRE_MAX_LEN =      30;
        #endregion

        #region Utilities
        public const int    RANDOM_WAIT_TIMEOUT =                       20;
        #endregion
    }
}
