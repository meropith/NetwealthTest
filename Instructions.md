#Instructions: 
In order to run the project locally you need to deploy the database and change the connection strings in the appsettings.json files for both the api and the function projects.
After you have deployed the db and updated the connection strings, you need to run the azure function in order to populate the rates table in the database. 
Then you can run an instance of the Exchange.API project and an instance of the Exchange.UI. 

There are 3 tiers of users:
* API user
* DB User
* Free user

The logins are:
tierdb@tierdb.com
tierapi@tierapi.com
free@free.com

pwd is the same for all: 159632478A@a

The only tier that can work without running the function is the api tier. 

