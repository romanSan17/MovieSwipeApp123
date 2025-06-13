using MovieSwipeApp.Models;
using MovieSwipeApp.Services;
using System.Collections.ObjectModel;

namespace MovieSwipeApp.Views;

public partial class ProfilePage : ContentPage
{
    readonly User _user;

    public class LikedItem
    {
        public Movie Movie { get; init; } = null!;
        public UserMovie UserMovie { get; init; } = null!;
    }

    public ObservableCollection<LikedItem> LikedItems { get; } = new();

    public ProfilePage(User user)
    {
        InitializeComponent();
        _user = user;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadLikedAsync();
    }

    async Task LoadLikedAsync()
    {
        LikedItems.Clear();
        var list = await DatabaseService.GetLikedMoviesAsync(_user.Id);
        foreach (var (movie, um) in list)
        {
            LikedItems.Add(new LikedItem
            {
                Movie = movie,
                UserMovie = um
            });
        }
    }

    async void OnStarsTapped(object sender, EventArgs e)
    {
        if (sender is not Label lbl) return;
        if (lbl.BindingContext is not LikedItem li) return;

        string result = await DisplayPromptAsync(
            "Îöåíêà",
            $"Ââåäèòå ðåéòèíã äëÿ «{li.Movie.Title}» (0–5):",
            initialValue: li.UserMovie.Rating.ToString(),
            maxLength: 1,
            keyboard: Keyboard.Numeric);

        if (int.TryParse(result, out int r) && r >= 0 && r <= 5)
        {
            li.UserMovie.Rating = r;
            await DatabaseService.UpdateRatingAsync(li.UserMovie);
            await LoadLikedAsync();
        }
    }
}
