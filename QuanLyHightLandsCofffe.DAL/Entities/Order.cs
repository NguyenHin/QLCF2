namespace QuanLyHightLandsCofffe.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int id { get; set; }

        public int CustomerId { get; set; }

        public int BillId { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
