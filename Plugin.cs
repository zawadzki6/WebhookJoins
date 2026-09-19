using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace WebhookJoins;

public class Plugin : Plugin<Config> {

    public override string Name => "WebhookJoins";
    public override string Author => "Zawadzki";
    public override string Prefix => "whook_joins";
    public override Version Version => new Version(1, 2, 0);

    public static Plugin? Instance;

    public override void OnEnabled() {
        Instance = this;
	Player.Verified += Events.Joined;
	if (Config.LogLeaves)
	    Player.Left += Events.Left;
	base.OnEnabled();
    }
    public override void OnDisabled() {
	Instance = null;
	Player.Verified -= Events.Joined;
	if (Config.LogLeaves)
	    Player.Left -= Events.Left;
	base.OnDisabled();
    }
    public static string Sanitize(string msg) {
	msg = msg.Replace("`", "\\`");
	msg = msg.Replace("@everyone", "`@everyone`");
	msg = msg.Replace("@here", "`@here`");

	return msg;
    }
}
