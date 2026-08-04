class Client
{
    // Client properties
    public string? Name { get; set; }

    // Private backing field for CPF
    private string? cpf;

    // Property with validation and exception handling
    public string? Cpf
    {
        get { return cpf; }
        set
        {
            if (!ValidateCpf(value))
            {
                throw new ArgumentException("Invalid CPF format or digits.");
            }
            cpf = value;
        }
    }
    public string? Cnh { get; set; }
    private int age;
    public int Age // legal age validation
    {
        get { return age; }
        set
        {
            if (value < 18)
            {
                throw new ArgumentException("The client must be of legal driving age in the current country.");
            }
            else
            {
                age = value;
            }
        }
    }

    public Client(string name, string cpf, string cnh, int age) // client constructor
    {
        Name = name;
        Cpf = cpf;
        Cnh = cnh;
        Age = age;
    }
    public Client(){} // Constructor to create an empty object

    public static bool ValidateCpf(string? cpf) // Método estático responsável por calcular e validar os dígitos do CPF
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        // 1. Remove qualquer caractere que não seja número (pontos e hífen)
        string cleanedCpf = new string(cpf.Where(char.IsDigit).ToArray());

        // 2. Deve possuir exatamente 11 dígitos
        if (cleanedCpf.Length != 11)
            return false;

        // 3. Rejeita CPF com todos os números iguais (ex: 111.111.111-11)
        if (cleanedCpf.All(c => c == cleanedCpf[0]))
            return false;

        // 4. Validação do 1º Dígito Verificador
        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (cleanedCpf[i] - '0') * multiplier1[i];
        }

        int remainder = sum % 11;
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        if ((cleanedCpf[9] - '0') != digit1)
            return false;

        // 5. Validação do 2º Dígito Verificador
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        sum = 0;

        for (int i = 0; i < 10; i++)
        {
            sum += (cleanedCpf[i] - '0') * multiplier2[i];
        }

        remainder = sum % 11;
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        return (cleanedCpf[10] - '0') == digit2;
    }

    public static int CalculateAge(DateTime birthDate) // method to calculate age by birth date
    {
        DateOnly birth = new DateOnly(birthDate.Year, birthDate.Month, birthDate.Day);
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        int age = today.Year - birth.Year;
        if (today < birth.AddYears(age))
        {
            age--;
        }
        return age;
    }

    public string ShowClient() => $"\nClient name: {Name}\nCPF: {Cpf}\nCNH: {Cnh}\nAge: {age}"; // method to return a client object
    
}
