using System;
using System.Collections.Generic;

namespace MediaService
{
    public class Movie

    {
        public int movieId { get; set; }
        public string title { get; set; }
        public List<string> genre { get; set; }
        public Movie()
        {
            genre = new List<string>();
        }

    }
}