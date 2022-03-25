namespace Wedding.Mail;

public static class DependencyInjection
{
    public static IServiceCollection RegisterEmailSettings(
        this IServiceCollection services,
        IConfiguration config)
    {
        var settings = config
            .GetSection("MailSettings")
            .Get<Settings>();

        return services
            .AddSingleton(settings)
            .AddTransient<Sender>();
    }
}
