using LowCostFligtsBrowser.Application.Common.Interfaces;
using LowCostFligtsBrowser.Application.TodoLists.Queries.ExportTodos;
using LowCostFligtsBrowser.Infrastructure.Files.Maps;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;

namespace LowCostFligtsBrowser.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
        public static IEnumerable<T> ReadInCSV<T>(string absolutePath)
        {
            List<T> records;
            using (var reader = new StreamReader(absolutePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
               records = csv.GetRecords<T>().ToList();
            }
            return records;
        }
    }
}
