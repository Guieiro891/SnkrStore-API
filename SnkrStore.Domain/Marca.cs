using System;
public class Marca
{
    public int Id { get; set;  }
    public string Nome { get; set; } = string.Empty;
    public bool Disponivel { get; set; }

    public Marca() { }
    public Marca(int id, string nome, bool disponivel)
    {
        Id = id;
        Nome = nome;
        Disponivel = disponivel;
    }
}