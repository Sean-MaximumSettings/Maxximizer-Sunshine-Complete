using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour 
{
	public  bool isOn;

	public Color onColorBg;
	public Color offColorBg;

    public Color HandleonColorBg;
    public Color HandleoffColorBg;

    public Image toggleBgImage;
	public RectTransform toggle;

	public GameObject handle;
	private RectTransform handleTransform;

	private float handleSize;
	private float onPosX;
	private float offPosX;

	public float handleOffset;


	public float speed;
	static float t = 0.0f;

	private bool switching = false;


	void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 1) - (handleSize/1) - handleOffset;
		offPosX = onPosX * -1;

	}


	void Start()
	{
		if(isOn)
		{
            toggleBgImage.color = onColorBg;
            handle.GetComponent<Image>().color = HandleonColorBg;
			handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
		}
		else
		{
			toggleBgImage.color = offColorBg;
            handle.GetComponent<Image>().color = HandleoffColorBg;
            handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
		}
	}
		
	void Update()
	{

		if(switching)
		{
			Toggle(isOn);
		}
	}

	public void DoYourStuff()
	{
		/*Debug.Log(isOn);
         Writes if the bool is true or false, you can of course here add everything you want for your options
         */
	}

	public void Switching()
	{
		switching = true;
	}
		


	public void Toggle(bool toggleStatus)
	{

        if (toggleStatus)
		{
			toggleBgImage.color = SmoothColor(onColorBg, offColorBg);
            handle.GetComponent<Image>().color = SmoothColor(HandleonColorBg, HandleoffColorBg);
            handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
		}
		else 
		{
			toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
            handle.GetComponent<Image>().color = SmoothColor(HandleoffColorBg, HandleonColorBg);
            handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
		}
			
	}


	Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	{
		// diminuire la velocità
		Vector3 position = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
		StopSwitching();
		return position;
	}

	Color SmoothColor(Color startCol, Color endCol)
	{
		Color resultCol;
		resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
		return resultCol;
	}

	void StopSwitching()
	{
		if(t > 1.0f)
		{
			switching = false;

			t = 0.0f;
			switch(isOn)
			{
			case true:
				isOn = false;
                    DoYourStuff();
				break;

			case false:
				isOn = true;
                    DoYourStuff();
				break;
			}

		}
	}

}
