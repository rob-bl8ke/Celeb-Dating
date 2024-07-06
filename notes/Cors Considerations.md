# CORS considerations

- [Udemy Bookmark](https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/learn/lecture/44315036#overview)

Since the Angular application is served on a different host (different port) to the API attempts to call `localhost:5001` will result in  CORS policy block error. So `Access-Control-Allow-Origin` header needs to be set by the API.

CORS is a browser security feature. It's the API's responsibility to add the header above to allow the client browser to trust that there is a policy in place that allows the client and the server to talk to each other. Otherwise our client could download some dodgy code or data.

The API server knows to add CORS services using this code:

```csharp
services.AddCors();
```

The API server add this header using the following code:

```csharp
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200", "https://localhost:4200"));
```

Once this is done, the `Access-Control-Allow-Origin` header will be sent with every response.