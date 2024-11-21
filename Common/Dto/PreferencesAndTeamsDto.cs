namespace Common.Dto;

public record PreferencesAndTeamsDto(
    List<PreferenceDto> Preferences,
    List<TeamDto> Teams
);