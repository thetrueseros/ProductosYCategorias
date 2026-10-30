using System.Globalization;

namespace ProductosYCategorias.Infrastructure.Formatting;

/// <summary>
/// Convierte las fechas UTC almacenadas en la base de datos a la hora de Colombia
/// (UTC-5), independientemente de la zona horaria del servidor.
/// </summary>
public static class ColombiaTime
{
    private static readonly TimeZoneInfo Zone = ResolveZone();

    public static DateTime FromUtc(DateTime value) =>
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(value, DateTimeKind.Utc), Zone);

    private static TimeZoneInfo ResolveZone()
    {
        // America/Bogota en Linux y macOS; SA Pacific Standard Time en Windows.
        foreach (var id in new[] { "America/Bogota", "SA Pacific Standard Time" })
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(id);
            }
            catch (TimeZoneNotFoundException)
            {
            }
            catch (InvalidTimeZoneException)
            {
            }
        }

        return TimeZoneInfo.Utc;
    }
}
