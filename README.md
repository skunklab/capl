Claims Authorization Policy Language
====================================

----------
```
author: Matt Long
email: theskunklab@gmail.com
specification: http://www.authz.org 
```
Introduction
--------
Claims Authorization Policy Language (CAPL) is a security token agnostic, serializable, logic and claims-based language for distributed authorization.  CAPL allows an author to create simple or complex expressions that make authorization decisions and distribute those policies to applications.  Applications use the policies to make authorization decisions in the context of the application.  CAPL policies are designed to be simple for applications to use and execute very quickly.

CAPL was designed with the following parameters: 
simple for applications to use 
execute very quickly  
easy to distribute  
easy to cache 
extensible  
mathematically consistent


 ```
 <AuthorizationPolicy PolicyID="http://www.example.org/policy/rbacId" Delegation="false" xmlns="http://schemas.authz.org/capl">
     <Rule Evaluates="true">
         <Operation Type="http://schemas.authz.org/capl/operation#Equal">manager</Operation>
         <Match Type="http://schemas.authz.org/capl/match#Literal" ClaimType="http://www.example.org/claims/role" Required="true" />
     </Rule>
</AuthorizationPolicy>
```
The serialized policy states that the identity must have a claim type of 'http://www.example.org/claims/role' with a value of 'manager'.  

Code Example of this policy
```
EvaluationOperation operation = new EvaluationOperation(EqualOperation.OperationUri, "manager");
Match match = new Match(LiteralMatchExpression.MatchUri, roleClaimType);
Rule rule = new Rule(match, operation);
Uri rbacPolicyId = new Uri("http://www.example.org/policy/rbacId");
return new AuthorizationPolicy(rule, rbacPolicyId);
```



##Table Contents##
TBA






