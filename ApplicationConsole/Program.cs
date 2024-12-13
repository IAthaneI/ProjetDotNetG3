using ApplicationConsole.Repository;
using ApplicationConsole.Utilities;
using Azure;
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
        while (!end) 
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
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad7:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
                    break;
                case ConsoleKey.NumPad8:
                    Console.WriteLine("\nCette foncionnalité n'a pas encore été implémenter désolé");
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
                        default : 
                            typeError = true;
                            Console.WriteLine("Cette option n'existe pas, abandon de l'ajout"); 
                            break;
                    }

                    if (!typeError) 
                    {
                        int idCB = carteBancaireRepository.GetIdCarteBancaireByNumCarte(NumCarte);
                        if (idCB == -1)
                            Console.WriteLine("Carte bancaire introuvable ");
                        else 
                        {
                            bool opRealisable = true;
                            if (type.Equals(TypeOperation.Retrait) || type.Equals(TypeOperation.Facture))
                            {
                                opRealisable = compteBancaireRepository.CheckNegativeOperation(idCB,Montant);
                            }

                            if (opRealisable)
                            {
                                CompteBancaire comp = compteBancaireRepository.GetCompteBancaireByIdCarte(idCB);
                                if (type.Equals(TypeOperation.Retrait) || type.Equals(TypeOperation.Facture))
                                {
                                    comp.Solde = comp.Solde - Montant;
                                }
                                else 
                                {
                                    comp.Solde = comp.Solde + Montant;
                                }
                                compteBancaireRepository.UpdateSoldeCompteBancaire(comp);
                                Enregistrement enr = new Enregistrement(id, NumCarte, Montant, type, DateTime.Now, idCB);
                                bool result = enregistrementRepository.InsertEnregistrement(enr);
                                if (result)
                                {
                                    Console.WriteLine("La nouvelle operation a bien été ajouter");
                                }
                                else
                                {
                                    Console.WriteLine("Impossible d'ajouter la nouvelle operation");
                                }
                            }
                            else 
                            {
                                Console.WriteLine("L'operation n'est pas realisable car le solde est trop bas");
                            }
                        }
                    }
                    Console.WriteLine("[|-----------------------------------------|]");
                    Console.WriteLine("Appuyer sur une touche pour continuer ... ");
                    Console.ReadKey();
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
}