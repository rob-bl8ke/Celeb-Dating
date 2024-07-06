# .NET Web API

- Use `Developer: Reload Window` to solve 99% of your problems in VS Code.
- To see what changes you've made to your VS Code Settings, open Settings and look for the "Open Settings (JSON)" button. This will show you all the global settings that you've made to VS Code.

```
dotnet run
```

- Check Swagger: `http://localhost:5151/swagger/index.html`.
- [Learn more about configuring Swagger/OpenAPI](https://aka.ms/aspnetcore/swashbuckle)

### Windows Security (Defender) blocking the executable.
```
Unhandled exception: System.ComponentModel.Win32Exception (5): An error occurred trying to start process ... . Access is denied.
```
- Go to Windows Security -> Virus and Threat protection settings -> Exclusions and add your exe to the exclusion list.

## Required Developement Software
- `node.js`
- [Postman]
- [NVM for Windows](https://winget.run/pkg/CoreyButler/NVMforWindows)

### VS Code Extensions

- [C# DevKit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) - One of the cool things added by this extension other than C# support is the "Solution Explorer" window.
- [Nuget Gallery](https://marketplace.visualstudio.com/items?itemName=patcx.vscode-nuget-gallery) - It will create a new tab in your terminal window "NUGET". Use this window to manage packages.

Use `dotnet new list` to see a list of project templates you can use to create .NET projects.

```
dotnet new sln
dotnet new webapi -n API
dotnet sln add API/API.csproj
```

## Development Commands

```
dotnet watch
```
`dotnet watch` enables hot reload (unless you've disable the feature in your `launchSettings.json`). Doesn't always work correctly. To run the API Server you'll use `dotnet watch --no-hot-reload`. To update the database after making changes you'll use `dotnet ef database update`. To run Angular you'll use use `ng serve`.

To disable hot reload you can try going to `Properties/launchSettings.json` and add the following piece of JSON. However this currently doesn't seem to work.

```json
{
  "profiles": {
    "api": {
		...
      "hotReloadEnabled": false,
		...
    }
  }
}
```

So the option is just using `--no-hot-reload`.

### .NET Core SDK

Use `dotnet --info` to get information for your current SDK version and its build information as well as some information about your system. Other .NET SDK installations are also shown.

Use `dotnet sdk check` to check the current support status, version status, and patch information for your installed .NET SDKs.

If you do have multiple versions of .NET installed and you wish to use a specific version that isn't the latest version you can specify this in a `global.json` file. You can generate this file using `dotnet new globaljson` in your project folder.

It creates a file like:

```json
{
  "sdk": {
    "version": "7.0.101"
  }
}
```

### Self-signed Certificate

.NET should install a self-signed certificate when the SDK is installed so that it's trusted by the browser but if you're getting certificate problems try running:

```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```
You'll need to run the above commands as an administrator.

## Installing Entity Framework and SQLite

Ensure to install the following two packages:

- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Design` - Code-first.

### When using code-first EF is used for...
Performing database operations such as querying, change tracking, saving, concurrency (default is optimistic concurrency), transactions, caching, built-in conventions, and database schema migrations.

### SQLite Extension
Install the SQLite extension by `alexcvzz` to view your database. You'll open the command palatte with CTRL+SHIFT+P... And choose the option to open a database. Your database will show up in the list if you've already done a `dotnet ef database update` and you'll be able to access it in your explore pane. Look for SQLite Explorer.

Take a look at `appsettings.Development.json` to see how the connection string is added.

Open the SQLite explorer by using the `CTRL+SHIFT+P` shortcut. Pick our database. You'll see a pane appear under your explorer pane in VS Code.
- Righ click on a table to view, query, etc.

### Data Migrations

- [`dotnet-ef` on `nuget`](https://www.nuget.org/packages/dotnet-ef/)
- Use `dotnet tool install --global dotnet-ef --version` in order to install `dotnet-ef`. You'd normally want to install this globally unless you need a specific version for a specific project.
- Use `dotnet tool list` to get the listing of tools. This will display the `dotnet-ef` version if one is installed. Use `dotnet tool list -g` to see the global tool list. `dotnet tool list --local` to list all locally installed tools.
```
dotnet tool install --global dotnet-ef --version 8.0.6
dotnet ef migrations -h
dotnet ef migrations add InitialCreate -o Data/Migrations
```
If you get the following error (which you've done twice in a row now):

```
SQLite Error 1: 'no such table: __EFMigrationsHistory'.
```
Take a good look at where you've added the `ConnectionStrings` property. Twice you've added it under "Logging" in `appsettings.Development.json`.
```
"ConnectionStrings": {
  "DefaultConnection": "Data source=datingapp.db"
}
```
For whatever reason, you may wish to use a specific version of dotnet-ef for a project (perhaps you’re learning something on Udemy). You can set up a manifest file for your solution. Take a look at [this article](https://learn.microsoft.com/en-us/dotnet/core/tools/local-tools-how-to-use). When you create a new tool manifest it will create a file here: .config\dotnet-tools.json and add your version to it.

The commands that you can use are also listed on the [official nuget](https://www.nuget.org/packages/dotnet-ef/) dotnet-ef site.

### Updating the database

Before running the `dotnet-ef database update` it will be useful to make sure your solutions builds with a `dotnet build`.

# Angular

```
ng new client
```
```
? Which stylesheet format would you like to use? CSS             [ https://developer.mozilla.org/docs/Web/CSS                     ]
? Do you want to enable Server-Side Rendering (SSR) and Static Site Generation (SSG/Prerendering)? (y/N) N
```
```
? Which stylesheet format would you like to use? CSS             [ https://developer.mozilla.org/docs/Web/CSS                     ]
? Do you want to enable Server-Side Rendering (SSR) and Static Site Generation (SSG/Prerendering)? No
```

```
cd client
ng serve
```

## Standalone Components

From Angular 16, the default is to use stand-alone components instead of the old modular style where one would declare all components inside an app module (`app.module.ts`).

# References

- [`DatingApp`](https://github.com/trycatchlearn/datingapp) and [here on Udemy](https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch)
  - also take note of the [legacy code for Angular 16](https://github.com/TryCatchLearn/DatingApp-DotNet7Angular16)

### Angular Links

- [Angular Compatibility Guide](https://angular.dev/reference/versions) - Angular, Node, TypeScript and RxJs compatibility sets.
- [Angular versioning and releases](https://angular.dev/reference/releases)
- [npm Semantic Versioning](https://docs.npmjs.com/about-semantic-versioning) - How it works and what the carats and squigglies mean.

### Node Packages
- [ng-gallery](https://www.npmjs.com/package/ng-gallery)

