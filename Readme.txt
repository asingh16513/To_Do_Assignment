Name: ToDoService

Description:
A rest api project to do CRUD operations for todoitems or lists based on user. It includes functionality to create labels which can be assigned to
items or lists. It also includes user management which only allows registered users to acess or create the respective entities. The database 
is automatically created when the application runs. The only thing which must be taken care of is connection string in appsettings.json.
It also logs each and every request/response or error if any. It can also run in container using Docker. While creating new user, the password has to be sent in
Base64Encoding. 
JWT is used for authentication. Once the user has generated token, he has to send the same of type 'Bearer {token}' in header every time
with the request to authenticate.

Instructions for use:
1. End user should create register user by using register api and get the token which will be used for authentication purpose for subsequent requests.
2. User should create labels so that it can associate the same with items and lists.
3. After that user can create item or list and associate labels to the same.