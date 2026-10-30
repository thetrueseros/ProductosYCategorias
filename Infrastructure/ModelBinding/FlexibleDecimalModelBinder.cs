using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProductosYCategorias.Infrastructure.ModelBinding;

/// <summary>
/// Registra un binder para <see cref="decimal"/> que acepta el separador decimal
/// colombiano (5499900,50), el invariante (5499900.50) y separadores de miles
/// con punto (5.499.900). Necesario porque la aplicación usa la cultura es-CO
/// y los navegadores pueden enviar los inputs con punto o con coma.
/// </summary>
public sealed class FlexibleDecimalModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var type = context.Metadata.ModelType;

        return type == typeof(decimal) || type == typeof(decimal?)
            ? new FlexibleDecimalModelBinder()
            : null;
    }
}

public sealed class FlexibleDecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);

        var raw = valueResult.FirstValue;
        var displayName = bindingContext.ModelMetadata.GetDisplayName();

        if (string.IsNullOrWhiteSpace(raw))
        {
            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName,
                $"El campo {displayName} es obligatorio.");
            return Task.CompletedTask;
        }

        if (TryParse(raw, out var value))
        {
            bindingContext.Result = ModelBindingResult.Success(value);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName,
                $"El campo {displayName} debe ser un número válido (por ejemplo 5499900 o 5.499.900).");
        }

        return Task.CompletedTask;
    }

    private static bool TryParse(string raw, out decimal value)
    {
        var text = raw.Trim().Replace("$", string.Empty).Trim();

        var lastComma = text.LastIndexOf(',');
        var lastDot = text.LastIndexOf('.');

        if (lastComma >= 0 && lastDot >= 0)
        {
            // El último separador es el decimal; el otro es de miles.
            text = lastComma > lastDot
                ? text.Replace(".", string.Empty).Replace(',', '.')
                : text.Replace(",", string.Empty);
        }
        else if (lastComma >= 0)
        {
            text = IsGrouped(text, ',')
                ? text.Replace(",", string.Empty)
                : text.Replace(',', '.');
        }
        else if (lastDot >= 0 && IsGrouped(text, '.'))
        {
            text = text.Replace(".", string.Empty);
        }

        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }

    /// <summary>
    /// Determina si los separadores son de miles: bloques de tres dígitos
    /// después del primero, que debe tener entre uno y tres dígitos.
    /// </summary>
    private static bool IsGrouped(string text, char separator)
    {
        var parts = text.Split(separator);

        if (parts.Length < 2 ||
            parts[0].Length is < 1 or > 3 ||
            parts[0] == "0" ||
            !parts[0].All(char.IsDigit))
        {
            return false;
        }

        return parts.Skip(1).All(part => part.Length == 3 && part.All(char.IsDigit));
    }
}
