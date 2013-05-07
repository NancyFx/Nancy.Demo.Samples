# Nancy.Demo.Samples

This site/demo shows a simple Nancy website that aggregates Nancy demo projects from multiple GitHub accounts that follow the	Nancy.Demo.xxxx naming convention.
		
## The Tech</h2>

For rendering the views this site uses the SuperSimpleViewEngine, which is build into Nancy core, along with <a href="http://knockoutjs.com/">Knockout.js</a>
for binding data to the view.

On the storage front, we're using <a href="http://www.10gen.com/products/mongodb">MongoDB</a>, with the live samples site using <a href="http://mongohq.com/">MongoHQ</a>. If you're running locally you will need to make sure you have a local instance of MongoDB running on your machine, or edit the <em>Configuration.cs</em> class and alter the connection string to point to your own MongoHQ account.
