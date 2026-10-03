using System;
using System.ComponentModel.DataAnnotations;

public class ItemPedido
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O ID do pedido deve ser maior que zero")]
    public int PedidoId { get; set; }

    [Range(1, int.MaxValue,ErrorMessage = "O ID do tênis deve ser maior que zero")]
    public int TenisId { get; set; }

    [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000 unidades")]
    public int Quantidade { get; set; }

    [Range(0.01, 99999.99, ErrorMessage = "O preço pago deve estar entre R$ 0,01 e R$ 99.999,99")] 
    public decimal PrecoPago { get; set; }

    public ItemPedido() { }
    public ItemPedido(int id, int pedidoId, int tenisId, int quantidade, decimal precoPago)
    {
        Id = id;
        PedidoId = pedidoId;
        TenisId = tenisId;
        Quantidade = quantidade;
        PrecoPago = precoPago;
    }
}