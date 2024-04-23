using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using TMPro;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;
using Player;
using Newtonsoft.Json;
using UnityEngine.Networking;
public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    private PlayerManager pm;

    public string webClientId = "<your client id here>";
    private string status;

    private FirebaseAuth auth;
    public FirebaseUser user;
    private DatabaseReference DBreference;
    private GoogleSignInConfiguration configuration;
    private DependencyStatus dependencyStatus;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        CheckInternet();
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        StartCoroutine(CheckFirebaseDependencies());
    }

    private void Start()
    {
        pm = PlayerManager.Instance;
    }
    private void CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Network Not Available");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("Network is Available through wifi");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Network is Available through data");
        }
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
            SignInWithGoogle();
            //UIManager.instance.LoginScreen();
        }
    }
    private void SignedInUser()
    {
        if(user != null)
        {
            AddToInformation(user.UserId);
            //UIManager.instance.UserDataScreen();
            StartCoroutine(LoadPlayer());
        }
        else
        {

            SignInWithGoogle();
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
        AddToInformation("Sign Out");
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
                {
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
                    SignInAnonymous();
                }
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
        AddToInformation(user.UserId);
        LoadData();
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

    private void AddToInformation(string str) { status += "\n" + str; }

    public void SaveData()
    {
        StartCoroutine(SavePlayer());
    }
    public void LoadData()
    {
        StartCoroutine(LoadPlayer());
    }
    private IEnumerator SavePlayer()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["Coin"] = pm.Coin.ToString();
        result["Trashes"] = JsonConvert.SerializeObject(pm.trashes);
        var DBTask = DBreference.Child("users").Child(user.UserId).UpdateChildrenAsync(result);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("UPDATED");
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
                pm.Coin = int.Parse(snapshot.Child("Coin").Value.ToString());
                var result = snapshot.Child("Trashes").Value.ToString();
                Dictionary<string, bool> convert = JsonConvert.DeserializeObject<Dictionary<string, bool>>(result);

                foreach (var kvp in convert) 
                {
                    pm.SetTrash(kvp.Key, kvp.Value);
                }
                Debug.Log("Data Exist");
            }
            else
            {
                Debug.Log("Data Not Exist");

            }
        }    
    }
    //public void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(50, 50, 100, 100));
    //    GUILayout.Label(user.UserId);
    //    GUILayout.Label(status);
    //    GUILayout.EndArea();
    //}
}