using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.ObjectModels.RequestModels
{
    public abstract class JsonSchemaBaseObject
    {

    }

    /// <summary>
    ///     Function parameter is a JSON Schema object. 
    ///     https://json-schema.org/understanding-json-schema/reference/object.html
    /// </summary>
    public class JsonSchemaObject : JsonSchemaBaseObject
    {
        /// <summary>
        ///     Required. Function parameter object type. Default value is "object".
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "object";

        /// <summary>
        ///     Optional. List of "function arguments", as a dictionary that maps from argument name
        ///     to an object that describes the type, maybe possible enum values, and so on.
        /// </summary>
        [JsonPropertyName("properties")]
        public IDictionary<string, JsonSchemaBaseObject>? Properties { get; set; }

        /// <summary>
        ///     Optional. List of "function arguments" which are required.
        /// </summary>
        [JsonPropertyName("required")]
        public IList<string>? Required { get; set; }
    }

    /// <summary>
    ///     Each property value is a JSON Schema object with its own keys and values.
    ///     The documentation (https://platform.openai.com/docs/guides/gpt/function-calling)
    ///     suggests that only a few specific keys are used: type, description, and sometimes enum.
    /// </summary>
    public class JsonSchemaPlainValue : JsonSchemaBaseObject
    {
        /// <summary> 
        ///     Argument type (e.g. string, integer, and so on). 
        ///     For examples, see https://json-schema.org/understanding-json-schema/reference/object.html
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "string";

        /// <summary>
        ///     Optional. Argument description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Optional. List of allowed values for this argument.
        /// </summary>
        [JsonPropertyName("enum")]
        public IList<string>? Enum { get; set; }


    }

    public class JsonSchemaArrayValue : JsonSchemaBaseObject
    {
        /// <summary> 
        ///     Argument type (e.g. string, integer, and so on). 
        ///     For examples, see https://json-schema.org/understanding-json-schema/reference/object.html
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "array";

        /// <summary>
        ///     Optional. Argument description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Item types
        /// </summary>
        [JsonPropertyName("items")]
        public JsonSchemaBaseObject Items { get; set; }
    }

}
