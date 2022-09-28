using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZCoord;

    public float lowerYBound;
    public float lowerXBound;
    public float upperXBound;

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        //mousePoint.z = mouseZCoord;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePoint);
        return worldPos;
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos() + mouseOffset;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, lowerXBound, upperXBound), Mathf.Max(lowerYBound, newPos.y), transform.position.z);
    }


}
