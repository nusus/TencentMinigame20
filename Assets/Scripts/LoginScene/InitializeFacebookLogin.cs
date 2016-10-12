using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Facebook.Unity;
using Facebook.MiniJSON;

public class InitializeFacebookLogin : MonoBehaviour
{
    [Header("用户ID显示器")]
    [Tooltip("用于显示登陆用户ID的GUIText组件")]
    public Text text;

    [Header("头像显示")]
    [Tooltip("这个物体用于显示用户头像的")]
    public GameObject headMesh;

    public GameObject uiButton;

    private GameDatabase database;

    void Start()
    {
        text.text = null;
        Screen.orientation = ScreenOrientation.Portrait;
        database = GameDatabase.GetInstance();
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

    //IEnumerator<WWW> getImage(AccessToken CurToken)
    //{
    //    WWW www = new WWW("https://graph.facebook.com/" + CurToken.UserId + "/picture?type=large");
    //    yield return www;
    //    headMesh.GetComponent<Image>().material.mainTexture = www.texture;
    //}

    private void loginName(IResult result)
    {
        if (result.Error != null)
            Debug.Log("ERROR:" + result.Error);
        else if (!FB.IsLoggedIn)
            Debug.Log("User not login");
        else
        {
            IDictionary<string, string> dict;
            dict = Json.Deserialize(result.ToString()) as IDictionary<string, string>;
            database.name = dict["name"].ToString();
            Debug.Log(database.name);
        }
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
            uiButton.SetActive(false);
            var aToken = AccessToken.CurrentAccessToken;
            //Debug.Log(aToken.TokenString);
            //StartCoroutine(getImage(AccessToken.CurrentAccessToken));
            //text.text = aToken.TokenString;
            database.token = aToken.ToString();
            database.userID = aToken.UserId;
            FB.API("/me?fields=name", HttpMethod.GET, loginName);
            text.text = ("Welcome Back! " + database.name);

            //foreach (string perm in aToken.Permissions)
            //{
            //    Debug.Log(perm);
            //}
            SceneManager.LoadScene("mainui", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Empty response");
        }
    }
}
