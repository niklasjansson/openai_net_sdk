﻿using System.Text.Json.Serialization;

namespace OpenAI.SDK.Models.ResponseModels
{
    public record UploadFilesResponse : BaseResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("bytes")] public int Bytes { get; set; }

        [JsonPropertyName("created_at")] public int CreatedAt { get; set; }

        [JsonPropertyName("filename")] public string FileName { get; set; }

        [JsonPropertyName("purpose")] public string Purpose { get; set; }
    }
}