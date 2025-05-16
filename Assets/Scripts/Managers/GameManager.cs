// Assets/Scripts/Managers/GameManager.cs

using SolitaireGame.Infrastructure;
using UnityEngine;

namespace SolitaireGame.Managers
{
    /// <summary>
    /// Bootstrap to wire EventBus to CommandBus and delegate deck initialization.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Reference to DeckManager to initialize and manage the card deck.")]
        [SerializeField] private DeckManager _deckManager;

        private void Awake()
        {
            if (_deckManager == null)
                Debug.LogError("GameManager: DeckManager reference is missing.");

            // Subscribe EventBus to execute commands
            EventBus.CommandRequested += CommandBus.Instance.Execute;
        }

        private void OnDestroy()
        {
            EventBus.CommandRequested -= CommandBus.Instance.Execute;
        }

        private void Start()
        {
            // Initialize deck (shuffle, instantiate, parent, colliders)
            _deckManager.InitializeDeck();
        }
    }
}
