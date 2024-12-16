﻿using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using BankLib.Models;
using BankLib.Utilities;

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
        bool end = false;
        Console.WriteLine("[| Vous êtes connecter ! |]");
        //ClientTests();
        //CompteBancaireTests();
        CarteBancaireTests(false);
        OperationXmlTest(false);
    }

    private static void ClientTests()
    {
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

    private static void CompteBancaireTests()
    {
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
        cbr.InsertCarteBancaire(new CarteBancaireModel() { CompteBancaireId = 1, NomTitulaire = "Bill" });
    }

    private static void OperationXmlTest(bool run = true)
    {
        if(!run) return;
        OperationRepository opr = new OperationRepository();
        DateTime debut = DateTime.Today.AddMonths(-3);
        DateTime fin = DateTime.Today;
        ParserTool.OperationToXml(opr.GetOperations(debut, fin));
        ParserTool.OperationToXml(opr.GetOperations(debut, fin, "3T6DA3JHT5"));
    }


}