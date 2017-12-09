using UnityEngine;
using System.Collections;

public static class Initiate
{
    static Fader Fader;
    static void SetFader(Fader _fader)
    {
        Fader = _fader;
    }
    static Fader GetFader()
    {
        return Fader;
    }

    //Create Fader object and assing the fade scripts and assign the variables
    public static void Fade (string scene, Color col, float dampIn, float dampOut)
    {
        if (Time.timeScale < 1)
            Time.timeScale = 1;

        GameObject init = new GameObject ();
		init.AddComponent<Fader> ();
		Fader _Fader = init.GetComponent<Fader> ();
        SetFader(_Fader);
        _Fader.fadeInDamp = dampIn;
        _Fader.fadeOutDamp = dampOut;
        _Fader.fadeScene = scene;
        _Fader.fadeColor = col;
        _Fader.start = true;
	}

    // Fader with break after fade out
    public static void Fade (string scene, Color col, float dampIn, float dampOut, bool breakBeforeFadeIn, float breakLast, float breakAcceleration)
    {
        Fade(scene, col, dampIn, dampOut);
        Fader _Fader = GetFader();
        _Fader.breakBeforeIn = breakBeforeFadeIn;
        _Fader.breakLast = breakLast;
        _Fader.breakAcceleration = breakAcceleration;
	}
}
