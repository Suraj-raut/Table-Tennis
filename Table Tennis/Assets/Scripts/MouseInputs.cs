using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputs : MonoBehaviour    ///---Mouse click movement for touch inputs--//
{
    float IPosition;
	Vector3 offset;
	Camera MainCamera;
	bool Dragging;
	
	// Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
		IPosition = MainCamera.WorldToScreenPoint(transform.position).z;  //--Get the position of z and convert to screen point//
    }

    // Update is called once per frame
    void Update()
    {
        if(Dragging)
		{
			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y , IPosition); //---get the mouse position
			transform.position = MainCamera.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
			                                     //--Move the player to the mouse position--//
		}
    }
	
	void OnMouseDown()               //---If the mouse is click--//
	{
		if(!Dragging)
		{
			BeginDrag();
		}
	}
	
	void OnMouseUp()                //---If the mouse is not click--//
	{
		EndDrag();
	}
	
	public void BeginDrag()
	{
		Dragging = true;
		offset = MainCamera.WorldToScreenPoint(transform.position) - Input.mousePosition;  //--get the position of player--//
	}
	
	public void EndDrag()
	{
		Dragging = false;
	}
}
