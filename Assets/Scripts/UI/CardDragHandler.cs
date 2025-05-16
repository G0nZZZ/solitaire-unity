// Assets/Scripts/UI/CardDragHandler.cs
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class CardDragHandler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPos;
    private Transform _startParent;
    private SpriteRenderer _sr;
    private Collider2D _collider;
    private int _originalOrder;

    // Exponemos el valor original para que DropZone lo use en Undo/Redo
    public int OriginalSortingOrder => _originalOrder;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        _startPos = transform.position;
        _startParent = transform.parent;

        
        if (_sr != null)
            _originalOrder = _sr.sortingOrder;

        
        if (_sr != null)
            _sr.sortingOrder = 100;

        
        if (_collider != null)
            _collider.enabled = false;
    }

    public void OnDrag(PointerEventData e)
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(e.position);
        p.z = 0;
        transform.position = p;
    }

    public void OnEndDrag(PointerEventData e)
    {
        if (_collider != null)
            _collider.enabled = true;

        StartCoroutine(ResetIfNoDrop());
    }

    private IEnumerator ResetIfNoDrop()
    {
        yield return new WaitForEndOfFrame();
        if (transform.parent == _startParent)
            transform.position = _startPos;
    }
}