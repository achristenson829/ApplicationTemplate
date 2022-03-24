using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MediaService.Services;

public class MovieService : IMovieService
{
    private readonly ILogger<IMovieService> _logger;

    public MovieService(ILogger<IMovieService> logger)
    {
        _logger = logger;
    }

    public void Read()
    {
        string file = "Files/movies.csv";
        if (File.Exists(file))
        {
            StreamReader sr = new StreamReader(file);

            Console.WriteLine("How many records do you want to see? Or A for All Records");
            var choice = Console.ReadLine().ToUpper();
            sr.ReadLine();
            if (choice != "A")
            {
                int counter = 0;
                while (counter < int.Parse(choice))
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                    counter++;
                }
            }
            else
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }


            sr.Close();
        }
        else
        {
            Console.WriteLine("File not found");
        }
    }


    public void Write()
    {
        try
        {
            List<Movie> movies = new List<Movie>();
            string file = "Files/movies.csv";
            using (var reader = new StreamReader(file))
            {
                Movie movie = new Movie();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    string[] movieDetails = line.Split(',');
                    movie.movieId = int.Parse(movieDetails[0]);
                    movie.title = movieDetails[1];
                    movie.genre = movieDetails[2].Split('|').ToList();
                    movies.Add(movie);
                }

                reader.Close();


                StreamWriter sw = new StreamWriter(file, true);
                string resp = "";
                do
                {
                    movie.movieId = movies.Max(m => m.movieId) + 1;

                    Console.WriteLine("Enter movie title");
                    string title = Console.ReadLine();
                    if (movie.title.Contains(title))
                    {
                        Console.WriteLine("Movie already exists");
                        Console.WriteLine("Enter different movie title");
                        title = Console.ReadLine();
                        if (title.Contains(','))
                        {
                            title = "\"title\"";
                        }
                    }


                    var choice = "";
                    var genre = new List<string>();
                    do
                    {
                        Console.WriteLine("Enter movie genre");
                        genre.Add(Console.ReadLine());
                        Console.WriteLine("Is there another genre to add? Y/N ");
                        choice = Console.ReadLine().ToUpper();
                    } while (choice != "N");

                    string genres = string.Join("|", genre);

                    sw.WriteLine("{0},{1},{2}", movie.movieId, title, genres);
                    Console.WriteLine("Do you want to add another movie (Y/N)?");
                    resp = Console.ReadLine().ToUpper();
                } while (resp != "N");

                sw.Close();

            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("File not found");
        }

    }
}
