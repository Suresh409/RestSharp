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
        public String getApiConfigValue(string keyName)
        {
            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
            String projectPath = new Uri(actualPath).LocalPath;
            String reportPath = projectPath + "\\Support\\configuration.json";
            string jsonData = File.ReadAllText(reportPath);
       
            var dict = JObject.Parse(jsonData).SelectToken("apiInfo").ToObject<Dictionary<string, string>>();
            return dict[keyName];
        }

        public static String getTestDataValue(string keyName)
        {
         
            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
            String projectPath = new Uri(actualPath).LocalPath;
            //String reportPath = projectPath + "\\Support\\configuration.json";
            String reportPath = projectPath + "\\TestData\\TestData.json";
            string jsonData = File.ReadAllText(reportPath);
            string tcName = ScenarioContext.Current.ScenarioInfo.Title;
            Console.WriteLine("TEST CASE  NAME: " + tcName);
            var reportResults = JObject.Parse(jsonData);
            var output = reportResults[tcName][keyName].ToString();
            Console.WriteLine(output);
            return output;
        }
    }
}
