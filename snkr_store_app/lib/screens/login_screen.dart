import 'package:flutter/material.dart';
import '../services/api_service.dart';
import '../main.dart';
import '../sessao.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _emailController = TextEditingController();
  final _senhaController = TextEditingController();
  final _apiService = ApiService();
  bool _carregando = false;

  Future<void> _fazerLogin() async {
    setState(() => _carregando = true);

    final resultado = await _apiService.login(
      _emailController.text,
      _senhaController.text,
    );

    setState(() => _carregando = false);

    if (resultado != null) {
      Sessao.clienteLogado = resultado;
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (_) => const ListaTenisPage()),
      );
    } else {
      // Login falhou
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Email ou senha inválidos')),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Snkr Store - Login'),
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(Icons.shopping_bag, size: 80),
            const SizedBox(height: 32),
            TextField(
              controller: _emailController,
              decoration: const InputDecoration(
                labelText: 'Email',
                border: OutlineInputBorder(),
              ),
              keyboardType: TextInputType.emailAddress,
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _senhaController,
              decoration: const InputDecoration(
                labelText: 'Senha',
                border: OutlineInputBorder(),
              ),
              obscureText: true,
            ),
            const SizedBox(height: 24),
            _carregando
                ? const CircularProgressIndicator()
                : ElevatedButton(
                    onPressed: _fazerLogin,
                    style: ElevatedButton.styleFrom(
                      minimumSize: const Size.fromHeight(50),
                    ),
                    child: const Text('Entrar'),
                  ),
          ],
        ),
      ),
    );
  }
}