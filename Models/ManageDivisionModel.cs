using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS_Digital_Recruitment.Models
{
    [Table("tlkp_divisi")]
    public class MasterDivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int div_id { get; set; }

        public string div_kode { get; set; }

        public string div_nama { get; set; }

        public string div_deskripsi { get; set; }

        public bool? div_status { get; set; }

        [Column("div_created_date")]
        public DateTime? div_createDate { get; set; }

        [Column("div_created_by")]
        public string div_createBy { get; set; }

        [Column("div_modif_date")]
        public DateTime? div_modifDate { get; set; }

        [Column("div_modif_by")]
        public string div_modifBy { get; set; }
    }



}