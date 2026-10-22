# Ecommerce

This is a project where i primarily test new (to me) techonology and practice  
The final product should be a functioning Ecommerce API which integrates with Stripe sandbox, to simulate payments, which is built with microservices working together

### AI used
None :) Only AI used in this project is brainstorming about architecture.   
All code is written by hand

### Technology used

- In general
  - .Net 10
  - Entity Framework ORM w/ SQLite
- OrderService
  - Customer facing HotChocolate GraphQL API
  - Integrates with InventoryService
- InventoryService
  - Minimal API (REST) to handle inventory

### Technology i want to use
- RabbitMQ
  - Implement event-based microservices (for example NotificationService on a order.successful event)

#### Tests?
This project has no tests (for now), since it is used as a practice ground. If a new testing practice coems out (or i read something interesting about it), maybe i will write some :)