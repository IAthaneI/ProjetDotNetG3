using BankLib.Models;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace BankLib.Utilities
{
    /// <summary>
    /// Classe de génération de fichier
    /// </summary>
    public class ParserTool
    {
        /*TODO : Relier avec xslt pour formatter*/
        /// <summary>
        /// Opérations avec censures de données
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="mask"></param>
        /// fichier de sortie dans ApplicationConsole\bin\Debug\
        public static void OperationToXml(OperationModel operation, bool mask = false)
        {
            string dateOnly = DateTime.Now.ToString("yyyy-MM-dd");
            string xmlFilePath = $"operations-{dateOnly}.xml";
            string styleSheet = "operationStyleSheet.xsl";
            string htmlFilePath = $"operations-{dateOnly}.html";



            FileStream fileStream = File.Create(xmlFilePath);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OperationModel), "Operations");
            xmlSerializer.Serialize(fileStream, operation);
            fileStream.Close();

            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(styleSheet);
            myXslTrans.Transform(xmlFilePath, htmlFilePath);
        }
        
    }
}
