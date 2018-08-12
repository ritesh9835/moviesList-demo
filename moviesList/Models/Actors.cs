using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesList.DbEntities
{
    public class Actors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }
        public string Dp { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<MovieActorMapping> MovieActorMapping { get; set; }

    }
}
