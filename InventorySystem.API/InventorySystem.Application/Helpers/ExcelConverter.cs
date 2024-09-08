using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Reflection;

namespace InventorySystem.Application.Helpers
{
    public class ExcelConverter
    {
        public static async Task<byte[]?> ConvertToExcel<T>(string sheetName, List<string> columns, dynamic data)
        {

            // Creating an instance
            // of ExcelPackage
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                // name of the sheet
                var workSheet = package.Workbook.Worksheets.Add(sheetName);

                // setting the properties
                // of the work sheet 
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                // Setting the properties
                // of the first row
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Row(1).Style.Font.Bold = true;

                // Header of the Excel sheet
                int columnIndex = 1;
                foreach (var column in columns)
                {
                    workSheet.Cells[1, columnIndex].Value = column;
                    columnIndex++;
                }


                // Inserting the article data into excel
                // sheet by using the for each loop
                // As we have values to the first row 
                // we will start with second row
                int recordIndex = 2;

                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var d in data)
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();

                    columnIndex = 2;
                    foreach (var property in properties)
                    {
                        workSheet.Cells[recordIndex, columnIndex].Value = d.GetType().GetProperty(property.Name).GetValue(d, null);
                        columnIndex++;

                    }

                    recordIndex++;
                }

                // By default, the column width is not 
                // set to auto fit for the content
                // of the range, so we are using
                // AutoFit() method here. 
                for (int i = 0; i < columns.Count; i++)
                {
                    workSheet.Column(i + 1).AutoFit();
                }

                package.Save();
            }

            return stream.ToArray();
        }
    }
}
