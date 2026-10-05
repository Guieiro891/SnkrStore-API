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
              return Card(
                margin: const EdgeInsets.all(8),
                child: ListTile(
                  leading: const Icon(Icons.receipt_long, size: 40),
                  title: Text('Pedido #${p['id']}'),
                  subtitle: Text(
                    'Status: ${_nomeStatus(status)}\n'
                    'Data: ${p['data'].toString().substring(0, 10)}',
                  ),
                  isThreeLine: true,
                  trailing: Text(
                    'R\$ ${(p['valorTotal'] as num).toStringAsFixed(2)}',
                    style: const TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 16,
                    ),
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