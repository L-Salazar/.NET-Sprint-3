using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eficientiza_s3.Models
{
    [Table("tb_mtt_estacao_c3")]
    public class EstacaoEntity
    {
        [Key]
        [Column("id_estacao")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo estação é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [Column("nm_estacao")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Campo não pode ter mais que 200 caracteres")]
        [Column("ds_localizacao")]
        public string? Localizacao { get; set; }

        [Column("nr_capacidade")]
        public int? Capacidade { get; set; }

        [Required(ErrorMessage = "Campo status é obrigatório")]
        [StringLength(30, ErrorMessage = "Campo não pode ter mais que 30 caracteres")]
        [Column("ds_status")]
        public string Status { get; set; } = string.Empty;

        [Column("dt_criacao")]
        public DateTime? DataCriacao { get; set; }

        [Column("dt_ultima_atualizacao")]
        public DateTime? DataUltimaAtualizacao { get; set; }

        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [Column("ds_responsavel")]
        public string? Responsavel { get; set; }
    }
}
