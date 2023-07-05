using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using NJsonSchema;

namespace OpenAI.Utilities
{
    /// <summary>
    /// Class for creating json documents for function calls
    /// </summary>
    public static class FunctionHelpers
    {
        /// <summary>
        /// Build a parameter object for a function call for a given Type
        /// </summary>
        /// <typeparam name="T">The expected arguments</typeparam>
        /// <returns></returns>
        public static JsonDocument BuildJsonDocumentForType<T>()
        {
            var schema = JsonSchema.FromType<T>();
            return BuildJsonDocumentFromString(schema.ToJson());            
        }

        /// <summary>
        /// Build a parameter object for a function call for a type given as a Json schema.
        /// Will remove things that OpenAI don't care about
        /// </summary>
        /// <param name="schema">Json schema as string</param>
        /// <returns></returns>
        public static JsonDocument BuildJsonDocumentFromString(string schema)
        {            
            var node = JsonNode.Parse(schema)!.AsObject();
            node.Remove("additionalProperties");
            node.Remove("$schema");
            node.Remove("title");
            var document = JsonDocument.Parse(node.ToJsonString());
            return document;
        }
    }
}
