namespace Core.Extensions;

public static class StringExtensions
{
    public static string FirstLetterToLower(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        char[] a = str.ToCharArray();
        a[0] = char.ToLower(a[0]);
        return new string(a);
    }

    //TODO: first letter to underline to lower

}