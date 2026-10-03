using System;
using System.ComponentModel.DataAnnotations;

public class Tenis
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O modelo é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O modelo deve ter entre 3 e 100 caracteres")]
    public string Modelo { get; set; } = string.Empty;

    [Range(1, 50, ErrorMessage = "O tamanho deve estar entre 1 e 50")]
    public int Tamanho { get; set; }

    [Required(ErrorMessage = "A cor é obrigatória")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "A cor deve ter entre 2 e 50 caracteres")]
    public string Cor { get; set; } = string.Empty;

    [Range(0.01, 99999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 99.999,99")]
    public decimal Preco { get; set; }

    [Range(0, 9999, ErrorMessage = "O estoque deve estar entre 0 e 9999 unidades")]
    public int Estoque { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O ID da marca deve ser maior que zero")]
    public int MarcaId { get; set; }

    public Tenis() { }
    public Tenis(int id, string modelo, int tamanho, string cor, decimal preco, int estoque, int marcaId)
    {
        Id = id;
        Modelo = modelo;
        Tamanho = tamanho;
        Cor = cor;
        Preco = preco;
        Estoque = estoque;
        MarcaId = marcaId;
    }
}