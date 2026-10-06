import 'package:flutter/material.dart';
import '../services/api_service.dart';

class MeusPedidosScreen extends StatefulWidget {
  const MeusPedidosScreen({super.key});

  @override
  State<MeusPedidosScreen> createState() => _MeusPedidosScreenState();
}

class _MeusPedidosScreenState extends State<MeusPedidosScreen> {
  final ApiService _apiService = ApiService();
  late Future<List<Map<String, dynamic>>> _futuroPedidos;

  @override
  void initState() {
    super.initState();
    _futuroPedidos = _apiService.buscarMeusPedidos();
  }

  String _nomeStatus(int status) {
    switch (status) {
      case 0:
        return 'Pendente';
      case 1:
        return 'Pago';
      case 2:
        return 'Enviado';
      case 3:
        return 'Entregue';
      case 4:
        return 'Cancelado';
      default:
        return 'Desconhecido';
    }
  }

  Future<void> _cancelarPedido(int pedidoId) async {
    final confirmar = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Cancelar pedido'),
        content: Text('Deseja cancelar o pedido #$pedidoId? O estoque será devolvido.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text('Não'),
          ),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            child: const Text('Sim, cancelar'),
          ),
        ],
      ),
    );

    if (confirmar != true) return;

    final sucesso = await _apiService.cancelarPedido(pedidoId);

    if (!mounted) return;

    if (sucesso) {
      setState(() {
        _futuroPedidos = _apiService.buscarMeusPedidos();
      });
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Pedido cancelado com sucesso!')),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Não foi possível cancelar o pedido.')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Meus Pedidos'),
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
      ),
      body: FutureBuilder<List<Map<String, dynamic>>>(
        future: _futuroPedidos,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }
          if (snapshot.hasError) {
            return Center(child: Text('Erro: ${snapshot.error}'));
          }
          final pedidos = snapshot.data ?? [];
          if (pedidos.isEmpty) {
            return const Center(child: Text('Nenhum pedido encontrado'));
          }
          return ListView.builder(
            itemCount: pedidos.length,
            itemBuilder: (context, index) {
              final p = pedidos[index];
              final status = p['status'] as int;
              final podeCancelar = status != 3 && status != 4;

              return Card(
                margin: const EdgeInsets.all(8),
                child: Padding(
                  padding: const EdgeInsets.all(12),
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      const Icon(Icons.receipt_long, size: 40),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              'Pedido #${p['id']}',
                              style: const TextStyle(
                                fontWeight: FontWeight.bold,
                                fontSize: 16,
                              ),
                            ),
                            const SizedBox(height: 4),
                            Text('Status: ${_nomeStatus(status)}'),
                            Text('Data: ${p['data'].toString().substring(0, 10)}'),
                          ],
                        ),
                      ),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Text(
                            'R\$ ${(p['valorTotal'] as num).toStringAsFixed(2)}',
                            style: const TextStyle(
                              fontWeight: FontWeight.bold,
                              fontSize: 15,
                            ),
                          ),
                          if (podeCancelar)
                            TextButton(
                              onPressed: () => _cancelarPedido(p['id']),
                              style: TextButton.styleFrom(
                                padding: const EdgeInsets.symmetric(
                                    horizontal: 4, vertical: 0),
                                minimumSize: const Size(0, 28),
                                tapTargetSize:
                                    MaterialTapTargetSize.shrinkWrap,
                              ),
                              child: const Text(
                                'Cancelar',
                                style: TextStyle(fontSize: 12),
                              ),
                            ),
                        ],
                      ),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}