# QPIV

An extension to the .NET DataAnnotations library with custom validation attributes to solve the most common query parameter interdependencies.

*Creating your own custom validation attributes or using [FluentValidation](https://fluentvalidation.net/ "FluentValidation") is recommended for more complex and specific validation use cases. This library covers only the **most common** query parameter interdependencies.*

**QPIV** stands for **Q**uery **P**arameters **I**nterdependency **V**alidation.


------------


![Build Status](https://github.com/cosic196/QPIV/actions/workflows/ci.yml/badge.svg)
- [What?](#what)
- [How To Use](#how-to-use)
	- [Constraints](#constraints)
	- [RequiresAttribute](#requiresattribute)
	- [OrAttribute](#orattribute)
	- [OnlyOneAttribute](#onlyoneattribute)
	- [AllOrNoneAttribute](#allornoneattribute)
	- [ZeroOrOneAttribute](#zerooroneattribute)
- [Getting Started](#getting-started)
- [Inspiration And Credit](#inspiration-and-credit)

## What?

Have you ever built a web API with these or similiar descriptions for your query parameters:
- > Required if X is not provided
- > Use either X or Y
- > X or Y must be set
- > Mutually exclusive with X

If so, then you have dealt with query parameter interdependencies or inter-parameter dependencies in web APIs.

And if you&#39;re building your API in **.NET** then you&#39;re in luck because **QPIV** gives you multiple validation attributes for validating these kinds of dependencies.


## How To Use
QPIV gives you access to 5 custom validation attributes:
- Requires
- Or
- OnlyOne
- AllOrNone
- ZeroOrOne

### Constraints:
- All of the attributes are class attributes.
- All of the attributes require non-null strings as input.
- All of the attributes support multiple parameter names as input.
- If an input string is not a field/property name of the attributed class, the attribute will throw a MemberAccessException.
- QPIV is using reflection to get field/parameter information and it creates a cache of needed field/property information on the first request


------------


### RequiresAttribute
The presence of a *targetParameter*  requires the presence of other *parameters*.

First parameter name given in the constructor of a RequiresAttribute is the *targetParameter*&#39;s name.

**Example:**
```cs
[Requires(nameof(X), nameof(Y))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string z;
}
```
:x:An instance of the input model above is not valid if X has a value and Y is null.  
:heavy_check_mark:An instance of the input model above is valid if X is null.  
:heavy_check_mark:An instance of the input model above is valid if both X and Y have values.  

```cs
[Requires(nameof(z), nameof(Y), nameof(X))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string z;
}
```
:x:An instance of the input model above is not valid if z has a value and Y is null.  
:x:An instance of the input model above is not valid if z has a value and X is null.  
:x:An instance of the input model above is not valid if z has a value and both X and Y are null.  
:heavy_check_mark:An instance of the input model above is valid if z is null.  
:heavy_check_mark:An instance of the input model above is valid if all of z, X and Y have values.  


------------


### OrAttribute
 Given a set of *parameters* one or more of them must be included.
 
 **Example:**
 ```cs
[Or(nameof(z), nameof(Y), nameof(X))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string z;
}
```
:x:An instance of the input model above is not valid if all of z, Y and X are null.  
:heavy_check_mark:An instance of the input model above is valid if any of z, Y or X have values.  


------------


### OnlyOneAttribute
Given a set of *parameters* one, and only one of them must be included.

 **Example:**
 ```cs
[OnlyOne(nameof(Y), nameof(z), nameof(X))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string Z;
}
```
:x:An instance of the input model above is not valid if more than one of Y, z and X have values.  
:heavy_check_mark:An instance of the input model above is valid if only one of Y, z and X has a value.  


------------


### AllOrNoneAttribute
Given a set of *parameters* either all of them are provided or none of them.

 **Example:**
 ```cs
[AllOrNone(nameof(z), nameof(Y), nameof(X))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string Z;
}
```
:x:An instance of the input model above is not valid if some of z, Y and X are null and some have values.  
:heavy_check_mark:An instance of the input model above is valid if all of z, Y and X are null.  
:heavy_check_mark:An instance of the input model above is valid if all of z, Y and X have values.  


------------


### ZeroOrOneAttribute
Given a set of *parameters*  zero or one can be present.

 **Example:**
 ```cs
[ZeroOrOne(nameof(z), nameof(Y), nameof(X))]
public class InputModel
{
    public int? X { get; set; }
    public double? Y { get; set; }
    public string Z;
}
```
:x:An instance of the input model above is not valid if more than one of z, Y and X have values.  
:heavy_check_mark:An instance of the input model above is valid if all of z, Y and X are null.  
:heavy_check_mark:An instance of the input model above is valid if all of z, Y and X have values.  

## Getting Started
Install **QPIV.ValidationAttributes** NuGet package from nuget.org to use QPIV custom validation attributes.

```
dotnet add package QPIV.ValidationAttributes
```

[![NuGet](https://img.shields.io/nuget/v/QPIV.ValidationAttributes.svg)](https://nuget.org/packages/QPIV.ValidationAttributes)

## Inspiration And Credit
OpenAPI doesn&#39;t support specifying these kinds of dependencies.

Developers have been requesting it for a long time. There is an active issue opened all the way back in 2015 (https://github.com/OAI/OpenAPI-Specification/issues/256).

A [comment](https://github.com/OAI/OpenAPI-Specification/issues/256#issuecomment-547569202 "comment") from the mentioned discussion/issue has sparked interest and was the main inspiration for QPIV.  
Someone from a research group called [ISA Group](https://www.isa.us.es/3.0/ "ISA Group") commented on the issue and has given links to:
- [a paper they&#39;ve published regarding this issue](https://personal.us.es/amarlop/wp-content/uploads/2019/10/A-Catalogue-of-Inter-Parameter-Dependencies-in-RESTful-Web-APIs.pdf "a paper they've published regarding this issue")
- [their resulting dataset](https://drive.google.com/file/d/1VoD2iaiqOCTHyaS6Q6Xa0SB-3KVn4X-_/view "resulting dataset")

This made it much easier to spec out needed validation attributes and also *give them names* (the dread of every developer). So big thanks goes to [AML14](https://github.com/AML14 "AML14") and the rest of the ISA Group.
