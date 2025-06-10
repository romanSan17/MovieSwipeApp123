using Microsoft.Extensions.Logging.Abstractions;
using MovieSwipeApp.Services;

namespace MovieSwipeApp.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage() => InitializeComponent();

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        // ������� ���������
        if (string.IsNullOrWhiteSpace(LoginEntry.Text) ||
            string.IsNullOrWhiteSpace(PassEntry.Text))
        {
            await DisplayAlert("������", "������� ����� � ������", "OK");
            return;
        }

        if (PassEntry.Text != ConfirmEntry.Text)
        {
            await DisplayAlert("������", "������ �� ���������", "OK");
            return;
        }

        // ������� ����������������
        bool success = await DatabaseService.RegisterAsync(LoginEntry.Text!, PassEntry.Text!);
        if (!success)
        {
            await DisplayAlert("������", "����� ����� ��� ����������", "OK");
            return;
        }

        // ��������� ����� ����� �������� �����������
        var user = await DatabaseService.LoginAsync(LoginEntry.Text!, PassEntry.Text!);

        if (user is null)             // ������������, �� ��������
        {
            await DisplayAlert("������", "�� ������� ����� ����� �����������", "OK");
            return;
        }

        // ��������� �� ������ ��������
        Page next = user.Role == "admin"
                    ? new AdminPage(user)
                    : new MainPage(user);

        // ������� ���� (����� �� Back �� ������������ � ������)
        Application.Current!.MainPage = new NavigationPage(next);
    }

    void OnBack(object sender, EventArgs e) => Navigation.PopAsync();
}
