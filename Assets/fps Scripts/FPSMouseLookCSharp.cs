using UnityEngine;
using System.Collections;

public class FPSMouseLookCSharp : MonoBehaviour {
	
	public enum RotationAxis {MouseXAndY = 0, MouseX = 1 , MouseY = 2}
	
	public RotationAxis RotXY = RotationAxis.MouseXAndY | RotationAxis.MouseX | RotationAxis.MouseY;
	
	public float sensitivityX = 400f;
	public float minimumX = -360f;
	public float maximumX = 360f;
	private float rotationX = 0f;
	
	public float sensitivityY = 400f;
	public float minimumY = -50f;
	public float maximumY = 50f;
	private float rotationY = 0f;
		
	public Quaternion originalRotation;
	
	
	// Use this for initialization
	void Start () {
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (RotXY == RotationAxis.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
			
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (RotXY == RotationAxis.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);

			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);

			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}
	public static float ClampAngle (float Angle, float Min, float Max){
		if(Angle < -360){
			Angle += 360;
		}
		
		if(Angle > 360){
			Angle -= 360;
		}
		
		return Mathf.Clamp (Angle, Min, Max);
	}
}
