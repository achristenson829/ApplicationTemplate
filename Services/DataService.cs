using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace MediaService.Services
{

    public class DataService : IDataService
    {
        private readonly ILogger<IDataService> _logger;

        public DataService(ILogger<IDataService> logger)
        {
            _logger = logger;
        }



        public void Read()
        {
            try
            {
                List<Movie> movies = new List<Movie>();
                string fileName = "Files/movies.json";
                StreamReader r = new StreamReader(fileName);
                while (!r.EndOfStream)
                {
                    string json = r.ReadLine();
                    var m = JsonConvert.DeserializeObject<Movie>(json);
                    movies.Add(m);
                }

                foreach (Movie movie in movies)
                {
                    Console.Write("Id:{0} Title:{1} Genres: ", movie.movieId, movie.title);
                    movie.genre.ForEach(g => Console.Write(g + ", "));
                    Console.WriteLine();
                }

                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("No movies in file");
            }

        }

        public void Write()
        {
            List<Movie> movies = new List<Movie>();
            string fileName = "Files/movies.json";
            StreamReader r = new StreamReader(fileName);
            while (!r.EndOfStream)
            {
                string jsonString = r.ReadLine();
                var m = JsonConvert.DeserializeObject<Movie>(jsonString);
                movies.Add(m);
            }

            r.Close();

            Movie movie = new Movie();
            movie.movieId = movies.Count > 0 ? movies.Max(m => m.movieId + 1) : 1;
            Console.WriteLine("Enter movie title");
            movie.title = Console.ReadLine();
            var choice = "";
            movie.genre = new List<string>();
            do
            {
                Console.WriteLine("Enter movie genre");
                movie.genre.Add(Console.ReadLine());
                Console.WriteLine("Is there another genre to add? Y/N ");
                choice = Console.ReadLine().ToUpper();
            } while (choice != "N");

            string json = JsonConvert.SerializeObject(movie);

            File.WriteAllText(fileName, json);
        }


    }
}
