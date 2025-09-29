using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eficientiza_s3.Models
{
    [Table("tb_mtt_moto_c3")]
    public class MotoEntity
    {
        [Key]
        [Column("id_moto")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo placa é obrigatório")]
        [StringLength(10, ErrorMessage = "Campo não pode ter mais que 10 caracteres")]
        [Column("ds_placa")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo modelo é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [Column("nm_modelo")]
        public string Modelo { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Campo não pode ter mais que 30 caracteres")]
        [Column("ds_cor")]
        public string? Cor { get; set; }

        [Required(ErrorMessage = "Campo ano é obrigatório")]
        [Column("nr_ano")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Campo status é obrigatório")]
        [StringLength(50, ErrorMessage = "Campo não pode ter mais que 50 caracteres")]
        [Column("ds_status")]
        public string Status { get; set; } = string.Empty;
    }
}
