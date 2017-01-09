namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public interface IRedirectValues
    {
        string Action { get; }

        string Controller { get; }

        object RouteValues { get; }
    }
}
