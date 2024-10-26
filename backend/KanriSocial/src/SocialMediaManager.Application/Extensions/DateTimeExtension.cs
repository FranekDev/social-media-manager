namespace SocialMediaManager.Application.Extensions;

public static class DateTimeExtension
{
    public static bool IsUtcDateIfNotConvertToUtc(this DateTime dateTime, out DateTime dateTimeUtc)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            dateTimeUtc = dateTime;
            return true;
        }

        dateTimeUtc = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(dateTimeUtc, TimeZoneInfo.Local);
        return true;
    }
}