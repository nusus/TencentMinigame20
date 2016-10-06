using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Facebook.Unity;

public class InitializeFacebookLogin : MonoBehaviour
{
    [Header("用户ID显示器")]
    [Tooltip("用于显示登陆用户ID的GUIText组件")]
    public Text text;

    void Start()
    {
        text.text = null;
    }

    //Initialize Facebook SDK here
    public void Click()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(initCallback, onHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
        CallFBLogin();
    }
    
    private void initCallback()
    {
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize facebook SDK");
        }
    }

    private void onHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void CallFBLogin()
    {
        FB.LogInWithReadPermissions(new List<string>()
        {
            "public_profile",
            "email",
            "user_friends"
        }, HandleResult);
    }

    protected void HandleResult(IResult result)
    {
        if (result == null)
        {
            Debug.Log("No response");
        }

        // Some platforms return the empty string instead of null.
        if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error");
        }
        else if (result.Cancelled)
        {
            Debug.Log("User cancelled login");
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success");
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.TokenString);
            text.text = aToken.TokenString;
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
            SceneManager.LoadScene("DrinkWater");
        }
        else
        {
            Debug.Log("Empty response");
        }
    }

}
