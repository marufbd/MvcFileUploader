MvcFileUploader
===============

A nuget package for easy blueimp JQuery file upload plugin integration into an ASP.NET Mvc application


Utilises:
	* [jQuery File Upload plugin](http://blueimp.github.com/jQuery-File-Upload/)  
	* [twitter bootstrap](http://twitter.github.com/bootstrap/) for javascript for modal dialog load from ajax


MvcFileUploader Provides Html helper and file saving utility to keep your code clean

Features:
	* The frontend view gets included into the project. So provides full control to design.
	* Configure filetypes, filesize, returnUrl when closing the dialog and Upload action	


Usage scenario
================
For just a single file upload as part of some form submission, this is not recommended. It might be useful when multiple file upload is required with ajax and server side needs some custom logic to implement.

Typically in any ASP.NET mvc application when creating or updating an entity object, often the requirement is to upload a number of files and associate them with that entity.
We may also want to save some entries for the file being uploaded.

To achieve this we typically download some javascript plugin, setup css, js into our project for integrating that plugin into the view. 
And in the server we typically need to implement action methods according to the plugin doc to make it work seamlessly. 
In upload actions we typically save the uploaded file details in some entities and associate them with some other entities and save them as well.

This nuget package sets you up with those plumbing and keeps you DRY with Html helpers and a FileSaver utility class.

The demo Mvc web app shows how to add custom code to generate thumbnail url(using ImageResizer http module) and pass custom entityId for saving your associations.