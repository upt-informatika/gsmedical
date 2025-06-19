using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS_Digital_Recruitment.Models
{
    [Table("tlkp_pengguna_rekrut")]
    public class MasterUserRecruiter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 prk_id { get; set; }
        public string prk_username { get; set; }
        public string prk_password { get; set; }
        public string prk_email { get; set; }
        public int? prk_id_divisi { get; set; }
        public bool? prk_status { get; set; }
        public string prk_created_by { get; set; }
        public DateTime? prk_created_date { get; set; }
        public string prk_modif_by { get; set; }
        public DateTime? prk_modif_date { get; set; }
    }

}