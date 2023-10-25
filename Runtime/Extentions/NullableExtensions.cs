namespace CommonUtility.Extensions
{
    public static class NullableExtensions
    {
        public static bool Empty<T>(this T? value) where T : struct
        {
            return value.HasValue == false;
        }
    }
}