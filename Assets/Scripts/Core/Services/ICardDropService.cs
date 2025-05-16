// ICardDropService.cs
using UnityEngine;
using UnityEngine.EventSystems;

namespace SolitaireGame.Services
{
    /// <summary>
    /// Servicio encargado de manejar la lógica de drop de cartas: cálculo de sortingOrder,
    /// reparenting y creación de comandos de movimiento.
    /// </summary>
    public interface ICardDropService
    {
        /// <summary>
        /// Procesa la lógica de soltar una carta en una zona de drop.
        /// </summary>
        /// <param name="cardTransform">Transform de la carta que se suelta.</param>
        /// <param name="dropZoneTransform">Transform de la zona donde se suelta la carta.</param>
        /// <param name="originalOrder">Sorting order original para Undo/Redo.</param>
        void Drop(Transform cardTransform, Transform dropZoneTransform, int originalOrder);
    }
}