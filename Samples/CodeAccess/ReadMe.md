Code Access Security Sample
====================================
---



##Introduction##
The sample demonstrates the use of CAPL to provide declarative code access security on a method.  The sample uses a declarative attribute, CaplCodeAccessAttribute, derived from the CodeAccessSecurityAttribute to perform access control, i.e., allowing or denying access to a method.  

##Instructions##
Open the Sample.CodeAccess solution found in capl/samples/codeaccess folder.

- Compile the solution
-  Run the solution in debug mode
    -  A Web app and 2 console apps with open
- Select the Policy Manager console app and press enter
- Select the Code Access Policy console app and press enter
    - The app should continue to call the method showing that the method was executed.
- Select the Policy Manager console app and press "Y" to change the policy.
    - Enter "foo" at the prompt and press Enter
- The Code Access Policy app should show the method cannot be called due to not authorized.
- Select the Policy Manager console app and press "Y" to change the policy.
    - Enter "manager" at the prompt and press Enter
- The Code Access Policy app should the method can now be accessed again.







