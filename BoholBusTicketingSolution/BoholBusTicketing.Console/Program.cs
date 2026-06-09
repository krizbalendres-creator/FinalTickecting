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
        Console.WriteLine("=== BOHOL BUS TICKETING SYSTEM ===");

        
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

        // SIMPLE DEMO FLOW

        Console.WriteLine("System Ready!");

        while (true)
        {
            Console.WriteLine("\n1. Create Ticket");
            Console.WriteLine("2. Exit");
            Console.Write("Select option: ");

            var input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("Creating ticket...");

                // Example usage (adjust based on your DTO)
                var result = await ticketService.CreateTicketAsync(new CreateTicketInputDto
                {
                    // fill your fields here
                });

                Console.WriteLine("Ticket created!");
            }
            else if (input == "2")
            {
                break;
            }
        }

        Console.WriteLine("Application closed.");
    }
}