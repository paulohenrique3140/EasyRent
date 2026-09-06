public class ClientServices
{
    // Properties
    public List<Client> Clients { get; } = new List<Client>();

    // Methods
    public void ShowClientList()
    {
        foreach (var client in Clients)
        {
            Console.WriteLine(client.ShowClient());
        }

    }

    /*public List<Client> FindClientsByEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return new List<Client>();

        return Clients.Where(client => client.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
    }*/

    public Client? SearchClient()
    {
        while (true)
        {
            ClientRepository repository = new ClientRepository();
            Console.Write("\nEnter the client email [type r to return]: ");
            string? emailSearch = Console.ReadLine();

            if (emailSearch?.ToLower() == "r")
                return null;

            if (repository.GetClientByEmail(emailSearch) == null)
            {
                Console.WriteLine("\nThere's no client with this email.");
            }
            else
            {
                Console.WriteLine(repository.GetClientByEmail(emailSearch).ShowClient());
            }
        }
    }
}
