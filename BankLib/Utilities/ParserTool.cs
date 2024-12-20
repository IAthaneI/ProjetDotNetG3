﻿using BankLib.Models;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace BankLib.Utilities
{
    /// <summary>
    /// Classe de génération de fichier
    /// </summary>
    public class ParserTool
    {
        /// <summary>
        /// Génération de fichier xml et html a partir des opérations
        /// </summary>
        /// <param name="operation">List<OperationModel></param>
        /// fichier de sortie dans ApplicationConsole\bin\Debug\
        public static void OperationToXml(List<OperationModel> operations)
        {
            Console.WriteLine(operations.Count);
            string dateOnly = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string xmlFilePath = $".\\OPERATION\\operations-{dateOnly}.xml";
            string styleSheetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "operationStyleSheet.xml");
            string htmlFilePath = $".\\OPERATION\\operations-{dateOnly}.html";

            #region Test
            //Console.WriteLine(styleSheet);
            //Console.WriteLine(Environment.CurrentDirectory);
            #endregion

            FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.Write);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OperationModel>));
            xmlSerializer.Serialize(fileStream, operations);
            fileStream.Close();

            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(styleSheetPath);
            myXslTrans.Transform(xmlFilePath, htmlFilePath);
        }
        
    }
}
