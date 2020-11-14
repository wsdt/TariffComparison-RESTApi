# TariffComparison-RESTApi ![.NET Core](https://github.com/wsdt/TariffComparison-RESTApi/workflows/.NET%20Core%20Tests/badge.svg) ![Docker Image CI](https://github.com/wsdt/TariffComparison-RESTApi/workflows/Docker%20Image%20CI/badge.svg)

### Possible ToDo's
1. Add more (lightweight) runtime/health checks to /api/info
2. Use Repositories -> Decoupling Services from Persistence specific logic
3. Add Client/Server Caching (e.g. Redis, Browser-native Caching-HTTP Headers, ...)
4. Add Docker-Swarm to Docker-Compose File or integrate with Kubernetes
5. Add more Documentation
6. Add more Tests
7. Maybe use generic interface for persistence operations to easily switch underlying data storage

### Interfaces
I removed/skipped creating some interfaces for e.g. DTOs as I didn't had an explicit use-case in this project and think it would have been overengineered here. 

### Persistence Layer
To avoid overengineering, I decided to have the majority of my persistence logic within my Services. Nevertheless, I would later introduce a Facade/Repository-Pattern or simply some sort of "PersistenceService<T>" to have basic methods like FindById(), FindAll(), usw. in one place & to reduce possibly code reduncancy (+ separation of concerns). 
