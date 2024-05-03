using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.HapticEngine;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour
{
    public StrikerDevice mavrik;
    public Transform mavrikFirePoint;

    public Rigidbody currentRB;
    public GameObject currentObj;
    public GameObject lastObject;

    private bool isHighlighted;

    public Button button;

    public RaycastHit selectionPoint;

    public HapticEffectAsset highlightHaptic;

    // Define the delegate type for the new round event
    public delegate void ButtonSelectedEventHandler();

    // Declare the new round event using the delegate type
    public event ButtonSelectedEventHandler OnButtonSelected;

    void Awake()
    {
        currentRB = null;
        button = null;
        lastObject = null;
        isHighlighted = false;

    }

    void Update()
    {
        HighlightObjectInRay();
                
        SelectionTriggerDown();
        ButtonTriggerDown();

        if (!isHighlighted)
        {
            ClearHighlighted();
        }

    }

    void HighlightObject(GameObject gameObject)
    {
        if (lastObject != gameObject && !isHighlighted)
        {   
            //ClearHighlighted();
            
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            isHighlighted = true;
            lastObject = gameObject;
            mavrik.FireHaptic(highlightHaptic);
            InvokeRepeating(nameof(HighlightHaptic), 1f, 1f);
        }
        
    }

    void ClearHighlighted()
    {
        // If there is a last object or current object with a child object, deactivate the child object and nullify objects needed for raycast. 
        if ((lastObject != null && lastObject.transform.GetChild(0).gameObject != null) || currentRB == null || (currentObj != null && !currentObj.CompareTag("Selectable")))
        {
            lastObject.transform.GetChild(0).gameObject.SetActive(false);
            lastObject = null;
        }

        if (currentObj != null && currentObj.transform.GetChild(0).gameObject != null)
        {
            currentObj.transform.GetChild(0).gameObject.SetActive(false);
            currentObj = null;
        }

        CancelInvoke("HighlightHaptic");

        
        
    }

    void HighlightObjectInRay()
    {
       

        // Check if we hit something.
        if (Physics.Raycast(mavrikFirePoint.position, mavrikFirePoint.forward, out selectionPoint))
        {
            // asssign currentRB if a rigidbody is hit by the ray
            if(selectionPoint.rigidbody != null)
            {
                // Get the object that was hit.
                currentRB = selectionPoint.rigidbody;
            }
            else
            {
                currentRB = null;
            }

            // if there is a rigidbody being hit by the ray assign its object as the current object apply highlight effect
            if (currentRB.gameObject != null)
            {
                currentObj = currentRB.gameObject;
           
                HighlightObject(currentRB.gameObject);
            }
            else
            {
                currentObj = null;
            }

            // clear the highlight if the current object is not "Selectable" or the ray is hitting no rigidbody
            if (!selectionPoint.rigidbody.gameObject.CompareTag("Selectable") || selectionPoint.rigidbody == null)
            {
                isHighlighted = false;
                
            }
        }
        else
        {
            isHighlighted = false;
              
        }

       


    }

    // Selection made on Trigger Down
    public void SelectionTriggerDown()
    {
        if (mavrik.GetTriggerDown())
        {
            //button.onClick.Invoke();
            InvokeButtonSelectedEvent();
        }
    }

    // Selection made on Trigger Down
    public void ButtonTriggerDown()
    {
        if (currentObj != null)
        {
            button = currentObj.GetComponent<Button>();

            if (mavrik.GetTriggerDown())
            {
                button.onClick.Invoke();
            }
        }
    }

    //Fire haptic to go with highlighting a selection 
    public void HighlightHaptic()
    {
        mavrik.FireHaptic(highlightHaptic);
    }

    // Method to invoke the new round event
    public void InvokeButtonSelectedEvent()
    {
        if (OnButtonSelected != null)
        {
           OnButtonSelected.Invoke();
        }
    }
}
