# TariffComparison-RESTApi


### Interfaces
I removed/skipped creating interfaces for e.g. DTOs as I didn't had an explicit use-case in this project and think it would have been overengineered here. 

### Persistence Layer
To avoid overengineering, I decided to have the majority of my persistence logic within my Services. Nevertheless, I would later introduce a Facade or some sort of "PersistenceService<T>" 
to have basic methods like FindById(), FindAll(), usw. in one place & to reduce possibly code reduncancy. 
