# Exercise 2

How would you go about breaking up a large e-commerce web application to support multiple teams delivering independently and regularly while keeping the application stable as a whole.

How would you break up the application? What would be your criteria for doing so and what trade-offs would you make?

# Distributed approach for E-Commerce Applications

## Introduction

Until relatively recently, most ecommerce platforms were supported by monolith architectures. These are the original self-sufficient, self-hosted, jack-of-all-trades systems. They provided businesses a centralised, on-premise, feature-rich system to meet their needs. However, as a business gets more complex, these systems can often be slow and difficult to scale.

Distributed or Microservice architecture offers a decentralised, decoupled approach. Instead of one solution that does everything, it separates different business requirements into different services which then communicate with each other. This means even as the business and its needs grow more complex, microservices can offer speed and flexibility. They are scalable, easy to deploy, can be tested independently, and allow agile teams to adapt them into their workflow easily.

One of the biggest advantages of using a Distributed architecture or Microservices is that services can work independently and isolation to each other. Unlike monolithic applications, they do not have a single point of failure. These advantages can help multiple teams work on the same application in an agile setting.

However, these architectures can have trade-offs. Unlike monolithic applications, they can be complex and have a steep learning curve. They need to be monitored continuously for availability and integration issues. It can also be argued that if measures are not taken, microservices can affect security and have lower performance compared to monolithic architecture. The other complexity can be handling data storage and the persistence layer. Since requests can be divided between multiple replicas of the same service, keeping consistent state between requests can be a challenge. 

![API REST-based topology (Richards, 2015)](https://user-images.githubusercontent.com/94787187/171494652-51561b6e-1e03-4bb0-ab31-074d8cbbd1fb.png)

*API REST-based topology (Richards, 2015)*

Richards, M. (2015) describes the API REST-based topology in his book, Software architecture patterns. This topology is one of the widely used approaches for coupling and interfacing these microservices together. Components communicate with each other using HTTP REST Web APIs and typically use JSON data-interchange format. 

## How I would break up an e-commerce web application.

![How headless commerce works (bigcommerce.co.uk)](https://user-images.githubusercontent.com/94787187/171495075-31e58410-e9d6-45e6-8453-35f58b635b8e.png)

*How headless commerce works (bigcommerce.co.uk)*

If the application is a greenfield project (i.e. starting from scratch) and isn’t encumbered by legacy services, I would suggest using “Headless Commerce” or “Backend-for-Frontend” (BFF) approach for the application architecture. Headless Commerce entails decoupling the front-end presentation layer from the back-end e-commerce engine.The philosophy of using Headless Commerce architecture is that changes can be made to either systems without affecting the other. Also, the flexibility allows the Backend to be used to multiple UIs. However the philosophy of the BFF architecture is to have a separate Backend (or an abstraction layer) for each User Experience. The difference between these is how the services are decoupled. Microservices approach can be fully decoupled with regards to keeping Separation of Concerns design principle in mind. 

While Headless commerce architecture can still be considered simplistic in a manner, it can be combined with microservices architecture as per requirements. For example, The Frontend UI can be part of a single service whereas the backend can be distributed with regards to the context the services operate in.

![Microservices Architecture at Netflix (dinarys.com)](https://user-images.githubusercontent.com/94787187/171495243-ec45648a-9120-4694-9df8-93734d67b634.png)

*Microservices Architecture at Netflix (dinarys.com)*

Netflix is known to be one of the pioneers in microservices and handling scalability. Having decided to go micro in 2009 because of scaling issues, the company managed to get a reputation as a first-class service in its niche market, and it remains so to this day, serving up to 200 million subscribers worldwide. Adrian Cockcroft, well known for his role as Cloud architect at Netflix, developed his own definition for microservices, “Loosely coupled service oriented architecture with bounded contexts”. Therefore, the separation of services can be at the discretion of the requirements. 

## Criteria for breaking the platform

The criteria I would suggest for architecting the platform are as follows:

- High availability of the web application
- Scalability (Capable of high volume of transactions and traffic)
- Resiliency (Tolerant to failures)
- Platform agnostic and portability
- DevOps Centric organisation.
    
    ### 1. High availability of the web application.
    
    Arguably the most important aspect of e-commerce web applications. Businesses that rely on the e-commerce platform as their major source of revenue need to minimise outages. The modular nature of distributed applications allow individual components to be distributed across multiple data centres or even geographical locations to prevent single point of failures and/or balancing the load. 
    
    ### 2. Scalability (Capable of high volume of transactions and traffic)
    
    Scalable systems should allow to react to changing workloads automatically via elastic capacity management, as offered by cloud infrastructures. With distributed architectures, you can dynamically replicate those microservices to cloud infrastructures that are under heavy load. It is not necessary to scale the complete system, as it would be required with a monolithic system. Small services are easier to deploy, and since they are autonomous, are less likely to cause system failures when they go wrong. With e-commerce applications, customer demand can change at unexpected times, the application can be tailored to scale to accommodate higher volume of transactions and traffic in a cost-saving method. 
    
    ### 3. Resiliency (Tolerant to failures)
    
    Nonfunctional attributes, such as scalability and fault tolerance for high availability, are addressed by microservice architectures. A consequence of using microservices is that applications need to be designed such that they can tolerate the failure of individual services. Since services can fail at any time, it is important to be able to detect the failures quickly
    and, if possible, automatically restore services. Microservice applications put a lot of emphasis on real-time monitoring of the application, checking both technical metrics (e.g. how many requests per second is the database getting) and business relevant metrics (such as how many orders per minute are received). Monitoring can provide an early warning system of something going wrong that triggers development teams to follow up.
    
    ### 4. Platform agnostic and portability.
    
    Microservice architecture should enable flexibility in the organisation. Technologies such as Docker and Kubernetes allow engineers to be platform agnostic and not rely on platform lock-ins. The flexibility would allow organisations to be agile and move their application workloads across platforms whether on-premise or cloud hosted.
    
    ### 5. DevOps Centric organisation.
    
    The well known Conway’s Law states that organisations which design systems are constrained to produce designs which are copies of the communication structures of these organisations. The development should be highly independent, having members of all roles and skills that are required to build and maintain their microservices. Microservices reinforce modular structure, which is particularly important for larger teams. Decoupling teams is as relevant as decoupling software modules. This can be useful for agile teams
    
    Agile teams can also enable Continuous delivery. With distributed architectures and microservices, integration can be complex. DevOps culture and automation toolchains enable teams to deploy and deliver code within minutes using CI/CD pipelines. The same pipelines should also be responsible for integrating modular services and testing before delivery.
    
    Microservices also help instil a nature of service ownership in teams. Any microservices development team must be focused on the lifecycle of a particular service up until it reaches its end consumer. Being fully responsible for the work outcome nourishes a culture of ownership, defining team boundaries and motivating teams to be more productive and inventive.
    

One of the popular methods of meeting some of these criteria from an infrastructure point of view can be though containerising the application and using container orchestration infrastructure such as Kubernetes or Hashicorp Nomad. Containers can modularise applications and can be portable, which can accelerate development and enable continuous delivery. 

An alternative can be to use Platform-as-a-Service (PaaS) services provided by Cloud Service Providers (CSP) (AWS, Azure, GCP, etc) such as Serverless Code platforms, Managed Database services, API Gateways, Messaging Queues, etc. These services can be fully managed by CSPs and require minimal maintenance. These can be helpful for smaller organisations. However, this can incur expensive costs if not appropriately managed. For most organisations, using a hybrid approach and incorporating a mix of these approaches can be useful. 

## Trade-offs

Compared to the simplistic nature of monolithic architecture, Distributed architecture can have trade-offs:

- Switching to a distributed service or a Microservice architecture doesn’t just require a change to how your ecommerce platform is organised. It can cause a change to your whole organisational structure. Managing the different microservices requires cross-functional, vertical teams who can work together to develop and maintain the site. If the organisation does not adapt to the changing needs, maintaining the service can be complex.
- Another potential consideration of switching to a Microservice system is that it might change the infrastructure and tools you need to monitor the different microservices.
- If not planned well, Microservices can end up being costly. Scaling down resources when they aren’t needed can help keep the costs down
- The Persistence layer or the data layer can make the architecture complex. A user’s request may travel through Distributed services or replicas of the same service which  may not have the same user state. Therefore the application would have to support concurrent requests through the services and database or designed to be as stateless as possible.
- Technology Lock-in: Reliance on CSPs can cause outages. For example, if the application is dependent on AWS, an outage in AWS may cause issues with the application. This should be factored in during the design phase to prepare a safety net.

## Conclusion

Using a hybrid of Headless commerce and Microservices architectural design paradigms can help build resilient e-commerce web applications. Combining technologies such as Cloud PaaS and containerisation can enable applications to be built in a way that they are scalable, resilient, highly available, portable, and support multiple teams delivering independently and regularly while keeping the application stable as a whole.

## References

Rahman, M. and Gao, J. (2015) ‘A reusable automated acceptance testing architecture for microservices in behavior-driven development’, *Proceedings - 9th IEEE International Symposium on Service-Oriented System Engineering, IEEE SOSE 2015*. IEEE, 30, pp. 321–325. doi: 10.1109/SOSE.2015.55.

Larrucea, X. *et al.* (2018) ‘Microservices’, *IEEE Software*, 35(3), pp. 96–100. doi: 10.1109/MS.2018.2141030.

Richards, M. (2015) *Software architecture patterns : understanding common architecture patterns and when to use them*. First edit. O’Reilly Media, Inc. Available at: https://www.oreilly.com/library/view/software-architecture-patterns/9781491971437/ (Accessed: 15 March 2019).

[openedu.org](http://openedu.org) (No Date). Available at: [https://www.open.edu/openlearn/science-maths-technology/computing-ict/introduction-e-commerce-and-distributed-applications/content-section-0?active-tab=description-tab](https://www.open.edu/openlearn/science-maths-technology/computing-ict/introduction-e-commerce-and-distributed-applications/content-section-0?active-tab=description-tab)

[bigcommerce.co.uk](http://bigcommerce.co.uk) (No Date). Available at: [https://www.bigcommerce.co.uk/articles/ecommerce-website-development/microservices](https://www.bigcommerce.co.uk/articles/ecommerce-website-development/microservices)

[smartbear.com](http://smartbear.com) (No Date). Available at: [https://smartbear.com/blog/why-you-cant-talk-about-microservices-without-ment/](https://smartbear.com/blog/why-you-cant-talk-about-microservices-without-ment/)

[dinarys.com](http://dinarys.com/) (No Date). Available at: [https://dinarys.com/blog/microservices](https://dinarys.com/blog/microservices)
