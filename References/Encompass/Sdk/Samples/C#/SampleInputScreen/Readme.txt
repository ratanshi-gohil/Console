This project demonstrates creating a custom codebase assembly for an input form.
To demonstrate this capability you need to fo the following:

1) Open the References for the project and, if the the EncompassObjects and 
   EncompassAutomation assembly references are broken, remove them and then
   re-add them. The assemblies will be located in your Encompass installation
   folder (e.g. C:\Program Files\Encompass).
   
2) Build the assembly to the file SampleInputScreen.dll.

3) Open the Encompass Input Form Builder and select the Import option. 
   Browse to the "Insurance Form.emfrm" file that is included with this project.
   
4) Select the Form1 object from the Properties dropdown. Select the CodeBase 
   property and click the "...". Click the Browse button and locate the
   SampleInputScreen.dll file you built in step #2. Once selected, pick the 
   SampleInputScreen.DemoForm from the dropdown. Click OK.
   
5) The sample requires that you have a Loan Custom Field defined named
   CX.INSURANCE. To define this field, select the Tools/Custom Field Editor
   menu. Click the Add button and create a field with these specifications:
   
   Field ID:     CX.INSURANCE
   Description:  Custom Insurance Quote
   Format:       DECIMAL_2
   
   Click OK to save the field.
   
6) Save the imported form to your Encompass system. To do this, use the
   File/Save To Encompass menu. Give the form a name (such as Insurance Form)
   and save it. When you are prompted to upload the CodeBase Assembly, say
   Yes.
   
7) Open Encompass as "admin" and open any loan. The new Insurance Form should
   be listed in the Forms list. Select the form and click the Quote button.
   The custom quoting interface defined in this assembly should appear.


