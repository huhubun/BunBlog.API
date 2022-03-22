# Bun Blog Web API

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/huhubun/BunBlog.API/ASP.NET%20Core%20CI)
[![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/huhubun/BunBlog.API)](https://github.com/huhubun/BunBlog.API/releases)
[![Docker Pulls](https://img.shields.io/docker/pulls/huhubun/bunblog.api)](https://hub.docker.com/r/huhubun/bunblog.api)

- Runtime: `.NET 6`
- Database: `PostgreSQL`

## Docker image

```bash
docker push huhubun/bunblog.api
```

## Migrations
### Package Manager Console in Visual Studio

```powershell
Add-Migration Init -StartupProject "BunBlog.API" -Project "BunBlog.Data"

Update-Database -StartupProject "BunBlog.API" -Project "BunBlog.Data"
```

### .NET CLI (6.0)
If it is the first time to run, you need to install `dotnet-ef` first

```bash
dotnet tool install --global dotnet-ef
```

If you have installed `dotnet-ef` before version 6, you need to upgrade `dotnet-ef`

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