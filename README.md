# TariffComparison-RESTApi ![.NET Core](https://github.com/wsdt/TariffComparison-RESTApi/workflows/.NET%20Core%20Tests/badge.svg) ![Docker Image CI](https://github.com/wsdt/TariffComparison-RESTApi/workflows/Docker%20Image%20CI/badge.svg)

## Get Started
The api is tested as well as the corresponding Docker-image is pushed to **[Dockerhub](https://hub.docker.com/repository/docker/wsdt/wavect_tariffcomparison_api)** via Github-Actions (see badges above). 

### Run api locally
The api is pushed to `wsdt/wavect_tariffcomparison_api` on [Dockerhub](https://hub.docker.com/repository/docker/wsdt/wavect_tariffcomparison_api).
I've created a docker-compose file too, to also have a MSSQL-database in order to ensure that everything works as expected.

Just execute `docker-compose up` in `Wavect.TariffComparison\`.

Please note, that the database is **transient** as we have no write-operations in the api at the moment and who doesn't like a clean drive :-).

### ApiVersion
In my experience nothing is more painful than changing your ERM or project structure. Therefore, adding support for ApiVersions might seem over-engineered now, but can save a lot of resources in the long run as introducing this later might become increasingly complex/messy. 


### Swagger
All routes are listed at `/swagger`. This route is disabled for release builds.


## Possible ToDo's
1. Add more (lightweight) runtime/health checks to /api/info
2. Use Repositories -> Decoupling Services from Persistence specific logic
3. Add Client/Server Caching (e.g. Redis, Browser-native Caching-HTTP Headers, ...)
4. Add Docker-Swarm to Docker-Compose File or integrate with Kubernetes
5. Add more Documentation
6. Add more Tests
7. Maybe use generic interface for persistence operations to easily switch underlying data storage
8. Add Authentication & Authorization

### Interfaces
I removed/skipped creating some interfaces for e.g. DTOs as I didn't had an explicit use-case in this project and think it would have been overengineered here. 

### Persistence Layer
To avoid overengineering, I decided to have the majority of my persistence logic within my Services. Nevertheless, I would later introduce a Facade/Repository-Pattern or simply some sort of "PersistenceService<T>" to have basic methods like FindById(), FindAll(), usw. in one place & to reduce possibly code reduncancy (+ separation of concerns). 
