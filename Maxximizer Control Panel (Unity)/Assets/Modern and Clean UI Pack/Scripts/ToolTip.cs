using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public GameObject tooltip;
    public Canvas canvas;
    public float fadeTime = 0.1f;
    bool inside;


    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public static IEnumerator FadeIn(CanvasGroup group, float alpha, float duration)
    {
        var time = 0.0f;
        var originalAlpha = group.alpha;
        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
            yield return new WaitForEndOfFrame();
        }

        group.alpha = alpha;
    }
    public static IEnumerator FadeOut(CanvasGroup group, float alpha, float duration)
    {
        var time = 0.0f;
        var originalAlpha = group.alpha;
        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
            yield return new WaitForEndOfFrame();
        }

        group.alpha = alpha;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (tooltip != null)
        {
            StartCoroutine(FadeIn(tooltip.GetComponent<CanvasGroup>(), 1.0f, fadeTime));

            inside = true;
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (tooltip != null)
        {
            StartCoroutine(FadeOut(tooltip.GetComponent<CanvasGroup>(), 0.0f, fadeTime));
        }
    }
    void Update()
    {
        if (inside)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            tooltip.transform.position = canvas.transform.TransformPoint(pos);
            tooltip.GetComponent<RectTransform>().localPosition = new Vector3(tooltip.GetComponent<RectTransform>().localPosition.x-25f, tooltip.GetComponent<RectTransform>().localPosition.y+23f, 1);
        }
    }
}
