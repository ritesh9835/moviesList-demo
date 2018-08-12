using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesList.DbEntities
{
    public class MovieActorMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Movies"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }
        [ForeignKey("Actors"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActorId { get; set; }

        public virtual Actors Actors { get; set; }
        public virtual Movies Movies { get; set; }
    }
}
