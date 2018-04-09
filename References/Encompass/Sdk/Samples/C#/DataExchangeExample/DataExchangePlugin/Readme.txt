This project demonstrates creating a plugin assembly that uses the DataExchange event to
receive data from an external application. It then allows the user to create a new loan using
the data received.

To demonstrate this capability you need to do the following:

1) Open the References for the DataExhangePlugin project and, if the the EncompassObjects and 
   EncompassAutomation assembly references are broken, remove them and then
   re-add them. The assemblies will be located in your Encompass installation
   folder (e.g. C:\Program Files\Encompass).
   
   Repeat this process for the DataExchangeApp program, although this program
   depends only on the EncompassObjects assembly.
   
2) Build the DataExchangePlugin assembly to the file DataExchangePlugin.dll.

3) Copy only the DataExchangePlugin.dll file to the EncompassData\Data\Plugins
   folder on your Encompass Server (or, if your are running in Offline mode,
   the corresponding folder on your local machine).
   
4) In the DataExcchangeApp/DataExchangeForm.cs file, modify the code in 
   btnSend_Click to specify a valid Encompass server name, login ID and password.
   
5) Run Encompass and log in to your Encompass Server. The server you use here
   must be the same one you specified in step #4 above. 

6) Compile and run the DataExchangeApp. In the "Send To" field enter the User ID
   with which you logged into Encompass in step #5. Click Send.
   
When you do this, you should receive a message in Encompass asking if you would like to
create a new loan. If you say Yes, a new loan will be started and the data from the
DataExchangeApp will be loaded into the form.


   

   
 
