using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    void Update()
    {
         //This function takes care of the actual movement of the object, and making sure the camera stays focused
        if (dragging)
        {
            //Move object, taking into account original offset.
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    
    }

    //Controls picking up the object! In our case, our weapons.
    private void OnMouseDown()
    {
        //Record the difference between the objects centre, and the clicked point on the camera plane
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    //Controls setting our object down! In our case, our weapon.
    private void OnMouseUp()
    {

        //stop dragging
        dragging = false;
    }
}
