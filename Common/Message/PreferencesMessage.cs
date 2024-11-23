using Common.Dto;
using Common.Model;

namespace Common.Message;

public class PreferencesMessage
{
    public required PreferenceDto Preference { get; set; }
}