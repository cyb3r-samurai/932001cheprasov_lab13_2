using System.Text.Json;

namespace lab13_2.Utilities
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            return session.GetString(key) switch
            {
                { } value => JsonSerializer.Deserialize<T>(value),
                null => default
            };
        }

    }
}
