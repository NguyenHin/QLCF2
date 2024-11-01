namespace QuanLyHightLandsCofffe.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Waitstaff")]
    public partial class Waitstaff
    {
        public int id { get; set; }

        public int StaffId { get; set; }

        public int TableId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual TableFood TableFood { get; set; }
    }
}
