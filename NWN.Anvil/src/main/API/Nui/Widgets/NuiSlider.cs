using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A slider bar with integer values.
  /// </summary>
  public sealed class NuiSlider : NuiWidget
  {
    [JsonConstructor]
    public NuiSlider(NuiProperty<int> value, NuiProperty<int> min, NuiProperty<int> max)
    {
      Value = value;
      Min = min;
      Max = max;
    }

    [JsonPropertyName("max")]
    public NuiProperty<int> Max { get; set; }

    [JsonPropertyName("min")]
    public NuiProperty<int> Min { get; set; }

    [JsonPropertyName("step")]
    public NuiProperty<int> Step { get; set; } = 1;

    public override string Type => "slider";

    [JsonPropertyName("value")]
    public NuiProperty<int> Value { get; set; }
  }
}
