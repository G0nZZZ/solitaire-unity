// Assets/Scripts/Commands/MoveCommand.cs


using UnityEngine;

namespace SolitaireGame.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly Transform _card;
        private readonly Transform _fromParent;
        private readonly Transform _toParent;
        private readonly int _fromOrder;
        private readonly int _toOrder;

        public MoveCommand(Transform card, Transform fromParent, int fromOrder,
            Transform toParent, int toOrder)
        {
            _card = card;
            _fromParent = fromParent;
            _fromOrder = fromOrder;
            _toParent = toParent;
            _toOrder = toOrder;
        }

        public void Execute()
        {
            // Reparent to target drop zone at child origin
            _card.SetParent(_toParent, worldPositionStays: false);
            _card.localPosition = Vector3.zero;

            // Restore sorting order at destination
            var sr = _card.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sortingOrder = _toOrder;
        }

        public void Undo()
        {
            var sr = _card.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sortingOrder = _fromOrder;   

            _card.SetParent(_fromParent, worldPositionStays: false);
            _card.localPosition = Vector3.zero;
        }
    }
}
