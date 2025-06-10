namespace MovieSwipeApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new WelcomePage(new Views.WelcomePage());
    }
}
