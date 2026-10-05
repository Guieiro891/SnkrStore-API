import 'package:flutter/material.dart';
import 'models/tenis.dart';
import 'services/api_service.dart';
import 'screens/login_screen.dart';

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

  Future<void> _comprarTenis(Tenis tenis) async {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(child: CircularProgressIndicator()),
    );

    final sucesso = await _apiService.comprarTenis(tenis);

    if (mounted) Navigator.pop(context);

    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(sucesso
              ? 'Compra realizada! ${tenis.modelo} (Tam ${tenis.tamanho})'
              : 'Erro ao comprar. Tente novamente.'),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Snkr Store'),
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
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
                        onPressed: () => _comprarTenis(t),
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