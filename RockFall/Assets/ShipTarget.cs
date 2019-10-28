using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : MonoBehaviour
{
    public Sprite targetImage;

    // Start is called before the first frame update
    void Start()
    {
        IndicatorManager.instance.AddIndicator(gameObject, Color.yellow, targetImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
