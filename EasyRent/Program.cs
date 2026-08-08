// INSTANTIATING RENTALSERVICE OBJECT
RentalService rentalService = new RentalService();

Console.WriteLine(@"
       ______
  ____/|_||_\`.__
 (   _    _ _   _\
 =`-(_)--(_)-'
");

while (true)
{
    // DISPLAYING MAIN MENU
    Console.WriteLine("\n=-=-=-=-=-=-= Main Menu =-=-=-=-=-=-=");

    Console.WriteLine("\n[1] Client" +
                      "\n[2] Vehicle" +
                      "\n[3] Rental" +
                      "\n[0] Exit\n");

    int mainMenuOption = RentalService.ReadMenuOption(3);

    if (mainMenuOption == 0)
    {
        Console.WriteLine("Closing...");
        break;
    }

    switch (mainMenuOption)
    {
        case 1:
            Console.WriteLine("Client case");
            break;

        case 2:
            Console.WriteLine("Vehicle case");
            break;

        case 3:
            Console.WriteLine("Rental case");
            break;
    }
}