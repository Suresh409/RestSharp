Feature: Employee Details
	this feature will validate the Create and get employe details

	
@mytag
Scenario: Get All Employees
	Given the customer is set all the headers to run the Api
	When the Customer try to view the list of customers
	Then verify the status code as 200

		
@mytag
Scenario: Get Perticular Employee Details
	Given the customer is set all the headers to run the Api
	When the Customer try to view the perticuler customer details
	| Key        | Vaue |
	| employeeId | 1    |
	Then verify the status code as 200
	#Then get the employeeName
	Then The employee should have the following values
|Key			  |Value		 |
| id              | 1         |
| employee_name   | Tiger Nixon	 |
| employee_salary | 320800        |
| employee_age    | 61           |
| profile_image	  |				 |


@mytag
Scenario: Create Employee
	Given the customer is set all the headers to run the Api
	When the Customer try to create Employee
	Then verify the status code as 200