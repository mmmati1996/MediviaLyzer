using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MediviaLyzer.Others
{
    public static class Constants
    {
        private static string _myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string _nameOfDocumentFolder = "MediviaLyzer";
        public static string FullDocumentsPath = Path.Combine(_myDocumentsPath, _nameOfDocumentFolder);
        public static string SettingsFilePath = Path.Combine(FullDocumentsPath, "medivialyzer.settings");
    }
}
