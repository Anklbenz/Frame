using System;
using UnityEngine;

public abstract class MouseMove {
    protected Vector2 mousePosition{ get; private set; }
    protected float zoomAmount{ get; private set; }

    public void Update(){

        if (Input.GetMouseButton(0)) Drag();
        
        if (Input.GetMouseButtonUp(0)) StopDrag();
        
        if (Input.GetMouseButton(1)) Rotate();
        
        if (Input.GetMouseButtonUp(1)) StopRotate();
        
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            mousePosition = Input.mousePosition;

        if (Input.GetAxis("Mouse ScrollWheel") != 0){
            zoomAmount = Input.mouseScrollDelta.y;
            Zoom();
        }
    }

    protected abstract void StopRotate();
    protected abstract void StopDrag();
    protected abstract void Drag();
    protected abstract void Zoom();
    protected abstract void Rotate();
}