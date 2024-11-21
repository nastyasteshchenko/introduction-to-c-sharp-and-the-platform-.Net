namespace Common.Dto;

public record PreferencesAndTeamDto(
    List<PreferenceDto> Preferences,
    List<TeamDto> Teams
);