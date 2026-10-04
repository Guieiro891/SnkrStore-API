import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/tenis.dart';

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
}