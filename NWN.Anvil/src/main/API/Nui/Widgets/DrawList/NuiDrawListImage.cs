using System.Text.Json.Serialization;

namespace Anvil.API
{
  public sealed class NuiDrawListImage : NuiDrawListItem
  {
    [JsonConstructor]
    public NuiDrawListImage(NuiProperty<string> resRef, NuiProperty<NuiRect> rect) : base(null, null, null)
    {
      ResRef = resRef;
      Rect = rect;
    }

    [JsonPropertyName("image_aspect")]
    public NuiProperty<NuiAspect> Aspect { get; set; } = NuiAspect.Exact;

    [JsonPropertyName("image_halign")]
    public NuiProperty<NuiHAlign> HorizontalAlign { get; set; } = NuiHAlign.Left;

    [JsonPropertyName("rect")]
    public NuiProperty<NuiRect> Rect { get; set; }

    [JsonPropertyName("image")]
    public NuiProperty<string> ResRef { get; set; }

    public override NuiDrawListItemType Type => NuiDrawListItemType.Image;

    [JsonPropertyName("image_valign")]
    public NuiProperty<NuiVAlign> VerticalAlign { get; set; } = NuiVAlign.Top;
  }
}
