# Event-Booking-API
This is my WEB-API project built on .NET5, which allows users registered in the system to view events and book the desired ones.
## Details
After running the code, 2 Users are automatically registered in the database, the first User with the role of admin, who has all rights. It can add new events, update and delete existing events.
And the second User is an ordinary user who can only receive services and has no right to make adjustments to existing events. He can see the events registered in the system by the admin and book them according to his ID. Also monitor own events, see his List.
## How to use
In order to use the API, it is necessary for the user to register first in order to be able to receive the services. Unauthorized user has no right to do anything before registration.
After registration, the user must log in to the system, after that a token is assigned to him, which allows him to receive services. Can view all events and book desired ones.It is also possible to search for an event by a specific location, for example Tbilisi or Batumi.

### Register Form:
/api/User/Register
{
"firstName" : "string",
"lastName" : "string",
"age" : "int",
"phone" : "string",
"userName" : "string",
"password" : "string",
}

### Login Form:
/api/User/Login
{
  "userName": "string",
  "password": "string"
}

### Events:
/api/Event/GetAllEvents   (Get all events, what are registered in the system by Admin)
/api/Event/GetEventByID/{id}   (Get event by "eventID")
/api/Event/FindEventByAddress   (Find event by "eventLocation")

### Booking:
/api/Booking/EventBooking{eventId} (Book event by "eventId")
/api/Booking/GetOwnBookingList  (Get all own booking list)

### Admin rights:
/api/Event/CreateEvent
{
  "eventName": "string",
  "eventDescription": "string",
  "eventLocation": "string",
  "eventDate": "2023-08-21T08:25:01.112Z"
}

/api/Event/UpdateEvent/{id}   (Update event by "eventId")
{
  "eventName": "string",
  "eventDescription": "string",
  "eventLocation": "string",
  "eventDate": "2023-08-21T08:28:11.941Z"
}
/api/Event/DeleteEvent/{id}   (Delete event by "eventId")

## What I have made:
I created models: User, Booking, Event. I have done validation for UserRegisterModel and Event. I used FluentValidator. I have created services and controllers for these models. I've created a TokenGenerator service that generates a token on login, conditionally specifying a 365-day token validation period. (JWT, Authentication, Mvc.NewtonsoftJson) I created Helpers, where I defined AppSettings Secret and HashSettings, which ensures that the password entered by the user is transferred to the database as a hash. (TweetinviAPI) I created a SQL database, performed migrations and connected the code to it (ORM) Entity Framework through I also added the logs and log data is automatically saved in the database in case of login. 
I tested the project with Postman and Swagger and it returned the status codes I expected. Everything works fine. 
I wrote the tests for the project on xUnit. I created fake services and tested all services.
