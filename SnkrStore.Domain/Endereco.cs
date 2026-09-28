using System;

public class Endereco
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Rua { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;

    public Endereco() { }
    public Endereco(int id, int clienteId, string rua, string numero, string cidade, string estado, string cep)
    {
        Id = id;
        ClienteId = clienteId;
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
    }
}