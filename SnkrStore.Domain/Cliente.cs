using System;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
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