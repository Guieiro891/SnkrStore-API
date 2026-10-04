class Tenis {
  final int id;
  final String modelo;
  final int tamanho;
  final String cor;
  final double preco;
  final int estoque;
  final int marcaId;

  Tenis({
    required this.id,
    required this.modelo,
    required this.tamanho,
    required this.cor,
    required this.preco,
    required this.estoque,
    required this.marcaId,
  });

  factory Tenis.fromJson(Map<String, dynamic> json) {
    return Tenis(
      id: json['id'],
      modelo: json['modelo'],
      tamanho: json['tamanho'],
      cor: json['cor'],
      preco: (json['preco'] as num).toDouble(),
      estoque: json['estoque'],
      marcaId: json['marcaId'],
    );
  }
}