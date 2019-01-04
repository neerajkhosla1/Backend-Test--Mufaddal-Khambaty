# Project Title

Created a MVC WebApi application with Entity Framework (code first) with the following endpoints.

GET /books - get all books
GET /book/{id} - get book by id
DELETE /book/{id} - delete book (should not delete item in DB, only mark it is deleted)
POST /book/{id} - create book
PATCH /book/{id} - update book (partial, update only fields which was sent)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development purposes.
* Download the project from the URL.
* Place into your local machine.
* Build and run the Project.
* Database and tables will create automatically.

### Prerequisites

* Need Latest Visual Studio. This application build into visual studiio 2017 environment
* Microsoft SQL Server

### What have done

* Used the Code First approach.
* Used the IHttpActionResult class to response data.
* Book Id is not AutoIncremented, Need to send bookid in POST Method as well. As per mentioned in the job "POST /book/{id} - create book".
* In Patch request(Partial Update), I have updated only the two fields.
* For author I have created a seperate table instead of adding comma seperated in the same field for scalability and best solution.

#Requests 

1. GET /books - get all books

Url: http://localhost:50253/books
Method: GET
Response: [
    {
        "id": "f8fcb5b4-a13a-41b5-99eb-b1165f3b8a18",
        "name": "Test Book 7",
        "numberOfPages": 74,
        "dateOfPublication": 636821102126594895,
        "authors": [
            {
                "name": "Samual"
            },
            {
                "name": "Glen"
            }
        ]
    },
    {
        "id": "1ac49ef6-92f5-40c0-9371-ee0135924829",
        "name": "Times",
        "numberOfPages": 3,
        "dateOfPublication": 636821119986354818,
        "authors": [
            {
                "name": "John"
            },
            {
                "name": "Rex"
            }
        ]
    }
]

2.  GET /book/{id} - get book by id

http://localhost:50253/book/1ac49ef6-92f5-40c0-9371-ee0135924829
Method: GET
Response:

{
    "id": "1ac49ef6-92f5-40c0-9371-ee0135924829",
    "name": "Times",
    "numberOfPages": 3,
    "dateOfPublication": 636821119986354818,
    "authors": [
        {
            "name": "John"
        },
        {
            "name": "Rex"
        }
    ]
}

3. DELETE /book/{id} - delete book (should not delete item in DB, only mark it is deleted)

URL: http://localhost:50253/book/1ac49ef6-92f5-40c0-9371-ee0135924829
Method: DELETE
Response:
"Book 1ac49ef6-92f5-40c0-9371-ee0135924829 deleted successfully."


4. POST /book/{id} - create book
URL: http://localhost:50253/book/f9fcb5b4-a13a-41b5-99eb-b1165f3b8a15
Method: POST
Request: 
{
      
        "name": "Test Book2 7",
        "numberOfPages": 742,
        "dateOfPublication": 636821102126594895,
        "authors": [
            {
                "name": "Samual"
            },
            {
                "name": "Glen"
            }
        ]
    }
Response: 
"Book f9fcb5b4-a13a-41b5-99eb-b1165f3b8a15 created successfully."


5. PATCH /book/{id} - update book (partial, update only fields which was sent)
URL: http://localhost:50253/book/f9fcb5b4-a13a-41b5-99eb-b1165f3b8a15
Method: PATCH
Request: 
 {
      
        "name": "Test Book 7",
        "numberOfPages": 74
        
 }
Response: 
"Book f9fcb5b4-a13a-41b5-99eb-b1165f3b8a15 updated successfully."


