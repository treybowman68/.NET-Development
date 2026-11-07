// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

var salesreport = CreateSaleSummary(salesFiles);
File.WriteAllText(Path.Combine(salesTotalDir, "salesreport.txt"), salesreport);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        
        // The file name will contain the full path, so only check the end of it
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}


double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}


string CreateSaleSummary(IEnumerable<string> salesFiles)
{
    var sb = new StringBuilder();

    sb.AppendLine("Sales Summary");
    sb.AppendLine("----------------------------");

    double salesTotal = CalculateSalesTotal(salesFiles);
    sb.AppendLine($"Total Sale: ${salesTotal:N2}");
    sb.AppendLine();
    sb.AppendLine("Details:");
    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        sb.AppendLine($"File: {file}, Total: ${data?.Total:N2}");
    }

    return sb.ToString();
}

record SalesData (double Total);