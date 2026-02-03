namespace CvApi.Application.Extensions;

/// <summary>Extensions</summary>
public static class GenericTypeExtensions
{
    /// <summary>Gets the name of the generic type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The type name</returns>
    public static string GetGenericTypeName(this Type type)
    {
        if (type.IsGenericType)
        {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }

        return type.Name;
    }

    /// <summary>Gets the name of the generic type.</summary>
    /// <param name="object">The object.</param>
    /// <returns>The type name</returns>
    public static string GetGenericTypeName(this object @object)
    {
        return @object.GetType().GetGenericTypeName();
    }
}