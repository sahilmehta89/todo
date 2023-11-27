# Todo

This project contains an API and UI for the Todo Application

## Set up your development environment

### Dependencies

This project requires you to have the following installed on your machine.

- Docker
- npm
- dotnet core cli
- Azure App Registration

### Notes for Windows

If you have issues getting Docker for windows running, checkout these docs:

- https://docs.docker.com/docker-for-windows/troubleshoot/
- https://docs.docker.com/docker-for-windows/troubleshoot/#virtualization

### Setup

1. [Register a new application](https://docs.microsoft.com/azure/active-directory/develop/scenario-spa-app-registration) in the [Azure Portal](https://portal.azure.com). Ensure that the application is enabled for the [authorization code flow with PKCE](https://docs.microsoft.com/azure/active-directory/develop/v2-oauth2-auth-code-flow). This will require that you redirect URI configured in the portal is of type `SPA`.
2. Clone this repository `git clone https://github.com/sahilmehta89/todo.git`
3. Open the [UI/src/app/app.module.ts](./src/app/app.module.ts) file and provide the required configuration values.
    1. Replace the string `"Enter_the_Application_Id_Here"` with your app/client ID on AAD Portal.
    2. Replace the string `"<Enter_the_Tenant_Info_Here>"` with tenant ID
    4. Replace the string `"Enter_the_Redirect_Uri_Here"` with the redirect uri you setup on AAD Portal.
    5. Replace the string `"Enter_the_Graph_Endpoint_Herev1.0/me"` with `"https://graph.microsoft.com/v1.0/me"`
    6. On the command line, navigate to the root of the repository, and run `npm install` to install the project dependencies via npm.

### Starting the project

If you are using OSX or a linux based operating system, you can use the Makefile to run `make` in the terminal. This will spin up a PostgreSQL docker container, start the .NET api and start the angular frontend.

If you are not using a linux/unix based operating system (ex: Windows/DOS), you should be able to use the following commands to start the app.

- Run `docker-compose up -d` in powershell or git bash. This will start a PostgreSQL database using docker.
- Run `dotnet restore` inside the `API` directory. This will install the C# dependencies needed to run the api.
- Then `dotnet run --project Todo.API` inside the `API` directory. This will start the api server.
- Inside another git bash or powershell directory, go to the `UI` directory and run `npm install`.
- After above command is complete, run `npm run start` in the `UI` directory.

You will know everything is running correctly when you go to http://localhost:4200 in your browser, and see a todo list.

## Development

We recommend below development tools:
- Visual Studio Code for Frontend (UI) development
- Visual Studio for Backend developmentt

However, you can use whatever code editor you would like. To help, here are some good Visual Studio Code extensions we recommend.

There are 5 api endpoints that need to be wired up in the api. GetAll, Get, Create, Update, and Delete.

This ToDo App has the below features:

- Shows the list of all ToDo's.
- User can create a new todo by addign the Todo title and clicking on Save button.
- User can edit a Todo by clicking on the edit button alongside each Todo. With this, a user can edit the title of the Todo.
- User can mark a Todo as done/undone by clicking on the done/undone button alongside each Todo.
- User can delete a Todo by clicking on the delete button alongside each Todo. This will soft delete the Todo in the database and remove the Todo from the frontend


If you get stuck getting your development environment setup, reach out to er.sahilmehta89@gmail.com
