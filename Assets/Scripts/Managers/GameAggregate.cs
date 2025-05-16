// Aggregate root to rebuild and handle commands

using System;
using System.Collections.Generic;
using System.Linq;
using SolitaireGame.Core;
using SolitaireGame.Managers;
using Soliteire.EventSourcing;

public class GameAggregate
    {
        private readonly List<IGameEvent> _changes = new();
        public List<CardStack> Stacks { get; private set; } = new();

        public GameAggregate(IEnumerable<IGameEvent> history)
        {
            foreach (var e in history) Apply(e);
        }

        public void MoveCard(Guid cardId, Guid fromStackId, Guid toStackId, int fromIndex, int toIndex)
        {
            var ev = new CardMovedEvent(cardId, fromStackId, toStackId, fromIndex, toIndex);
            Apply(ev);
            _changes.Add(ev);
        }

        public void FlipCard(Guid cardId, bool faceUp)
        {
            var ev = new CardFlippedEvent(cardId, faceUp);
            Apply(ev);
            _changes.Add(ev);
        }

        public void StartGame()
        {
            var ev = new GameStartedEvent();
            Apply(ev);
            _changes.Add(ev);
        }

        public void ResetGame()
        {
            var ev = new GameResetEvent();
            Apply(ev);
            _changes.Add(ev);
        }

        public IEnumerable<IGameEvent> GetUncommitted() => _changes;
        public void MarkCommitted() => _changes.Clear();

        private void Apply(IGameEvent ev)
        {
            switch (ev)
            {
                case CardMovedEvent m: Handle(m); break;
                case CardFlippedEvent f: Handle(f); break;
                case GameStartedEvent s: Initialize(); break;
                case GameResetEvent r: Initialize(); break;
            }
        }

        private void Handle(CardMovedEvent ev)
        {

        }

        private void Handle(CardFlippedEvent ev)
        {

        }

        private void Initialize()
        {

        }
    }