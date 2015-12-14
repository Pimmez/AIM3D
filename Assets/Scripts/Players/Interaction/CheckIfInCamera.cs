using UnityEngine;

public class CheckIfInCamera : WaitForCheck {

    protected override void Check()
    {
        base.Check();

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        //if i am in the camera
        if (GeometryUtility.TestPlanesAABB (planes, GetComponent<Renderer> ().bounds)) {
			Visible ();
		} 
		else {
			NotVisible();
		}
    }

    protected virtual void Visible() {
        Debug.Log("Object is in Camera");
		

    }

	protected virtual void NotVisible() {
		Debug.Log("Object is in not Camera");
		
				
	}
}
