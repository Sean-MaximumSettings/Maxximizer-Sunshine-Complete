using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialFill : MonoBehaviour {

    public Image fillImage;
    public float Speed = 2f;
    public TextMeshProUGUI percentage;


    public bool restart = true;

    void LateUpdate()
    {
        if (fillImage.fillAmount == 1 && restart)
        {
            fillImage.fillAmount = 0;
        }
        fillImage.fillAmount += Speed * Time.deltaTime;
        percentage.text = (fillImage.fillAmount * 100).ToString("0") + "%";
    }

}
