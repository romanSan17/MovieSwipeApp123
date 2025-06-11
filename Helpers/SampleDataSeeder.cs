using SQLite;
using MovieSwipeApp.Models;

namespace MovieSwipeApp.Helpers;

public static class SampleDataSeeder
{
    public static async Task SeedAsync(SQLiteAsyncConnection db)
    {
        if (await db.Table<Movie>().CountAsync() > 0) return;

        var movies = new[]
        {
        // — Ужасы
        new Movie{ Title="Крик",        Genre="horror",   PosterUrl="scream.jpg", Description="Студенты попадают в смертельную игру неизвестного убийцы в маске, проверяя свою храбрость."},
        new Movie{ Title="Заклятие",    Genre="horror",   PosterUrl="zak.jpg", Description="Супружеская пара, публикующая экзорцизм, сталкивается с настоящим демоническим ужасом."},
        new Movie{ Title="Прочь",       Genre="horror",   PosterUrl="pro.jpg", Description="Молодой афро-американец едет в гости к семье своей подруги и вскрывает нечто зловещее."},
        new Movie{ Title="Астрал",      Genre="horror",   PosterUrl="ast.jpg", Description="Семья пытается спасти сына от паранормальных сил, заглянув в мир «Астрала»."},
        new Movie{ Title="Нечто",       Genre="horror",   PosterUrl="nech.png", Description="Исследователи на отдалённой базе сталкиваются с формо-подобным пришельцем, меняющим облик."},

        // — Комедия
        new Movie{ Title="Мальчишник в Вегасе", Genre="comedy",  PosterUrl="mal.jpg", Description="Троица друзей просыпается после эпичной вечеринки и пытается восстановить, что произошло."},
        new Movie{ Title="Суперперцы",       Genre="comedy", PosterUrl="sup.jpg", Description="Два неуклюжих подростка решают устроить запоминающийся выпускной вечер перед колледжем."},
        // … и ещё 3

        // — Драма
        new Movie{ Title="Зелёная миля",    Genre="drama",    PosterUrl="zel.jpg", Description="Охранник тюрьмы обнаруживает сверхъестественные силы у приговорённого к казни заключённого."},
        // … плюс ещё 4

        // — Фантастика
        new Movie{ Title="Матрица",        Genre="sci-fi",   PosterUrl="mat.jpg", Description="Программист открывает, что реальный мир — лишь симуляция, и вступает в борьбу с машинами."},
        new Movie{ Title="Интерстеллар", Genre="sci-fi",   PosterUrl="inter.jpg", Description="Группа исследователей отправляется через червоточину, чтобы спасти человечество от гибели." }
        // … плюс ещё 4 
    };

        await db.InsertAllAsync(movies);
    }

}
