using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACNotes.ACModels; 

namespace ACNotes.ACModels
{
    internal class ACNote
    {
        public string ACFilename { get; set; }
        public string ACText { get; set; }
        public DateTime ACDate { get; set; }

        public ACNote()
        {
            ACFilename = $"{Path.GetRandomFileName()}.notes.txt";
            ACDate = DateTime.Now;
            ACText = "";
        }

        public void AC_Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, ACFilename), ACText);

        public void AC_Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, ACFilename));

        public static ACNote AC_Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return new()
            {
                ACFilename = Path.GetFileName(filename),
                ACText = File.ReadAllText(filename),
                ACDate = File.GetLastWriteTime(filename)
            };
        }

        public static IEnumerable<ACNote> AC_LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(filename => ACNote.AC_Load(Path.GetFileName(filename)))
                .OrderByDescending(note => note.ACDate);
        }
    }
}