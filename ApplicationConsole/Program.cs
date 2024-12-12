using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Models;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        //bool logged = false;
        bool logged = true;
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
                    Console.WriteLine("- Quel est votre nom d'utilisateur ?");
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
                    Console.WriteLine("- Quel est votre nom d'utilisateur ?");
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
        Console.WriteLine("[| Vous êtes connecter ! |]");
        //ClientRepository clientRepo = new ClientRepository();
        //List<Client> clients = clientRepo.getClients();
        //clients.ForEach(client => Console.WriteLine(client.toString()));
        //clientRepo.InsertClient(new ClientPart(3, "BETY", new Adresse("12, rue des Oliviers","","94000","CRETEIL"),"bety@gmail.com",2,new DateTime(1985,11,12),"Daniel",Sexe.Homme));
        //clientRepo.InsertClient(new ClientPro(4, "AXA", new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS"), "info@axa.fr", 2, "125487956411", StatutJuridique.SARL, new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS")));
        //Console.WriteLine("----------------------------------------");
        //clients = clientRepo.getClients(); 
        //clients.ForEach(client => Console.WriteLine(client.toString())); 

        CompteBancaireTests();

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