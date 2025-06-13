using Microsoft.Extensions.Logging.Abstractions;
using MovieSwipeApp.Services;

namespace MovieSwipeApp.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage() => InitializeComponent();

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LoginEntry.Text) ||
            string.IsNullOrWhiteSpace(PassEntry.Text))
        {
            await DisplayAlert("Îøèáêà", "Ââåäèòå ëîãèí è ïàðîëü", "OK");
            return;
        }

        if (PassEntry.Text != ConfirmEntry.Text)
        {
            await DisplayAlert("Îøèáêà", "Ïàðîëè íå ñîâïàäàþò", "OK");
            return;
        }

        bool success = await DatabaseService.RegisterAsync(LoginEntry.Text!, PassEntry.Text!);
        if (!success)
        {
            await DisplayAlert("Îøèáêà", "Òàêîé ëîãèí óæå ñóùåñòâóåò", "OK");
            return;
        }

        var user = await DatabaseService.LoginAsync(LoginEntry.Text!, PassEntry.Text!);

        if (user is null)           
        {
            await DisplayAlert("Îøèáêà", "Íå óäàëîñü âîéòè ïîñëå ðåãèñòðàöèè", "OK");
            return;
        }


        Page next = user.Role == "admin"
                    ? new AdminPage(user)
                    : new MainPage(user);


        Application.Current!.MainPage = new NavigationPage(next);
    }

    void OnBack(object sender, EventArgs e) => Navigation.PopAsync();
}
