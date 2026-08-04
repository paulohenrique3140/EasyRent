// INSTANTIATING CLASS OBJECTS
Client? cliente = null;
Vehicle? veiculo = null;
Rental? locacao = null;

while (true)
{
    // DISPLAYING INITIAL MENU
    Console.WriteLine("\n### EASY RENT - VEHICLE RENTAL ###");
    Console.WriteLine("\nMenu: \n" +
                      "\n[1] Register client" +
                      "\n[2] Register vehicle" +
                      "\n[0] Exit\n");
    Console.Write("\nEnter the desired option: ");
    var entrada = Console.ReadLine();

    // VALIDATING THE ENTERED INPUT FOR THE MENU
    bool conversaoValida = int.TryParse(entrada, out int opcaoMenuInicial);
    bool? validaEntrada = ValidaEntrada(conversaoValida, opcaoMenuInicial);
    if (validaEntrada == false) { break; }
    else if (validaEntrada == true) { continue; }

    // INITIAL MENU SWITCH
    switch (opcaoMenuInicial)
    {
        case 1: // CREATING THE CLIENT CLASS OBJECT
            Console.WriteLine("\n@@@ Client Registration @@@");
            Console.Write("\nFull name: ");
            string? nome = Console.ReadLine();
            Console.Write("CPF: ");
            string? cpf = Console.ReadLine();
            Console.Write("Driver's License (CNH): ");
            string? cnh = Console.ReadLine();
            DateTime dataNascimento;
            while (true)
            {
                Console.Write("Date of birth [MM/DD/YYYY]: ");
                string? entradaData = Console.ReadLine();
                if (DateTime.TryParse(entradaData, out dataNascimento))
                    break;
                Console.WriteLine("Invalid date. Please try again.\n");
            }
            cliente = new Client(nome, cpf, cnh, Client.CalculateAge(dataNascimento));
            break;

        case 2: // CREATING THE VEHICLE CLASS OBJECT
            Console.WriteLine("\n@@@ Vehicle Registration @@@");
            Console.Write("\nModel: ");
            string? modelo = Console.ReadLine();
            Console.Write("License plate: ");
            string? placa = Console.ReadLine();
            Console.Write("Body style [1-Hatch / 2-Sedan / 3-SUV / 4-Utility]: ");
            int carroceria = Convert.ToInt32(Console.ReadLine());
            Console.Write("Daily rate: $ ");
            double valorDiaria = Convert.ToDouble(Console.ReadLine());
            Console.Write("Current mileage: ");
            int kmAtual = Convert.ToInt32(Console.ReadLine());
            veiculo = new Vehicle(modelo, placa, (CarBody)carroceria, valorDiaria, kmAtual);
            break;
        default:
            Console.Write("\nInvalid option! Please enter one of the listed options: \n");
            break;
    }

    // VALIDATING IF CLIENT AND VEHICLE WERE CREATED
    if (cliente != null && veiculo != null)
    {
        while (true)
        {
            // DISPLAYING SECOND MENU
            Console.WriteLine("\n### EASY RENT - VEHICLE RENTAL ###");
            Console.WriteLine("\nMenu: \n" +
                              "\n[1] Show client" +
                              "\n[2] Show vehicle" +
                              "\n[3] Rent vehicle" +
                              "\n[0] Return to previous menu\n");
            Console.Write("\nEnter the desired option: ");
            entrada = Console.ReadLine();

            // VALIDATING THE ENTERED INPUT FOR THE SECOND MENU
            conversaoValida = int.TryParse(entrada, out int opcaoSegundoMenu);
            validaEntrada = ValidaEntrada(conversaoValida, opcaoSegundoMenu);
            if (validaEntrada == false) { break; }
            else if (validaEntrada == true) { continue; }

            // SECOND MENU SWITCH
            switch (opcaoSegundoMenu)
            {
                case 1: // DISPLAYING CLIENT CLASS OBJECT
                    Console.WriteLine("\n@@@ SHOWING CLIENT @@@");
                    Console.WriteLine(cliente.ShowClient());
                    break;

                case 2: // DISPLAYING VEHICLE CLASS OBJECT
                    Console.WriteLine("\n@@@ SHOWING VEHICLE @@@");
                    Console.WriteLine(veiculo.ShowVehicle());
                    break;
                case 3: // CREATING A RENTAL CLASS OBJECT
                    Console.WriteLine("\n@@@ Reserving vehicle @@@");
                    Console.WriteLine("\n" + veiculo.ShowVehicle() + "\n");
                    Console.Write("\nEnter the number of rental days: ");
                    int quantidadeDiarias = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nWould you like to add insurance for $50.00 per day? [y/n]: ");
                    var seguro = Console.ReadLine();
                    bool possuiSeguro = seguro == "y" || seguro == "s";
                    locacao = new Rental(cliente, veiculo, quantidadeDiarias, possuiSeguro, veiculo.CurrentMileage, RentStatus.Open);
                    break;
                default:
                    Console.Write("\nInvalid option! Please enter one of the listed options: \n");
                    break;
            }

            if (locacao != null)
            {
                while (true)
                {
                    // DISPLAYING THIRD MENU
                    Console.WriteLine("\n### EASY RENT - VEHICLE RENTAL ###");
                    Console.WriteLine("\nMenu: \n" +
                                      "\n[1] Complete rental (return vehicle)" +
                                      "\n[2] Cancel reservation" +
                                      "\n[3] Show rental details" +
                                      "\n[0] Return to previous menu\n");
                    Console.Write("\nEnter the desired option: ");
                    entrada = Console.ReadLine();

                    // VALIDATING THE ENTERED INPUT FOR THE THIRD MENU
                    conversaoValida = int.TryParse(entrada, out int opcaoTerceiroMenu);
                    validaEntrada = ValidaEntrada(conversaoValida, opcaoTerceiroMenu);
                    if (validaEntrada == false) { break; }
                    else if (validaEntrada == true) { continue; }

                    // THIRD MENU SWITCH
                    switch (opcaoTerceiroMenu)
                    {
                        case 1: // CALLING METHOD TO COMPLETE RENTAL (RETURN VEHICLE)
                            Console.WriteLine("@@@ Return Panel @@@");
                            Console.Write("Enter current vehicle mileage: ");
                            int kmFinal = Convert.ToInt32(Console.ReadLine());
                            locacao.CloseRental(kmFinal);
                            Console.WriteLine("\nRental closed!" + locacao.ShowSummary(veiculo.CurrentMileage));
                            break;
                        case 2: // CALLING METHOD TO CANCEL RESERVATION
                            locacao.CancelRental();
                            Console.WriteLine("\n" + locacao.ShowSummary(veiculo.CurrentMileage));
                            break;
                        case 3: // DISPLAYING RENTAL DETAILS
                            Console.WriteLine("@@@ Showing rental @@@");
                            Console.WriteLine("\n" + locacao.ShowSummary(veiculo.CurrentMileage));
                            break;
                        default:
                            Console.Write("\nInvalid option! Please enter one of the listed options: \n");
                            break;
                    }
                    if (locacao.Status != RentStatus.Open)
                    {
                        break;
                    }
                }
            }
        }
    }
}

static bool? ValidaEntrada(bool conversaoValida, int opcaoMenu)
{
    if (!conversaoValida)
    {
        Console.WriteLine("\nInvalid option! Please enter numbers only.\n");
        return true;
    }

    if (opcaoMenu < 0 || opcaoMenu > 8)
    {
        Console.Write("\nInvalid option! Please enter one of the listed options: \n");
        return true;
    }

    if (opcaoMenu == 0)
    {
        Console.WriteLine("\nCLOSING ...");
        return false;
    }
    return null;
}