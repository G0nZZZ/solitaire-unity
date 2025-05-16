// Assets/UI/CardView.cs
using UnityEngine;
using SolitaireGame.Core;

namespace SolitaireGame.UI
{
    /// <summary>
    /// Binds a Card model (with CardData) to its SpriteRenderer GameObject,
    /// tracks its current stack, and handles its visual state.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class CardView : MonoBehaviour
    {
        public Card Model { get; private set; }
        public CardStack CurrentStack { get; set; }

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Initializes this view with the model data and assigns to a stack.
        /// </summary>
        public void Initialize(CardData data, CardStack initialStack)
        {
            // Create model
            Model = new Card(data.Suit, data.Rank);
            CurrentStack = initialStack;
            Model.View = this.gameObject;

            // Set sprite
            _spriteRenderer.sprite = data.FaceSprite;
            // Position in world-space
            transform.position = initialStack.StackOrigin;
        }

        /// <summary>
        /// Updates this view's stack reference and world position.
        /// </summary>
        public void MoveToStack(CardStack newStack)
        {
            CurrentStack = newStack;
            transform.position = newStack.StackOrigin;
        }
    }
}