OsenoTaskManagementSystem API
Description: This is a task management system restful api that includes CRUD functions, JWT authentication and implements web sockets.

Database : MongoDB. Details located in appsettings.json. 
Programming Stack/ Framework: C#, Asp.NET Core 6.

Instructions on how to run project locally: 
-Clone project
-Ensure .net 6 framework is installed, build project in visual studio and restore nuget packages and run project. 
-This can be tested using Postman or Swagger.
-To access API via Swagger, click on the authorize button and enter the generated token. 
-To access API via Postman, go to header and specify authorization and then 'Bearer + [authtoken]'
-After each CRUD function, web socket sends clients a notification, streaming the newly created data in real time. 
NOTE: Due to authentication, there is no access to any task endpoint without a generated jwt. Endpoint to generate token is highlighted below.

Data Models
-Task 
->Parameters: Id (string), Name(string), Description(string), DateCreated(DateTime), IsCompleted(bool)
-AddTaskModel : Name(string), Description(string)
-UpdateTaskModel : Name(string), Description(string), IsCompleted(bool) 

-User
->contains variables: Id(string), Username(string), Password(string), DateCreated(DateTime)
-AddUserModel : Username(string), Password(string)
-UserTokenModel : UserId (string)

Endpoints
-Task (../api/Task/)

-GetAll
This gets all tasks that have been created.
--https://localhost:7004/api/Task/GetAll
Parameters: None.
Response: 200 if successful.

-GetById
This displays a task based on the task id.
--https://localhost:7004/api/Task/GetById?id=1
Parameters: id (string) 
Response: 200 if successful, 404 if not found.

-CreateTask
This creates a new task
--https://localhost:7004/api/Task/CreateTask
Parameters: None
Request: Title, Description
Response: 200 if successful, bad request if doesn't pass validation check.
Validations: 
  -Title and description cannot be null or empty. 
  -Title cannot have more than 50 characters and description cannot have more than 200. 
  -They can contain numbers. 

-UpdateTask
This updates the task title, iscompleted and description.
--https://localhost:7004/api/Task/UpdateTask?id=1
Parameters: id (string) 
Request: Title, Description, IsCompleted
Response: 200 if successful, bad request if doesn't pass validation check.
Validations: same as above

--DeleteTask
This deletes a task.
--https://localhost:7004/api/Task/DeleteTask?id=1
Parameters: id (string) 
Request: id
Response: 200 and success message.

-User (../api/User/)

-GetAll
This gets all created users.
--https://localhost:7004/api/User/GetAll
Response: 200 if successful.

-GenerateToken
This generates a user token.
--https://localhost:7004/api/User/GenerateToken
Parameters: UserId
Response: Token (String)

-Register
This registers a new user
--https://localhost:7004/api/User/Register
Parameters: Username, Password
Response: 200 if successful.
Validation:
  -Username cannot be null, empty, contain spaces or symbol.
  -Username can contain numbers and must be less than like 20. 
  -Password cannot have username in it, must not be less than six or greater than 20 and must have at least one number, one lowercase, one uppercase. 
