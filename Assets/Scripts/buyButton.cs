using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyButton : MonoBehaviour
{
    public Vector3 offset;
    public GameObject objectTobuy;
    GameObject clone = null;
    public money MoneyController;
    float turretPrice;
    public bool CanSpam = false;
    private bool canPlace = false;
    public static buyButton currentButton = null;
    public bool isSelected;

    void Update()
    {
        if (isSelected == true && currentButton != this)
        {
            Destroy(clone);
            clone = null;
            currentButton = null;
            isSelected = false;
        }

        if (currentButton == this && clone)
        {
            
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                clone.GetComponent<CircleRange>().ToggleRangeVisual(true,false);
                Debug.LogWarning(clone.GetComponent<CircleRange>().rangeVisualisation.active);


                clone.transform.position = hit.collider.transform.position;

                if (hit.collider.name == "vertical")
                {
                    clone.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                }
                else {
                    clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }

                Vector3 temp = clone.transform.position;
                if (offset.magnitude > 0)//if there is a change, apply the offset. 
                {
                    temp += offset;
                }
                else//if not, keep the og
                {
                    temp.y = 18.91f;
                }
                clone.transform.position = temp;
                if (CanSpam == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        canPlace = true;
                        foreach(Collider cldr in Physics.OverlapBox(clone.transform.position, new Vector3(2, 0.25f, 2)))
                        {
                            if (cldr.GetComponent<AllTurrets>() != null && cldr.gameObject != clone)
                            {
                                canPlace = false;
                                break;
                            }
                        }
                        /*
                        if (Physics.CheckBox(clone.transform.position, new Vector3(2, 0.25f, 2)))
                        {
                            Debug.LogWarning("Not stacking");

                        }
                        else
                        */
                        if(canPlace == true)
                        {
                            if (clone.transform.CompareTag("Pathblocker") && hit.collider.CompareTag("Main Path"))//if tag = Pathblocker and it's on the main path, build
                            {
                                MoneyController.spendMoney(turretPrice, false);
                                clone.GetComponent<AllTurrets>().dragging = false;
                                clone.GetComponent<Collider>().enabled = true;
                                clone = null;
                                Buy(turretPrice);

                            }
                            else if (!clone.transform.CompareTag("Pathblocker") && !hit.collider.CompareTag("Main Path") && !hit.collider.CompareTag("tower"))//opposite of the lines above
                            {
                                MoneyController.spendMoney(turretPrice, false);
                                clone.GetComponent<AllTurrets>().dragging = false;
                                clone.GetComponent<Collider>().enabled = true;
                                clone.GetComponent<CircleRange>().ToggleRangeVisual(false,false);
                                //remove this later
                                Debug.LogWarning("Visual is Turned off (else if statement)");
                                clone = null;
                                Buy(turretPrice);
                            }
                        }
                    }
                }
                else
                {
                    if (!hit.collider.CompareTag("tower"))
                    {
                        if (Input.GetMouseButtonDown(0) && clone.GetComponent<AllTurrets>().iscollidingwithtower == false)
                        {
                            MoneyController.spendMoney(turretPrice, false);
                            clone.GetComponent<AllTurrets>().dragging = false;
                            clone.GetComponent<CircleRange>().ToggleRangeVisual(false,false);
                            //remove this later
                            Debug.LogWarning("Visual is Turned off (if statement)");
                            clone = null;
                        }
                    }
                    
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Destroy(clone);
                    clone = null;
                    currentButton = null;
                }

            }

        }

    }
    public void Buy(float price)
    {
        if (MoneyController.spendMoney(price, true))
        {

            Vector3 pointerPos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
            clone = Instantiate(objectTobuy, transform.position+offset, transform.rotation);
            clone.GetComponent<AllTurrets>().dragging = true;
            turretPrice = price;
            clone.GetComponent<AllTurrets>().buyPrice = turretPrice;
            currentButton = this;
            isSelected = true;
            clone.GetComponent<CircleRange>().mainRangeMultiplier = clone.GetComponent<AllTurrets>().range;
            clone.GetComponent<CircleRange>().ToggleRangeVisual(false,false);
        }

    }
}