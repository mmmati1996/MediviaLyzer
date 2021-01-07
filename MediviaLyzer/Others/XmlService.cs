using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MediviaLyzer.Others
{
    public static class XmlService
    {
        public static T DeserializeXml<T>(string name = null)
        {
            using StreamReader reader = new StreamReader(Constants.SettingsFilePath);
            using var xmlReader = XmlReader.Create(reader);
            if (name != null)
            {
                xmlReader.ReadToDescendant(name);
                var xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(name));
                return (T)xmlSerializer.Deserialize(xmlReader.ReadSubtree());
            }
            return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }
        public static void SerializeToXml<T>(T anyobject)
        {
            if (!Directory.Exists(Constants.FullDocumentsPath))
                Directory.CreateDirectory(Constants.FullDocumentsPath);
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());
            using StreamWriter writer = new StreamWriter(Constants.SettingsFilePath);
                xmlSerializer.Serialize(writer, anyobject);
        }
    }
}
