using System;
using System.ComponentModel.DataAnnotations;

public class Pedido
{
    public int Id { get; set;  }

    [Range(1, int.MaxValue, ErrorMessage = "O ID do cliente deve ser maior que zero")]
    public int ClienteId { get; set; }
    public DateTime Data { get; set; }

    [Range(0, 999999.99, ErrorMessage = "O desconto deve estar entre R$ 0,00 e R$ 999.999,99")]
    public decimal Desconto { get; set; }

    [Range(0, 99999.99, ErrorMessage = "O frete deve estar entre R$ 0,00 e R$ 99.999,99")]
    public decimal Frete { get; set; }

    [Range(0, 999999.99, ErrorMessage = "O valor total deve estar entre R$ 0,00 e R$ 999.999,99")]
    public decimal ValorTotal { get; set; }
    public StatusPedido Status { get; set; }
    public StatusEntrega Entrega { get; set; }

    [StringLength(200, ErrorMessage = "O comprovante fiscal deve ter no máximo 200 caracteres")]
    public string ComprovanteFiscal { get; set; } = string.Empty;
    
    [StringLength(20,ErrorMessage = "O cupom deve ter no máximo 20 caracteres")]
    public string Cupom { get; set; } = string.Empty;

    public Pedido() { }
    public Pedido (int id, int clienteId, DateTime data, decimal desconto,decimal frete, decimal valorTotal, StatusPedido status, StatusEntrega entrega, string comprovanteFiscal, string cupom)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
        Desconto = desconto;
        Frete = frete;
        ValorTotal = valorTotal;
        Status = status;
        Entrega = entrega;
        ComprovanteFiscal = comprovanteFiscal;
        Cupom = cupom;
    }

}