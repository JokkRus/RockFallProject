using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IndicatorManager.instance.AddIndicator(this.gameObject, Color.green);
    }
}
