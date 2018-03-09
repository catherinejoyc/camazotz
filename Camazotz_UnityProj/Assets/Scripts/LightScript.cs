using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    public Light myLight;

    public void ToggleLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}
