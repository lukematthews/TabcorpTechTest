# .NET C# Technical Test
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
