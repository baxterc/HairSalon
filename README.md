# HairSalon

#### A scheduling database for a hair salon.

#### By Charlie Baxter

## Description
This site uses a database to allow users to create, view, update and delete stylists, clients, and appointments at a hair salon.  Each client is assumed to have only one stylist.  Appointments are linked to both a client and to that customer's stylist.

## Setup
* Clone this repository
* Install a C# framework such as Mono, which is available from [www.mono-project.com](http://www.mono-project.com/)
* Install Microsoft's SQL Server Management Studio, available from [this link](https://msdn.microsoft.com/en-us/library/mt238290.aspx).
* Navigate to the project folder using your terminal/shell and run the command "dnu restore" to load dependencies.
* Open the SQL Server Management Studio and open the file "hair_salon.sql" in the project folder.
* On top of the script text that should load, add the following lines:  
###### CREATE DATABASE [your_database_name]  
###### GO  
* Save the file that you added the text to.
* Click "Execute", located next to a red exclamation point.
* Back in your command prompt, type the command "dnx kestrel" to load the project.  The following message should appear:   
Hosting environment: Production   
Now listening on: http://localhost:5004   
Application started. Press Ctrl+C to shut down.
* In your web browser, type in http://localhost:5004
* Once finished, type control-C in your terminal or shell.

## Technologies Used
* C#
* Nancy
* Razor
* HTML

## Known Bugs
* Testing in Xunit showed that deleting appointments and updating client information do not update consistently, but this has not been replicated on the actual page.

## Contact & Support
If you run into any issues with this page, have any questions, ideas, or concerns, feel free to email me at charlie.r.baxter@gmail.com.

## Legal
Copyright (c) 2016 Charlie Baxter.  This software is licensed under the MIT License.
