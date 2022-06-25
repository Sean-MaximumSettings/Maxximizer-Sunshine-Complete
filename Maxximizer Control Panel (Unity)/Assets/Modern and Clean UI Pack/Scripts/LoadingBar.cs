using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public float speed = 0.1f;
    public TextMeshProUGUI percentage;
    public bool restart = true;

    void LateUpdate()
    {
        if (this.GetComponent<Slider>().value == this.GetComponent<Slider>().maxValue && restart)
        {
            this.GetComponent<Slider>().value = 0;
        }
        this.GetComponent<Slider>().value = this.GetComponent<Slider>().value + speed * Time.deltaTime;
        percentage.text = (this.GetComponent<Slider>().value * 100).ToString("0") + "%";
    }
}
