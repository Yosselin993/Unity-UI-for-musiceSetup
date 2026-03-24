using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaylistItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parent;
    private LayoutElement layoutElement;
    private int originalIndex;

    private void Awake()
    {
        parent = transform.parent;
        layoutElement = GetComponent<LayoutElement>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalIndex = transform.GetSiblingIndex();
        layoutElement.ignoreLayout = true; // supposed to allow the item to be dragged
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child == transform) continue; 

            float distance = Mathf.Abs(transform.position.y - child.position.y);

            if (distance < 40f)
            {
                int newIndex = child.GetSiblingIndex();
                transform.SetSiblingIndex(newIndex); // I'm not too sure about the number, but this is supposed to allow the item to be dragged and swap places with the item its crossing
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        layoutElement.ignoreLayout = false;
        transform.localPosition = Vector3.zero;

        PlaylistUI.Instance.UpdateOrderFromUI(); // Supposed to change song order in SongManager
    }
}