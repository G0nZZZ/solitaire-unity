// Assets/Scripts/Services/CardDropService.cs  
using SolitaireGame.Commands;  
using SolitaireGame.Core;  
using SolitaireGame.Infrastructure;
using SolitaireGame.Managers;
using SolitaireGame.UI;  
using UnityEngine;

namespace SolitaireGame.Services
{
    public class CardDropService : MonoBehaviour, ICardDropService
    {
        public void Drop(Transform cardT, Transform dropZoneT, int originalOrder)
        {
            var view      = cardT.GetComponent<CardView>();
            var fromStack = view.CurrentStack;
            var newStack  = DeckManager.Instance.GetStackByTransform(dropZoneT);
    
            // calcula toOrder igual que antes…
            int toOrder = cardT.GetSiblingIndex() + 1;

            var cmd = new MoveCommand(view, fromStack, originalOrder, newStack, toOrder);
            CommandBus.Instance.Execute(cmd);
        }
    }
}