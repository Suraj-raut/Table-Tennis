using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 3.0f;
    private float force = 3.0f;
	
    [SerializeField]
    private Transform aimTarget;
	
	
   	
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		if(h != 0 || v != 0 )
		{
			transform.Translate(new Vector3(v, 0, h) * speed * Time.deltaTime);
		}
		
		aimTarget.Translate(new Vector3(0, 0, -h) * speed * Time.deltaTime);
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ball"))
		{
			Vector3 dir = aimTarget.position - transform.position;
			Vector3 offset = new Vector3(0, 0.2f, 0);
			
			other.transform.position = Vector3.SmoothDamp(other.transform.position, aimTarget.position, ref dir,force * Time.deltaTime);
			other.GetComponent<Rigidbody>().velocity = dir.normalized * force * offset.y;
		}
	}
}
