namespace TodoApp.Settings;

public sealed record CookieSettings
{
    public required string LoginPath { get; init; }
    public required string LogoutPath { get; init; }
    public required string AccessDeniedPath { get; init; }
    public required TimeSpan CookieExpiry { get; init; }
}