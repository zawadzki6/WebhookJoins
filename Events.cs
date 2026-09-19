using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;

namespace WebhookJoins;

public class Events {
    public static void Joined(VerifiedEventArgs ev) {
        string message = Plugin.Instance!.Config.Joined;
        message = message.Replace("%name%", ev.Player.Nickname);
        message = message.Replace("%steamid%", ev.Player.UserId);
        message = message.Replace("%id%", ev.Player.Id.ToString());
        message = Plugin.Sanitize(message);
        Log.Debug($"parsed message: {message}");

        if (Plugin.Instance!.Config.ShowFlags) {
            string flags = Plugin.Instance!.Config.Flags;
            if (ev.Player.HasReservedSlot)
            flags = flags + "`ReservedSlot` ";
            if (ev.Player.DoNotTrack)
                flags = flags + "`DNT` ";
            if (ev.Player.GlobalBadge != null)
                flags = flags + "`GlobalBadge` ";
            if (ev.Player.IsGlobalModerator)
                flags = flags + "`GlobalModerator` ";
            if (ev.Player.IsGlobalMuted)
                flags = flags + "`GlobalMuted` ";
            if (ev.Player.IsHost)
                flags = flags + "`Host` ";
            if (ev.Player.IsNorthwoodStaff)
                flags = flags + "`NorthwoodStaff` ";
            if (ev.Player.IsStaffBypassEnabled)
                flags = flags + "`StaffBypass` ";
            if (ev.Player.IsWhitelisted && Server.IsWhitelisted)
                flags = flags + "`Whitelist` ";
            if (ev.Player.RemoteAdminAccess)
                flags = flags + "`RemoteAdmin` ";
            if (ev.Player.AuthenticationType == Exiled.API.Enums.AuthenticationType.Discord)
                flags = flags + "`DiscordAuth`";

            Log.Debug($"parsed flags: {flags}");
            if (flags != Plugin.Instance!.Config.Flags) {
                message = message + '\n';
                message = message + flags;
            }
        }
        Requests.Send(message);
    }
    public static void Left(LeftEventArgs ev) {
        string message = Plugin.Instance!.Config.Left;
        message = message.Replace("%name%", ev.Player.Nickname);
        message = message.Replace("%steamid%", ev.Player.UserId);
        message = message.Replace("%class%", ev.Player.Role.Name);
        message = Plugin.Sanitize(message);
        Log.Debug($"parsed message: {message}");

        Requests.Send(message);
    }
}
