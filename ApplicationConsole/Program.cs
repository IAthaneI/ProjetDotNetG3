using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using BankLib.Entities;
using BankLib.Exceptions;
using BankLib.Model;
using BankLib.Models;
using BankLib.Utilities;
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
        ClientTests(false);
        CompteBancaireTests(false);
        CarteBancaireTests(false);
        OperationXmlTest();
    }

    private static void ClientTests(bool run = true) 
    {
        if (!run) return;
        try
        {
            ClientRepository clientRepo = new ClientRepository();
            List<Client> clients = clientRepo.getClients();
            clients.ForEach(client => Console.WriteLine(client.toString()));
            //clientRepo.InsertClient(new ClientPart(3, "BETY", new Adresse("12, rue des Oliviers", "", "94000", "CRETEIL"), "bety@gmail.com", 2, new DateTime(1985, 11, 12), "Daniel", Sexe.Homme));
            clientRepo.InsertClient(new ClientPro(4, "AXA", new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS"), "info@axa.fr", 2, "125487956411", StatutJuridique.SARL, new Adresse("125 rue lafayette", "Digicode 1432", "94120", "FONTENAY SOUS BOIS")));
            Console.WriteLine("----------------------------------------");
            clients = clientRepo.getClients();
            clients.ForEach(client => Console.WriteLine(client.toString()));
        }
        catch (ClientException e)
        {
            Console.WriteLine($"Une erreur a été détecter : Code - {e.Code} {e.Mes}");
        }
    }

    private static void CompteBancaireTests(bool run = true)
    {
        if (!run) return;
        CompteBancaireRepository cbr = new CompteBancaireRepository();
        List<CompteBancaireModel> cbmList = cbr.GetCompteBancaires();
        foreach (var c in cbmList)
        {
            Console.WriteLine($"{c.NumCompte} ouvert au {c.DateOuverture}. Solde :\t{c.Solde} Eur");
        }

        CompteBancaireModel cbm = cbr.GetCompteBancaire(1);
        Console.WriteLine($"{cbm.NumCompte} ouvert au {cbm.DateOuverture}. Solde :\t{cbm.Solde} Eur");

        Console.WriteLine("Insertion " + cbr.InsertCompteBancaire(new CompteBancaireModel()));
    }

    private static void CarteBancaireTests(bool run = true)
    {
        if (!run) return;
        CarteBancaireRepository cbr = new CarteBancaireRepository();
        List<CarteBancaireModel> cbmList = cbr.GetCarteBancaires();
        foreach (var c in cbmList)
        {
            Console.WriteLine($"Carte {c.NumCarte} pour le compte {c.CompteBancaireId} de {c.NomTitulaire}, expire au {c.DateExpiration}");
        }

        CarteBancaireModel cbm = cbr.GetCarteBancaire(2);
        if(cbm != null)
            Console.WriteLine($"Carte {cbm.NumCarte} pour le compte {cbm.CompteBancaireId} de {cbm.NomTitulaire}, expire au {cbm.DateExpiration}");

        Console.WriteLine("Insertion " + cbr.InsertCarteBancaire(new CarteBancaireModel() { NomTitulaire = "Hardman", CompteBancaireId = 1}));
    }

    private static void OperationXmlTest(bool run = true)
    {
        if(!run) return;
        OperationModel op = new OperationModel();
        op.Id = 1;
        op.NumCarte = "4949494949494949";
        op.Montant= 500;
        op.Type = BankLib.Models.Type.Depot;
        op.DateOp = DateTime.Today;
        //op.IdCarteBancaire = 2;
        ParserTool.OperationToXml(op);
    }


}