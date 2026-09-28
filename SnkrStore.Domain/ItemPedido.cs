using Microsoft.VisualBasic;
using System;

public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int TenisId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoPago { get; set; }

    public ItemPedido() { }
    public ItemPedido(int id, int pedidoId, int tenisId, int quantidade, int precoPago)
    {
        Id = id;
        PedidoId = pedidoId;
        TenisId = tenisId;
        Quantidade = quantidade;
        PrecoPago = precoPago;
    }
}