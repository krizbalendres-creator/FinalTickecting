using BoholBusTicketing.Core.DTOs;
using BoholBusTicketing.Core.Interfaces;
using BoholBusTicketing.Core.Services;
using BoholBusTicketing.Data.Data;
using BoholBusTicketing.Data.Repositories;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        // Setup console encoding for better characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // CONNECTION STRING
        var connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=BusTicketingDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        // DB CONTEXT (MANUAL SETUP)
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        using var db = new ApplicationDbContext(options);

        // SEED DATABASE
        db.Database.EnsureCreated();
        var seeder = new DatabaseSeeder(db);
        await seeder.SeedAsync();

        // REPOSITORIES (MANUAL INJECTION)
        ITicketRepository ticketRepo = new TicketRepository(db);
        IBarangayRepository barangayRepo = new BarangayRepository(db);
        IMunicipalityRepository municipalityRepo = new MunicipalityRepository(db);

        // SERVICES
        ILocationService locationService = new LocationService();
        ITicketService ticketService = new TicketService(ticketRepo, barangayRepo);

        // Display welcome screen
        DisplayWelcomeScreen();
        await MainMenu(ticketService, barangayRepo, municipalityRepo);
    }

    static void DisplayWelcomeScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n");
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                    ║");
        Console.WriteLine("║         🚌 BOHOL BUS TICKETING SYSTEM 🚌          ║");
        Console.WriteLine("║                                                    ║");
        Console.WriteLine("║              Book Your Journey Today!              ║");
        Console.WriteLine("║                                                    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("\nSystem initialized successfully. Loading...\n");
        System.Threading.Thread.Sleep(1500);
    }

    static async Task MainMenu(ITicketService ticketService, IBarangayRepository barangayRepo, IMunicipalityRepository municipalityRepo)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│        MAIN MENU - What's Next?     │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine("\n  [1] 🎫 Book a New Ticket");
            Console.WriteLine("  [2] 📍 View Municipalities");
            Console.WriteLine("  [3] 🏘️  View Barangays");
            Console.WriteLine("  [4] ℹ️  About This System");
            Console.WriteLine("  [5] 🚪 Exit Application\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  Enter your choice (1-5): ");
            Console.ResetColor();

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await BookTicket(ticketService, barangayRepo);
                    break;
                case "2":
                    await ViewMunicipalities(municipalityRepo);
                    break;
                case "3":
                    await ViewBarangays(barangayRepo);
                    break;
                case "4":
                    DisplayAbout();
                    break;
                case "5":
                    DisplayGoodbye();
                    return;
                default:
                    DisplayError("Invalid choice! Please select 1-5.");
                    break;
            }
        }
    }

    static async Task BookTicket(ITicketService ticketService, IBarangayRepository barangayRepo)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("┌─────────────────────────────────────┐");
        Console.WriteLine("│      🎫 BOOK YOUR TICKET 🎫         │");
        Console.WriteLine("└─────────────────────────────────────┘");
        Console.ResetColor();

        try
        {
            // Get all barangays for selection
            var barangays = (await barangayRepo.GetByMunicipalityIdAsync(1)).ToList();
            if (barangays.Count == 0)
            {
                DisplayError("No barangays available for booking.");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("\n📍 Select Origin Barangay:");
            Console.WriteLine("─────────────────────────");
            for (int i = 0; i < barangays.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}] {barangays[i].Name}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter barangay number: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int fromIndex) || fromIndex < 1 || fromIndex > barangays.Count)
            {
                DisplayError("Invalid barangay selection!");
                WaitForKeyPress();
                return;
            }

            var fromBarangay = barangays[fromIndex - 1];

            Console.WriteLine("\n📍 Select Destination Barangay:");
            Console.WriteLine("───────────────────────────────");
            for (int i = 0; i < barangays.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}] {barangays[i].Name}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter barangay number: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int toIndex) || toIndex < 1 || toIndex > barangays.Count)
            {
                DisplayError("Invalid barangay selection!");
                WaitForKeyPress();
                return;
            }

            var toBarangay = barangays[toIndex - 1];

            if (fromBarangay.Id == toBarangay.Id)
            {
                DisplayError("Origin and destination cannot be the same!");
                WaitForKeyPress();
                return;
            }

            // Calculate distance and fare
            var distance = await ticketService.EstimateDistanceAsync(fromBarangay.Id, toBarangay.Id);
            var fare = ticketService.ComputeFare(distance);

            // Display ticket summary
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("┌────────────────────────────────────────────────┐");
            Console.WriteLine("║         ✅ TICKET SUMMARY ✅                   ║");
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine($"\n  From:              {fromBarangay.Name}");
            Console.WriteLine($"  To:                {toBarangay.Name}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  Distance:          {distance} km");
            Console.WriteLine($"  Fare:              ₱ {fare:F2}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n  Confirm booking? (Y/N): ");
            Console.ResetColor();

            if (Console.ReadLine()?.ToUpper() != "Y")
            {
                DisplayCancelled("Booking cancelled.");
                WaitForKeyPress();
                return;
            }

            // Create ticket
            var dto = new CreateTicketInputDto
            {
                FromBarangayId = fromBarangay.Id,
                ToBarangayId = toBarangay.Id,
                Distance = distance,
                Fare = fare
            };

            var ticketId = await ticketService.CreateTicketAsync(dto);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║           ✅ TICKET BOOKED SUCCESSFULLY ✅     ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine($"║  Ticket ID: {ticketId}                                  ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.ResetColor();

            WaitForKeyPress();
        }
        catch (Exception ex)
        {
            DisplayError($"Error booking ticket: {ex.Message}");
            WaitForKeyPress();
        }
    }

    static async Task ViewMunicipalities(IMunicipalityRepository municipalityRepo)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("┌─────────────────────────────────────┐");
        Console.WriteLine("│      📍 MUNICIPALITIES 📍           │");
        Console.WriteLine("└─────────────────────────────────────┘");
        Console.ResetColor();

        try
        {
            var municipalities = await municipalityRepo.GetAllAsync();
            var municipalityList = municipalities.ToList();

            if (municipalityList.Count == 0)
            {
                DisplayError("No municipalities found.");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("\n");
            int count = 1;
            foreach (var municipality in municipalityList)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  [{count}] ");
                Console.ResetColor();
                Console.WriteLine($"{municipality.Name}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"      └─ {municipality.Barangays?.Count ?? 0} barangays");
                Console.ResetColor();
                count++;
            }

            WaitForKeyPress();
        }
        catch (Exception ex)
        {
            DisplayError($"Error loading municipalities: {ex.Message}");
            WaitForKeyPress();
        }
    }

    static async Task ViewBarangays(IBarangayRepository barangayRepo)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("┌─────────────────────────────────────┐");
        Console.WriteLine("│       🏘️  BARANGAYS 🏘️             │");
        Console.WriteLine("└─────────────────────────────────────┘");
        Console.ResetColor();

        try
        {
            Console.WriteLine("\nEnter Municipality ID (or press Enter for municipality 1): ");
            var input = Console.ReadLine();
            int municipalityId = string.IsNullOrEmpty(input) ? 1 : int.Parse(input);

            var barangays = await barangayRepo.GetByMunicipalityIdAsync(municipalityId);
            var barangayList = barangays.ToList();

            if (barangayList.Count == 0)
            {
                DisplayError("No barangays found for this municipality.");
                WaitForKeyPress();
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│       🏘️  BARANGAYS LIST 🏘️         │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine("\n");
            int count = 1;
            foreach (var barangay in barangayList)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [{count}] ");
                Console.ResetColor();
                Console.WriteLine($"{barangay.Name}");
                count++;
            }

            WaitForKeyPress();
        }
        catch (Exception ex)
        {
            DisplayError($"Error loading barangays: {ex.Message}");
            WaitForKeyPress();
        }
    }

    static void DisplayAbout()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌────────────────────────────────────────────────┐");
        Console.WriteLine("║              ℹ️  ABOUT THE SYSTEM ℹ️           ║");
        Console.WriteLine("└────────────────────────────────────────────────┘");
        Console.ResetColor();

        Console.WriteLine("""

  🚌 BOHOL BUS TICKETING SYSTEM
  Version 1.0

  This system allows you to:
  ✓ Book bus tickets for various routes
  ✓ Calculate automatic fares based on distance
  ✓ View available municipalities and barangays
  ✓ Get distance estimates between locations

  Base Fare:        ₱ 12.00
  Per KM Rate:      ₱ 2.20
  Minimum Distance: 5 km

  Technical Details:
  ✓ Built with C# .NET 8.0
  ✓ SQL Server LocalDB
  ✓ Entity Framework Core
  ✓ Service-based architecture

  Support:
  For issues or questions, contact system administrator.
  """);

        WaitForKeyPress();
    }

    static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n  ❌ ERROR: {message}");
        Console.ResetColor();
    }

    static void DisplayCancelled(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  ⚠️  {message}");
        Console.ResetColor();
    }

    static void DisplayGoodbye()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n");
        Console.WriteLine("╔════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                ║");
        Console.WriteLine("║     Thank you for using Bohol Bus Ticketing!   ║");
        Console.WriteLine("║                                                ║");
        Console.WriteLine("║              Safe travels! 🚌                  ║");
        Console.WriteLine("║                                                ║");
        Console.WriteLine("╚════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void WaitForKeyPress()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\n  Press any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
    }
}
