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

    public List<Client> FindClientsByEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return new List<Client>();

        return Clients.Where(client => client.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Client? SearchClient()
    {
        while (true)
        {
            Console.Write("\nEnter the client email [type r to return]: ");
            string? emailSearch = Console.ReadLine();

            if (emailSearch?.ToLower() == "r")
                return null;

            List<Client> clientsFound = FindClientsByEmail(emailSearch);

            if (clientsFound.Count == 0)
            {
                Console.WriteLine("\nNo clients were found. Please try again.");
                continue;
            }

            if (clientsFound.Count == 1)
            {
                return clientsFound[0];
            }

            Console.WriteLine("\nClients found:");

            for (int i = 0; i < clientsFound.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {clientsFound[i].Email} - ID: {clientsFound[i].Id}");
            }

            Console.WriteLine("\nSelect a client: ");

            if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= clientsFound.Count)
            {
                return clientsFound[option - 1];
            }

            Console.WriteLine("\nInvalid option. Please try again.");
        }
    }
}
