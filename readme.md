# Release Details

## Installation
Clone this repository and execute the INSTALL.SH script.
The following steps can be followed:
- Automated test execution with results
- Docker build will automatically download images and dependencies from Docker Hub
- Docker execution. At this point you will receive the exposed address and port to access the application
- If Google Chrome is installed on the machine, the browser will be launched and display the API Documentation

## API Documentation
- Documentation is exposed using Swagger and allows the user to test all endpoints with pre-defined sample data
- Click on each endpoint to read details
- API authentication is made using Auth0
- Create your account using the /api/SignUp endpoint
- Sign in at the endpoint /api/SignIn using your credentials. The return value of the call is the API Token, already formatted
- At the top right of the page you can click in the 'Authorize' button and paste the API Token received in the Sign In
- Now you can test other endpoints that are secured by Auth0
- Use the 'Try it out' button when expanding a endpoint to test the API and check the CURL version of the call

## Project details
- The Database container is running a Microsoft SQL Express instance with all tables already created
- A image can be found at the Docker Hub (https://hub.docker.com/r/luizrodolpho/portal-telemedicina-db/)
- The solution is build to run on Windows
- The Unit Test project was created using xUnit and the mock data is created using the EntityFramework InMemoryDatabase instead of other common frameworks
- Domain methods details are described as a Summary in each respective interface.

# C# Test for candidates


## Technical Requisites

The usage of the following are demanded
- Language C# Latest Version
- .Net Core Latest Version
- Auth0 Authentication and Authorization 
- Entity Framework
- Sql Server
- Docker


## Main Tasks 

> 1. Create the following non-authenticated service endpoints
>  * `/signin` - *POST* - receiving an user name and a password
>  * `/signup` - *POST* - receiving an user full display name, an user name, a password and an e-mail address. Upon save time, add the current date and time to the database. An unique id must be created and used throughout the `/order/` POST endpoint described later in this document

> 2. Create the following authenticated service endpoints
>  * `/users` - *GET* - return all inserted users considering the following search and filter parameters:
>    * search and filter by user name 
>    * search and filter by all or parts of the full display name
>    * search and filter by an interval of creation dates
>    * search and filter by e-mail address
>    * all search criterias can be infinitely combined
>    * all singular textual search criterias need to support either ascending and descending sorting

>  * `/products` - *GET* - return all inserted products considering the same search requisites from previous task
>  * `/product/` - *POST* - insert a new product to the product table with the following fields: id, name, description, price, creation date
>  * `/product/` - *PUT* - update all passed fields in its appropriate record
>  * `/order/` - *POST* - inserts an order receiving an user id and a list of products id with the current price and quantity
>  * `/orders/` - *GET* - returns all orders from the database. In here you are free to determine what and how your endpoint should return the data.


## Services Requisites
- Run all services in a single docker container
- All endpoints must have automated unit tests that will prove the requisites are implemented
- Use as many design patterns and best practices as you see fit
- All database interactions should be done with Entity Framework
- Use async methods anywhere you find it is needed


## Database Requisites
- You are the responsible the create the database structure as best as you can think of. Conciseness, coherence and best practices are going to be considered
- Microsoft Sql Server must be running in a separate container image from the services one


## Running and Executing Requisites
- Make your project running with the minimum needed interactions will be considered important in the analysis of your performance.
- Make it as easy as possible
- The ideal scenario will be to clone your repository and execute it through a single command such as `./INSTALL` or `./RUN`
- Considering this is a C# test, the deployment can be supported only at Windows SO running machines or virtual machines but it will be considered a plus if you can manage to make it work at linux and/or mac osx OS's as well


## Documentation Requisites
- It can be done in portuguese although being in english, french or spanish will also be considered a plus
- Should be easy to read and understand the usage (from a client developer's perspective) of your services
- Must include at least one example of `curl` or `wget` usage of all your endpoints
- It should be  easy to undertand how to execute your tests

## Last Requisites
- Use your github to deliver this test
- You can fork from here to get started
- Try to keep your commits to a reasonable atomic capacity
- Use as much best practices you see fit to address the commits and/or branch naming 

Feel free to ask me (*Marco Santana* - **marco@portaltelemedicina.com.br**) any question.

You have 5 days counting from tomorrow to finish and deliver us the address of your github repository. Please, let us know if you need more time.



*Thank you for giving us this opportunity to get to know you and your work.*




