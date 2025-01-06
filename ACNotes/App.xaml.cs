namespace ACNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ACAppShell();
        }
    }
}
