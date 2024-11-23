using Common.Dto;

namespace Common.Message;

public class SendPreferences
{
    public required PreferenceDto Preference { get; set; }
}