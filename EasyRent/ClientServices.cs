public class ClientServices
{
    // Properties
    ClientRepository clientRepository = new ClientRepository();

    // Methods
    public void CreateClient(Client? client)
    {
        clientRepository.AddClient(client);
    }
    public void ShowClientList()
    {
        foreach (var client in clientRepository.GetAllClients())
        {
            Console.WriteLine(client.ShowClient());
        }
    }

    public Client? SearchClient(string? emailSearch)
    {
        Client? client = clientRepository.GetClientByEmail(emailSearch);
        return client;
    }

    public void UpdateClientEmail(Client client, string? newEmail)
    {
        clientRepository.UpdateClientEmail(client.Email, newEmail);
    }

    public void DeleteClient(Client client)
    {
        clientRepository.DeleteClient(client);
    }
}
