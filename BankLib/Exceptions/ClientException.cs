using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib.Exceptions
{
    public class ClientException : Exception
    {
        private int code;
        private string? mes;
        
        public ClientException() { }

        public ClientException(int code) 
        {
            switch (code) 
            {
                case 0:
                    throw new ClientException(code, "ERROR - Le champs nom d'un client ne peux pas faire plus de 50 caractères");
                case 1:
                    throw new ClientException(code, "ERROR - Le champs mail d'un client doit contenir au moins un @");
                case 2:
                    throw new ClientException(code, "ERROR - Le champs prenom d'un client ne peux pas faire plus de 50 caractères");
                case 3:
                    throw new ClientException(code, "ERROR - Le Siret d'un client doit faire exactement 14 caractères");
                default:
                    throw new ClientException(code, "ERROR - Une erreur non gérer a été déclanché lors de la création d'un client ");
            }
        }

        public ClientException(int code, string message)
        {
            Code = code;
            Mes = message;
        }

        public int Code { get => code; set => code = value; }
        public string? Mes { get => mes; set => mes = value; }
    }
}
