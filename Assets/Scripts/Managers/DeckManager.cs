// Assets/Scripts/Managers/DeckManager.cs  
using System.Collections.Generic;
using System.Linq;
using SolitaireGame.Core;
using SolitaireGame.UI;
using UnityEngine;

namespace SolitaireGame.Managers
{
    /// <summary>
    /// Handles creation, shuffling, and initial setup of the card deck.
    /// </summary>
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager Instance { get; private set; }

        [Tooltip("Assign the Card prefab here.")]     
        [SerializeField] private GameObject _cardPrefab;
        [Tooltip("All CardData assets (should contain 52 entries)")]     
        [SerializeField] private CardData[] _cardDataArray;
        [Tooltip("Container transform for initial deck in hierarchy.")]     
        [SerializeField] private Transform _initialStackParent;
        [Tooltip("All drop‐zone Transforms in your scene.")]     
        [SerializeField] private DropZone[] _dropZoneArray;

        private CardFactory _cardFactory;
        private readonly List<CardStack> _stacks = new List<CardStack>();
        private readonly Dictionary<Transform, CardStack> _stackMap = new Dictionary<Transform, CardStack>();

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        public void InitializeDeck()
        {
            // prepare factory & data
            _cardFactory = new CardFactory(_cardPrefab);
            var dataList = new List<CardData>(_cardDataArray);
            for (int i = dataList.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (dataList[i], dataList[j]) = (dataList[j], dataList[i]);
            }

            // create stacks and map their Transforms
            _stacks.Clear();
            _stackMap.Clear();

            var initialStack = new CardStack(_initialStackParent);
            _stacks.Add(initialStack);
            _stackMap[_initialStackParent] = initialStack;

            foreach (var dz in _dropZoneArray)
            {
                var s = new CardStack(dz.transform);
                _stacks.Add(s);
                _stackMap[dz.transform] = s;
            }

            // instantiate each card into the initial stack
            for (int i = 0; i < dataList.Count; i++)
            {
                var data = dataList[i];
                var go = _cardFactory.Create();
                go.name = $"Card_{data.Suit}_{data.Rank}";
                var view = go.GetComponent<CardView>();
                view.Initialize(data, initialStack);     // sets Model, View, and parent :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}

                // sorting order (to see the stack nicely)
                var sr = go.GetComponent<SpriteRenderer>();
                if (sr != null) sr.sortingOrder = i;

                // push into model and visuals
                initialStack.Push(view.Model);
            }
        }

        /// <summary>Lookup the CardStack for any drop‐zone Transform.</summary>
        public CardStack GetStackByTransform(Transform zone)
        {
            if (_stackMap.TryGetValue(zone, out var stack))
                return stack;

            // Debug: lista todas las claves
            var keys = string.Join(", ", _stackMap.Keys.Select(t => t.name));
            Debug.LogError($"[DeckManager] Clave no encontrada para {zone.name}. Pilas registradas: {keys}");
            return null;
        }
            
    }
}
