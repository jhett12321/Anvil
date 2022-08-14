using System.Text.Json.Serialization;

namespace Anvil.API
{
  public abstract class NuiDrawListItem
  {
    protected NuiDrawListItem(NuiProperty<Color>? color, NuiProperty<bool>? fill, NuiProperty<float>? lineThickness)
    {
      Color = color;
      Fill = fill;
      LineThickness = lineThickness;
    }

    [JsonPropertyName("color")]
    public NuiProperty<Color>? Color { get; set; }

    [JsonPropertyName("enabled")]
    public NuiProperty<bool> Enabled { get; set; } = true;

    [JsonPropertyName("fill")]
    public NuiProperty<bool>? Fill { get; set; }

    [JsonPropertyName("line_thickness")]
    public NuiProperty<float>? LineThickness { get; set; }

    [JsonPropertyName("type")]
    public abstract NuiDrawListItemType Type { get; }
  }
}
