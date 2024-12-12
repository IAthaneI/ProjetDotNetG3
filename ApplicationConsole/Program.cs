using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Exceptions;
using BankLib.Model;
using BankLib.Models;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        bool logged = false;
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
        bool end = false;
        Console.WriteLine("[| Vous êtes connecter ! |]");
        while (!end) 
        {
            Console.WriteLine("- Que souhaitais vous faires ?");
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
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad2:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad3:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad4:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad5:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad6:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad7:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad8:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad9:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.Q:
                    Console.WriteLine("\nAu revoir !");
                    Console.WriteLine("[| Vous avez été déconnecté |]");
                    end = true;
                    break;
                default:
                    break;
            }
        }

    }

    private static void EnregistrementTests() 
    {
        EnregistrementRepository enregistrementRepository = new EnregistrementRepository();
        //enregistrementRepository.InsertEnregistrement(new Enregistrement(3, "4974018502230236", 28.5, TypeOperation.Depot, DateTime.Now, 3));
        enregistrementRepository.GetEnregistrements().ForEach(e => Console.WriteLine($"{e.NumCarte} {e.Id} {e.Type} {e.Montant} {e.Date:dd/MM/yyyy}"));
    }

    private static void ClientTests() 
    {
        try
        {
            ClientRepository clientRepo = new ClientRepository();
            List<Client> clients = clientRepo.getClients();
            clients.ForEach(client => Console.WriteLine(client.toString()));
            //clientRepo.InsertClient(new ClientPart(3, "BETY", new Adresse("12, rue des Oliviers", "", "94000", "CRETEIL"), "bety@gmail.com", 2, new DateTime(1985, 11, 12), "Daniel", Sexe.Homme));
            clientRepo.InsertClient(new ClientPro(4, "AXA", new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS"), "info@axa.fr", 1 , 2, "12548795641120", StatutJuridique.SARL, new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS")));
            Console.WriteLine("----------------------------------------");
            clients = clientRepo.getClients();
            clients.ForEach(client => Console.WriteLine(client.toString()));

        }
        catch (ClientException e)
        {
            Console.WriteLine($"Une erreur a été détecter : Code - {e.Code} {e.Mes}");
        }
    }

    private static void CompteBancaireTests()
    {
        CompteBancaireRepository cbr = new CompteBancaireRepository();
        List<CompteBancaireModel> cbmList = cbr.GetCompteBancaires();
        foreach (var c in cbmList)
        {
            Console.WriteLine($"{c.NumCompte} ouvert au {c.DateOuverture}. Solde :\t{c.Solde}");
        }

        CompteBancaireModel cbm = cbr.GetCompteBancaire(1);
        Console.WriteLine($"{cbm.NumCompte} ouvert au {cbm.DateOuverture}. Solde :\t{cbm.Solde}");

        CompteBancaire cb= new CompteBancaire();
        cbr.InsertCompteBancaire(cb);
    }
}