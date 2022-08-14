using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Anvil.API
{
  public sealed class NuiValueJsonConverter<T> : JsonConverter<NuiValue<T>>
  {
    private readonly JsonConverter<T>? valueConverter;
    private readonly Type valueType;

    public NuiValueJsonConverter(JsonSerializerOptions options)
    {
      valueConverter = (JsonConverter<T>?)options.GetConverter(typeof(T));
      valueType = typeof(T);
    }

    public override NuiValue<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      T? value;
      if (valueConverter != null)
      {
        reader.Read();
        value = valueConverter.Read(ref reader, valueType, options);
      }
      else
      {
        value = JsonSerializer.Deserialize<T>(ref reader, options);
      }

      return new NuiValue<T>(value);
    }

    public override void Write(Utf8JsonWriter writer, NuiValue<T> value, JsonSerializerOptions options)
    {
      if (value.Value == null)
      {
        writer.WriteNull(string.Empty);
        return;
      }

      if (valueConverter != null)
      {
        valueConverter.Write(writer, value.Value, options);
      }
      else
      {
        JsonSerializer.Serialize(writer, value.Value, options);
      }
    }
  }
}
