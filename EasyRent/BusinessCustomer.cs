
public class BusinessCustomer : Client
{
    // Properties
    public string? CompanyName { get; set; }
    public string? Cnpj { get; set; }
    public DateTime OpeningDate { get; set; }

    public BusinessCustomer(string? email, string? phone, string? companyName, string? cnpj, DateTime openingDate) : base(email, phone)
    {
        CompanyName = companyName;
        Cnpj = cnpj;
        OpeningDate = openingDate;
    }
    public BusinessCustomer() : base()
    {}
    public override string ShowClient()
    {
        return
               $"\nClient id: {Id}" +
               $"\nCompany name: {CompanyName}" +
               $"\nEmail: {Email}" +
               $"\nPhone: {Phone}" +
               $"\nCnpj: {Cnpj}" +
               $"\nOpening date: {OpeningDate}";
    }
}
