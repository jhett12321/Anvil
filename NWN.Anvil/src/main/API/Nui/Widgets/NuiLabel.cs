using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A single-line, styleable, non-editable text field.
  /// </summary>
  public sealed class NuiLabel : NuiWidget
  {
    [JsonConstructor]
    public NuiLabel(NuiProperty<string> label)
    {
      Label = label;
    }

    [JsonPropertyName("text_halign")]
    public NuiProperty<NuiHAlign> HorizontalAlign { get; set; } = NuiHAlign.Left;

    [JsonPropertyName("value")]
    public NuiProperty<string> Label { get; set; }

    public override string Type => "label";

    [JsonPropertyName("text_valign")]
    public NuiProperty<NuiVAlign> VerticalAlign { get; set; } = NuiVAlign.Top;
  }
}
