using NUnit.Framework;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Support
{
    public class ExcelReader
    {
        [Obsolete]
        public static void readExecl() {

            Dictionary<string, Dictionary<string, string>> testCaseWithColumnValues = new Dictionary<string, Dictionary<string, string>>();



            try
            {
                string s = null;
                var d = new DirectoryInfo(@"C:\Users\VARUNP\source\repos\APIAutomation\APIAutomation\TestData");
                var files = d.GetFiles("*.xlsx");
                var usersList = new List();
                foreach (var file in files)
                {
                    var fileName = file.FullName;
                    Console.WriteLine(fileName);
                    using var package = new ExcelPackage(file);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheets sheets = package.Workbook.Worksheets;
                    String testCaseName = null;

                    int numberOfSheets = sheets.Count();

                    for (int i = 0; i < numberOfSheets; i++)
                    {
                        ExcelWorksheet sheet = sheets[i];
                        Console.WriteLine("SHEET NAME: " + sheet.Name);
                        var noOfCol = sheet.Dimension.End.Column;
                        var noOfRow = sheet.Dimension.End.Row;
                        ExcelRow headerRow = null;

                        for (int rowIterator = 0; rowIterator <= noOfRow; rowIterator++)
                        {
                            Dictionary<string, String> currentHash = new Dictionary<string, String>();
                            if (rowIterator == 0)
                            {

                            }
                            else
                            {
                                testCaseName = (string?)sheet.GetValue(rowIterator, 1);
                                Console.WriteLine("TESTCASENAME: "+testCaseName);
                                for (int colIterator = 2; colIterator <= noOfCol; colIterator++)
                                {
                                    //Console.WriteLine(sheet.GetValue(1, colIterator));
                                    //Console.WriteLine(sheet.GetValue(rowIterator, colIterator));
                                    currentHash.Add((string)sheet.GetValue(1, colIterator), (string)sheet.GetValue(rowIterator, colIterator));
                                }

                                testCaseWithColumnValues.Add(testCaseName, currentHash);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
    
}
        static void Main()
        {

            readExecl();
        }

    }


  
}
