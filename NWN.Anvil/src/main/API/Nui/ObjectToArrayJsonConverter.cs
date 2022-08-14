using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Anvil.API
{
  public sealed class ObjectToArrayJsonConverter<T> : JsonConverter<T> where T : class, new()
  {
    private readonly PropertyByOrderComparer propertyByOrderComparer = new PropertyByOrderComparer();
    private readonly List<PropertyInfo> sortedProperties = new List<PropertyInfo>();

    public ObjectToArrayJsonConverter()
    {
      Type type = typeof(T);
      PropertyInfo[] properties = type.GetProperties();

      foreach (PropertyInfo property in properties)
      {
        JsonPropertyOrderAttribute? propertyOrder = property.GetCustomAttribute<JsonPropertyOrderAttribute>();
        if (propertyOrder != null)
        {
          sortedProperties.InsertOrdered(property, propertyByOrderComparer);
        }
      }
    }

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (!reader.Read() || reader.TokenType == JsonTokenType.Null)
      {
        return default;
      }

      if (reader.TokenType != JsonTokenType.StartArray)
      {
        throw new SerializationException($"Expected token {JsonTokenType.StartArray} but received {reader.TokenType}");
      }

      T retVal = new T();
      foreach (PropertyInfo property in sortedProperties)
      {
        reader.Read();
        switch (reader.TokenType)
        {
          case JsonTokenType.EndArray:
            return retVal;
          default:
            property.SetValue(retVal, JsonSerializer.Deserialize(ref reader, property.PropertyType, options));
            break;
        }
      }

      throw new SerializationException($"Expected token {JsonTokenType.EndArray} but reached end of stream");
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
      writer.WriteStartArray();

      if (value != null)
      {
        foreach (PropertyInfo property in sortedProperties)
        {
          object? propertyValue = property.GetValue(value);
          JsonSerializer.Serialize(writer, propertyValue, options);
        }
      }

      writer.WriteEndArray();
    }

    private sealed class PropertyByOrderComparer : Comparer<PropertyInfo>
    {
      public override int Compare(PropertyInfo? x, PropertyInfo? y)
      {
        int? xOrder = x?.GetCustomAttribute<JsonPropertyOrderAttribute>()?.Order;
        int? yOrder = y?.GetCustomAttribute<JsonPropertyOrderAttribute>()?.Order;

        return Nullable.Compare(xOrder, yOrder);
      }
    }
  }
}
