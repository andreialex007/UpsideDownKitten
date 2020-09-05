 **Important points:**

- Default user name and password located [here](https://github.com/andreialex007/UpsideDownKitten/blob/master/UpsideDownKitten/UpsideDownKitten.DL/UsersRepository.cs#L13): 
- In order to use basic authentication, it is necessary to set **IsBasicAuth : true** [here](https://github.com/andreialex007/UpsideDownKitten/blob/master/UpsideDownKitten/UpsideDownKitten/appsettings.json#L10), otherwise will be used bearer authentication

**Features:**
- User can get blurred/rotated/black-white random images of cats.
- It is possible to create one user, get one user or list of users
- There are two types of authentication in project.
- There is a validation in project, i.e. if user provided wrong username or password it will be notified by api, if user with same name already exists web api also gives an error.

**Architecture brief overview:**
- Repository used for data access.
- CatsClient used for fetching data from cats rest service
- Main business logic placed in Services (CatsService,UsersService)
- Image manipulations code moved to ImagesProcessor class.
- Documentation represented as swagger UI generated from classes and method comments
