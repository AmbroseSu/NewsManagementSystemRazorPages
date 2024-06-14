var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(10));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();

        if (!path.StartsWith("/login") && !path.StartsWith("/login/login") && !path.StartsWith("/newsarticlemanagement"))
        {
            var userEmail = context.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                context.Response.Redirect("/Login/Login");
                return;
            }
        }



    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
