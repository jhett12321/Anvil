using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A checkbox with a label to the right of it.
  /// </summary>
  public sealed class NuiCheck : NuiWidget
  {
    [JsonConstructor]
    public NuiCheck(NuiProperty<string> label, NuiProperty<bool> selected)
    {
      Label = label;
      Selected = selected;
    }

    [JsonPropertyName("label")]
    public NuiProperty<string> Label { get; set; }

    [JsonPropertyName("value")]
    public NuiProperty<bool> Selected { get; set; }

    public override string Type => "check";
  }
}
