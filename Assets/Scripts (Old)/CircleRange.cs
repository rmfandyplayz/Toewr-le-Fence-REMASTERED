using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRange : MonoBehaviour
{
    public float mainRangeMultiplier = 1f;
    public GameObject rangeVisualisation;

    [SerializeField]
    private Vector3 startScale;

    [SerializeField]
    private float startScaleMultiplier = 1;

    [SerializeField]
    private bool DEBUG = false;

    void Start()
    {
        startScale = rangeVisualisation.transform.localScale;

        UpdateRange();
    }

    public void ChangeStartScaleMultiplier(float value)
    {
        startScaleMultiplier = value;
        UpdateRange();
    }
    /*
    private void Update()
    {
        if (DEBUG)
        {
            UpdateRange();
            if (Input.GetKeyDown(KeyCode.V))
            {

                ToggleRangeVisual(!rangeVisualisation.activeSelf);
            }
        }
    }
    */
    public void UpdateRange()
    {
        if (rangeVisualisation != null)
        {
            rangeVisualisation.transform.localScale = startScale * startScaleMultiplier * mainRangeMultiplier;

        }

    }

    public void ToggleRangeVisual(bool toggled, bool update = true)
    {
        if (update == true)
        {
            UpdateRange();

        }
        rangeVisualisation.SetActive(toggled);

    }

}
