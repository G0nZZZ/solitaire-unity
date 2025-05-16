// Assets/Scripts/UI/DropZone.cs

using System;
using SolitaireGame.Commands;
using SolitaireGame.Infrastructure;
using SolitaireGame.Services;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour, IDropHandler
{
    // Inyección por inspector
    [SerializeField] private GameObject _dropServiceBehaviour;
   
    private  ICardDropService _dropService;
    void Awake()
    {
        _dropService = _dropServiceBehaviour.GetComponent<ICardDropService>();
    }
    
    public void OnDrop(PointerEventData e)
    {
        var cardGO = e.pointerDrag;
        if (cardGO == null) return;

        // Recuperar sorting original
        var drag = cardGO.GetComponent<CardDragHandler>();
        int originalOrder = drag != null
            ? drag.OriginalSortingOrder
            : cardGO.GetComponent<SpriteRenderer>()?.sortingOrder ?? 0;

        // Delegar al servicio
        _dropService.Drop(cardGO.transform, transform, originalOrder);
    }
}

    
