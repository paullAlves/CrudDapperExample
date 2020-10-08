using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebMotors.Anuncio.Model.Entity
{
    [Table("tb_AnuncioWebmotors")]
    public class AnuncioWebMotors
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }

        [Column("marca")]
        [Required]
        [StringLength(45)]
        public string Marca { get; set; }

        [Column("modelo")]
        [Required]
        [StringLength(45)]
        public string Modelo { get; set; }

        [Column("versao")]
        [Required]
        [StringLength(45)]
        public string Versao { get; set; }

        [Column("ano")]
        [Required]
        public int Ano { get; set; }

        [Column("quilometragem")]
        [Required]
        public int Quilometragem { get; set; }

        [Column("observacao", TypeName = "text")]
        [Required]
        public string Observacao { get; set; }
    }
}
