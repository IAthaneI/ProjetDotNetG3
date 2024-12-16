using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serveur.Datas;
using Serveur.Entities;
using Serveur.Services;


namespace Serveur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnregistrementsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ExchangeRateService _exchangeRateService;

        public EnregistrementsController(MyDbContext context, ExchangeRateService exchangeRateService)
        //public EnregistrementsController(MyDbContext context)
        {
            _context = context;
            _exchangeRateService = exchangeRateService ?? throw new ArgumentNullException(nameof(exchangeRateService));
        }

        // GET: api/Enregistrements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enregistrement>>> GetEnregistrements()
        {
            return await _context.Enregistrements.ToListAsync();
        }

        // GET: api/Enregistrements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enregistrement>> GetEnregistrement(int id)
        {
            var enregistrement = await _context.Enregistrements.FindAsync(id);

            if (enregistrement == null)
            {
                return NotFound();
            }

            return enregistrement;
        }

        // GET: api/Enregistrements/numCarte
        [HttpGet("numCarte/{numCarte}")]
        public async Task<ActionResult<Enregistrement>> GetEnregistrementByNumCard(string numCarte)
        {
            var enregistrement = await _context.Enregistrements.FirstOrDefaultAsync(e =>  e.NumCarte == numCarte);

            if (enregistrement == null)
            {
                return NotFound();
            }

            return enregistrement;
        }

        // PUT: api/Enregistrements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnregistrement(int id, Enregistrement enregistrement)
        {
            if (id != enregistrement.Id)
            {
                return BadRequest();
            }

            _context.Entry(enregistrement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnregistrementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Enregistrements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enregistrement>> PostEnregistrement(Enregistrement enregistrement)
        {
            _context.Enregistrements.Add(enregistrement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnregistrement", new { id = enregistrement.Id }, enregistrement);
        }

        // DELETE: api/Enregistrements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnregistrement(int id)
        {
            var enregistrement = await _context.Enregistrements.FindAsync(id);
            if (enregistrement == null)
            {
                return NotFound();
            }

            _context.Enregistrements.Remove(enregistrement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnregistrementExists(int id)
        {
            return _context.Enregistrements.Any(e => e.Id == id);
        }



        
        /// Traite un fichier CSV contenant des opérations bancaires.     
        [HttpPost("ajout-bdd")]
        public async Task<IActionResult> ProcessFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            using var stream = new StreamReader(file.OpenReadStream());
            var enregistrements = new List<Enregistrement>();
            var anomalies = new List<Anomalie>();

            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var fields = line.Split(';');

                // Vérification du nombre de colonnes attendues
                if (fields.Length != 5)
                    continue; // Ignore les lignes mal formatées

                // Conversion du type d'opération (enum)
                if (!Enum.TryParse<TypeOperation>(fields[2], true, out var typeOperation))
                {
                    anomalies.Add(new Anomalie
                    {
                        NumCarte = fields[0] // Ajoute comme anomalie si le type est invalide
                    });
                    continue;
                }
                // Créer une opération à partir des données du fichier
                var enregistrement = new Enregistrement
                {
                    NumCarte = fields[0],
                    Montant = decimal.Parse(fields[1]),
                    TypeOperation = typeOperation,
                    DateOperation = DateTime.Parse(fields[3]),
                    Devise = fields[4]
                };
                // Vérification du numéro de carte avec l'algorithme de Luhn
                if (!ValiderCB.AlgoLuhn(enregistrement.NumCarte))
                {
                    anomalies.Add(new Anomalie
                    {
                        NumCarte = enregistrement.NumCarte
                    });
                }
                else
                {
                    enregistrement.EstValide = true;
                    enregistrements.Add(enregistrement);
                }
            }
            // Sauvegarder les données dans la base
            try
            {
                _context.Enregistrements.AddRange(enregistrements);
            _context.Anomalies.AddRange(anomalies);
            await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            return Ok(new { Processed = enregistrements.Count, Anomalies = anomalies.Count });
        }

        private string GetNextFilePath()
        {
            // Chemin de base pour le dossier Data/Files
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Datas/Files");

            // Créer le répertoire s'il n'existe pas
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Recherche du dernier fichier avec un nom incrémenté
            int fileIndex = 1;
            string filePath;
            bool fileExists = false;

            // Cherche le premier fichier disponible avec le format "enregistrement{index}.csv"
            do
            {
                filePath = Path.Combine(directoryPath, $"enregistrement{fileIndex}.csv");

                // Vérifier si le fichier existe en utilisant Directory.GetFiles
                var existingFiles = Directory.GetFiles(directoryPath, $"enregistrement{fileIndex}.csv");
                fileExists = existingFiles.Length > 0;

                fileIndex++;
            } while (fileExists); // Tant que le fichier existe, on incrémente l'index

            return filePath;


        }



        /// <summary>
        /// Crée un fichier CSV avec des enregistrements d'opérations.
        /// </summary>
        [HttpPost("creer-csv")]
        public IActionResult CreateCsv()
        {
            // Exemple de données d'opérations (générées manuellement ici)
            var enregistrements = new List<Enregistrement>
        {
            new Enregistrement() { Montant = 200.00m, TypeOperation = TypeOperation.Retrait, DateOperation = DateTime.Now, Devise = "GBP"},
            new Enregistrement() { Montant = 10.00m, TypeOperation = TypeOperation.FactureCarteBleue, DateOperation = DateTime.Now, Devise = "USD"},
            new Enregistrement() { Montant = 1500.00m, TypeOperation = TypeOperation.DepotGuichet, DateOperation = DateTime.Now, Devise = "JPY" }
        };

            // Obtenir le chemin du fichier à générer
            var filePath = GetNextFilePath();

            // Générer le fichier CSV avec les données d'opérations
            GenereFichier.GenererFichierEnregistrement(enregistrements, filePath);

            // Retourner le chemin du fichier généré dans la réponse
            return Ok(new { FilePath = filePath });
        }



        [HttpGet("json-journalier")]
        public async Task<IActionResult> GenerateDailyReport()
        {
            if (_exchangeRateService == null)
            {
                return BadRequest("Le service de taux de change n'a pas été initialisé.");
            }

            // Récupère toutes les lignes de la base sans filtre
            var allOperations = await _context.Enregistrements.ToListAsync();

            // Filtre les opérations valides en mémoire en utilisant AlgoLuhn
            var validOperations = allOperations
                .Where(op => ValiderCB.AlgoLuhn(op.NumCarte))
                .ToList();

            // Construire le rapport en tenant compte des devises étrangères
            var report = new List<object>();
            foreach (var operation in validOperations)
            {
                decimal? taux = null;
                if (operation.Devise != "EUR")
                {
                    // Appel asynchrone pour obtenir le taux
                    taux = await _exchangeRateService.GetExchangeRate(operation.Devise);
                }

                // Ajouter les données au rapport
                report.Add(new
                {
                    operation.NumCarte,
                    operation.Montant,
                    operation.TypeOperation,
                    operation.DateOperation,
                    operation.Devise,
                    Taux = taux
                });
            }

            // Sérialisation en JSON
            var jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);

            // Créer un nom unique pour le fichier
            var fileName = $"DailyReport_{DateTime.Now:yyyyMMdd}.json";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Datas", "Files", fileName);

            // S'assurer que le dossier existe
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Sauvegarder le fichier JSON dans le répertoire
            await System.IO.File.WriteAllTextAsync(filePath, jsonReport);

            // Retourner le chemin du fichier généré
            return Ok(new { FilePath = filePath });
        }




    }
}
