using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager : MonoBehaviour
{

    public void OnSocialButton(string webName)
    {
        Application.OpenURL(webName);
    }
}