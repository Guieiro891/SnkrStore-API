using System;

public class Tenis
{
    public int Id { get; set;  }
    public string Modelo { get; set; } = string.Empty;
    public int Tamanho { get; set; }
    public string Cor { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int MarcaId { get; set; }

    public Tenis() { }
    public Tenis(int id, string modelo, int tamanho, string cor, decimal preco, int marcaId)
    {
        Id = id;
        Modelo = modelo;
        Tamanho = tamanho;
        Cor = cor;
        Preco = preco;
        MarcaId = marcaId;
    }
}