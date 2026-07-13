using System.Diagnostics;

class Veiculo
{
    public string? Modelo { get; set; }
    public string? Placa { get; set; }
    public Carroceria Carroceria { get; set; }
    public double ValorDiaria { get; set; }
    public int KmAtual { get; set; }

    public Veiculo(string? modelo, string? placa, Carroceria carroceria, double valorDiaria, int kmAtual)
    {
        Modelo = modelo;
        Placa = placa;
        Carroceria = carroceria;
        ValorDiaria = valorDiaria;
        KmAtual = kmAtual;
    }

    public Veiculo(){}

    public void AtualizarQuilometragem(int kmAtual)
    {
        KmAtual = kmAtual;
    }

    public string ExibirVeiculo()
    {
        return $"\nModelo: {Modelo}\nPlaca: {Placa}\nCarroceria: {Carroceria}\nValor da diária: R$ {ValorDiaria:F2}\nQuilometragem atual: {KmAtual} km";
    }
}