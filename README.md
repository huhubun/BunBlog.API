The code is for reference only, do not use in a production environment! I do not provide after-sales service!

# BunBlog
[![Build status](https://ci.appveyor.com/api/projects/status/05h1ndygkw6k2lgh?svg=true)](https://ci.appveyor.com/project/huhubun/bunblog)
[![Build Status](https://travis-ci.org/huhubun/BunBlog.svg?branch=master)](https://travis-ci.org/huhubun/BunBlog)

## Technical Points
* [ASP.NET Core MVC and Web API](https://github.com/aspnet/Mvc)
* [Entity Framework Core](https://github.com/aspnet/EntityFramework)
* [Npgsql](https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL)
* [Angular 2](https://github.com/angular/angular)
* [TypeScript](https://github.com/Microsoft/TypeScript)

## Typings
若要添加新的 TypeScript 类型声明文件（`.d.ts`），请进入 `Bun.Blog.Web` 文件夹下执行。
1. 查找源
```powershell
typings search kendo
```
得到结果为
```
NAME     SOURCE HOMEPAGE                        DESCRIPTION VERSIONS UPDATED                 
kendo-ui dt     http://www.telerik.com/kendo-ui             2        2017-02-21T22:59:39.000Z
```

2. 添加
```powershell
typings install dt~kendo-ui --global --save
```
注意 Source 的指定方式