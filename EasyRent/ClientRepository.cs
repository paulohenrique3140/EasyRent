using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class ClientRepository
{
    private readonly string connectionString = "Server=PAULO;DataBase=EASY_RENT;Integrated Security=True;TrustServerCertificate=True;";

    public void AddClient(Client client)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlClient = @"
            INSERT INTO CLIENT (Email, Phone)
            OUTPUT INSERTED.Id
            VALUES (@Email, @Phone);";

        using SqlCommand commandClient = new SqlCommand(sqlClient, connection);

        commandClient.Parameters.AddWithValue("@Email", client.Email);
        commandClient.Parameters.AddWithValue("@Phone", (object?)client.Phone ?? DBNull.Value);

        int clientId = Convert.ToInt32(commandClient.ExecuteScalar());
        client.Id = clientId;

        if (client is PersonalCustomer personalCustomer)
        {
            string sqlPersonalCustomer = @"
                INSERT INTO PERSONAL_CUSTOMER
                    (ClientId, Name, Cpf, Cnh, Birth_Date)
                VALUES
                    (@ClientId, @Name, @Cpf, @Cnh, @Birth_Date);";

            using SqlCommand commandPersonal = new SqlCommand(sqlPersonalCustomer, connection);

            commandPersonal.Parameters.AddWithValue("@ClientId", clientId);
            commandPersonal.Parameters.AddWithValue("@Name", personalCustomer.Name);
            commandPersonal.Parameters.AddWithValue("@Cpf", personalCustomer.Cpf);
            commandPersonal.Parameters.AddWithValue("@Cnh", personalCustomer.Cnh);
            commandPersonal.Parameters.AddWithValue("@Birth_Date", personalCustomer.BirthDate);

            commandPersonal.ExecuteNonQuery();
        }
        else if (client is BusinessCustomer businessCustomer)
        {
            string sqlBusinessCustomer = @"
                INSERT INTO BUSINESS_CUSTOMER
                    (ClientId, Company_Name, Cnpj, Opening_Date)
                VALUES
                    (@ClientId, @Company_Name, @Cnpj, @Opening_Date);";

            using SqlCommand commandBusiness = new SqlCommand(sqlBusinessCustomer, connection);

            commandBusiness.Parameters.AddWithValue("@ClientId", clientId);
            commandBusiness.Parameters.AddWithValue("@Company_Name", businessCustomer.CompanyName);
            commandBusiness.Parameters.AddWithValue("@Cnpj", businessCustomer.Cnpj);
            commandBusiness.Parameters.AddWithValue("@Opening_Date", businessCustomer.OpeningDate);

            commandBusiness.ExecuteNonQuery();
        }
    }

    public Client? GetClientByEmail(string email)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlGetCommand = @"
        SELECT * FROM CLIENT WHERE Email = @email";
        using SqlCommand getCommand = new SqlCommand(sqlGetCommand, connection);
        getCommand.Parameters.AddWithValue("@email", email);
        SqlDataReader reader = getCommand.ExecuteReader();
        
        if (!reader.Read())
        {
            return null;
        }
        int id = (int)reader["Id"];
        string findEmail = reader["Email"].ToString();
        string phone = reader["Phone"].ToString();
        reader.Close();

        string pcCommand = @"
            SELECT * FROM PERSONAL_CUSTOMER WHERE ClientId = @Id";
        using SqlCommand pcClientCommand = new SqlCommand(pcCommand, connection);
        pcClientCommand.Parameters.AddWithValue("@Id", id);
        reader = pcClientCommand.ExecuteReader();
        if (reader.Read())
        {
            PersonalCustomer pc = new PersonalCustomer();
            pc.Id = id;
            pc.Email = findEmail;
            pc.Phone = phone;
            pc.Name = reader["Name"].ToString();
            pc.Cnh = reader["Cnh"].ToString();
            pc.Cpf = reader["Cpf"].ToString();
            pc.BirthDate = (DateTime)reader["Birth_Date"];

            return pc;
        }
        reader.Close();

        string bcCommand = @"
            SELECT * FROM BUSINESS_CUSTOMER WHERE ClientId = @Id";
        using SqlCommand bcClientCommand = new SqlCommand(bcCommand, connection);
        bcClientCommand.Parameters.AddWithValue("@Id", id);
        reader = bcClientCommand.ExecuteReader();
        
        if (reader.Read())
        {
            BusinessCustomer bc = new BusinessCustomer();
            bc.Id = id;
            bc.Email = findEmail;
            bc.Phone = phone;
            bc.CompanyName = reader["Company_Name"].ToString();
            bc.Cnpj = reader["Cnpj"].ToString();
            bc.OpeningDate = (DateTime)reader["Opening_Date"];

            return bc;
        }
        reader.Close();

        return null;
    }
}
