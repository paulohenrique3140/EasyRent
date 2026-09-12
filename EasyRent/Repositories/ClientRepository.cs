using Microsoft.Data.SqlClient;

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
                    (ClientId, Name, CPF, CNH, Birth_Date, Rideshare_Driver)
                VALUES
                    (@ClientId, @Name, @Cpf, @Cnh, @Birth_Date, @Rideshare_Driver);";

            using SqlCommand commandPersonal = new SqlCommand(sqlPersonalCustomer, connection);

            commandPersonal.Parameters.AddWithValue("@ClientId", clientId);
            commandPersonal.Parameters.AddWithValue("@Name", personalCustomer.Name);
            personalCustomer.Cpf = new string(personalCustomer.Cpf.Where(char.IsDigit).ToArray());
            commandPersonal.Parameters.AddWithValue("@Cpf", personalCustomer.Cpf);
            commandPersonal.Parameters.AddWithValue("@Cnh", personalCustomer.Cnh);
            commandPersonal.Parameters.AddWithValue("@Birth_Date", personalCustomer.BirthDate);
            commandPersonal.Parameters.AddWithValue("@Rideshare_Driver", personalCustomer.RideshareDriver);

            commandPersonal.ExecuteNonQuery();
        }
        else if (client is BusinessCustomer businessCustomer)
        {
            string sqlBusinessCustomer = @"
                INSERT INTO BUSINESS_CUSTOMER
                    (ClientId, Company_Name, CNPJ, Opening_Date)
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
            pc.Cnh = reader["CNH"].ToString();
            pc.Cpf = reader["CPF"].ToString();
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
            bc.Cnpj = reader["CNPJ"].ToString();
            bc.OpeningDate = (DateTime)reader["Opening_Date"];

            return bc;
        }
        reader.Close();

        return null;
    }

    public List<Client> GetClientsByEmail2(string word)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlCommand = @"
            SELECT * FROM CLIENT WHERE EMAIL LIKE '%' + @Word + '%'
        ";
        string? email = string.Empty;
        string? phone = string.Empty;
        int id = 0;
        List<Client> clients = new List<Client>();

        SqlCommand getCommand = new SqlCommand(sqlCommand, connection);
        getCommand.Parameters.AddWithValue("@Word", word);
        SqlDataReader reader = getCommand.ExecuteReader();
        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            email = Convert.ToString(reader["Email"]);
            phone = Convert.ToString(reader["Phone"]);
            using SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            string sqlGetCommand2 = @"
            SELECT * FROM PERSONAL_CUSTOMER
            WHERE ClientId = @Id";
            using SqlCommand getCommand2 = new SqlCommand(sqlGetCommand2, connection2);
            getCommand2.Parameters.AddWithValue("Id", id);
            SqlDataReader reader2 = getCommand2.ExecuteReader();
            if (reader2.Read())
            {
                PersonalCustomer pc = new PersonalCustomer();
                pc.Id = id;
                pc.Email = email;
                pc.Phone = phone;
                pc.Name = reader2["Name"].ToString();
                pc.Cnh = reader2["CNH"].ToString();
                pc.Cpf = reader2["CPF"].ToString();
                pc.BirthDate = (DateTime)reader2["Birth_Date"];
                pc.RideshareDriver = Convert.ToBoolean(reader2["Rideshare_Driver"]);
                clients.Add(pc);
            }
            reader2.Close();
            connection2.Close();

            using SqlConnection connection3 = new SqlConnection(connectionString);
            connection3.Open();
            string sqlGetCommand3 = @"
            SELECT * FROM BUSINESS_CUSTOMER
            WHERE ClientId = @Id";
            using SqlCommand getCommand3 = new SqlCommand(sqlGetCommand3, connection3);
            getCommand3.Parameters.AddWithValue("Id", id);
            SqlDataReader reader3 = getCommand3.ExecuteReader();
            if (reader3.Read())
            {
                BusinessCustomer bc = new BusinessCustomer();
                bc.Id = id;
                bc.Email = email;
                bc.Phone = phone;
                bc.CompanyName = reader3["Company_Name"].ToString();
                bc.Cnpj = reader3["CNPJ"].ToString();
                bc.OpeningDate = (DateTime)reader3["Opening_Date"];
                clients.Add(bc);
            }
            reader3.Close();
            connection3.Close();
        }
        reader.Close();
        connection.Close();

        return clients;
    }

    public List<Client>? GetAllClients()
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlGetCommand = @"
            SELECT * FROM CLIENT;
        ";
        string email = string.Empty;
        string phone = string.Empty;
        int id = 0;
        List<Client> clients = new List<Client>();
        using SqlCommand getCommand = new SqlCommand(sqlGetCommand, connection);
        SqlDataReader reader = getCommand.ExecuteReader();
        while (reader.Read())
        {
            id = Convert.ToInt32(reader["Id"]);
            email = (string?)reader["Email"];
            phone = (string?)reader["Phone"];

            using SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            string sqlGetCommand2 = @"
            SELECT * FROM PERSONAL_CUSTOMER
            WHERE ClientId = @Id";
            using SqlCommand getCommand2 = new SqlCommand(sqlGetCommand2, connection2);
            getCommand2.Parameters.AddWithValue("Id", id);
            SqlDataReader reader2 = getCommand2.ExecuteReader();
            if (reader2.Read())
            {
                PersonalCustomer pc = new PersonalCustomer();
                pc.Id = id;
                pc.Email = email;
                pc.Phone = phone;
                pc.Name = reader2["Name"].ToString();
                pc.Cnh = reader2["CNH"].ToString();
                pc.Cpf = reader2["CPF"].ToString();
                pc.BirthDate = (DateTime)reader2["Birth_Date"];
                pc.RideshareDriver = Convert.ToBoolean(reader2["Rideshare_Driver"]);
                clients.Add(pc);
            }
            reader2.Close();
            connection2.Close();

            using SqlConnection connection3 = new SqlConnection(connectionString);
            connection3.Open();
            string sqlGetCommand3 = @"
            SELECT * FROM BUSINESS_CUSTOMER
            WHERE ClientId = @Id";
            using SqlCommand getCommand3 = new SqlCommand(sqlGetCommand3, connection3);
            getCommand3.Parameters.AddWithValue("Id", id);
            SqlDataReader reader3 = getCommand3.ExecuteReader();
            if (reader3.Read())
            {
                BusinessCustomer bc = new BusinessCustomer();
                bc.Id = id;
                bc.Email = email;
                bc.Phone = phone;
                bc.CompanyName = reader3["Company_Name"].ToString();
                bc.Cnpj = reader3["CNPJ"].ToString();
                bc.OpeningDate = (DateTime)reader3["Opening_Date"];
                clients.Add(bc);
            }
            reader3.Close();
            connection3.Close();
        }
        reader.Close();
        connection.Close();
        return clients;
    }

    public void UpdateClientEmail(string? email, string? newEmail)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sqlCommand = @"
           UPDATE CLIENT
           SET EMAIL = @newEmail
           WHERE Email = @email
        ";

        using SqlCommand command = new SqlCommand(sqlCommand, connection);
        command.Parameters.AddWithValue("@newEmail", newEmail);
        command.Parameters.AddWithValue("@email", email);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteClient(Client client)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();


        if (client is PersonalCustomer)
        {
            string sqlDeleteCommand = @"
                    DELETE FROM PERSONAL_CUSTOMER
                    WHERE ClientId = @Id";

            SqlCommand deleteCommand = new SqlCommand(sqlDeleteCommand, connection);
            deleteCommand.Parameters.AddWithValue("@Id", client.Id);
            deleteCommand.ExecuteNonQuery();
        }
        else
        {
            string sqlDeleteCommand2 = @"
                    DELETE FROM BUSINESS_CUSTOMER
                    WHERE ClientId = @Id";

            SqlCommand deleteCommand2 = new SqlCommand(sqlDeleteCommand2, connection);
            deleteCommand2.Parameters.AddWithValue("@Id", client.Id);
            deleteCommand2.ExecuteNonQuery();
        }

        string sqlCommand = @"
            DELETE FROM CLIENT
            WHERE Id = @Id";

        SqlCommand command = new SqlCommand(sqlCommand, connection);
        command.Parameters.AddWithValue("@Id", client.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }
}
