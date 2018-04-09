This project demonstrates creating a plugin assembly that allows the user
to select a different starting page when logging into Encompass.

To demonstrate this capability you need to fo the following:

1) Open the References for the project and, if the the EncompassObjects and 
   EncompassAutomation assembly references are broken, remove them and then
   re-add them. The assemblies will be located in your Encompass installation
   folder (e.g. C:\Program Files\Encompass).
   
2) Build the assembly to the file DefaultScreenPlugin.dll.

3) Copy only the DefaultScreenPlugin.dll file to the EncompassData\Data\Plugins
   folder on your Encompass Server (or, if your are running in Offline mode,
   the corresponding folder on your local machine).
   
4) Run Encompass and log in. You should get prompted for a new startup screen.
   If you exit Encompass and come back in, you will go directly to that screen
   without being prompted.
   
 
