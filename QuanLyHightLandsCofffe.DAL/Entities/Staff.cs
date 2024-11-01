namespace QuanLyHightLandsCofffe.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Waitstaffs = new HashSet<Waitstaff>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public DateTime HireDate { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Waitstaff> Waitstaffs { get; set; }
    }
}
