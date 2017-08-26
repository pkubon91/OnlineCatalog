using System.Web;

namespace OnlineCatalog.Web.Main.Common
{
    public static class SessionExtensions
    {
        public static bool TryGetKey<T>(this HttpSessionStateBase session, string key, out T value)
        {
            value = default(T);
            if (session[key] == null) return false;
            bool parsedT = session[key] is T;
            if (!parsedT) return false;
            value = (T)session[key];
            return true;
        }
    }
}