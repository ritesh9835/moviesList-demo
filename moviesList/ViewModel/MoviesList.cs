using System;
using System.Collections.Generic;

namespace moviesList.ViewModel
{
    public class MoviesList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public List<ActorsList> Actors { get; set; }
        public List<ProducerList> Producers { get; set; }
    }

    public class ActorsList 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProducerList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
