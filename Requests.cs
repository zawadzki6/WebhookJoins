using System.Net.Http;
using System.Text.Json;
using System.Text;
using Exiled.API.Features;
using MEC;

namespace WebhookJoins;

class Requests {
    private static readonly HttpClient client = new();

    // this block is pretty much completely skidded from the web
    public static void Send(string msg) {
	var payload = new { content = msg };
	var json = JsonSerializer.Serialize(payload);
	using var payloaded = new StringContent(json, Encoding.UTF8, "application/json");

	Task<HttpResponseMessage> response = client.PostAsync(Plugin.Instance!.Config.WebhookUrl, payloaded);
	Log.Debug(response.Result);

	// 204 No Content is expected
	if ((int)response.Result.StatusCode != 204) {
	    response.Result.Content.ReadAsStringAsync().ContinueWith(t => {
		ReadOnlyMemory<byte> rom = Encoding.UTF8.GetBytes(t.Result);
		if (!JsonDocument.Parse(rom).RootElement.TryGetProperty("retry_after", out JsonElement ele)) {
		    Log.Error("Failed to get \"retry_after\". Are you sure it's a ratelimit?");
		    return;
		}
		float retry = ele.GetSingle();

		Log.Info($"rate limited! retrying in {retry}");
		Timing.CallDelayed(retry, () => Send(msg));  
	    });
	}
    }
}
