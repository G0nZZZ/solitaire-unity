// File: FileEventStore.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Soliteire.EventSourcing
{
    // Marker interface for domain events
    public interface IGameEvent { DateTime OccurredAt { get; } }

    [Serializable]
    internal class StoredEvent
    {
        public string Type;
        public string Data;
    }

    public interface IEventStore
    {
        Task AppendAsync(IGameEvent @event);
        Task<IReadOnlyList<IGameEvent>> LoadAllAsync();
    }

    public class FileEventStore : IEventStore
    {
        private readonly string _filePath;

        public FileEventStore(string filePath)
        {
            _filePath = filePath;
        }

        public async Task AppendAsync(IGameEvent @event)
        {
            var stored = new StoredEvent
            {
                Type = @event.GetType().AssemblyQualifiedName,
                Data = JsonUtility.ToJson(@event)
            };
            var jsonLine = JsonUtility.ToJson(stored);
            await File.AppendAllTextAsync(_filePath, jsonLine + Environment.NewLine);
        }

        public async Task<IReadOnlyList<IGameEvent>> LoadAllAsync()
        {
            if (!File.Exists(_filePath))
                return Array.Empty<IGameEvent>();

            var lines = await File.ReadAllLinesAsync(_filePath);
            var events = new List<IGameEvent>(lines.Length);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var stored = JsonUtility.FromJson<StoredEvent>(line);
                var type = Type.GetType(stored.Type)
                           ?? throw new TypeLoadException($"Unknown event type: {stored.Type}");

                var @event = JsonUtility.FromJson(stored.Data, type) as IGameEvent
                             ?? throw new InvalidCastException($"Failed casting event to IGameEvent: {stored.Type}");
                events.Add(@event);
            }

            return events;
        }
    }

    // --- Domain Events ---

    [Serializable]
    public class CardMovedEvent : IGameEvent
    {
        public string CardId;
        public string FromStackId;
        public string ToStackId;
        public int FromIndex;
        public int ToIndex;
        public string OccurredAt; // ISO 8601

        public DateTime OccurredAtTime => DateTime.Parse(OccurredAt, null, System.Globalization.DateTimeStyles.RoundtripKind);
        DateTime IGameEvent.OccurredAt => OccurredAtTime;

        public CardMovedEvent() { }

        public CardMovedEvent(Guid cardId, Guid fromStackId, Guid toStackId, int fromIndex, int toIndex)
        {
            CardId = cardId.ToString();
            FromStackId = fromStackId.ToString();
            ToStackId = toStackId.ToString();
            FromIndex = fromIndex;
            ToIndex = toIndex;
            OccurredAt = DateTime.UtcNow.ToString("o");
        }
    }

    [Serializable]
    public class CardFlippedEvent : IGameEvent
    {
        public string CardId;
        public bool IsFaceUp;
        public string OccurredAt;

        public DateTime OccurredAtTime => DateTime.Parse(OccurredAt, null, System.Globalization.DateTimeStyles.RoundtripKind);
        DateTime IGameEvent.OccurredAt => OccurredAtTime;

        public CardFlippedEvent() { }

        public CardFlippedEvent(Guid cardId, bool isFaceUp)
        {
            CardId = cardId.ToString();
            IsFaceUp = isFaceUp;
            OccurredAt = DateTime.UtcNow.ToString("o");
        }
    }

    [Serializable]
    public class GameStartedEvent : IGameEvent
    {
        public string OccurredAt = DateTime.UtcNow.ToString("o");
        public DateTime OccurredAtTime => DateTime.Parse(OccurredAt, null, System.Globalization.DateTimeStyles.RoundtripKind);
        DateTime IGameEvent.OccurredAt => OccurredAtTime;
    }

    [Serializable]
    public class GameResetEvent : IGameEvent
    {
        public string OccurredAt = DateTime.UtcNow.ToString("o");
        public DateTime OccurredAtTime => DateTime.Parse(OccurredAt, null, System.Globalization.DateTimeStyles.RoundtripKind);
        DateTime IGameEvent.OccurredAt => OccurredAtTime;
    }
}
