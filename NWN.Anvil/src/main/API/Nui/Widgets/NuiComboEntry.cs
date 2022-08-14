using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A combo/list element for use in <see cref="NuiCombo"/>.
  /// </summary>
  [JsonConverter(typeof(ObjectToArrayJsonConverter<NuiComboEntry>))]
  public sealed class NuiComboEntry
  {
    public NuiComboEntry(string label, int value)
    {
      Label = label;
      Value = value;
    }

    public NuiComboEntry() {}

    [JsonPropertyOrder(1)]
    public string? Label { get; set; }

    [JsonPropertyOrder(2)]
    public int? Value { get; set; }
  }
}
