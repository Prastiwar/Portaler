using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {
    [HideInInspector]
	public bool start = false;
    [HideInInspector]
    public float fadeInDamp = 0.0f;
    [HideInInspector]
    public float fadeOutDamp = 0.0f;
    [HideInInspector]
    public string fadeScene;
    [HideInInspector]
    public float alpha = 0.0f;
    [HideInInspector]
    public Color fadeColor;
    [HideInInspector]
    public bool isFadeIn = false;

    [HideInInspector]
    public bool breakBeforeIn = false;
    [HideInInspector]
    public float breakLast = 0.0f;
    [HideInInspector]
    public float breakAcceleration = 0.0f;

    //Set callback
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    //Remove callback
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    //Create a texture , Color it, Paint It , then Fade Away
    void OnGUI ()
    {
        //Fallback check
        if (!start)
			return;

        //Assign the color with variable alpha
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        //Temp Texture
		Texture2D myTex;
		myTex = new Texture2D (1, 1);
		myTex.SetPixel (0, 0, fadeColor);
		myTex.Apply ();

        //Print Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), myTex);

        //Fade in and out control
        if (isFadeIn)
        {
            // alpha decrease to 0f
            alpha = Mathf.Lerp(alpha, -0.1f, fadeInDamp * Time.deltaTime); // scene loaded - fade in
            if (breakBeforeIn)
            {
                if (alpha < 1f)
                    fadeInDamp = breakAcceleration; // To make it after break faster => value can be changed in project.
            }
        }
        else
        {
            // alpha increase to 1f
            alpha = Mathf.Lerp(alpha, 1.1f, fadeOutDamp * Time.deltaTime); // scene is loading - fade out
        }

        //Load scene
		if (alpha >= 1 && !isFadeIn)
        {
            if (breakBeforeIn)
            {
                alpha = breakLast; // To make fade in longer (a little break before fade in starts)
            }

            SceneManager.LoadScene(fadeScene);
            DontDestroyOnLoad(gameObject);		
		}
        else if (alpha <= 0 && isFadeIn)
        {
            Destroy(gameObject);
		}
	}

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //We can now fade in
        isFadeIn = true;

        // Change state after load
        StateMachineManager.ChangeStateTo(scene.name);
    }

}
