using Microsoft.Maui.Controls;
using ACNotes.Views;

namespace ACNotes
{
    public partial class ACAppShell : Shell
    {
        public ACAppShell()
        {
            InitializeComponent();

            
            Routing.RegisterRoute(nameof(ACNotePage), typeof(ACNotes.Views.ACNotePage));
        }

        
    }
}
