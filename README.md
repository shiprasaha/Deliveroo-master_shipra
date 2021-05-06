## About 'Cron Expression Parser' Application
- Cron Parser Application is built to parse a cron expression and help you
  interpret the expression in terms of the actual time units.
- The application is built as a command line tool


## Inputs arguments for parser
The application parses the cron expression of the following format :
(Minute) (Hour) (Day of month) (Month) (Day of week) (Command)
eg : `1/15 23 5,6 * 1/5 /usr/bin/find`


## Acceptable expression symbols and meaning :
- `*` Means all possible time unit within the applicable range (eg : *)
- `-` Range of time unit between the given integral values (eg : 1-5)
- `,` Comma separated individual time units (eg : 1,5)
- `/` Time unit increments where the left is the starting point of the applicable time unit and the right is the interval till the max of the applicable time unit (eg : 1/5)

````
Note :
- All the expression and the integers provided with them will give results if they are within the applicable range of the respective time unit or will throw the applicable exception.
- Only positive Integers are accepted with or without the symbols, given they are in the range of the applicable time unit.
````

## Example input and output
`"1/15 23 5,6 * 1-5 /usr/bin/find"`

For the above expression the ouput should be :
````
Minute              1 16 31 46
Hour                23
Day of month        5 6
Month               1 2 3 4 5 6 7 8 9 10 11 12
Day of week         1 2 3 4 5
Command             /usr/bin/find
````


# To run the application:

- Navigate to the root folder of the respository (this should be where this README is living) in a terminal
- Run the following commands
  - `dotnet build`
  - `dotnet run --project Deliveroo.Cron/Deliveroo.Cron.csproj "{your cron expression}"`
    - E.g. dotnet run --project Deliveroo.Cron/Deliveroo.Cron.csproj "1/15 23 5,6 * 1-5 /usr/bin/find"

````
Note :
- If you are on MAC, and Dotnet is not available, then you will have to download and install the same
via this link : https://docs.microsoft.com/en-us/dotnet/core/install/macos?tabs=netcore2x#dependencies.
- To verify if dotnet is installed check : "dotnet --version"
```` 