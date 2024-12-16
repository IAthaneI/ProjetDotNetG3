using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Exceptions;
using BankLib.Model;
using BankLib.Models;
using BankLib.Utilities;
using System.Numerics;
using System.Runtime.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        bool logged = false;
        ClientRepository clientRepository = new ClientRepository();
        EnregistrementRepository enregistrementRepository = new EnregistrementRepository();
        CarteBancaireRepository carteBancaireRepository = new CarteBancaireRepository();
        CompteBancaireRepository compteBancaireRepository = new CompteBancaireRepository();
        Console.WriteLine("[| Bienvenue sur l'application console |]");
        while (!logged)
        {
            Console.WriteLine("- Que souhaitais vous faires ?");
            Console.WriteLine("1) S'inscrire");
            Console.WriteLine("2) Se connecter");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.NumPad1:
                    Console.WriteLine("\n- Quel est votre nom d'utilisateur ?");
                    string? loginR = Console.ReadLine();
                    Console.WriteLine("- Quel est votre mot de passe ?");
                    string? passwordR = Console.ReadLine();
                    if (Authent.Register(loginR, passwordR))
                    {
                        logged = true;
                    }
                    else
                    {
                        Console.WriteLine("L'inscription a échoué veuillez réessayer");
                    }

                    break;
                case ConsoleKey.NumPad2:
                    Console.WriteLine("\n- Quel est votre nom d'utilisateur ?");
                    string? loginL = Console.ReadLine();
                    Console.WriteLine("- Quel est votre mot de passe ?");
                    string? passwordL = Console.ReadLine();
                    if (Authent.Login(loginL, passwordL))
                    {
                        logged = true;
                    }
                    else
                    {
                        Console.WriteLine("La connection a échoué veuillez réessayer");
                    }
                    break;
                default:
                    break;
            }
        }
        OperationXmlTest();
        bool end = false;
        Console.WriteLine("[| Vous êtes connecter ! |]");
        while (!end) 
        {
            try
            {
                Console.WriteLine("\n- Que souhaitais vous faires ?");
                Console.WriteLine("1) Lister les clients");
                Console.WriteLine("2) Lister les comptes");
                Console.WriteLine("3) Lister les operations");
                Console.WriteLine("4) Lister les operations d'un client");
                Console.WriteLine("5) Voir les details d'un client");
                Console.WriteLine("6) Saisir un client");
                Console.WriteLine("7) Saisir un compte");
                Console.WriteLine("8) Saisir une carte bancaire");
                Console.WriteLine("9) Saisir une operation");
                Console.WriteLine("Q) Quitter");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        clientRepository.getClients().ForEach(c => Console.WriteLine(c.toString()));
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        compteBancaireRepository.GetCompteBancaires().ForEach(c => Console.WriteLine(c.ToString()));
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        enregistrementRepository.GetEnregistrements().ForEach(e => Console.WriteLine(e.ToString()));
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad4:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        Int32.TryParse(Console.ReadLine(), out int idOpt4);
                        enregistrementRepository.GetEnregistrementsOfClient(idOpt4).ForEach(e => Console.WriteLine(e.ToString()));
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad5:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        Int32.TryParse(Console.ReadLine(), out int idOpt5);
                        Console.WriteLine("Client : ");
                        Client? client = clientRepository.getClient(idOpt5);
                        if (client != null)
                        {
                            Console.WriteLine(client.toString());
                            Console.WriteLine("Compte :");
                            Console.WriteLine(compteBancaireRepository.GetCompteBancaire(client.IdCompte).ToString());
                            Console.WriteLine("Carte bancaire : ");
                            carteBancaireRepository.GetCarteBancaireOfCompte(client.IdCompte).ForEach(c => Console.WriteLine(c.ToString()));
                            Console.WriteLine("Opération : ");
                            enregistrementRepository.GetEnregistrementsOfClient(idOpt5).ForEach(e => Console.WriteLine(e.ToString()));
                        }
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad6:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        Console.WriteLine("Quel genre de clients voulez vous créez ? ");
                        Console.WriteLine("1) Client Particulier");
                        Console.WriteLine("2) Client Professionnel");
                        key = Console.ReadKey();
                        try
                        {
                            switch (key.Key)
                            {
                                case ConsoleKey.NumPad1:
                                    ClientPart cpa = new ClientPart();
                                    GetClientInformation(cpa);

                                    cpa.Ident = clientRepository.GetNewMaxId("Id", "ClientsParticuliers");
                                    Console.WriteLine("Quel est le prénom du client ? ");
                                    cpa.Prenom = Console.ReadLine();
                                    Console.WriteLine("Quel est le sexe du client ?");
                                    Console.WriteLine("1) Homme");
                                    Console.WriteLine("2) Femme");
                                    Console.WriteLine("3) Autre");
                                    var keySexe = Console.ReadKey();
                                    switch (keySexe.Key)
                                    {

                                        case ConsoleKey.NumPad1:
                                            cpa.Sexe = Sexe.Homme;
                                            break;
                                        case ConsoleKey.NumPad2:
                                            cpa.Sexe = Sexe.Femme;
                                            break;
                                        default:
                                            cpa.Sexe = Sexe.Autre;
                                            break;
                                    }
                                    Console.WriteLine("\nQuel est la date de naissance du client ? (Format : dd/MM/yyyy)");
                                    String[] date = Console.ReadLine().Split("/");
                                    cpa.DateNaissance = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                                    bool insertCpa = clientRepository.InsertClient(cpa);
                                    if (insertCpa) Console.WriteLine("Le nouveau compte à bien été créer");
                                    else Console.WriteLine("Le nouveau compte n'a pas pus être créer");
                                    break;
                                case ConsoleKey.NumPad2:
                                    ClientPro cpr = new ClientPro();
                                    GetClientInformation(cpr);

                                    cpr.Ident = clientRepository.GetNewMaxId("Id", "ClientsProfessionnels");
                                    Adresse a = new Adresse();
                                    Console.WriteLine("Quel est le siret ? ");
                                    cpr.Siret = Console.ReadLine();
                                    Console.WriteLine("Quel est le libelle de l'adresse du siege ?");
                                    a.Libelle = Console.ReadLine();
                                    Console.WriteLine("Quel est le Complement de l'adresse du siege ?");
                                    a.Complement = Console.ReadLine();
                                    Console.WriteLine("Quel est le code postale de l'adresse du siege ?");
                                    a.Cp = Console.ReadLine();
                                    Console.WriteLine("Quel est la ville de l'adresse du siege ?");
                                    a.Ville = Console.ReadLine();
                                    cpr.Siege = a;
                                    Console.WriteLine("Quel est le statut juridique du client ?");
                                    Console.WriteLine("1) SARL");
                                    Console.WriteLine("2) SAS");
                                    Console.WriteLine("3) SA");
                                    Console.WriteLine("4) EURL");
                                    var keyST = Console.ReadKey();
                                    switch (keyST.Key)
                                    {
                                        case ConsoleKey.NumPad1:
                                            cpr.StatutJuridique = StatutJuridique.SARL;
                                            break;
                                        case ConsoleKey.NumPad2:
                                            cpr.StatutJuridique = StatutJuridique.SAS;
                                            break;
                                        case ConsoleKey.NumPad3:
                                            cpr.StatutJuridique = StatutJuridique.SA;
                                            break;
                                        default:
                                            cpr.StatutJuridique = StatutJuridique.EURL;
                                            break;
                                    }
                                    bool insertCpr = clientRepository.InsertClient(cpr);
                                    if (insertCpr) Console.WriteLine("\nLe nouveau compte à bien été créer");
                                    else Console.WriteLine("\nLe nouveau compte n'a pas pus être créer");
                                    break;
                                default:
                                    Console.WriteLine("\nL'option selectionné n'existe pas");
                                    break;
                            }
                        }
                        catch (ClientException e)
                        {
                            Console.WriteLine($"Une erreur a été détécté : Code ${e.Code} - ${e.Mes}");
                            Console.WriteLine("Ajout du client annulé");
                        }
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad7:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        CompteBancaireModel toInsertCo = new CompteBancaireModel();
                        toInsertCo.DateOuverture = DateTime.Now;
                        toInsertCo.Solde = 1000;
                        bool insertCo = compteBancaireRepository.InsertCompteBancaire(toInsertCo);
                        if (insertCo) Console.WriteLine("Le nouveau compte à bien été créer");
                        else Console.WriteLine("Le nouveau compte n'a pas pus être créer");
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad8:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        CarteBancaireModel toInsert = new CarteBancaireModel();
                        Console.WriteLine("Sur quelle compte voulez vous ajoutez une Carte ?");
                        Int32.TryParse(Console.ReadLine(), out int idCompte);
                        toInsert.DateExpiration = DateTime.Now.AddYears(5);
                        toInsert.CompteBancaireId = idCompte;
                        toInsert.NomTitulaire = compteBancaireRepository.GetNomClientByIdCompte(idCompte);
                        if (!String.IsNullOrEmpty(toInsert.NomTitulaire))
                        {
                            bool insert = carteBancaireRepository.InsertCarteBancaire(toInsert);
                            if (insert) Console.WriteLine("La nouvelle carte à bien été ajouté");
                            else Console.WriteLine("La nouvelle carte n'a pas pus être ajouté");
                        }
                        else
                        {
                            Console.WriteLine("Le client ou le Compte associé n'a pas été trouvé");
                        }
                        Console.WriteLine("[|-----------------------------------------|]");
                        Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad9:
                        Console.WriteLine("\n[|-----------------------------------------|]");
                        int id = enregistrementRepository.GetNewMaxId();
                        Console.WriteLine("Sur quelle numero de carte voulez vous effectuez l'operation ?");
                        string NumCarte = Console.ReadLine();
                        Console.WriteLine("Quel est le montant de l'operation ? ");
                        double Montant = Double.Parse(Console.ReadLine());
                        Console.WriteLine("Quel type d'operation voulez vous effectuez ? ");
                        Console.WriteLine("1) Depot");
                        Console.WriteLine("2) Retrait");
                        Console.WriteLine("3) Facture Carte bancaire");
                        key = Console.ReadKey();
                        bool typeError = false;
                        TypeOperation type = TypeOperation.Depot;
                        switch (key.Key)
                        {
                            case ConsoleKey.NumPad1:
                                type = TypeOperation.Depot;
                                break;
                            case ConsoleKey.NumPad2:
                                type = TypeOperation.Retrait;
                                break;
                            case ConsoleKey.NumPad3:
                                type = TypeOperation.Facture;
                                break;
                            default:
                                typeError = true;
                                Console.WriteLine("\nCette option n'existe pas, abandon de l'ajout");
                                break;
                        }
                        if (!typeError)
                        {
                            int idCb = carteBancaireRepository.GetIdCarteBancaireByNumCarte(NumCarte);
                            CompteBancaire compteBancaire = compteBancaireRepository.GetCompteBancaireByIdCarte(idCb);
                            Enregistrement enregistrement = new Enregistrement(id, NumCarte, Montant, type, DateTime.Now, idCb);
                            bool canBeInsert = true;
                            if (type == TypeOperation.Retrait || type == TypeOperation.Facture)
                            {
                                canBeInsert = compteBancaireRepository.CheckNegativeOperation(idCb, Montant);
                                compteBancaire.Solde = compteBancaire.Solde - Montant;
                            }
                            else
                            {
                                compteBancaire.Solde = compteBancaire.Solde + Montant;
                            }

                            if (canBeInsert)
                            {
                                compteBancaireRepository.UpdateSoldeCompteBancaire(compteBancaire);
                                bool isEnrInsert = enregistrementRepository.InsertEnregistrement(enregistrement);
                                if (isEnrInsert)
                                {
                                    Console.WriteLine("\nL'operation a bien été enregistré");
                                }
                                else 
                                {
                                    Console.WriteLine("\nIl y a eu une erreur lors de l'enregistrement");                                
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nLe solde du compte {compteBancaire.NumCompte} est trop bas, opération impossible");
                            } 
                        }
                        break;
                    case ConsoleKey.Q:
                        Console.WriteLine("\n[|Vous avez été déconnécté.|]");
                        end = true;
                        break;
                    default:
                        Console.WriteLine("\nCette option n'existe pas");
                        break;
                }
            }
            catch (ClientException e)
            {
                Console.WriteLine($"ERREUR : Une erreur lors de la récupération/Creation d'un client cde {e.Code} {e.Mes}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Une exception a été levée : {e.Message}");
            }
        }
    }

    private static void OperationXmlTest(bool run = true)
    {
        if (!run) return;
        OperationRepository opr = new OperationRepository();
        DateTime debut = DateTime.Today.AddMonths(-3);
        DateTime fin = DateTime.Today;
        ParserTool.OperationToXml(opr.GetOperations(debut, fin));
        Console.WriteLine("Veuillez appuyez sur une touche....");
        Console.ReadKey();
        ParserTool.OperationToXml(opr.GetOperations(debut, fin, "9998887410"));
    }

    public static void GetClientInformation(Client c)
    {
        ClientRepository clientRepository = new ClientRepository();
        Adresse a = new Adresse();
        c.Identifiant = clientRepository.GetNewMaxId("Identifiant", "Clients");
        Console.WriteLine("\nQuel est le nom du client ? ");
        c.Nom = Console.ReadLine();
        Console.WriteLine("Quel est le libelle de l'adresse ?");
        a.Libelle = Console.ReadLine();
        Console.WriteLine("Quel est le Complement de l'adresse ?");
        a.Complement = Console.ReadLine();
        Console.WriteLine("Quel est le code postale de l'adresse ?");
        a.Cp = Console.ReadLine();
        Console.WriteLine("Quel est la ville de l'adresse ?");
        a.Ville = Console.ReadLine();
        c.Adresse = a;
        Console.WriteLine("Quel est l'adresse mail du client ? ");
        c.Mail = Console.ReadLine();
        Console.WriteLine("Quel est l'id du Compte du client ?");
        c.IdCompte = Int32.Parse(Console.ReadLine());
    }
}