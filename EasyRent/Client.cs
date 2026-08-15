using System.Text.RegularExpressions;

class Client
{
    // Client properties
    public string Name { get; set; }
    public string Cnh { get; set; }

    // Private backing field for CPF
    private string cpf = string.Empty;

    // CPF property with validation
    public string Cpf
    {
        get { return cpf; }
        set
        {
            if (!ValidateCpf(value))
                throw new ArgumentException("Invalid CPF format or verification digits.");

            cpf = value;
        }
    }

    // Client birth date
    public DateTime BirthDate { get; set; }

    // Age is calculated dynamically from the birth date
    public int Age => CalculateAge(BirthDate);

    // Constructor used to create a complete Client object
    public Client(string name, string cpf, string cnh, DateTime birthDate)
    {
        Name = name;
        Cpf = cpf;
        Cnh = cnh;
        BirthDate = birthDate;

        if (Age < 18)
            throw new ArgumentException(
                "The client must be at least 18 years old."
            );
    }

    // Empty constructor
    public Client()
    {
        Name = string.Empty;
        Cnh = string.Empty;
    }

    // Validates the CPF format and verification digits
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

    // Validates whether the CPF follows one of the accepted formats
    private static bool IsCpfFormatValid(string cpf)
    {
        Regex regex = new Regex(
            @"^([0-9]{11}|[0-9]{3}\.[0-9]{3}\.[0-9]{3}-[0-9]{2})$"
        );

        return regex.IsMatch(cpf);
    }

    // Calculates the client's age based on the birth date
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

    // Returns the client's information
    public string ShowClient()
    {
        return $"\nName: {Name}" +
               $"\nCPF: {Cpf}" +
               $"\nCNH: {Cnh}" +
               $"\nBirth Date: {BirthDate:dd/MM/yyyy}" +
               $"\nAge: {Age}";
    }
}