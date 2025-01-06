using Microsoft.Maui.Controls;
using ACNotes.ACViewModels;

namespace ACNotes.Views
{
    public partial class ACAllNotesPage : ContentPage
    {
        public ACAllNotesPage()
        {
            InitializeComponent();
        }

        
        private void AC_ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            notesCollection.SelectedItem = null; 
        }
    }
}
