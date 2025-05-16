// Assets/Scripts/Infrastructure/EventBus.cs
using System;
using SolitaireGame.Commands;

namespace SolitaireGame.Infrastructure
{
    public static class EventBus
    {
        public static event Action<ICommand> CommandRequested;
        public static void Raise(ICommand cmd) => CommandRequested?.Invoke(cmd);
    }
}
