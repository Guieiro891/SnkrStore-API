using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Cliente
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número de telefone é obrigatório")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "O número deve ter entre 8 e 20 caracteres")]
    public string Numero { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é valido")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve ter entre 11 e 14 caracteres")]
    public string CPF { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caractere")]
    public string SenhaHash { get; set; } = string.Empty;

    public Cliente() { }
    public Cliente (int id, string nome, string numero, string email, string cpf, string senhaHash)
    {
        Id = id;
        Nome = nome;
        Numero = numero;
        Email = email;
        CPF = cpf;
        SenhaHash = senhaHash;
    }
}