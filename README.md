# TariffComparison-RESTApi ![.NET Core](https://github.com/wsdt/TariffComparison-RESTApi/workflows/.NET%20Core%20Tests/badge.svg) ![Docker Image CI](https://github.com/wsdt/TariffComparison-RESTApi/workflows/Docker%20Image%20CI/badge.svg)


### Interfaces
I removed/skipped creating interfaces for e.g. DTOs as I didn't had an explicit use-case in this project and think it would have been overengineered here. 

### Persistence Layer
To avoid overengineering, I decided to have the majority of my persistence logic within my Services. Nevertheless, I would later introduce a Facade or some sort of "PersistenceService<T>" 
to have basic methods like FindById(), FindAll(), usw. in one place & to reduce possibly code reduncancy. 
