import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/tenis.dart';
import '../sessao.dart';

class ApiService {
  static const String baseUrl = 'http://localhost:5000/api';

  Future<List<Tenis>> buscarTenis() async {
    final response = await http.get(Uri.parse('$baseUrl/Tenis'));

    if (response.statusCode == 200) {
      final List<dynamic> listaJson = jsonDecode(response.body);
      return listaJson.map((json) => Tenis.fromJson(json)).toList();
    } else {
      throw Exception('Falha ao carregar tênis: ${response.statusCode}');
    }
  }

    Future<Map<String, dynamic>?> login(String email, String senha) async {
    final response = await http.post(
      Uri.parse('$baseUrl/Cliente/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'senha': senha}),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      return null;
    }
  }
    Future<bool> comprarTenis(Tenis tenis) async {
    // 1. Cria o pedido
    final pedidoResponse = await http.post(
      Uri.parse('$baseUrl/Pedido'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'clienteId': Sessao.clienteId,
        'data': DateTime.now().toIso8601String(),
        'desconto': 0,
        'frete': 29.90,
        'valorTotal': 0,
        'status': 0,
        'entrega': 0,
        'comprovanteFiscal': '',
        'cupom': '',
      }),
    );

    if (pedidoResponse.statusCode != 201) return false;
    final pedido = jsonDecode(pedidoResponse.body);
    final pedidoId = pedido['id'];

    // 2. Cria o item do pedido
    final itemResponse = await http.post(
      Uri.parse('$baseUrl/ItemPedido'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'pedidoId': pedidoId,
        'tenisId': tenis.id,
        'quantidade': 1,
        'precoPago': tenis.preco,
      }),
    );

    if (itemResponse.statusCode != 201) return false;

    // 3. Finaliza o pedido (calcula total e muda status para Pago)
    final finalizarResponse = await http.post(
      Uri.parse('$baseUrl/Pedido/$pedidoId/finalizar'),
    );

    return finalizarResponse.statusCode == 200;
  }
}