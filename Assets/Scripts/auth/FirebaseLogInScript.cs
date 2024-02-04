using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using TMPro;
using UnityEngine;
using Firebase.Extensions;
using System.Collections;
using Firebase.Database;

public class FirebaseLogInScript : MonoBehaviour
{
    [Header("LOGIN")]
    public string webClientId = "<your client id here>";
    public TMP_Text infoText;
    public TMP_Text playerInfo;

    private FirebaseAuth auth;
    public FirebaseUser user;
    private DatabaseReference DBreference;
    private GoogleSignInConfiguration configuration;
    private DependencyStatus dependencyStatus;

    [Header("USER DATA")]
    public PlayerData playerData;
    public TMP_Text playerLevel;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        StartCoroutine(CheckFirebaseDependencies());
    }

    private IEnumerator CheckFirebaseDependencies()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => dependencyTask.IsCompleted);
        dependencyStatus = dependencyTask.Result;
        if ( dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();
            yield return new WaitForEndOfFrame();
            StartCoroutine(CheckSignedInUser());
        }
        else
        {
            Debug.Log("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
    }

    private void InitializeFirebase()
    {

        Debug.Log("Setting Up Firebase");
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        AuthStateChanged(this, null);
        
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
    private IEnumerator CheckSignedInUser()
    {
        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();
            yield return new WaitUntil(() => reloadUserTask.IsCompleted);
            SignedInUser();
        }
        else
        {
            //SignInWithGoogle();
            //UIManager.instance.LoginScreen();
        }
    }
    private void SignedInUser()
    {
        if(user != null)
        {
            playerInfo.text = user.UserId;
            //UIManager.instance.UserDataScreen();
            StartCoroutine(LoadPlayer());
        }
        else
        {

            //SignInWithGoogle();
            //UIManager.instance.LoginScreen();
        }
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        if (!user.IsAnonymous)
        {
            GoogleSignIn.DefaultInstance.SignOut();
        }
        playerInfo.text = "Sign Out";
        auth.SignOut();
        user.DeleteAsync();
        //UIManager.instance.LoginScreen();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                user = task.Result;
                AddToInformation("Sign In Successful.");
                SuccessLogin();
            }
        });
    }
    public async void SignInAnonymous()
    {
        await OnSignInAnonymous();
    }
    async Task OnSignInAnonymous()
    {
        await auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                AddToInformation("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                AddToInformation("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            AddToInformation("Login Success");
            user = task.Result.User;
            AddToInformation("Guest Id: " + task.Result.User.UserId);
            SuccessLogin();
            
        });

    }
    private void SuccessLogin()
    {
        playerInfo.text = user.UserId;
        StartCoroutine(LoadPlayer());
        //UIManager.instance.UserDataScreen();
    }
    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void AddToInformation(string str) { infoText.text += "\n" + str; }

    public void SaveDataButton()
    {
        StartCoroutine(SavePlayer());
    }
    private IEnumerator SavePlayer()
    {
        string json = JsonUtility.ToJson(playerData);
        var DBTask = DBreference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("UPDATED");
            StartCoroutine(LoadPlayer());
        }

    }
    private IEnumerator LoadPlayer()
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.Log($"ERROR UPDATE username {DBTask.Exception}");
        }
        else if (DBTask.Result.Value != null)
        {
            DataSnapshot snapshot = DBTask.Result;
            if (snapshot != null)
            {
                var level = snapshot.Child("Level").Value.ToString();
                Debug.Log(level);
                playerLevel.text = level;
                Debug.Log("Data Exist");
            }
            else
            {
                Debug.Log("Data Not Exist");

            }
        }    
    }
    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 100, 100));
        GUILayout.Label(user.UserId);
        GUILayout.EndArea();
    }
}