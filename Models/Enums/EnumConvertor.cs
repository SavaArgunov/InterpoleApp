using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia;
using Avalonia.Data.Converters;

namespace InterpoleApp.Models.Enums;

public class EnumConvertor
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return string.Empty;

        var key = $"{value.GetType().Name}_{value}";

        if (Application.Current?.Resources.TryGetResource(key, null, out var resource) == true)
            return resource;

        return value.ToString();
    }
}