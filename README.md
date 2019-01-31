# Library - Stuart Gill and James Cho

##### Epicodus Independent Project C# Week 3 & 4 - 011/21/2018.

## Description

This is an app for a library. The admin should be able to add a list of the authors, and for each author, add books written by the author.

## Behavior-driven Development
1. As a library admin, I want to be able to add a book to circulation. 
2. Every book should have a title and at least one author. 
3. I want to be able to add additional authors to any given book already in the system
4. I want to be able to search for books by author
5. I want to be able to search for books by title
6. I want to be able to view all books
7. I want to be able to view all authors
8. If a book already exists in circulation, when I add another book with the same author and title, I want to note that this book now has one additional copy in circulation. 
9. I want to be able to add a patron to the system
10. I want to be able to check out a copy of any book to a patron, and have that checked out copy no longer available to be checked out 
11. I want to be able to see a list of patrons
12. I want to be able to search for patrons by name
13. I want to be able to see which books a patron has checked out. 
14. I want to be able to return a copy of a book to circulation when the patron has returned it
15. For any given book, I want to see its author or authors, and be able to find all other books by this author or authors. 
16. For any given author, I want to see a list of books by this author, and be able to click on any book and find out more information about this book, including additional authors
17. I want copies of a book to be unique-- i.e., every copy of a book has its own unique ID, allowing its history to be tracked. 

-

## Setup/Installation Requirements

1. Clone this repository:

```
    $ git clone https://github.com/stuart-gill/Library.Solution
```

2. Change into the work directory::

```
    $ cd Library.Solution
```

3. To edit the project, open the project in your preferred text editor.

4. To run the tests, move into the Test directory and run this command:

```
    $ dotnet test
```

## Re-creating MySql Database on Mac

1. Open MAMP and hit Start Servers.

2. Open "Tools" from the nav bar and select PHP Admin

3. Create a new database with the name "library"

4. In the database "library", click on the "import" tab and select the "library.sql" file in the project directory.

5. Once this is completed, return to the terminal and run

```
    $ cd library   [or make sure you are in the Library.Solution/library folder]

    $ dotnet restore

    $ dotnet run
```

6. Then follow the link provided to the local server, where you will find the app running in your browser.

## Known Bugs

- none at this time

## Support and contact details

Please contact us on github for more information and/or feedback.

## Technologies Used

- C#
- .NET Core 1.1
- MySql
- MAMP
- Git
- GitHub

### License: MIT.

#### Copyright (c) 2018 Stuart Gill and James Cho
