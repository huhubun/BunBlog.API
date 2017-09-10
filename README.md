The code is for reference only, do not use in a production environment! I do not provide after-sales service!

# BunBlog
[![Build status](https://ci.appveyor.com/api/projects/status/05h1ndygkw6k2lgh?svg=true)](https://ci.appveyor.com/project/huhubun/bunblog)
[![Build Status](https://travis-ci.org/huhubun/BunBlog.svg?branch=master)](https://travis-ci.org/huhubun/BunBlog)

## Technical Points
* [ASP.NET Core MVC and Web API](https://github.com/aspnet/Mvc)
* [Entity Framework Core](https://github.com/aspnet/EntityFramework)
* [Npgsql](https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL)
* ~~[Angular 2](https://github.com/angular/angular)~~
* ~~[TypeScript](https://github.com/Microsoft/TypeScript)~~
* [Semantic UI](https://semantic-ui.com)

## Migration
1. Add migration  
将**程序包管理器控制台**的**默认项目**切换为`Presentation\Bun.Blog.Web.Admin`，然后执行下面的代码添加迁移。由于映射和迁移文件都在 `Bun.Blog.Data` 项目下，所以需要单独通过 `-Project` 以及 `-OutputDir` 指定。

```powershell
Add-Migration __MigrationTitle__ -Project Bun.Blog.Data -Context BlogContext -OutputDir Migrations
```

2. Update database
```powershell
Update-Database -Project Bun.Blog.Data
```