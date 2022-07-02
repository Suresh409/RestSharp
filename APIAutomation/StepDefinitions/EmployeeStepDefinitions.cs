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
        private static Create new_emloyee;
        string jsonBody;


        [Given("the customer is set all the headers to run the Api")]
        public void setTheRequest()
        {
            WS.setUrl("baseUrl");
         }

        [When("the Customer try to view the list of customers")]
        public void getAllEmployees()
        {
          
            Console.WriteLine("WHEN EXECUTED");
            Console.WriteLine("USERNAME : "+ getTestDataValue("UserName"));

           WS.setEndPoint("listAllEmployees");
           WS.setDefaultHeaders();
           WS.get();
        }

        [When("the Customer try to view the perticuler customer details")]
        public void getPerticulerCustomerDetails(Table table)
        {
            var dictionary = TableExtension.ToDictionary(table);
            var empId = dictionary["employeeId"];
            Console.WriteLine("WHEN EXECUTED");
            Console.WriteLine("USERNAME : " + getTestDataValue("UserName"));
            WS.setEndPoint("perticularEmployeeDetails");
            WS.setPathParam("empId", empId);
            WS.get();
        }
        [Then("verify the status code as 200")]
        public void WhenTheTwoNumbersAreAdded()
        {
            WS.verifyStatusCode(200);
        }

        [Then("get the employeeName")]
        public void getEmployeName()
        {
      //TODO
        }
        [Then("The employee should have the following values")]
        public void ThenTheEmployeeShouldHaveTheFollowingValues(Table table)
        {

             employee_info = JsonSerializer.Deserialize<Employee>(WS.response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine(WS.response.Content);
            Console.WriteLine("ACTUAL ID : " + employee_info.data.id);
            Console.WriteLine("EMP NAME:" + employee_info.status);
            var dictionary = TableExtension.ToDictionary(table);
            string id =  dictionary.FirstOrDefault(x => x.Key == "id").Value;
            Console.WriteLine("EXPECTED ID : " + id);

        }


        [When("the Customer try to create Employee")]
        public void createEmployee()
        {
            //JObject jobj2 = new JObject();
            Console.WriteLine("USERNAME : " + getTestDataValue("UserName"));
            Create c = new Create();
            c.name = "Suresh";
            c.salary = "10000";
            c.age = "35";
            WS.setEndPoint("create");
            WS.setDefaultHeaders();
            WS.addRequestBody(c);
            WS.post();
        }
    }
}