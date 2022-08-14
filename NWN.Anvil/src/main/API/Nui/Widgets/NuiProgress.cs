using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A generic progress bar.
  /// </summary>
  public sealed class NuiProgress : NuiWidget
  {
    [JsonConstructor]
    public NuiProgress(NuiProperty<float> value)
    {
      Value = value;
    }

    public override string Type => "progress";

    /// <summary>
    /// The current value of this progress bar (0-1).
    /// </summary>
    [JsonPropertyName("value")]
    public NuiProperty<float> Value { get; set; }
  }
}
