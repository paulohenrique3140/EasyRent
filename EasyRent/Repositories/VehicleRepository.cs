using Microsoft.Data.SqlClient;

public class VehicleRepository
{
    private readonly string connectionString = "Server=PAULO;DataBase=EASY_RENT;Integrated Security=True;TrustServerCertificate=True;";

    public void AddVehicle(Vehicle vehicle)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlVehicle = @"
            INSERT INTO VEHICLE (Model, License_Plate, CarBody, DailyRate, CurrentMileage)
            OUTPUT INSERTED.Id
            VALUES (@Model, @License_Plate, @CarBody, @DailyRate, @CurrentMileage)
        ";

        SqlCommand sqlCommand = new SqlCommand(sqlVehicle, connection);
        sqlCommand.Parameters.AddWithValue("@Model", vehicle.Model);
        sqlCommand.Parameters.AddWithValue("@License_Plate", vehicle.LicencePlate);
        sqlCommand.Parameters.AddWithValue("@CarBody", (int)vehicle.CarBody);
        sqlCommand.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
        sqlCommand.Parameters.AddWithValue("@CurrentMileage", vehicle.CurrentMileage);
        int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
        vehicle.Id = id;
    }

    public void UpdateVehicle(Vehicle vehicle, double dailyRate)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlVehicle = @"
           UPDATE VEHICLE
           SET DailyRate = @dailyRate
           WHERE Id = @Id 
        ";

        SqlCommand sqlCommand = new SqlCommand(sqlVehicle, connection);
        sqlCommand.Parameters.AddWithValue("@dailyRate", dailyRate);
        sqlCommand.Parameters.AddWithValue("@Id", vehicle.Id);
        sqlCommand.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdateMileage(Vehicle vehicle, int endingMileage)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlCommand = @"
            UPDATE VEHICLE
            SET CurrentMileage = @endingMileage
            WHERE Id = @Id";

        SqlCommand command = new SqlCommand(sqlCommand, connection);
        command.Parameters.AddWithValue("@endingMileage", endingMileage);
        command.Parameters.AddWithValue("@Id", vehicle.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public Vehicle? FindVehicleByLicensePlate(string? licensePlate)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlVehicle = @"
          SELECT * FROM VEHICLE WHERE LICENSE_PLATE = @LicensePlate
        ";

        SqlCommand sqlCommand = new SqlCommand(sqlVehicle, connection);
        sqlCommand.Parameters.AddWithValue("@LicensePlate", licensePlate);
        Vehicle vehicle = new();
        SqlDataReader reader = sqlCommand.ExecuteReader();
        if(reader.Read())
        {
            vehicle.Id = Convert.ToInt32(reader["Id"]);
            vehicle.Model = reader["Model"].ToString();
            vehicle.LicencePlate = reader["License_Plate"].ToString();
            vehicle.CarBody = (CarBody)Convert.ToInt32(reader["CarBody"]);
            vehicle.DailyRate = Convert.ToDouble(reader["DailyRate"]);
            vehicle.CurrentMileage = Convert.ToInt32(reader["CurrentMileage"]);
            return vehicle;
        }

        return null;
    }

    public Vehicle? FindVehicleById(int id)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlVehicle = @"
          SELECT * FROM VEHICLE WHERE ID = @Id
        ";

        SqlCommand sqlCommand = new SqlCommand(sqlVehicle, connection);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        Vehicle vehicle = new();
        SqlDataReader reader = sqlCommand.ExecuteReader();
        if (reader.Read())
        {
            vehicle.Id = Convert.ToInt32(reader["Id"]);
            vehicle.Model = reader["Model"].ToString();
            vehicle.LicencePlate = reader["License_Plate"].ToString();
            vehicle.CarBody = (CarBody)Convert.ToInt32(reader["CarBody"]);
            vehicle.DailyRate = Convert.ToDouble(reader["DailyRate"]);
            vehicle.CurrentMileage = Convert.ToInt32(reader["CurrentMileage"]);
            return vehicle;
        }

        return null;
    }

    public void DeleteVehicle(Vehicle vehicle)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlCommand = @"
            DELETE FROM VEHICLE WHERE ID = @Id
        ";

        SqlCommand deleteCommand = new SqlCommand(sqlCommand, connection);
        deleteCommand.Parameters.AddWithValue("@Id", vehicle.Id);
        deleteCommand.ExecuteNonQuery();
    }

    public List<Vehicle>? GetVehicles()
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlCommand = @"
            SELECT * FROM VEHICLE;
        ";

        SqlCommand command = new SqlCommand(sqlCommand, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = Convert.ToInt32(reader["Id"]);
            string? model = Convert.ToString(reader["Model"]);
            string? licensePlate = Convert.ToString(reader["License_Plate"]);
            CarBody carBody = (CarBody)Convert.ToInt32(reader["CarBody"]);
            double dailyRate = Convert.ToDouble(reader["DailyRate"]);
            int currentMileage = Convert.ToInt32(reader["CurrentMileage"]);
            Vehicle vehicle = new Vehicle(model, licensePlate, carBody, dailyRate, currentMileage);
            vehicle.Id = id;
            vehicles.Add(vehicle);
        }
        return vehicles;
    }
}
