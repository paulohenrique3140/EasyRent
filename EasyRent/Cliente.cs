class Cliente
{
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Cnh { get; set; }
    private int idade;
    public int Idade
    {
        get { return idade; }
        set
        {
            if (value < 18)
            {
                throw new ArgumentException("O cliente precisa ter no mínimo 18 anos.");
            }
            else
            {
                idade = value;
            }
        }
    }

    public Cliente(string nome, string cpf, string cnh, int idade)
    {
        Nome = nome;
        Cpf = cpf;
        Cnh = cnh;
        Idade = idade;
    }
    public Cliente(){}

    public static int CalculaIdade(DateTime dataNascimento)
    {
        DateOnly nascimento = new DateOnly(dataNascimento.Year, dataNascimento.Month, dataNascimento.Day);
        DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
        int idade = hoje.Year - nascimento.Year;
        if (hoje < nascimento.AddYears(idade))
        {
            idade--;
        }
        return idade;
    }

    public string ExibirCliente() => $"\nNome do cliente: {Nome}\nCPF: {Cpf}\nCNH: {Cnh}\nIdade: {idade}";
    
}
