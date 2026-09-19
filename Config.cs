using Exiled.API.Interfaces;

namespace WebhookJoins;

public class Config : IConfig {
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; }

    public string WebhookUrl { get; set; } = "";
    public string Joined { get; set; } = "Player %name% (%steamid%) joined [%id%]";
    public string Left { get; set; } = "Player %name% (%steamid%) left. They were %class%.";
    public string Flags { get; set; } = "The player has the following flag(s): ";
    public bool ShowFlags { get; set; } = true;
    public bool LogLeaves { get; set; } = true;
}

