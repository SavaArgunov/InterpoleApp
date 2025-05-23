using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace InterpoleApp.Systems;

public class DangerLevelTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Console.WriteLine("val " + value);
        // Console.WriteLine("Convert received value: " + value?.ToString());
        return value switch
        {
            null => "Все",
            int n => n.ToString(),
            _ => ""
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}