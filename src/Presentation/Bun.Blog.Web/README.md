# Bun Blog Web

* [aspnetcore-angular2-universal](https://github.com/MarkPieszak/aspnetcore-angular2-universal)
* [Semantic-UI](https://github.com/Semantic-Org/Semantic-UI)
* [ng2-semantic-ui](https://github.com/edcarroll/ng2-semantic-ui)

```powershell
npm install
```

## 关于 ReferenceError: MouseEvent is not defined 的问题
由于引入了 ng2-semantic-ui，导致 universal 的服务器端渲染失败：`ReferenceError: MouseEvent is not defined`（可能是由于服务器上并没有 `MouseEvent`），我现在[关掉了 universal 的服务器端渲染](https://github.com/MarkPieszak/aspnetcore-angular2-universal#how-can-i-disable-ssr-server-side-rendering)。  
值得庆幸的是 ng2-semantic-ui 下有相应 [issue](https://github.com/edcarroll/ng2-semantic-ui/issues/185)，作者也在着手解决这个问题。如果该问题解决