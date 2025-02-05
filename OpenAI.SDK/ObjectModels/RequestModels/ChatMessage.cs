﻿using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace OpenAI.ObjectModels.RequestModels;

/// <summary>
///     The contents of the message.
///     Messages must be an array of message objects, where each object has a role (either “system”, “user”, or
///     “assistant”) and content (the content of the message) and an optional name
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="role">The role of the author of this message. One of system, user, or assistant.</param>
    /// <param name="content">The contents of the message.</param>
    /// <param name="name">The name of the author of this message. May contain a-z, A-Z, 0-9, and underscores, with a maximum length of 64 characters.</param>
    /// <param name="functionCall">The name and arguments of a function that should be called, as generated by the model.</param>
    public ChatMessage(string role, string content, string? name = null, FunctionCall? functionCall = null)
    {
        Role = role;
        Content = content;
        Name = name;
        FunctionCall = functionCall;
    }

    /// <summary>
    ///   The role of the author of this message. One of system, user, or assistant.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; }

    /// <summary>
    ///     The contents of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }

    /// <summary>
    ///     The name of the author of this message. May contain a-z, A-Z, 0-9, and underscores, with a maximum length of 64 characters.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     The name and arguments of a function that should be called, as generated by the model.
    /// </summary>
    [JsonPropertyName("function_call")]
    public FunctionCall? FunctionCall { get; set; }

    public static ChatMessage FromAssistant(string content, string? name = null)
    {
        return new ChatMessage(StaticValues.ChatMessageRoles.Assistant, content, name);
    }
    public static ChatMessage FromFunction(string content, string? name = null)
    {
        return new ChatMessage(StaticValues.ChatMessageRoles.Function, content, name);
    }
    public static ChatMessage FromUser(string content, string? name = null)
    {
        return new ChatMessage(StaticValues.ChatMessageRoles.User, content, name);
    }

    public static ChatMessage FromSystem(string content, string? name = null)
    {
        return new ChatMessage(StaticValues.ChatMessageRoles.System, content, name);
    }
}

/// <summary>
///     Describes a function call returned from GPT.
///     A function call contains a function name, and a dictionary 
///     mapping function argument names to their values.
/// </summary>
public class FunctionCall
{
    /// <summary>
    ///     Function name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Function arguments, returned as a JSON-encoded dictionary mapping
    ///     argument names to argument values.
    /// </summary>
    [JsonPropertyName("arguments")]
    public string? Arguments { get; set; }

    public Dictionary<string, object> ParseArguments()
    {
        var result = !string.IsNullOrWhiteSpace(Arguments) ? JsonSerializer.Deserialize<Dictionary<string, object>>(Arguments) : null;
        return result ?? new();
    }
}

/// <summary>
///     Definition of a valid function call. 
/// </summary>
public class FunctionDefinition
{
    /// <summary>
    ///     Required. The name of the function to be called. Must be a-z, A-Z, 0-9, 
    ///     or contain underscores and dashes, with a maximum length of 64.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Optional. The description of what the function does.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     Optional. The parameters the functions accepts, described as a JSON Schema object. 
    ///     See the guide (https://platform.openai.com/docs/guides/gpt/function-calling) for examples, 
    ///     and the JSON Schema reference (https://json-schema.org/understanding-json-schema/)
    ///     for documentation about the format.
    /// </summary>
    [JsonPropertyName("parameters")]
    public JsonDocument? Parameters { get; set; }
}


public class FunctionDefinitionBuilder
{
    private readonly FunctionDefinition _definition;

    public FunctionDefinitionBuilder(string fnName, string? fnDescription)
    {
        ValidateName(fnName);

        _definition = new()
        {
            Name = fnName,
            Description = fnDescription
        };
    }

    private const string ValidNameChars =
        "abcdefghijklmnopqrstuvwxyz" +
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "0123456789-_";

    private static void ValidateName(string fnName)
    {
        if (fnName.Length > 64)
        {
            Throw($"Function name is too long. ");
        }

        foreach (var ch in fnName)
        {
            if (!ValidNameChars.Contains(ch))
            {
                Throw($"Function name contains an invalid character: '{ch}'. ");
            }
        }

        static void Throw(string message)
        {
            throw new ArgumentOutOfRangeException(nameof(fnName), message +
                " The name of the function must be a-z, A-Z, 0-9, or contain underscores and dashes, with a maximum length of 64.");
        }
    }    

    public FunctionDefinition Build() => _definition;
}