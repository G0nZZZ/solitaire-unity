// Assets/Core/Models/CardStack.cs  
using System.Collections.Generic;  
using UnityEngine;

namespace SolitaireGame.Core
{
    /// <summary>
    /// Represents a stack of cards at a given world Transform.
    /// </summary>
    public class CardStack
    {
        private readonly Stack<Card> _cards = new Stack<Card>();

        /// <summary>Transform under which all cards in this stack should be parented.</summary>
        public Transform StackOriginTransform { get; set; }

        /// <summary>World‐space origin point (for positioning) of this stack.</summary>
        public Vector3 StackOrigin => StackOriginTransform.position;

        public CardStack(Transform originTransform)
        {
            StackOriginTransform = originTransform;
        }

        public void Push(Card card)
        {
            _cards.Push(card);
            UpdateVisuals();
        }

        public Card Pop()
        {
            var c = _cards.Pop();
            UpdateVisuals();
            return c;
        }

        public Card Peek() => _cards.Peek();
        public int Count => _cards.Count;

        private void UpdateVisuals()
        {

            foreach (var card in _cards)
            {
                if (card.View == null) continue;
                Vector3 pos = StackOrigin + Vector3.down * 0.3f;
                card.SetPosition(pos);
                // also re‐parent the view under the origin Transform:
                card.View.transform.SetParent(StackOriginTransform, worldPositionStays: false);
            }
        }
    }
}