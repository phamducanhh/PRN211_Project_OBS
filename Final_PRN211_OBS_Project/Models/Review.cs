namespace Final_PRN211_OBS_Project.Models
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

        public int book_id { get; set; }

        public int user_id { get; set; }

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}
