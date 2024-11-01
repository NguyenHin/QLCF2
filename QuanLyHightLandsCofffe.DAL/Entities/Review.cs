namespace QuanLyHightLandsCofffe.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int id { get; set; }

        public int CustomerId { get; set; }

        public int FoodId { get; set; }

        public int Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
