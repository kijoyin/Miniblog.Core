    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;

    using WebEssentials.AspNetCore.OutputCaching;

    using WebMarkupMin.AspNetCore7;
    using WebMarkupMin.Core;

    using WilderMinds.MetaWeblog;
    using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
    using JavaScriptEngineSwitcher.V8;
    using IWmmLogger = WebMarkupMin.Core.Loggers.ILogger;
    using MetaWeblogService = Miniblog.Core.Services.MetaWeblogService;
    using WmmNullLogger = WebMarkupMin.Core.Loggers.NullLogger;
    using Miniblog.Brains.Services;
    using Miniblog.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Miniblog.Core;
    using Microsoft.AspNetCore.Rewrite;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddSingleton<IUserServices, BlogUserServices>();
    builder.Services.AddScoped<IBlogService, PresistantBlogService>();
    builder.Services.Configure<BlogSettings>(builder.Configuration.GetSection("blog"));
    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddMetaWeblog<MetaWeblogService>();
    builder.Services.AddDbContext<IBloggingContext, BloggingContext>(options =>
                                                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"), b => b.MigrationsAssembly("Miniblog.Core")).UseLowerCaseNamingConvention());
    // Progressive Web Apps https://github.com/madskristensen/WebEssentials.AspNetCore.ServiceWorker
    builder.Services.AddProgressiveWebApp(
        new WebEssentials.AspNetCore.Pwa.PwaOptions
        {
            OfflineRoute = "/shared/offline/"
        });

    // Output caching (https://github.com/madskristensen/WebEssentials.AspNetCore.OutputCaching)
    builder.Services.AddOutputCaching(
        options =>
        {
            options.Profiles["default"] = new OutputCacheProfile
            {
                Duration = 3600
            };
        });

    // Cookie authentication.
    builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(
            options =>
            {
                options.LoginPath = "/login/";
                options.LogoutPath = "/logout/";
            });

    // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
    builder.Services
        .AddWebMarkupMin(
            options =>
            {
                options.AllowMinificationInDevelopmentEnvironment = true;
                options.DisablePoweredByHttpHeaders = true;
            })
        .AddHtmlMinification(
            options =>
            {
                options.MinificationSettings.RemoveOptionalEndTags = false;
                options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
            });
    builder.Services.AddSingleton<IWmmLogger, WmmNullLogger>(); // Used by HTML minifier

    // Bundling, minification and Sass transpiration (https://github.com/ligershark/WebOptimizer)
    builder.Services.AddJsEngineSwitcher(options =>
       options.DefaultEngineName = V8JsEngine.EngineName
    ).AddV8();
    builder.Services.AddWebOptimizer(
        pipeline =>
        {
            pipeline.MinifyJsFiles();
            pipeline.CompileScssFiles()
                    .InlineImages(1);
        });

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Shared/Error");
        app.UseHsts();
    }

    app.Use(
        (context, next) =>
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            return next();
        });

    app.UseStatusCodePagesWithReExecute("/Shared/Error");
    app.UseWebOptimizer();

    app.UseStaticFilesWithCache();

    if (builder.Configuration.GetValue<bool>("forcessl"))
    {
        app.UseHttpsRedirection();
    }

    if (builder.Configuration.GetValue<bool>("forceWwwPrefix"))
    {
        app.UseRewriter(new RewriteOptions().AddRedirectToWwwPermanent());
    }

    app.UseMetaWeblog("/metaweblog");
    app.UseAuthentication();

    app.UseOutputCaching();
    app.UseWebMarkupMin();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(
        endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller=Blog}/{action=Index}/{id?}");
        });

    app.Run();
