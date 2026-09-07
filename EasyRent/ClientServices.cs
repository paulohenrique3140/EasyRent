public class ClientServices
{
    // Properties
    public List<Client> Clients { get; } = new List<Client>();

    // Methods
    public void ShowClientList()
    {
        ClientRepository rerpository = new ClientRepository();

        foreach(var client in rerpository.GetAllClients())
        {
            Console.WriteLine(client.ShowClient());
        }
    }

    public Client? SearchClient()
    {
        while (true)
        {
            ClientRepository repository = new ClientRepository();
            Console.Write("\nEnter the client email [type r to return]: ");
            string? emailSearch = Console.ReadLine();

            if (emailSearch?.ToLower() == "r")
                return null;

            Client? client = repository.GetClientByEmail(emailSearch);

            if (client == null)
            {
                Console.WriteLine("\nThere's no client with this email.");
                continue;
            }
            
            return client;
        }
    }
}
