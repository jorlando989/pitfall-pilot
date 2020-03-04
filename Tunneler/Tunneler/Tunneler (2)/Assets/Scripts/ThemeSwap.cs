using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ThemeSwap class
 * Handles switching out tunnel themes
 */ 
public class ThemeSwap : MonoBehaviour {

    public GameObject pipe;
	
    public void SwapToBasicTheme()
    {
        pipe.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.grey);
    }

    public void SwapToAlternateTheme()
    {
        pipe.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.cyan);
    }
}
