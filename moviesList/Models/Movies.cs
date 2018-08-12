using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesList.DbEntities
{
    public class Movies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        public string Poster { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<MovieActorMapping> MovieActorMapping { get; set; }
        public virtual ICollection<MovieProducerMapping> MovieProducerMapping { get; set; }
    }
}
