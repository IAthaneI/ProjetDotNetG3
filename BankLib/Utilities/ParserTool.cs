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
        public static void OperationToXml(List<OperationModel> operations, bool mask = false)
        {
            string dateOnly = DateTime.Today.ToString("yyyy-MM-dd");
            string xmlFilePath = $"operations-{dateOnly}.xml";
            string styleSheet = "operationStyleSheet.xsl";
            string htmlFilePath = $"operations-{dateOnly}.html";


            #region Test
            // fichier dans ConsoleAppTestApi\bin\Debug\
            var test = Environment.CurrentDirectory;
            Console.WriteLine(test);
            //FileStream fileStream = File.Create("OpErAtIoNxxxxxxxxxxxxxxxxxxxxxxx.xml");
            //FileStream fileStream = new FileStream("info.xml", FileMode.OpenOrCreate, FileAccess.Write);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(OperationModel));
            //xmlSerializer.Serialize(fileStream, operations[0]);
            #endregion

            //FileStream fileStream = File.Create(xmlFilePath);
            FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.Write);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OperationModel>), "Operations");
            xmlSerializer.Serialize(fileStream, operations);
            fileStream.Close();

            //XslCompiledTransform myXslTrans = new XslCompiledTransform();
            //myXslTrans.Load(styleSheet);
            //myXslTrans.Transform(xmlFilePath, htmlFilePath);
        }
        
    }
}
