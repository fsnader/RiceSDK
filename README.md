Rice.SDK
---------------------

Rice.SDK is a basic set of classes and interfaces used to develop .NET Core API's. It includes:
 - Classes to generate JWT tokens
 - An Entity Framework based generic repository with filter capabilities
 - Generic business classes to be used with these repositories
 - Exceptions that represent basic HTTP errors to be used in web applications
 - Some other usefull classes and extensions methods


It can be download from nuget: https://www.nuget.org/packages/Rice.SDK/

    PM> Install-Package Rice.SDK -Version 1.0.0


Rice.SDK Structure:
------------

- Voor.SDK:
    - Authentication: Classes responsible to create and manage JWT Tokens. It's principal class is TokenBuilder, used to generate the tokens.

    - Domain: Entities related interfaces
        - IIdentifiableEntity: Interface that represents a database entity

    - Business: Classes and interfaces used to implement a business logic layer

        - Contract/IBusiness: Business interface that includes all read/write methods that should be implemented in a basic business class.

        - Concrete/BaseBusiness: Generic business class that implements IBusiness. It's useful to be used in cases that no specific business logic implementation is required.

    - Exceptions: Contains exceptions that represent HTTP errors

    - Repository: Classes and interfaces related to repositories
        - Contract/IRepositoryReader: Repository read methods interface
        - Contract/IRepositoryWriter: Repository write methods interface
        - Contract/IRepository: Repository read/write methods interface

        - Concrete/BaseRepository: Generic class that implements IRepositoryReader, IRepositoryWriter and IRepository. Can be used to provide any database read/write operation.

    - Utils: Useful classes and extension methods


How to use
-------
The example project uses all the basic SDK functionalities. We recommend you to download it and see how is used (WIP)

- Examples:
    - RiceWebBase: A base Web API project that uses the Rice.SDK (WIP)
