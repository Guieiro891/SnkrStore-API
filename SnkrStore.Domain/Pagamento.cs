using System;
using System.ComponentModel.DataAnnotations;

public class Pagamento
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O ID do pedido deve ser maior do que zero")]
    public int PedidoId { get; set; }
    public TipoPagamento Tipo { get; set; }
    public StatusPagamento StatusP { get; set; }

    [Range(0.01, 999999.99, ErrorMessage = "O valor do pagamento deve estar entre R$ 0,01 e R$ 999.999,99")]
    public decimal Valor { get; set; }

    public Pagamento() { }
    public Pagamento(int id, int pedidoId, TipoPagamento tipo, StatusPagamento statuP, decimal valor)
    {
        Id = id;
        PedidoId = pedidoId;
        Tipo = tipo;
        StatusP = statuP;
        Valor = valor;
    }
}