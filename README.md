# What is this project?

This is a map repository I created using ASP.NET Core MVC the web app allows the user to create a map, a map don't have much meaning as of yet but it consists of a name a description and an image, after user submits the map it will be shown in the listing of the front page as well as under the "My Maps Uploaded" page, user can then also edit map.

### Features
 - **Custom User entity and basic User authentication**, and implementation of IdentityScaffolder for several user management pages such as Login/Register etc...
 - **Implemented Policies via AuthorizationHandler and Requirements**, for when an entity does not belong to a user e.g: A Map that belongs to a User can't be edited by someone else that is not the creator of the Map.
 - **Implemented EmailSender service with SendGrid**, configured secrets.json for Development sessions, and configured Azure Key Vaults, using a Managed User Identity to retrieve key values for Production sessions.
 - **Created Controller with Views** for entities, and implemented custom ViewModels for Map for when displaying an image in the View or bind to POST method
 - **Integrated Custom Libraries**, installed webcompiler for sass to replace site.css to site.scss and installed bootstrap-sass using libman for css isolation see Shared/Components/MapCard.razor to see a sample.