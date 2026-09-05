using System.Text.RegularExpressions;

public abstract class Client
{
    // Properties
    public int Id { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }

    // Constructors
    protected Client(string? email, string? phone)
    {
        // method to return id from database
        Email = email;
        Phone = phone;
    }
    protected Client()
    {

    }

    // Methods
    public abstract string ShowClient();
}