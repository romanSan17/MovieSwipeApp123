using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MovieSwipeApp.Models;
using MovieSwipeApp.Services;
using Windows.System;


namespace MovieSwipeApp.Views;

public partial class ProfilePage : ContentPage
{
    readonly User _user;

    // ������������� �����-������ ��� ��������
    class LikedItem
    {
        public Movie Movie { get; init; } = null!;
        public UserMovie UserMovie { get; init; } = null!;

        public string Title => Movie.Title;
        public string PosterUrl => Movie.PosterUrl;
        public int Rating
        {
            get => UserMovie.Rating;
            set => UserMovie.Rating = value;
        }
    }

    readonly ObservableCollection<LikedItem> _items = new();

    public ProfilePage(User user)
    {
        _user = user;
        InitializeComponent();
        LikedView.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadLikedAsync();
    }

    async Task LoadLikedAsync()
    {
        _items.Clear();

        var liked = await DatabaseService.GetLikedMoviesAsync(_user.Id);
        foreach (var (movie, um) in liked)
            _items.Add(new LikedItem { Movie = movie, UserMovie = um });
    }

    //async void OnRatingChanged(object sender, ValueChangedEventArgs e)
    //{
    //    if (sender is not RatingView rv || rv.BindingContext is not LikedItem li) return;


    //    // ��������� ��
    //    await DatabaseService.UpdateRatingAsync(li.UserMovie);
    //}
}
