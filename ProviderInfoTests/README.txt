Brief Description of the code.

* Used a repository pattern to carry user data from the persisten layer up into the UI layer
* Javascript was used slightly to submit the documents to the rest api (NancyFX library was used)
* The idea of a viewModel was introduced in the UI but not completely implemented
* NancyFx was used as a routing library for the Rest api. two verbs were added for this scope.
* A "business layer" is introduced and will allow the repository to be encapusalted within a repository class. Business layer will also be used to apply business rules and logic to the document model object in scope
* A repository class has been introduced but not completely implemented as it will require additional coding and time. The repository can introduce a standard patter to access different persisten options.

List of items that will require additional time and effort.

* Logging - there are numerous areas in the code that can benefit from logging. Error logging, progress logging, and debug logging
* Authentication - Authentication pseudo code was added but not implemented. We can use a token based authentication. We can provide a registration page to provide the user with a token
* Presentation Framework i.e. Angular - A full implementation of a presentation framework is out of scope in this task. But based on the potential scalability of document mangement a UI framework would be beneficial and it can also provide with additional tools to manage components
* Interface/DI - The application can also benefit from the implementation of a DI pattern and library, as there are numerous objects that are dependent on a concrete objects, and it will help with scalability and the open/closed principle


Additional idea for the current project.

This project can be expanded based on a previous project that I created for a different task. 

**Hypothetical** Use Case: 

Provider Info is a service that retrives provider details and specialties, but they are also a private list of providers working on undisclosed projects that will require users to agree on an NDA before they are able to see provider project details.

Problem: 

Need to be able to upload a signed NDA for each provider selected for viewing and stored the signed document in SalesForce tied to the user and the provider information.

Solution: 

Leverage the file upload form to submit a signed NDA document to SalesForce and use the provider id and user id to create a relationship that can be retrieved for review. This will also allow us to call on the provider info and retrieve the signed NDA document from SalesForce for that user. 
If no signed document is available, user must upload a signed NDA before they are able to see provider info project details

