using APIAutomation.Support;
using APIAutomation.Model.Response;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;
using APIAutomation.Model.Request;
using RestSharp.Serializers.NewtonsoftJson;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json.Linq;
using static APIAutomation.Support.JsonReader;

namespace APIAutomation.StepDefinitions
{
    [Binding]
    public sealed class EmployeeStepDefinitions
    { 
        private Employee employee_info;


        [Given("the customer is set all the headers to run the Api")]
        public static void SetTheRequest()
        {
            WS.SetUrl("baseUrl");
         }

        [When("the Customer try to view the list of customers")]
        public static void GetAllEmployees()
        {
           WS.SetEndPoint("listAllEmployees");
           WS.SetDefaultHeaders();
           WS.Get();
        }

        [When("the Customer try to view the perticuler customer details")]
        public static void GetPerticulerCustomerDetails(Table table)
        {
            var dictionary = TableExtension.ToDictionary(table);
            var empId = dictionary["employeeId"];
            //Console.WriteLine("USERNAME : " + getTestDataValue("UserName"));
            WS.SetEndPoint("perticularEmployeeDetails");
            WS.SetPathParam("empId", empId);
            WS.Get();
        }
        [Then("verify the status code as 200")]
        public static void VerifyStatusCode()
        {
            WS.VerifyStatusCode(GetTestDataValue("StatusCode"));
        }

        [Then("get the employeeName")]
        public static void GetEmployeName()
        {
                 //TODO
        }
        [Then("The employee should have the following values")]
        public void ThenTheEmployeeShouldHaveTheFollowingValues(Table table)
        {

             employee_info = JsonSerializer.Deserialize<Employee>(WSHelpers.response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine("ACTUAL ID : " + employee_info.data.id);
            Console.WriteLine("Actual status :" + employee_info.status);
            var dictionary = TableExtension.ToDictionary(table);
            string id =  dictionary.FirstOrDefault(x => x.Key == "id").Value;
            Console.WriteLine("EXPECTED ID : " + id);

        }

        [When("the Customer try to create Employee")]
        public static void CreateEmployee()
        {
            Create c = new()
            {
                name = GetTestDataValue("name"),
                salary = GetTestDataValue("salary"),
                age = GetTestDataValue("age")
            };
            WS.SetEndPoint("create");
            WS.SetDefaultHeaders();
            WS.AddRequestBody(c);
            WS.Post();
        }
    }
}