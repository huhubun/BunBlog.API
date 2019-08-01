# Bun Blog Web API

## Database
- PostgreSQL

### Migrations
#### Package Manager Console in Visual Studio

```powershell
Add-Migration Init -StartupProject "BunBlog.API" -Project "BunBlog.Data"

Update-Database -StartupProject "BunBlog.API" -Project "BunBlog.Data"
```

#### .NET CLI (2.2)
```powershell
dotnet ef migrations add Init --startup-project "./src/BunBlog.API/BunBlog.API.csproj" --project "./src/BunBlog.Data/BunBlog.Data.csproj"

dotnet ef database update --startup-project "./src/BunBlog.API/BunBlog.API.csproj" --project "./src/BunBlog.Data/BunBlog.Data.csproj"
```