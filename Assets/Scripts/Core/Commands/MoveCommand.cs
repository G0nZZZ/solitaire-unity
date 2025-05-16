// Assets/Scripts/Commands/MoveCommand.cs  
using SolitaireGame.Core;  
using SolitaireGame.UI;  
using UnityEngine;

namespace SolitaireGame.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly CardView _view;
        private readonly CardStack _from;
        private readonly CardStack _to;
        private readonly Card _card;
        private readonly int _fromOrder;
        private readonly int _toOrder;

        public MoveCommand(
            CardView view,
            CardStack fromStack, int fromOrder,
            CardStack toStack,   int toOrder)
        {
            _view      = view;
            _from      = fromStack;
            _to        = toStack;
            _card      = view.Model;
            _fromOrder = fromOrder;
            _toOrder   = toOrder;
        }

        public void Execute()
        {
            _from.Pop();             
            _to.Push(_card);        

            var t = _view.transform;
            t.SetParent(_to.StackOriginTransform, false);
            t.localPosition = Vector3.zero;
            _view.CurrentStack = _to;

            var sr = t.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = _toOrder;
        }

        public void Undo()
        {
            _to.Pop();              
            _from.Push(_card);      

            var t = _view.transform;
            t.SetParent(_from.StackOriginTransform, false);
            t.localPosition = Vector3.zero;
            _view.CurrentStack = _from;

            var sr = t.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = _fromOrder;
        }
    }
}