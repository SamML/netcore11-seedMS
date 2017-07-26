#netcore11-seedMS

1 - CORE: Two data contexts (SQL Databases)

CoreIdentityDbContext: Identity implementation. Administrator and User default Roles and test accounts.
CoreRepositoriesDbContext: Identity implementation. Administrator and User default Roles and test accounts. Plus Repositories (Product
, Customer, Order). With relation User->Order and claim based authorization implemented.

2 - MISC: Views and application helpers.

3 - WEB: Presentation layer (Actual Asp .Net Core 1.1)
