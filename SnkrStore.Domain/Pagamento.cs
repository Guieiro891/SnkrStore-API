using System;

public class Pagamento
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public TipoPagamento Tipo { get; set; }
    public StatusPagamento StatusP { get; set; }
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