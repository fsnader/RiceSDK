Rice.SDK
---------------------

Rice.SDK is a basic set of classes and interfaces used to develop .NET Core API's. It has the following structure:

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
