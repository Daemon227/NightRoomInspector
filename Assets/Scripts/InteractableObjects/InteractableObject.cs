using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InteractableObject : MonoBehaviour, IPointerClickHandler
{
    public bool canInteract = false;
    public bool canShowOption = true;

    public List<string> optionText;
    public GameObject buttonPrefab;
    public GameObject buttonGroup;

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            canInteract = true;
            InteractWithObject interact = collision.GetComponentInParent<InteractWithObject>();
            if (interact != null) interact.TurnOnNotification();
            if(GameManager.Instance.canInteract)
            {
                ShowOption();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            InteractWithObject interact = collision.GetComponentInParent<InteractWithObject>();
            if (interact != null) interact.TurnOfNotification();
        }
    }

    public void ShowOption()
    {      
        if(canInteract && canShowOption)
        {
            int index = 0;
            foreach(var s in optionText)
            {
                GameObject a = Instantiate(buttonPrefab, buttonGroup.transform);

                //set text
                TextMeshProUGUI option =  a.GetComponentInChildren<TextMeshProUGUI>();
                if(option != null)
                {
                    option.text = s;
                }
                else
                {
                    Debug.Log("Khong co TMP");
                }
                //set script for button
                int currentOption = index;
                a.GetComponent<Button>().onClick.AddListener(() => HandleOption(currentOption));
                index++;
            }
            canShowOption = false;
            // set khong cho di chuyen
            GameManager.Instance.canMove = false;
        }
    }

    public void ClearOption()
    {
        foreach(Transform child in buttonGroup.transform)
        {
            Destroy(child.gameObject);
        }
        canShowOption = true;
    }

    public virtual void HandleOption(int optionIndex) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("InteractableObject OnPointerClick");
        ShowOption();
    }
}

