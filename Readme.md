# Panda Service
A starting point for anyone making an asp.net core 2.1 service.

## Out of the box features
* MVC
* Logging to elasticsearch/kibana
* Exception handling middleware
* Clean setup for grabbing environment variables
* Entity Framework with PostgreSQL example. Automatic migrations when program starts.

### Building and Running
`docker-compose up --build` from the base directory of the project. This one command both creates the docker
images (which also results in the C# code being built) and starts the containers.

### Hitting the service
The service can be hit at `http://localhost:8080`. Look at the controller classes to see what endpoints
are available.

### Viewing Kibana
Kibana can be hit at `http://localhost:5601`. Make sure you hit `http://localhost:8080/api/appname` at least once
before trying to access kibana, otherwise there will be no log messages. The first time you hit Kibana, you will have
to create the default index pattern. Set it to `logstash-*` with `@timestamp` as the time filter
field name. You will now be able to see all log messages in the Discover section.

### Replace Entity Framework Setup/Schema with your own
When you replace the DB stuff with your own, be sure to delete the Migrations folder and regenerate the necessary
migrations for your own schema. Follow the instructions 
[here](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-2.1&tabs=netcore-cli)
