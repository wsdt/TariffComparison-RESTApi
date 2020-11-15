# TariffComparison-RESTApi ![.NET Core](https://github.com/wsdt/TariffComparison-RESTApi/workflows/.NET%20Core%20Tests/badge.svg) ![Docker Image CI](https://github.com/wsdt/TariffComparison-RESTApi/workflows/Docker%20Image%20CI/badge.svg) [![HitCount](http://hits.dwyl.com/wsdt/TariffComparison-RESTApi.svg)](http://hits.dwyl.com/wsdt/TariffComparison-RESTApi) [![Maintenance](https://img.shields.io/badge/Maintained%3F-no-red.svg)](https://bitbucket.org/lbesson/ansi-colors) [![GitHub license](https://img.shields.io/github/license/wsdt/TariffComparison-RESTApi.svg)](https://github.com/wsdt/TariffComparison-RESTApi/blob/master/LICENSE) [![Generic badge](https://img.shields.io/badge/Made%20with-CSharp-37f)](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/) [![Donate](https://img.shields.io/badge/Donate-Pay%20me%20a%20coffee-3cf)](https://github.com/wsdt/Global/wiki/Donation)

## Get Started
The api is tested as well as the corresponding Docker-image is pushed to **[Dockerhub](https://hub.docker.com/repository/docker/wsdt/wavect_tariffcomparison_api)** via Github-Actions (see badges above). 

### Run api locally
The api is pushed to `wsdt/wavect_tariffcomparison_api` on [Dockerhub](https://hub.docker.com/repository/docker/wsdt/wavect_tariffcomparison_api).
I've created a docker-compose file too, to also have a MSSQL-database in order to ensure that everything works as expected.

The connectionString is currently directly built into the api, as I honestly just ran out of time, so if you are not using Docker-Toolbox (same docker-machine ip) this image might not work for you. 

Alternatively, you just have to do 3 things: 
1. Run the database: `docker-compose -f docker-compose.yml up`
2. Change database host in `appsettings.Development.json`/`appsettings.json`.
* If you are using Docker Toolbox execute `docker-machine ip` to get the database host.
* For Docker-Desktop you can use localhost.
3. Run IIS as usual.

Please note, that the database is **transient** as we have no write-operations in the api at the moment and who doesn't like a clean drive :-).

### ApiVersion
In my experience nothing is more painful than changing your ERM or project structure. Therefore, adding support for ApiVersions might seem over-engineered now, but can save a lot of resources in the long run as introducing this later might become increasingly complex/messy. 

Current api-version is 1.0. This means you can call versionized routes via e.g. `/api/v1.0` or `/api/v1`

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
9. Make Docker-Compose File production ready (networks, volumes, maybe proxy, letsencrypt [valid certificate], ..)
10. Add HealthCheck to Docker-Compose file (e.g. automatic restart, slack/discord notification, ...)
11. General error handling concept (Security)
12. Make images configurable via environment variables (e.g. DB_HOST, ...)
13. Add Api Rate-Limiting
14. Logging & Analytics: E.g. push to Firebase and visualize data with Grafana
15. Add stricter CORS ruleset (esp. if used from UI later, ..)
16. Enhance data model, but enough for current use-case.

### Interfaces
I removed/skipped creating some interfaces for e.g. DTOs as I didn't had an explicit use-case in this project and think it would have been overengineered here. 

### Persistence Layer
To avoid overengineering, I decided to have the majority of my persistence logic within my Services. Nevertheless, I would later introduce a Facade/Repository-Pattern or simply some sort of "PersistenceService<T>" to have basic methods like FindById(), FindAll(), usw. in one place & to reduce possibly code reduncancy (+ separation of concerns). 
