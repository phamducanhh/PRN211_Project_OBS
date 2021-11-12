namespace Final_PRN211_OBS_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            Orderlines = new HashSet<Orderline>();
        }

        public int id { get; set; }

        public int user_id { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [Required]
        [StringLength(20)]
        public string telephone { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Required]
        [StringLength(20)]
        public string payment { get; set; }

        [Required]
        [StringLength(20)]
        public string status { get; set; }

        public double total { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orderline> Orderlines { get; set; }
    }
}
