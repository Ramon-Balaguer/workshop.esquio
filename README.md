# workshop.esquio


dotnet new webapi -o src/WebApi -n WebApi
cd src/WebApi
dotnet add package Esquio.AspNetCore
dotnet add package Esquio.Configuration.Store
dotnet add package Esquio.Http.Store
dotnet add package Swashbuckle.AspNetCore


cd..
ng new spa
