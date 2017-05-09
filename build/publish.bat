dotnet restore ../BunBlog.sln

cd ../src/Presentation/Bun.Blog.Web/
dotnet publish Bun.Blog.Web.csproj --configuration Release --framework netcoreapp1.1 --output ./bin/Release/PublishOutput