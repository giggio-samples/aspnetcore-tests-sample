# Sample ASP.NET Core + Angular project with tests

A project to help demonstrate how to do unit, integration and acceptance
tests with an web api project using ASP.NET Core and Angular 7 front end.

## Running the tests

From the terminal, in the project root, simply run:

```shell
dotnet test
```

Or run them from each test project directory (on the `test` subdir) or
directly from Visual Studio.

## Running the api app

Run `dotnet run` from `src/SampleApp`.

## Running the web front end

First run the api app, then run `npm start` from `src/FrontEnd`.

## Supported .NET SDK and CLI versions

This was compiled with the v2.2.2 sdk (dotnet version 2.2.104). Anything after
that should run. To try different versions, simply remove `global.json` from the root.

[Download the SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2).


## Contributing

Questions, comments, bug reports, and pull requests are all welcome.  Submit them at
[the project on GitHub](https://github.com/giggio-samples/aspnetcore-tests-sample).

Bug reports that include steps-to-reproduce (including code) are the
best. Even better, make them in the form of pull requests.

## Author

[Giovanni Bassi](https://github.com/giggio)

## License

Licensed under the Apache License, Version 2.0.
