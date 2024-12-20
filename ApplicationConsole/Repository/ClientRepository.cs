﻿using System.Data.Common;
using BankLib.Model;
using ApplicationConsole.Utilities;

namespace ApplicationConsole.Repository
{
    /// <summary>
    /// Cette classe va avoir toute les methodes pour acceder aux clients en base 
    /// </summary>
    internal class ClientRepository
    {
        private DbConnection? connection;
        public ClientRepository()
        {

        }

        /// <summary>
        /// Recupere tout les clients présent en base, Particuliers ET Professionnel
        /// </summary>
        /// <returns>
        /// La liste des clients présent en base
        /// </returns>
        public List<Client> getClients()
        {

            this.connection = DBUtilities.GetConnection();
            List<Client> clients = new List<Client>();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT * FROM Clients c JOIN ClientsParticuliers cp ON cp.IdentifiantClient = c.Identifiant";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Adresse adresse = new Adresse(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    Enum.TryParse(reader.GetString(11), out Sexe sex);
                    clients.Add(new ClientPart(reader.GetInt32(0), reader.GetString(1), adresse, reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8) , reader.GetDateTime(9), reader.GetString(10), sex));

                }

                connection.Close();

                connection.Open();

                query = "SELECT * FROM Clients c JOIN ClientsProfessionnels cp ON cp.IdentifiantClient = c.Identifiant";
                command = connection.CreateCommand();
                command.CommandText = query;
                DbDataReader reader2 = command.ExecuteReader();

                while (reader2.Read())
                {
                    Adresse adresse = new Adresse(reader2.GetString(2), reader2.GetString(3), reader2.GetString(4), reader2.GetString(5));
                    Adresse siege = new Adresse(reader2.GetString(11), reader2.GetString(12), reader2.GetString(13), reader2.GetString(14));
                    Enum.TryParse(reader2.GetString(10), out StatutJuridique statutJuridique);
                    clients.Add(new ClientPro(reader2.GetInt32(0), reader2.GetString(1), adresse, reader2.GetString(6), reader2.GetInt32(7), reader2.GetInt32(8), reader2.GetString(9), statutJuridique, siege));
                }
                connection.Close();
            }
            return clients;
        }

        /// <summary>
        /// Recupere un client en base celon l'Id, qu'ils soit un particulier ou un professionel
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Le client si il existe, null sinon 
        /// </returns>
        public Client? getClient(int id)
        {
            this.connection = DBUtilities.GetConnection();
            Client? client = null;
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT * FROM Clients c JOIN ClientsParticuliers cp ON cp.IdentifiantClient = c.Identifiant where c.Identifiant = @id";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "id", id, "Clients.Identifiant");
                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Adresse adresse = new Adresse(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    Enum.TryParse(reader.GetString(11), out Sexe sex);
                    client = new ClientPart(reader.GetInt32(0), reader.GetString(1), adresse, reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetDateTime(9), reader.GetString(10), sex);

                    return client;
                }
                connection.Close();

                connection.Open();

                query = "SELECT * FROM Clients c JOIN ClientsProfessionnels cp ON cp.IdentifiantClient = c.Identifiant where c.Identifiant = @id";
                command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "id", id, "Clients.Identifiant");
                DbDataReader reader2 = command.ExecuteReader();

                while (reader2.Read())
                {
                    Adresse adresse = new Adresse(reader2.GetString(2), reader2.GetString(3), reader2.GetString(4), reader2.GetString(5));
                    Adresse siege = new Adresse(reader2.GetString(11), reader2.GetString(12), reader2.GetString(13), reader2.GetString(14));
                    Enum.TryParse(reader2.GetString(10), out StatutJuridique statutJuridique);
                    client = new ClientPro(reader2.GetInt32(0), reader2.GetString(1), adresse, reader2.GetString(6), reader2.GetInt32(7), reader2.GetInt32(8), reader2.GetString(9), statutJuridique, siege);

                    return client;
                }

                connection.Close();


            }
            return client;
        }

        /// <summary>
        /// Va inserer un client en base 
        /// </summary>
        /// <param name="client"></param>
        /// <returns>
        /// True si il a bien été inserer, False sinon 
        /// </returns>
        public bool InsertClient(Client client)
        {
            this.connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "INSERT INTO Clients Values (@Id, @Nom, @Libelle, @Complement, @Cp, @Ville, @Mail, @IdCompte)";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                DBUtilities.AddParameter(command, "Id", client.Identifiant, "Identifiant");
                DBUtilities.AddParameter(command, "Nom", client.Nom, "Nom");
                DBUtilities.AddParameter(command, "Libelle", client.Adresse.Libelle, "LibellePostale");
                DBUtilities.AddParameter(command, "Complement", client.Adresse.Complement, "ComplementPostale");
                DBUtilities.AddParameter(command, "Cp", client.Adresse.Cp, "CpPostale");
                DBUtilities.AddParameter(command, "Ville", client.Adresse.Ville, "VillePostale");
                DBUtilities.AddParameter(command, "Mail", client.Mail, "Mail");
                DBUtilities.AddParameter(command, "IdCompte", client.IdCompte, "IdCompte");


                int result = command.ExecuteNonQuery();
                connection.Close();
            }
            if (client is ClientPart)
            {
                ClientPart toInsert = (ClientPart)client;
                if (connection != null)
                {
                    connection.Open();
                    string query = "INSERT INTO ClientsParticuliers Values (@Id, @DateNaiss, @Prenom, @Sexe, @IdClient)";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "Id", toInsert.Ident, "Id");
                    DBUtilities.AddParameter(command, "DateNaiss", toInsert.DateNaissance, "DateNaissance");
                    DBUtilities.AddParameter(command, "Prenom", toInsert.Prenom, "Prenom");
                    DBUtilities.AddParameter(command, "Sexe", toInsert.Sexe.ToString(), "Sexe");
                    DBUtilities.AddParameter(command, "IdClient", toInsert.Identifiant, "IdentifiantClient");


                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result > 0;
                }

                return false;
            }
            else 
            {
                ClientPro toInsert = (ClientPro)client;
                if (connection != null)
                {
                    connection.Open();
                    string query = "INSERT INTO ClientsProfessionnels Values (@Id, @Siret, @Statut, @Libelle, @Complement, @Cp, @Ville, @IdClient)";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DBUtilities.AddParameter(command, "Id", toInsert.Ident, "Id");
                    DBUtilities.AddParameter(command, "Siret", toInsert.Siret, "DateNaissance");
                    DBUtilities.AddParameter(command, "Statut", toInsert.StatutJuridique.ToString(), "Prenom");
                    DBUtilities.AddParameter(command, "Libelle", toInsert.Siege.Libelle, "Sexe");
                    DBUtilities.AddParameter(command, "Complement", toInsert.Siege.Complement, "IdentifiantClient");
                    DBUtilities.AddParameter(command, "Cp", toInsert.Siege.Cp, "IdentifiantClient");
                    DBUtilities.AddParameter(command, "Ville", toInsert.Siege.Ville, "IdentifiantClient");
                    DBUtilities.AddParameter(command, "IdClient", toInsert.Identifiant, "IdentifiantClient");

                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result > 0;
                }

                return false;
            }

        }

        public int GetNewMaxId(String champID, String table)
        {
            int id = -1;
            connection = DBUtilities.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = $"SELECT MAX({champID}) as idMax FROM {table} ";
                DbCommand command = connection.CreateCommand();
                command.CommandText = query;

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                connection.Close();
            }
            return ++id;
        }

    }
}
