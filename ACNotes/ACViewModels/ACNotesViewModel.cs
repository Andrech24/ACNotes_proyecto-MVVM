using ACNotes.ACModels;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace ACNotes.ACViewModels
{
    internal class ACNotesViewModel : IQueryAttributable
    {
        public ObservableCollection<ACNoteViewModel> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public ACNotesViewModel()
        {
            AllNotes = new ObservableCollection<ACNoteViewModel>(ACNote.AC_LoadAll().Select(n => new ACNoteViewModel(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ACNoteViewModel>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(ACNotes.Views.ACNotePage));
        }

        private async Task SelectNoteAsync(ACNoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(ACNotes.Views.ACNotePage)}?load={note.Identifier}");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                ACNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                ACNoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0); 
                }
               
                else
                    AllNotes.Insert(0, new ACNoteViewModel(ACNote.AC_Load(noteId)));
            }
        }
    }
}
