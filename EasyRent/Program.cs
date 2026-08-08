// INSTANTIATING RENTALSERVICE OBJECT
RentalService rentalService = new RentalService();
Console.WriteLine(@"
           ______
      ____/|_||_\`.__
     (   _    _ _   _\
     =`-(_)--(_)-'    

====================================
          E A S Y   R E N T
        VEHICLE RENTAL SYSTEM
====================================
");

while (true)
{
    // DISPLAYING MAIN MENU
    Console.WriteLine("\n=-=-=-=-=-=-= Main Menu =-=-=-=-=-=-=");
    Console.WriteLine("\n[1] Client" +
                      "\n[2] Vehicle" +
                      "\n[3] Rental" +
                      "\n[0] Exit\n");
    int mainMenuOption = RentalService.ReadMenuOption(3); // METHOD TO INPUT VALIDATIONS

    if(mainMenuOption == 0)
    {
        Console.WriteLine("Closing... ");
        break;
    }

    switch (mainMenuOption)
    {
<<<<<<< Updated upstream
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
=======
        case 1:
            Console.WriteLine("Client case");
            break;
        case 2:
            Console.WriteLine("Vehicle case");
            break;
        case 3:
            Console.WriteLine("Rental case");
            break;
>>>>>>> Stashed changes
    }
}
