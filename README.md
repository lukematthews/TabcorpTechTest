# .NET C# Technical Test

# Installation and running
* Download the code using the green "<> Code" button above. Cloning or zip is fine
* Open the solution in Visual Studio
* Run the "https" profile

Your browser should start with Swagger being shown.

The code on master at the moment has all of the tasks implemented. In order to use the application with TASK 4 implemented, you will need to create a JWT security token to use with the API calls that you are making.
* Call the "/api/Security/createToken" endpoint passing the username and password in the json body.
* Using the response of that call, pass the token in subsequent call in the authorization header "authorization: Bearer <token>"

# Implementation notes
* The database is currently an in-memory database. This was chosen to keep running it extremely simple. It performs really well on a small dataset but it is obviously not what you would pursue moving forward.
* DTO vs Domain objects. Each of the externally facing endpoints accept and return data using DTO classes. These classes are used to structure the request/response formats so that the underlying database structures are not exposed.
* Services: Using dependency injection, service classes allow us to keep controllers extremely lightweight. Looking at the Report and Transaction controllers you'll notice there are database calls. Just calls to the services
* The data type of field is string with the exception of: Transaction quantity, Transaction customer id, Product cost which are all numbers. (Cost is a decimal)
* Task 2: I was planning on using in built dotnet validation on the DTO for the transaction, but needing to know details about the product associcated with the transaction, I could not validate until after the product had been retrieved from the database.
* Task 2: "Date must not be in the past" I've interpreted this as date cannot be older than xxx minutes ago. For it to be date based, it could be configured to say that the threshold minutes are 1440 or I rewrite the validation to parse the date and make sure that the day number is the same as today.
* Task 3: "Number of transactions sold to customer from Australia" this endpoint allows you to select additional locations. If no locations are provided, it defaults to Australia
* Task 3: For the reports that return data about the customers, both count and cost are returned. A little bit more work needed there to filter the fields returned.

## TODO
* Unit testing is still being increased
* Accepting Binary transactions is a work in progress. The implementation will be using Web Sockets that will allow the user to effectively stream transactions to the server
* The user passwords are stored in plain text. This would need to be stored hashed

## Requirements
* Code must be completed within 5 days of receiving test
* Minimum .NET C# version 6
* Code must be checked into your own github account and link shared
* A working solution to the problem
* Code must have sufficient test coverage
* Add required documentation to github ReadMe file
* Clear and concise instructions on how to execute the solution
## Challenge
### TASK 1
Create a Microservice that can accept transactions in Binary or JSON format.

The Microservice will need to be capable of:
* Accepting large amount of transactions per second
* Data needs to be stored in the database for reporting (Any database can be used).

The following table details the data required in each transaction
| Field Name | Sample Data |
| --- | --- |
|Transaction Time | 2018-01-01 14:56|
|Customer ID | 104567 |
|Quantity | 1 |
|productCode | PRODUCT_001 |

The following tables will need to be created and pre-populated with data.

Customer Table
| Customer ID | First Name | Last Name | Age | Location |
| --- | --- | --- | --- | --- |
|10001 | Tony | Stark | tony.stark@gmail.com | Australia |
|10002 | Bruce | Banner | bruce.banner@gmail.com | US |
|10003 | Steve | Rogers | steve.rogers@hotmail.com | Australia |
|10004 | Wanda | Maximoff | wanda.maximoff@gmail.com | US |
|10005 | Natasha | Romanoff | natasha.romanoff@gmail.com | Canada |

Product Table
| Product Code | Cost | Status |
| --- | --- | --- |
|PRODUCT_001 |50 |Active|
|PRODUCT_002 |100 |Inactive|
|PRODUCT_003 |200 |Active|
|PRODUCT_004 |10 |Inactive|
|PRODUCT_005 |500 |Active|

### TASK 2
Add validation to the Microservice interface
* Date must not be in the past
* Total cost of transaction must not exceed 5000
* Product must be active
### TASK 3
Add some report endpoints to the microservice to the return the following information
* Total cost of transactions per customer
* Total cost of transactions per product
* Number of transactions sold to customer from Australia
### TASK 4
Add security to the Microservice
