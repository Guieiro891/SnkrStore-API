import 'package:flutter/material.dart';
import '../services/api_service.dart';

class CadastroScreen extends StatefulWidget {
  const CadastroScreen({super.key});

  @override
  State<CadastroScreen> createState() => _CadastroScreenState();
}

class _CadastroScreenState extends State<CadastroScreen> {
  final _nomeController = TextEditingController();
  final _numeroController = TextEditingController();
  final _emailController = TextEditingController();
  final _cpfController = TextEditingController();
  final _senhaController = TextEditingController();
  final _apiService = ApiService();
  bool _carregando = false;

  Future<void> _cadastrar() async {
    setState(() => _carregando = true);

    final sucesso = await _apiService.cadastrarCliente(
      nome: _nomeController.text,
      numero: _numeroController.text,
      email: _emailController.text,
      cpf: _cpfController.text,
      senha: _senhaController.text,
    );

    setState(() => _carregando = false);

    if (!mounted) return;

    if (sucesso) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Cadastro realizado com sucesso! Faça login.')),
      );
      Navigator.pop(context);
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Erro ao cadastrar. Verifique os dados.')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Cadastro'),
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
      ),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: ListView(
          children: [
            TextField(
              controller: _nomeController,
              decoration: const InputDecoration(
                labelText: 'Nome completo',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _numeroController,
              decoration: const InputDecoration(
                labelText: 'Telefone',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
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
              controller: _cpfController,
              decoration: const InputDecoration(
                labelText: 'CPF',
                border: OutlineInputBorder(),
              ),
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
                    onPressed: _cadastrar,
                    style: ElevatedButton.styleFrom(
                      minimumSize: const Size.fromHeight(50),
                    ),
                    child: const Text('Cadastrar'),
                  ),
          ],
        ),
      ),
    );
  }
}