class Client
{
    // Client properties
    public string? Name { get; set; }
    public string? Cpf { get; set; }
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

    public string ShowClient() => $"\nName: {Name}\nCPF: {Cpf}\nCNH: {Cnh}\nAge: {age}"; // method to return a client object
    
}
