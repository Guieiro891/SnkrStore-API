class Sessao {
  static Map<String, dynamic>? clienteLogado;

  static int? get clienteId => clienteLogado?['id'];
  static String? get clienteNome => clienteLogado?['nome'];

  static void limpar() {
    clienteLogado = null;
  }
}