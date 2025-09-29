using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eficientiza_s3.Models
{
    [Table("tb_mtt_usuario_c3")]
    public class UsuarioEntity
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [Column("nm_usuario")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo email é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [Column("ds_email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo não pode ter mais que 100 caracteres")]
        [Column("ds_senha")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo tipo de usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Campo não pode ter mais que 50 caracteres")]
        [Column("tp_usuario")]
        public string TipoUsuario { get; set; } = string.Empty;
    }
}
