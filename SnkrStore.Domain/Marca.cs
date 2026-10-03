using System;
using System.ComponentModel.DataAnnotations;

public class Marca
{
    
    public int Id { get; set;  }

    [Required(ErrorMessage = "O nome da marca é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome da marca deve ter entre 2 e 50 caracteres")]
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