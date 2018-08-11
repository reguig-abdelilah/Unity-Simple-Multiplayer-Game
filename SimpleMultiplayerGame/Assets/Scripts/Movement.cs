using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Rigidbody rb;
    Vector3 direction;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Move(float x,float z)
    {
        transform.Rotate(0, x, 0);
        direction = transform.forward * z;
        Vector3 velocity = direction.normalized * 5;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
    public GameObject canon, testObj  ; //Assign to the object you want to rotate

    public void RotateCanon(Vector2 mousePosition)
    {

        Ray cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay,out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            
            pointToLook = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
            

            var relativePos = pointToLook - transform.position;
            canon.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        }
    }



}
