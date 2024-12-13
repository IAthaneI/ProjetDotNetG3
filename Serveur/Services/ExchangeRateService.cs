using Newtonsoft.Json;

namespace Serveur.Services
{
    /*public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.exchangerate-api.com/v4/latest"; // Exemple de l'URL de l'API

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Méthode pour récupérer le taux de change
        public async Task<decimal> GetExchangeRateAsync(string baseCurrency, string targetCurrency)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}/{baseCurrency}");
            var exchangeRateData = JsonConvert.DeserializeObject<ExchangeRateResponse>(response);

            // On suppose que l'API renvoie un champ "rates" qui contient les taux de change
            if (exchangeRateData != null && exchangeRateData.Rates.ContainsKey("EUR"))
            {
                return exchangeRateData.Rates["EUR"]; // Renvoie le taux vers EUR
            }

            return 1.0m; // Si la devise est EUR, pas besoin de conversion, on retourne 1
        }
    }

    public class ExchangeRateResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }*/






    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.exchangerate-api.com/v4/latest"; // URL de l'API

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Récupère le taux de change pour une devise donnée par rapport à l'EUR.
        /// </summary>
        /// <param name="currency">La devise étrangère (exemple : "USD").</param>
        /// <returns>Le taux de conversion vers l'EUR.</returns>
        public async Task<decimal> GetExchangeRate(string currency)
        {
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("La devise ne peut pas être vide.", nameof(currency));

            try
            {
                // Appel API pour obtenir les taux
                var response = await _httpClient.GetStringAsync($"{ApiUrl}/EUR"); // Fixe EUR comme devise de base
                var exchangeRateData = JsonConvert.DeserializeObject<ExchangeRateResponse>(response);

                // Vérifie si la devise existe dans les résultats
                if (exchangeRateData.Rates.TryGetValue(currency, out var rate))
                {
                    return rate;
                }
                else
                {
                    throw new Exception($"Taux de change non disponible pour la devise : {currency}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du taux de change : {ex.Message}");
                throw;
            }
        }
    }

    /// <summary>
    /// Modèle pour désérialiser la réponse de l'API de taux de change.
    /// </summary>
    public class ExchangeRateResponse
    {
        public string Base { get; set; } // Devise de base (par exemple : "EUR")
        public DateTime Date { get; set; } // Date des taux
        public Dictionary<string, decimal> Rates { get; set; } // Taux de change
    }









}
