# BridgeIntServices
From time to time, our in-country tech teams are called upon to perform school visits to address issues that they find. We collect a bunch of data on an on-going basis about these devices and we’re interested in building a tool to identify teacher tablet batteries that are in need of replacement.

This service helps to provides the average daily battery usage for each tablet across the period, which will be used by various web and mobile clients.

## Contents
- [Prerequisites](#prerequisites)
- [Clone and Building Project](#cloneandbuildingproject)
- [Running Tests](#runningtests)

## Prerequisites
You need to install/Configure:
- Dotnet core version 3.0 or above.
- Visual Studio 2019 / Visual studio code.
- Daniel Palme’s ReportGenerator.(https://danielpalme.github.io/ReportGenerator/usage.html)
- Microsoft.CodeCoverage.(https://www.nuget.org/packages/Microsoft.CodeCoverage/)

## Clone and Building Project
Clone this repo to your local machine using https://github.com/dhirutech/BridgeIntServices

> To clone and build the the project

```shell
git clone <path>
dotnet build
```
> To run the project
```shell
dotnet run
```
This will start local developement server to test the service directly from Swagger UI or can be tested from Postman.

## Running Tests

> To run unit test cases
```shell
dotnet test <Path to *.csproj file> --results-directory:<Test Result directory> --collect:"Code Coverage"
```
This will generate *.coverage file inside a folder whose name corresponds to a GUID. Right now dotnet core CLI do not support custom name for *.coverage file.

> Convert *.coverage file to *.coveragexml file
```shell
<UserProfile>\.nuget\packages\microsoft.codecoverage\<version>\build\netstandard1.0\CodeCoverage\CodeCoverage.exe analyze  /output:<xml file name with Path>.coveragexml  <path to coverage file>
```
> Generate Reports using ReportGenerator
```shell
dotnet <UserProfile>\.nuget\packages\reportgenerator\<version>\tools\netcoreapp2.1\ReportGenerator.dll "-reports:<Coveragexml file path>" "-targetdir:<path to coverage report>"
```
This will generate reports in *.htm format in the given output folder. If you open the index.htm file you can view the report.
