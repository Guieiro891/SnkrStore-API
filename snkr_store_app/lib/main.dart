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
                  subtitle: Text('Tam: ${t.tamanho} | ${t.cor} | Estoque: ${t.estoque}'),
                  trailing: Text(
                    'R\$ ${t.preco.toStringAsFixed(2)}',
                    style: const TextStyle(fontWeight: FontWeight.bold),
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