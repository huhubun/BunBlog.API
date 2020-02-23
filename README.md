# Bun Blog Web API

- Runtime: `.NET Core 3.1`
- Database: `PostgreSQL`

## Migrations
### Package Manager Console in Visual Studio

```powershell
Add-Migration Init -StartupProject "BunBlog.API" -Project "BunBlog.Data"

Update-Database -StartupProject "BunBlog.API" -Project "BunBlog.Data"
```

### .NET CLI (3.1)
If it is the first time to run, you need to install `dotnet-ef` first

```bash
dotnet tool install --global dotnet-ef
```

If you have installed `dotnet-ef` before version 3.1, you need to upgrade `dotnet-ef`

```bash
dotnet tool update --global dotnet-ef
```

Then you can use the `dotnet-ef` command

```bash
# Add migration
dotnet-ef migrations add Init --startup-project "./src/BunBlog.API/BunBlog.API.csproj" --project "./src/BunBlog.Data/BunBlog.Data.csproj"

# Update database
dotnet-ef database update --startup-project "./src/BunBlog.API/BunBlog.API.csproj" --project "./src/BunBlog.Data/BunBlog.Data.csproj"

# Generate db script
dotnet-ef migrations script --startup-project "./src/BunBlog.API/BunBlog.API.csproj" --project "./src/BunBlog.Data/BunBlog.Data.csproj" --idempotent  --output ./scripts/script.sql
```