public class ClientServices
{
    // Properties
    public List<Client> Clients { get; } = new List<Client>();

    // Methods
    public void ShowClientList()
    {
        ClientRepository rerpository = new ClientRepository();

        foreach (var client in rerpository.GetAllClients())
        {
            Console.WriteLine(client.ShowClient());
        }
    }

    public Client? SearchClient(string? emailSearch)
    {
        ClientRepository repository = new ClientRepository();
        Client? client = repository.GetClientByEmail(emailSearch);
        return client;
    }

    public void UpdateClientEmail(Client client, string? newEmail)
    {
        ClientRepository repository = new ClientRepository();
        repository.UpdateClientEmail(client.Email, newEmail);
    }

    public void DeleteClient(Client client)
    {
        ClientRepository repository = new ClientRepository();
        repository.DeleteClient(client);
    }
}
