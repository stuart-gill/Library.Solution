# Library - Stuart Gill and James Cho

##### Epicodus Independent Project C# Week 3 & 4 - 011/21/2018.

## Description

This is an app for a library. The admin should be able to add a list of the authors, and for each author, add books written by the author.

## Behavior-driven Development

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
