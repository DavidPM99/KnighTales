using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class AuthManager : MonoBehaviour
{
    public AudioSource buttonPress;
    private static FirebaseFirestore db;
    private List<PlayerInfo> users = new List<PlayerInfo>();
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    [Header("Panel")]
    public GameObject banPanel;

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;

    public InputField cityField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    public Text warningRegisterText;
   

    void Awake()
    {
        Debug.Log("Awake");
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();

            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void Start()
    {
        banPanel.SetActive(false);

        StartCoroutine(PassData.Read());



    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;


    }

    //Function for the login button
    public void LoginButton()
    {
        buttonPress.Play();
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        buttonPress.Play();
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            PassData.setUserData(auth, User);
            PassData.checkBan(banPanel);


        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        } else if (_username.ToLower() == "admin")
        {
            warningRegisterText.text = "username not valid";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else if (cityField.text == "")
        {
            warningRegisterText.text = "Missing City Name";
        }
        else
        {
            bool ok = true;
            users = PassData.getUsersPHP();
            foreach (PlayerInfo user in users)
            {
                if (user.name.ToLower() == _username.ToLower())
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                //Call the Firebase auth signin function passing the email and password
                var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

                if (RegisterTask.Exception != null)
                {
                    //If there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                    FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WeakPassword:
                            message = "Weak Password";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "Email Already In Use";
                            break;
                    }

                    warningRegisterText.text = message;

                }
                else
                {
                    //User has now been created
                    //Now get the result
                    User = RegisterTask.Result;

                    if (User != null)
                    {
                        //Create a user profile and set the username
                        UserProfile profile = new UserProfile { DisplayName = _username };

                        //Call the Firebase auth update user profile function passing the profile with the username
                        var ProfileTask = User.UpdateUserProfileAsync(profile);
                        //Wait until the task completes
                        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                        if (ProfileTask.Exception != null)
                        {
                            //If there are errors handle them
                            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                            FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                            warningRegisterText.text = "Username Set Failed!";
                        }
                        else
                        {

                            //Username is now set
                            //Now return to login screen
                            UIManager.instance.LoginScreen();
                           
                            PassData.setUserData(auth, User);
                            PassData.setCity(cityField.text);
                            PassData.SaveNewPlayer();
                            PassData.CreateTable();
                            warningRegisterText.text = "";
                        }
                    }
                }
            }
            else
            {
                warningRegisterText.text = "This username is taken";
            }
        }
    }


    public void Back()
    {
        banPanel.SetActive(false);
        confirmLoginText.text = "";
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }


}