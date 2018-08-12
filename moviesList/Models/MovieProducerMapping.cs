using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesList.DbEntities
{
    public class MovieProducerMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Movies"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }
        [ForeignKey("Producers"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProducerId { get; set; }

        public virtual Movies Movies { get; set; }
        public virtual Producers Producers { get; set; }
    }
}
