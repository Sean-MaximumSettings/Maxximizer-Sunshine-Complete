using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

    public class InputFieldAnimation : MonoBehaviour, IPointerClickHandler
    {
        public GameObject Trigger;
        private TMP_InputField inputText;

        private bool clicked = false;

        void Start()
        {
            inputText = gameObject.GetComponent<TMP_InputField>();

        }

        void Update()
        {
            if (inputText.text != "")
            {
                this.gameObject.GetComponent<Animator>().Play("active");
            }

            else if (clicked == false)
            {
                this.gameObject.GetComponent<Animator>().Play("disable");
            }
        }

        public void FieldTrigger()
        {
            if (inputText.text != "")
            {
            Trigger.SetActive(false);
            clicked = false;
            }

            else
            {
              
                this.gameObject.GetComponent<Animator>().Play("disable");
                Trigger.SetActive(false);
                clicked = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clicked = true;
            this.gameObject.GetComponent<Animator>().Play("active");
            Trigger.SetActive(true);
        }
    }