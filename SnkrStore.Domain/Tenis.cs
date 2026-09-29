using System;

public class Tenis
{
    public int Id { get; set;  }
    public string Modelo { get; set; } = string.Empty;
    public int Tamanho { get; set; }
    public string Cor { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
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