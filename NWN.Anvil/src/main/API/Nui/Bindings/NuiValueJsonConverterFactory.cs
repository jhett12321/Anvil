using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Anvil.API
{
  public sealed class NuiValueJsonConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType.GetGenericTypeDefinition() == typeof(NuiValue<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      Type valueType = typeToConvert.GetGenericArguments()[0];
      return (JsonConverter)Activator.CreateInstance(typeof(NuiValueJsonConverter<>).MakeGenericType(valueType), BindingFlags.Instance | BindingFlags.Public, null, new object?[] { options }, null)!;
    }
  }
}
