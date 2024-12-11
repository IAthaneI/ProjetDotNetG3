using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace ApplicationConsole.Utilities
{
    /// <summary>
    /// Cette classe sert a generaliser des methodes de gestion de base de donnée 
    /// </summary>
    internal class DBUtilities
    {
        /// <summary>
        /// Cette methode va retourner une connection a la base de donnee via App.Config
        /// </summary>
        /// <returns>Connection a la base de donnée si réussie ou Null</returns>
        public static DbConnection? GetConnection()
        {
            var config = ConfigurationManager.ConnectionStrings["cnxBase"];
            DbProviderFactories.RegisterFactory(config.ProviderName, SqlClientFactory.Instance);
            DbProviderFactory dbpf = DbProviderFactories.GetFactory(config.ProviderName);
            DbConnection? connection = dbpf.CreateConnection();
            if (connection != null)
            {
                connection.ConnectionString = config.ConnectionString;
            }
            return connection;
        }

        /// <summary>
        /// Cette methode va permettre d'ajouter un parametre a une commande
        /// </summary>
        /// <param name="command"></param> 
        /// <param name="parameterName"></param> 
        /// <param name="value"></param> 
        /// <param name="sourceColumn"></param> 
        public static void AddParameter(DbCommand command, string parameterName, object value, string sourceColumn) 
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = parameterName;
            param.Value = value;
            param.SourceColumn = sourceColumn;
            command.Parameters.Add(param);
        }


    }
}
