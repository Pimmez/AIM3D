using UnityEngine;
using System.Collections;

public class CheckInput : CheckVisibility {

    [SerializeField]
    private GameObject particleEffect;

    [SerializeField]
    private string inputString = "Interact";

    [SerializeField]
    protected bool onlyUsableOnce = true;

    protected bool useAble = true;

	private bool inView;

    //When object is visible activited StartCoroutine
    protected override void Visible ()
	{
		base.Visible ();
        //if the we were previously not in view, and we are usable, start checking for input
		if (!inView && useAble)
		{
            //sets the boolean to true when its visible
            inView = true;
			StartCoroutine(WaitForInput());

            //particleEffect.SetActive(true);
        }
	}
	
	protected override void NotVisible ()
	{
		//When its not visible the coroutine cant be used
		base.NotVisible ();
        inView = false;

        //stop waiting for a input
        StopCoroutine("WaitForInput");

        //particleEffect.SetActive(false);
	}
	
	protected virtual void InputRecieved()
	{
		//continued in the next script

	}

	//IEnumerator  that will give you the input, the boolean will automaticly set false when its over.
	IEnumerator WaitForInput()
	{
		while (inView) 
		{
			if (Input.GetButtonDown(inputString))
			{
                //we recieved input and go on the the next script
                InputRecieved();

                particleEffect.SetActive(false);

                inView = false;
            }
			yield return null;
		}
    }
}