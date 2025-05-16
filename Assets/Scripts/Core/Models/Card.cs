// Assets/Scripts/Core/Card.cs
using UnityEngine;

namespace SolitaireGame.Core
{
    /// <summary>
    /// Represents the data of a single card in Solitaire.
    /// </summary>
    public class Card
    {
        public enum SuitType { Hearts, Diamonds, Clubs, Spades }
        public enum RankType
        {
            Ace = 1, Two, Three, Four, Five, Six,
            Seven, Eight, Nine, Ten, Jack, Queen, King
        }

        public SuitType Suit { get; private set; }
        public RankType Rank { get; private set; }
        public GameObject View { get; set; }  // Assigned by CardView.Initialize

        public Card(SuitType suit, RankType rank)
        {
            Suit = suit;
            Rank = rank;
        }

        /// <summary>
        /// Updates the visual position of the card.
        /// </summary>
        public void SetPosition(Vector3 worldPosition)
        {
            View.transform.position = worldPosition;
        }

        /// <summary>
        /// Enables or disables the card's interaction.
        /// </summary>
        public void SetInteractable(bool interactable)
        {
            var collider = View.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = interactable;
        }
    }
}
