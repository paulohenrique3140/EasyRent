using System.Text.RegularExpressions;

public class PersonalCustomer : Client
{
    // Properties
    public string? Name { get; set; }
    public string? Cnh { get; set; }

    private string? cpf = string.Empty;

    public string Cpf
    {
        get { return cpf; }
        set
        {
            // Validation
            if (!ValidateCpf(value))
                throw new ArgumentException("Invalid CPF format or verification digits.");

            cpf = value;
        }
    }

    public DateTime BirthDate { get; set; }

    // Constructor
    public PersonalCustomer(string? email, string? phone, string name, string cpf, string cnh, DateTime birthDate) : base(email, phone)
    {
        Name = name;
        Cpf = cpf;
        Cnh = cnh;
        BirthDate = birthDate;

        if (CalculateAge(birthDate) < 18)
            throw new ArgumentException(
                "The client must be at least 18 years old."
            );
    }

    public PersonalCustomer() : base()
    {}

    // Methods
    public static int CalculateAge(DateTime birthDate)
    {
        DateOnly birth = new DateOnly(
            birthDate.Year,
            birthDate.Month,
            birthDate.Day
        );

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        int age = today.Year - birth.Year;

        if (today < birth.AddYears(age))
            age--;

        return age;
    }

    private static bool IsCpfFormatValid(string cpf)
    {
        Regex regex = new Regex(
            @"^([0-9]{11}|[0-9]{3}\.[0-9]{3}\.[0-9]{3}-[0-9]{2})$"
        );

        return regex.IsMatch(cpf);
    }

    // A method that implements the mathematical rule for the existence and validation of a CPF uses a weighted sum algorithm with modulo 11 applied to its last two digits, known as the check digits.
    public static bool ValidateCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        if (!IsCpfFormatValid(cpf))
            return false;

        // Removes punctuation and keeps only numeric characters
        string cleanedCpf = new string(
            cpf.Where(char.IsDigit).ToArray()
        );

        // Rejects CPFs containing the same digit in all positions
        if (cleanedCpf.All(c => c == cleanedCpf[0]))
            return false;

        // Calculates the first verification digit
        int sum = 0;
        int multiplier = 10;

        for (int i = 0; i < 9; i++)
        {
            int digit = cleanedCpf[i] - '0';

            sum += digit * multiplier;
            multiplier--;
        }

        int remainder = sum % 11;

        int firstVerificationDigit =
            remainder < 2 ? 0 : 11 - remainder;

        if ((cleanedCpf[9] - '0') != firstVerificationDigit)
            return false;

        // Calculates the second verification digit
        sum = 0;
        multiplier = 11;

        for (int i = 0; i < 10; i++)
        {
            int digit = cleanedCpf[i] - '0';

            sum += digit * multiplier;
            multiplier--;
        }

        remainder = sum % 11;

        int secondVerificationDigit =
            remainder < 2 ? 0 : 11 - remainder;

        return (cleanedCpf[10] - '0') == secondVerificationDigit;
    }

    /*public List<Client> FindClientsByName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<Client>();

        return Clients.Where(client => client.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }*/

    

    public override string ShowClient()
    {
        return
               $"\nClient id: {Id}" +
               $"\nName: {Name}" +
               $"\nEmail: {Email}" +
               $"\nPhone: {Phone}" +
               $"\nCPF: {Cpf}" +
               $"\nCNH: {Cnh}" +
               $"\nBirth Date: {BirthDate:dd/MM/yyyy}" +
               $"\nAge: {CalculateAge(BirthDate)}";
    }
}