using APIAutomation.Support;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.StepDefinitions
{
    [Binding]
public sealed class Hooks
    {
private static ExtentTest featureName;
public static ExtentTest scenario;
private static ExtentReports extent;
public static string ReportPath;
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
    
        string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0", "");
            Console.WriteLine("PROJECT PATH->->->-> " + path1);
        string path = path1 + "Report\\index.html";
        ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
    }
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        //Create dynamic feature name
        featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        Console.WriteLine("BeforeFeature");
    }
    [BeforeScenario]
        [Obsolete]
        public void BeforeScenario()
    {
        Console.WriteLine("BeforeScenario");
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0", "");
            Console.WriteLine("PROJECT PATH->->->-> " + path1);
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);

    }
    [AfterStep]
        [Obsolete]
        public void InsertReportingSteps()
    {
        var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
        if (ScenarioContext.Current.TestError == null)
        {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Info, WS.LogRequest());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
        }
        else if(ScenarioContext.Current.TestError != null)
            {
            if (stepType == "Given")
            {
                scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            else if(stepType == "When")
                {
                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Log(Status.Info,WS.LogRequest()); 
            }
            else if(stepType == "Then") {
                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            else if(stepType == "And")
                {
                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
                
        }
    }
    [AfterScenario]
    public void AfterScenario()
    {
        Console.WriteLine("AfterScenario");
     
          
        }
    [AfterTestRun]
    public static void AfterTestRun()
    {
      
        extent.Flush();
    }
}
}