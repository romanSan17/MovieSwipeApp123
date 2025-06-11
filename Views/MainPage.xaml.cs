using System.Collections.ObjectModel;
using MovieSwipeApp.Models;
using MovieSwipeApp.Services;

namespace MovieSwipeApp.Views;

public partial class MainPage : ContentPage
{
    readonly User _user;
    readonly ObservableCollection<Movie> _movies = new();

    public IList<Movie> Movies => _movies;

    public MainPage(User user)
    {
        InitializeComponent();
        _user = user;

        // Настраиваем список жанров
        GenrePicker.ItemsSource = new[] { "Все", "horror", "comedy", "drama", "sci-fi" };
        GenrePicker.SelectedIndex = 0;

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMoviesAsync();
    }

    async Task LoadMoviesAsync()
    {
        var genre = GenrePicker.SelectedItem?.ToString();
        var list = await DatabaseService.GetMoviesAsync(genre == "Все" ? null : genre);

        _movies.Clear();
        foreach (var m in list)
            _movies.Add(m);

        // Сбрасываем позицию слайдера на первый фильм
        MovieCarousel.Position = 0;
    }

    // «Нравится» — лайкаем и удаляем из списка
    async void OnLikeClicked(object sender, EventArgs e)
    {
        if (MovieCarousel.CurrentItem is Movie m)
        {
            await DatabaseService.LikeMovieAsync(_user.Id, m.Id);
            _movies.Remove(m);
            MovieCarousel.ItemsSource = null;
            MovieCarousel.ItemsSource = Movies;
        }
    }

    // «Не нравится» — просто удаляем из списка
    void OnDislikeClicked(object sender, EventArgs e)
    {
        if (MovieCarousel.CurrentItem is Movie m)
        {
            _movies.Remove(m);
            MovieCarousel.ItemsSource = null;
            MovieCarousel.ItemsSource = Movies;
        }
    }

    // При смене жанра — перезагружаем
    async void OnGenreChanged(object sender, EventArgs e)
        => await LoadMoviesAsync();

    // Переход в профиль
    async void OnProfile(object sender, EventArgs e)
        => await Navigation.PushAsync(new ProfilePage(_user));
}
