using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace DAL.App.EF.Helpers;

public class DataInitializer
{
    private TextFieldParser SetCsvParserConfig(TextFieldParser csvParser)
    {
        // Habeeb recommended this strange library for CSV parsing https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
        csvParser.SetDelimiters(",");
        csvParser.HasFieldsEnclosedInQuotes = true;
        return csvParser;
    }
    
    public List<DAL.App.DTO.Company> ReadCompanies(string fileName)
    {
        using TextFieldParser csvParser = new TextFieldParser(fileName);
        SetCsvParserConfig(csvParser);
        
        // skip first row
        csvParser.ReadLine();

        var companies = new List<DAL.App.DTO.Company>();
        while (! csvParser.EndOfData)
        {
            string[]? fields = csvParser.ReadFields() ?? throw new ArgumentException($"Cannot read file {fileName}.");
            var id = Guid.Parse(fields[0]);
            var name = fields[1];
            var company = new DAL.App.DTO.Company()
            {
                Id = id,
                Name = name
            };
            companies.Add(company);
        }

        return companies;
    }
    
    public List<DAL.App.DTO.Location> ReadLocations(string fileName)
    {
        using TextFieldParser csvParser = new TextFieldParser(fileName);
        SetCsvParserConfig(csvParser);

        // skip first row
        csvParser.ReadLine();

        var locations = new List<DAL.App.DTO.Location>();
        while (! csvParser.EndOfData)
        {
            string[]? fields = csvParser.ReadFields() ?? throw new ArgumentException($"Cannot read file {fileName}.");
            var id = Guid.Parse(fields[0]);
            var planetarySystemName = fields[1];
            var planetName = fields[2];
            var planetLocationName = fields[3];
            var uniquePlanetLocation3LetterIdentifier = fields[4];
            var location = new DAL.App.DTO.Location()
            {
                Id = id,
                PlanetarySystemName = planetarySystemName,
                PlanetName = planetName,
                PlanetLocationName = planetLocationName,
                UniquePlanetLocation3LetterIdentifier = uniquePlanetLocation3LetterIdentifier,
            };
            locations.Add(location);
        }

        return locations;
    }
}