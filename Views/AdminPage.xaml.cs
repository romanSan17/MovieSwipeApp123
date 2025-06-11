using MovieSwipeApp.Models;
using MovieSwipeApp.Services;

namespace MovieSwipeApp.Views;

public partial class AdminPage : ContentPage
{
    readonly User _adminUser;
    List<Movie> _movies = new();

    public AdminPage(User admin)
    {
        _adminUser = admin;
        InitializeComponent();
    }

    /* ---------- ��������� ���� ---------- */
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshMovies();
    }

    /* ---------- ���������� ---------- */
    async void OnAddMovie(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            GenrePicker.SelectedItem is null)
        {
            await DisplayAlert("������", "������� �������� � ����", "OK");
            return;
        }

        var newMovie = new Movie
        {
            Title = TitleEntry.Text!.Trim(),
            Genre = GenrePicker.SelectedItem!.ToString()!,
            PosterUrl = PosterEntry.Text ?? "",
            Description = DescEditor.Text ?? ""
        };

        await DatabaseService.AddMovieAsync(newMovie);

        // �������� ����
        TitleEntry.Text = PosterEntry.Text = DescEditor.Text = string.Empty;
        GenrePicker.SelectedIndex = -1;

        await RefreshMovies();
    }

    /* ---------- �������� ����� ����� ---------- */
    async void OnDeleteSwipe(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipe &&
            swipe.BindingContext is Movie movie)
        {
            bool yes = await DisplayAlert("�������������",
                                          $"������� �{movie.Title}�?",
                                          "�������", "������");
            if (yes)
            {
                await DatabaseService.DeleteMovieAsync(movie);
                await RefreshMovies();
            }
        }
    }

    /* ---------- �������� ������ ---------- */
    async Task RefreshMovies()
    {
        _movies = await DatabaseService.GetMoviesAsync();
        MoviesView.ItemsSource = _movies;
    }
}
