using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public TextMeshProUGUI sliderValue;
    public string extraString = "%";
    public void ValueChange()
    {
       sliderValue.text = this.gameObject.GetComponent<Slider>().value.ToString("0")+ extraString;
    }
}
