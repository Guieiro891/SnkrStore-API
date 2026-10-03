using System;
using System.ComponentModel.DataAnnotations;

public class Endereco
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O ID do cliente deve ser maior do que zero")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "A rua é obrigatória")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "A rua deve ter entre 3 e 150 caracteres")]
    public string Rua { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório")]
    [StringLength(10, ErrorMessage = "O número deve ter no máximo 10 caracteres")]
    public string Numero { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "A cidade deve ter entre 2 e 100 caracteres")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado é obrigatório")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter exatamente 2 caracteres (sigla)")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CEP é obrigatório")]
    [StringLength(9, MinimumLength = 8, ErrorMessage = "O CEP deve ter entre 8 e 9 caracteres")]
    public string CEP { get; set; } = string.Empty;

    public Endereco() { }
    public Endereco(int id, int clienteId, string rua, string numero, string cidade, string estado, string cep)
    {
        Id = id;
        ClienteId = clienteId;
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
    }
}