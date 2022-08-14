using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A non-editable text field. Supports multiple lines and has a skinned border and a scrollbar if needed.
  /// </summary>
  public sealed class NuiText : NuiWidget
  {
    [JsonConstructor]
    public NuiText(NuiProperty<string> text)
    {
      Text = text;
    }

    [JsonPropertyName("value")]
    public NuiProperty<string> Text { get; set; }

    [JsonPropertyName("border")]
    public bool Border { get; set; } = true;

    [JsonPropertyName("scrollbars")]
    public NuiScrollbars Scrollbars { get; set; } = NuiScrollbars.Auto;

    public override string Type => "text";
  }
}
