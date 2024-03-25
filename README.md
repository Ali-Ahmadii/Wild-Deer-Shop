Wild Deer Shop
-
Backend:ASP.NET Core,MSS,Docker,Redis,ASP.NET Core WebAPIs,Swagger
<br>
<br>
<img align="center" alt="dotnetcore" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/dotnetcore/dotnetcore-original.svg">
<img align="center" alt="Docker" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/docker/docker-original.svg">
<img align="center" alt="Redis" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/redis/redis-original.svg">
<img align="center" alt="MSS" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/microsoftsqlserver/microsoftsqlserver-original.svg">
<img align="center" alt="Swagger" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/swagger/swagger-original.svg">
<br>
<br>
Fron End:HTML,CSS,JS,Bootstrap
<br>
<br>
<img align="center" alt="HTML" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/html5/html5-original.svg">
<img align="center" alt="CSS" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/css3/css3-original.svg">
<img align="center" alt="CSS" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/javascript/javascript-original.svg">
<img align="center" alt="BootStrap" height="50" width="50" src="https://github.com/devicons/devicon/blob/master/icons/bootstrap/bootstrap-original.svg">
<br>
<br>
-
Data Base And Models Of Projects Have Been Designed By DB First Approach
<br>

Packages In The Project
-
-     dotnet add package BCrypt.Net-Next
      dot net add packages Microsoft.EntityFrameworkCore
      dot net add packages Microsoft.EntityFrameworkCore.Design
      dot net add packages Microsoft.EntityFrameworkCore.Realtional
      dot net add packages Microsoft.EntityFrameworkCore.SqlServer
      dot net add packages Microsoft.EntityFrameworkCore.Tools
      dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
      dotnet add package Swashbuckle.AspNetCore
  
Connection To Database
-
For Connecting To Your Own SQL Server You Must Change appsetting.json file Connection String For Example
```C#
"ConnectionString": "Data Source=.;Initial Catalog=WildDeer;Integrated Security=True;Trust Server Certificate=True"
//Data Source = . if we are in localhost
```
And Also Add SQL Server Service To Program.cs file
```C#
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<WildDeerContext>(config =>
{
    config.UseSqlServer("ConnectionString");
});
```

Caching With Redis
-
I Have Used Redis For Caching Approaches<br>
<br>
After Installing StackExchangeRedis Package We Should Do Several Things In Program.cs File:
<br>
```C#
builder.Services.AddStackExchangeRedisCache(action => {
    var connection = "localhost:6379"; //remember this port number it will be needed later
    action.Configuration = connection;
});
```
Then We Will Use Docker For Our Approach
<br>
In CMD Or Package Manager Console If You Use Visual Studio Write This Command:
```CMD
docker pull redis
```
Then
```CMD
docker run -p 6379:6379 --name some-redis -d redis
```
6379 Is What We Determined In Program.cs File And
some-redis Is Name Of Our Image, You Can Choose Whatever You Want

API Documentation With Swagger
-
For Using API You Must Login As Admin Or HR : Username = Ali & Password = Ali
<br>
<img align="center" alt="BootStrap" height="1200" width="100%" src="https://github.com/Ali-Ahmadii/Wild-Deer-Shop/blob/main/API.jpg">

Data Base
-
In WildDeerDB Foler Database Tables Are Represented In Scripts You Can Use This For Creating You Own Tables In MSS
