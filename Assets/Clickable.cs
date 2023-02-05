using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Clickable : MonoBehaviour, IPointerClickHandler
{
    // [SerializeField] LayerMask targetLayer;
    private Pointer pointer;
    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.FindAnyObjectByType<Pointer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            pointer.AddTarget(transform);
        }
        else if (pointerEventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            pointer.RemoveTarget(transform);
        }
    }
}
