using Avalonia.ColorGenerator;

namespace AvaloniaAppTemplate.Extensions
{
    public static class ColorExtensions
    {
        public static string ToFormatString(this ColorWithValue color)
        {
            return $"{color.Name}: {color.Hex}";
        }
    }
}