using System.Net.Http.Headers;
using System.Text;

class Locacao
{    
    public Cliente? Cliente { get;  }
    public Veiculo? Veiculo { get;  }
    public int QuantidadeDias { get; private set; }
    public bool PossuiSeguro { get; private set; }
    public int KmInicial { get; private set; }
    public StatusLocacao Status { get; private set; }

    public Locacao(Cliente cliente, Veiculo veiculo, int quantidadeDias, bool possuiSeguro, int kmInicial, StatusLocacao status) 
    {
        Cliente = cliente;
        Veiculo = veiculo;
        QuantidadeDias = quantidadeDias;
        PossuiSeguro = possuiSeguro;
        KmInicial = kmInicial;
        Status = status;
    }

    public Locacao(){}

    public double CalculaValorBase(double diaria)
    {
        double total = QuantidadeDias * diaria;
        return total;
    }

    public double CalculaSeguro()
    {
        return PossuiSeguro ? QuantidadeDias * 50.00 : 0;
    } 

    public double CalculaMultaKm(int kmAtual)
    {
        double multa = 0;
        int totalKmRodado = kmAtual - KmInicial;
        if(totalKmRodado / QuantidadeDias > 100)
        {
            multa = (totalKmRodado - 100 * QuantidadeDias) * 1.2;
        }
        return multa;
    }

    public double CalculaTotal(double diaria, int kmAtual)
    {
        return CalculaValorBase(diaria) + CalculaSeguro() + CalculaMultaKm(kmAtual);
    }

    public void FinalizarLocacao(int kmFinal)
    {
        Veiculo.AtualizarQuilometragem(kmFinal);
        Status = StatusLocacao.Finalizada;
    }

    public void CancelarLocacao()
    {
        Status = StatusLocacao.Cancelada;
    }

    public string ExibirResumo(int kmAtual)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"\n### RESUMO DA LOCAÇÃO ###\n" +
                          $"\nNome do cliente: {Cliente.Nome}" +
                          $"\nVeículo: {Veiculo.Modelo}" +
                          $"\nDiárias: {QuantidadeDias}" +
                          $"\nValor da diária: R$ {Veiculo.ValorDiaria:F2}" +
                          $"\nStatus da locação: {Status}" +
                          $"\nKM inicial do veículo: {KmInicial}");
        if(Status == StatusLocacao.Cancelada)
        {
            sb.Clear();
            sb.Append("\nSua reserva está cancelada!");
        } else if(Status == StatusLocacao.Finalizada)
        {
            sb.Append($"\nKM final do veículo: {Veiculo.KmAtual}");
            sb.Append($"\nValor base: R$ {CalculaValorBase(Veiculo.ValorDiaria):F2}");
            if (CalculaMultaKm(kmAtual) > 0)
            {
                sb.Append($"\nTotal Km excedido [limite 100 km por dia]: {((Veiculo.KmAtual - KmInicial) - (100 * QuantidadeDias))} km" +
                          $"\nTotal multa [R$ 1,20 por km excedido]: R$ {CalculaMultaKm(kmAtual):F2}");
                
            }
            if (PossuiSeguro)
            {
                sb.Append($"\nValor seguro: R$ {CalculaSeguro():F2}");
            }
            sb.Append($"\n\n### TOTAL GERAL: R$ {CalculaTotal(Veiculo.ValorDiaria, Veiculo.KmAtual):F2} ###");
        }
        return sb.ToString();
    }
}