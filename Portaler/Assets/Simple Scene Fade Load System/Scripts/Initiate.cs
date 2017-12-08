using UnityEngine;
using System.Collections;

public static class Initiate
{
    //Create Fader object and assing the fade scripts and assign all the variables
    public static void Fade (string scene, Color col, float damp)
    {
        if (Time.timeScale < 1)
            Time.timeScale = 1;

        GameObject init = new GameObject ();
		init.AddComponent<Fader> ();
		Fader _Fader = init.GetComponent<Fader> ();

        _Fader.fadeDamp = damp;
        _Fader.fadeScene = scene;
        _Fader.fadeColor = col;
        _Fader.start = true;
	}
}
