# Event-Booking-API
This is my WEB-API project built on .NET5, which allows users registered in the system to view events and book the desired ones.
## Details
After running the code, 2 Users are automatically registered in the database, the first User with the role of admin, who has all rights. It can add new events, update and delete existing events.
And the second User is an ordinary user who can only receive services and has no right to make adjustments to existing events. He can see the events registered in the system by the admin and book them according to his ID. Also monitor own events, see his List.
## How to use
In order to use the API, it is necessary for the user to register first in order to be able to receive the services. Unauthorized user has no right to do anything before registration.
After registration, the user must log in to the system, after that a token is assigned to him, which allows him to receive services. Can view all events and book desired ones.It is also possible to search for an event by a specific location, for example Tbilisi or Batumi.

### Register Form:
https://localhost:44344/api/User/Register
{
"FirstName" : "string",
"LastName" : "string",
"Age" : "int",
"Phone" : "string",
"userName" : "string",
"password" : "string",
}

### Login Form:
https://localhost:44344/api/User/Login
{
  "userName": "string",
  "password": "string"
}

### Events:
https://localhost:44344/api/Event/GetAllEvents   (Get all events, what are registered in the system by Admin)
https://localhost:44344/api/Event/GetEventByID/{id}   (Get event by "eventID")
https://localhost:44344/api/Event/FindEventByAddress   (Find event by "eventLocation")

### Booking:
https://localhost:44344/api/Booking/EventBooking{eventId} (Book event by "eventId")
https://localhost:44344/api/Booking/GetOwnBookingList  (Get all own booking list)

### Admin rights:
https://localhost:44344/api/Event/CreateEvent
{
  "eventName": "string",
  "eventDescription": "string",
  "eventLocation": "string",
  "eventDate": "2023-08-21T08:25:01.112Z"
}

https://localhost:44344/api/Event/UpdateEvent/{id}   (Update event by "eventId")
{
  "eventName": "string",
  "eventDescription": "string",
  "eventLocation": "string",
  "eventDate": "2023-08-21T08:28:11.941Z"
}
https://localhost:44344/api/Event/DeleteEvent/{id}   (Delete event by "eventId")

## What I have made:
I created models: User, Booking, Event.
I made validations for UserRegisterModel and Event. I used FluentValidator.
I created services and controllers for these models.
I created a TokenGenerator service that generates a token when logging into the system, conditionally specifying a 365-day token validation period.(JWT,Authentication,Mvc.NewtonsoftJson)
I created Helpers, where I defined AppSettings Secret and HashSettings, which ensures that the password entered by the user is transferred to the database as a hash.(TweetinviAPI)
I created a SQL database and connected the code to it (ORM) through the Entity Framework. I have also added logs, and in case of logging in, the log data is automatically stored in the database.
I tested the project with Postman and Swagger and it returned the status codes I expected. Everything works fine.
I wrote tests for the project on xUnit. I created Fake Services and tested all services.
