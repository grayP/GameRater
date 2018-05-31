# GameRater

Open in Visual Studio.
Build - Nuget should restore all the packages.

Then run the SQL script which will build the database.
Check the Connection string values in the web.config, you will have to change to your PC name.

If the ASP.Net user tables are not automatically created then open the Package Manager Console and Run these two commands:
Enable-Migrations
Update-database

To use:
Opens by default to /OnlineGame/Index

Register as a User and then when you view the games you should see the Add and Delete button.

I have not put in the code yet to associate games with a user, currently all games go in with a UserId of 1.
