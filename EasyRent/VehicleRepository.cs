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
}
