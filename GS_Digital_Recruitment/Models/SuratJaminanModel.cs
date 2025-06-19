using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS_Digital_Recruitment.Models
{
    [Table("t_pengajuansuratjaminan")]
    public class TransaksiSuratJaminan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int psj_id { get; set; }

        // Data Karyawan
        [MaxLength(20)]
        public string psj_npk { get; set; }

        // Data Pasien
        [MaxLength(100)]
        public string psj_nama_pasien { get; set; }

        // Data Jaminan
        [Required]
        public DateTime psj_tanggal_pengajuan { get; set; }

        [MaxLength(50)]
        public string psj_jenis_surat_jaminan { get; set; }

        public DateTime? psj_tanggal_berobat { get; set; }

        [MaxLength(255)]
        public string psj_keterangan { get; set; }

        // Status
        [MaxLength(20)]
        public string psj_status { get; set; }

        // Audit Trail
        [MaxLength(100)]
        public string psj_created_by { get; set; }

        public DateTime? psj_created_date { get; set; }

        [MaxLength(100)]
        public string psj_modif_by { get; set; }

        public DateTime? psj_modif_date { get; set; }
    }
}
