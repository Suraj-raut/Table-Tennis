using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputs : MonoBehaviour
{
    float IPosition;
	Vector3 offset;
	Camera MainCamera;
	bool Dragging;
	
	// Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
		IPosition = MainCamera.WorldToScreenPoint(transform.position).z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Dragging)
		{
			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y , IPosition);
			transform.position = MainCamera.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
			
		}
    }
	
	void OnMouseDown()
	{
		if(!Dragging)
		{
			BeginDrag();
		}
	}
	
	void OnMouseUp()
	{
		EndDrag();
	}
	
	public void BeginDrag()
	{
		Dragging = true;
		offset = MainCamera.WorldToScreenPoint(transform.position) - Input.mousePosition;
	}
	
	public void EndDrag()
	{
		Dragging = false;
	}
}
