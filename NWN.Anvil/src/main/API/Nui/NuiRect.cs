using System.Text.Json.Serialization;

namespace Anvil.API
{
  public readonly struct NuiRect
  {
    [JsonConstructor]
    public NuiRect(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    [JsonPropertyName("h")]
    public float Height { get; }

    [JsonPropertyName("w")]
    public float Width { get; }

    [JsonPropertyName("x")]
    public float X { get; }

    [JsonPropertyName("y")]
    public float Y { get; }
  }
}
