using System;

public class Pedido
{
    public int Id { get; set;  }
    public int ClienteId { get; set; }
    public DateTime Data { get; set; }
    public decimal Desconto { get; set; }
    public decimal Frete { get; set; }
    public decimal ValorTotal { get; set; }
    public StatusPedido Status { get; set; }
    public StatusEntrega Entrega { get; set; }
    public string ComprovanteFiscal { get; set; } = string.Empty;

    public Pedido() { }
    public Pedido (int id, int clienteId, DateTime data, decimal desconto,decimal frete, decimal valorTotal, StatusPedido status, StatusEntrega entrega)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
        Desconto = desconto;
        Frete = frete;
        ValorTotal = valorTotal;
        Status = status;
        Entrega = entrega;
        ComprovanteFiscal = ComprovanteFiscal;
    }

}