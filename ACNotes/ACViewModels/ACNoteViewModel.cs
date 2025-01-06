using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ACNotes.ACViewModels
{
    internal class ACNoteViewModel : ObservableObject, IQueryAttributable
    {
        private ACModels.ACNote _note;

        public string Text
        {
            get => _note.ACText;
            set
            {
                if (_note.ACText != value)
                {
                    _note.ACText = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.ACDate;

        public string Identifier => _note.ACFilename;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ACNoteViewModel()
        {
            _note = new ACModels.ACNote();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public ACNoteViewModel(ACModels.ACNote note)
        {
            _note = note;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _note.ACDate = DateTime.Now;
            _note.AC_Save();
            await Shell.Current.GoToAsync($"..?saved={_note.ACFilename}");
        }

        private async Task Delete()
        {
            _note.AC_Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.ACFilename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = ACModels.ACNote.AC_Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _note = ACModels.ACNote.AC_Load(_note.ACFilename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}