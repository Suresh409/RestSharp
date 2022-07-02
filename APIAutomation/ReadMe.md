

<!-- ABOUT THE PROJECT -->

## About The Project

This project for build a powerful testing framework to test APIs with RestSharp, SpecFlow the framework built on .Net Framework4.6.1.


### Built With
The Framework built with below main packages
* [SpecFlow](https://specflow.org/)
* [RestSharp](http://restsharp.org)
* [FluentAssertions](https://fluentAssertions.com)
* [NUnit](https://nunit.org)
* [Extent Reports][https://www.extentreports.com/docs/versions/4/net/]

<!-- GETTING STARTED -->
## Getting Started

This is instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

* Visual Studio
* Create a Unit Test Project .net framework
* SpecFlow installed in Visual Studio
* Extent reports installed in Visual Studio

### Installation
* Install Packages from Nuget Manager
1. RestSharp
2. SpecFlow
3. Nunit
4. FluentAssertions
5. Extent reports
6. RestSharp.Serializers.NewtonsoftJson 108.0.1.

<!-- USAGE EXAMPLES -->
## Usage

To run tests use Test Explorer

or run below command from terminal(view-->terminal) to run the test based on tag name 

dotnet test --filter Category=tagname


<!-- USAGE EXAMPLES -->
## Writing tests

WS class will have all RestSharp methods, in step definations we can just call any method like WS.xxxxxx 
Make sure you have set below 2 steps before addingany other steps from WS class

WS.setURL("baseUrl"); // here baseUrl is the key name from  configuration.json file where we have declared our API host url This Step will initiate RestClient object
WS.setEndPoint("endPoingKeyName") # here endPoingKeyName  is the key name from configuration.json  file. this step will initiate RestRequest Object

We can declare all the Endpoints and BaseUrl, environemnts, versions etc in configuration.json file.

We can Add all the testdata in TestData.json Under Support folder

Ex:  
"Create Employee": {   
		"UserName": "12345678
	},


	here 'Create Employee' is the test case name which is diclared in feature files.
	'UserName' is the  filed/column  name  from whre you can get the data and insert in your tests.

	you can use like getTestDataValue("UserName")  in you step defination to retrived the testdata, this method internally wil read the testcase name from feature file.
	and based on that testcase name it wil fetch the 'Username' key value











