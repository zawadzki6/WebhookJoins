using Exiled.API.Interfaces;
using System.ComponentModel;

namespace WebhookJoins;

public class Config : IConfig {
    [Description("Is the plugin enabled")]
    public bool IsEnabled { get; set; } = true;
    [Description("Makes the plugin more verbose")]
    public bool Debug { get; set; }

    public string WebhookUrl { get; set; } = "";
    public string Joined { get; set; } = "Player %name% (%steamid%) joined [%id%]";
    public string Left { get; set; } = "Player %name% (%steamid%) left. They were %class%.";
    public string Flags { get; set; } = "The player has the following flag(s): ";
    public bool ShowFlags { get; set; } = true;
    public bool LogLeaves { get; set; } = true;
}

