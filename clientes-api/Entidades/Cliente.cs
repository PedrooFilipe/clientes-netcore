using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clientes_api.Entidades
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string TipoPessoa { get; set; }
        public string CodigoSap { get; set; }
        [Required(ErrorMessage ="Nome é obrigatório")]
        public string Nome { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Cmc { get; set; }
        public string EmailNfe { get; set; }
        public string NomeRepresentante { get; set; }
        
    }
}
