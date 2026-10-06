import 'package:flutter/material.dart';
import 'models/tenis.dart';
import 'services/api_service.dart';
import 'sessao.dart';
import 'screens/login_screen.dart';
import 'screens/meus_pedidos_screen.dart';

void main() {
  runApp(const SnkrStoreApp());
}

class SnkrStoreApp extends StatelessWidget {
  const SnkrStoreApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Snkr Store',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
      ),
      home: const LoginScreen(),
    );
  }
}

class ListaTenisPage extends StatefulWidget {
  const ListaTenisPage({super.key});

  @override
  State<ListaTenisPage> createState() => _ListaTenisPageState();
}

class _ListaTenisPageState extends State<ListaTenisPage> {
  final ApiService _apiService = ApiService();
  late Future<List<Tenis>> _futuroTenis;

  @override
  void initState() {
    super.initState();
    _futuroTenis = _apiService.buscarTenis();
  }

  Future<void> _escolherPagamento(Tenis tenis) async {
    int tipoSelecionado = 0;

    final confirmar = await showDialog<bool>(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setStateDialog) {
            return AlertDialog(
              title: Text('Comprar ${tenis.modelo} (Tam ${tenis.tamanho})'),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  const Text('Escolha a forma de pagamento:'),
                  const SizedBox(height: 16),
                  DropdownButtonFormField<int>(
                    value: tipoSelecionado,
                    items: const [
                      DropdownMenuItem(value: 0, child: Text('Pix')),
                      DropdownMenuItem(value: 1, child: Text('Cartão')),
                      DropdownMenuItem(value: 2, child: Text('Boleto')),
                    ],
                    onChanged: (valor) {
                      if (valor != null) {
                        setStateDialog(() => tipoSelecionado = valor);
                      }
                    },
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context, false),
                  child: const Text('Cancelar'),
                ),
                ElevatedButton(
                  onPressed: () => Navigator.pop(context, true),
                  child: const Text('Confirmar'),
                ),
              ],
            );
          },
        );
      },
    );

    if (confirmar == true) {
      await _comprarTenis(tenis, tipoSelecionado);
    }
  }

  Future<void> _comprarTenis(Tenis tenis, int tipoPagamento) async {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(child: CircularProgressIndicator()),
    );

    final sucesso = await _apiService.comprarTenis(tenis, tipoPagamento);

    if (mounted) Navigator.pop(context);

    if (!mounted) return;

    if (sucesso) {
      setState(() {
        _futuroTenis = _apiService.buscarTenis();
      });

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Compra realizada! ${tenis.modelo} (Tam ${tenis.tamanho})'),
        ),
      );

      Navigator.push(
        context,
        MaterialPageRoute(builder: (_) => const MeusPedidosScreen()),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Erro ao comprar. Tente novamente.')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Olá, ${Sessao.clienteNome ?? "Cliente"}'),
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        actions: [
          IconButton(
            icon: const Icon(Icons.receipt_long),
            tooltip: 'Meus Pedidos',
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => const MeusPedidosScreen()),
              );
            },
          ),
          IconButton(
            icon: const Icon(Icons.logout),
            tooltip: 'Sair',
            onPressed: () {
              Sessao.limpar();
              Navigator.pushReplacement(
                context,
                MaterialPageRoute(builder: (_) => const LoginScreen()),
              );
            },
          ),
        ],
      ),
      body: FutureBuilder<List<Tenis>>(
        future: _futuroTenis,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }
          if (snapshot.hasError) {
            return Center(child: Text('Erro: ${snapshot.error}'));
          }
          final tenis = snapshot.data ?? [];
          if (tenis.isEmpty) {
            return const Center(child: Text('Nenhum tênis encontrado'));
          }
          return ListView.builder(
            itemCount: tenis.length,
            itemBuilder: (context, index) {
              final t = tenis[index];
              return Card(
                margin: const EdgeInsets.all(8),
                child: ListTile(
                  title: Text(t.modelo),
                  subtitle: Text(
                      'Tam: ${t.tamanho} | ${t.cor} | Estoque: ${t.estoque}'),
                  trailing: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Text(
                        'R\$ ${t.preco.toStringAsFixed(2)}',
                        style: const TextStyle(fontWeight: FontWeight.bold),
                      ),
                      const SizedBox(height: 4),
                      ElevatedButton(
                        onPressed: () => _escolherPagamento(t),
                        style: ElevatedButton.styleFrom(
                          padding: const EdgeInsets.symmetric(
                              horizontal: 12, vertical: 4),
                          minimumSize: const Size(0, 32),
                          tapTargetSize: MaterialTapTargetSize.shrinkWrap,
                        ),
                        child: const Text('Comprar',
                            style: TextStyle(fontSize: 12)),
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