using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using TestTask_DevelopsToday.DB;

internal class Program
{

    private static readonly string CsvPath = "D:\\Testovi\\sample-cab-data.csv";
    private static readonly string DuplicatesPath = "D:\\Testovi\\duplicates.csv";

    static async Task Main()
    {
        if (!File.Exists(CsvPath))
        {
            Console.WriteLine("The specified file does not exist.");
            return;
        }

        var trips = new List<DataTrip>();
        var duplicates = new List<DataTrip>();
        var uniqueSet = new HashSet<string>();

        using var reader = new StreamReader(CsvPath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        });

        var est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        await foreach (var record in csv.GetRecordsAsync<DataTripString>())
        {
            string key = $"{record.tpep_pickup_datetime}_{record.tpep_dropoff_datetime}_{record.passenger_count}";

            if (!uniqueSet.Add(key))
            {
                duplicates.Add(record.ToTaxiTrip(est));
                continue;
            }

            trips.Add(record.ToTaxiTrip(est));
        }

        using (var writer = new StreamWriter(DuplicatesPath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            await csvWriter.WriteRecordsAsync(duplicates);
        }

        using var dbContext = new CsvDbContext();
        await dbContext.Database.EnsureCreatedAsync();

        int batchSize = 1000;
        for (int i = 0; i < trips.Count; i += batchSize)
        {
            var batch = trips.Skip(i).Take(batchSize).ToList();
            dbContext.DataTrips.AddRange(batch);
            await dbContext.SaveChangesAsync();
            Console.WriteLine($"Inserted {i + batch.Count} rows...");
        }

        Console.WriteLine($"Total Inserted rows: {trips.Count}");
    }
}