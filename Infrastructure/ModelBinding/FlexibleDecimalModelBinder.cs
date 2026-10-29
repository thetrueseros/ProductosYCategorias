using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProductosYCategorias.Infrastructure.ModelBinding;

/// <summary>
/// Registra un binder para <see cref="decimal"/> que acepta tanto el separador
/// decimal invariante (1299.99) como el español (1299,99). Necesario porque la
/// aplicación usa la cultura es-ES y los navegadores envían los inputs numéricos
/// con punto.
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
                $"El campo {displayName} debe ser un número válido (por ejemplo 1299,99).");
        }

        return Task.CompletedTask;
    }

    private static bool TryParse(string raw, out decimal value)
    {
        var text = raw.Trim();

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
            text = text.Replace(".", string.Empty).Replace(',', '.');
        }

        return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }
}
