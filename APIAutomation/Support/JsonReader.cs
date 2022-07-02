using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Support
{
    public class JsonReader
    {

        public static String GetApiConfigValue(string keyName)
        {
            String reportPath = GetProjectPath() + "\\Support\\configuration.json";
            string jsonData = File.ReadAllText(reportPath);
            var dict = JObject.Parse(jsonData).SelectToken("apiInfo").ToObject<Dictionary<string, string>>();

            return dict[keyName];
        }

        public static String GetTestDataValue(string keyName)
        {
            string output = null;
            String folderPath = GetProjectPath() + "\\TestData";
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.json"))
            {
                string contents = File.ReadAllText(file);
                string tcName = ScenarioContext.Current.ScenarioInfo.Title;
                //Console.WriteLine("TEST CASE  NAME: " + tcName);
                var reportResults = JObject.Parse(contents);
                if (reportResults.ContainsKey(tcName))
                {
                   // Console.WriteLine("Test Case : " + tcName + " Found in file "+file); 
                    output = reportResults[tcName][keyName].ToString();
                    Console.WriteLine(output);
                    break;
                }
            }
            return output;
        }

         public static string GetProjectPath()
        {

            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
#pragma warning disable IDE0057 // Use range operator
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
#pragma warning restore IDE0057 // Use range operator
            String projectPath = new Uri(actualPath).LocalPath;
            return projectPath;

        }
    }
}
