using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AuthController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI UsernameTMPG;
    public TextMeshProUGUI EmailTMPG;
    public TextMeshProUGUI PasswordTMPG;

    public string Email { get; set; } = "kpachac@ulasalle.edu.pe";
    public string Password { get; set; } = "123456";

    static Supabase.Client SupabaseClient;

    MainMenuController MainMenuController;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        MainMenuController = FindObjectOfType<MainMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async public void Initialize()
    {
        var url = "https://fvcgawtvqactxkctfidx.supabase.co";
        var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZ2Y2dhd3R2cWFjdHhrY3RmaWR4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTEwNjQwMDUsImV4cCI6MjAyNjY0MDAwNX0.MwbPFuEd1BBQg_y9q7Cpb_V_ZntdUGbGxGczehJARhY";

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        SupabaseClient = new Supabase.Client(url, key, options);
        await SupabaseClient.InitializeAsync();
        Debug.Log("Supabase initialized");
    }

    async public void SignUp()
    {
        try
        {
            if (SupabaseClient == null) Initialize();

            Debug.Log($"Email: {EmailTMPG.text}, Password: {PasswordTMPG.text}, Username: {UsernameTMPG.text}");

            var session = await SupabaseClient.Auth.SignUp(
                EmailTMPG.text.Replace("\u200B", ""),
                PasswordTMPG.text.Replace("\u200B", ""),
                new Supabase.Gotrue.SignUpOptions()
                {
                    Data = new Dictionary<string, object>()
                    {
                        { "username", UsernameTMPG.text.Replace("\u200B", "") ?? "InSaNe" }
                    }
                }
            );

            if (session != null)
            {
                Debug.Log($"User signed in:\n");
                Debug.Log($"{session.User.Email}");
                MainMenuController.ShowLogInUI();
            }
            else
            {
                Debug.Log("User not signed in");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    async public void LogIn()
    {
        try
        {
            if (SupabaseClient == null) Initialize();
            Debug.Log($"Email: {EmailTMPG.text}, Password: {PasswordTMPG.text}");

            var session = await SupabaseClient.Auth.SignIn(
                EmailTMPG.text.Replace("\u200B", ""), 
                PasswordTMPG.text.Replace("\u200B", "")
            );

            if (session != null)
            {
                Debug.Log($"User signed in:\n");
                Debug.Log($"{session.User.UserMetadata["username"]}");
                User.CurrentEmail = session.User.Email;
                MainMenuController.ShowPlayerProfileUI();
            }
            else
            {
                Debug.Log("User not signed in");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void LogOut()
    {
        try
        {
            if (SupabaseClient == null) Initialize();
            Debug.Log($"Email: {EmailTMPG.text}, Password: {PasswordTMPG.text}");

            var session = SupabaseClient.Auth.SignOut();

            User.CurrentEmail = null;
            User.CurrentUsername = null;

            MainMenuController.ShowLogInUI();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    async public void ResetPassword()
    {
        try
        {
            if (SupabaseClient == null) Initialize();
            Debug.Log($"Email: {EmailTMPG.text}");

            var session = await SupabaseClient.Auth.ResetPasswordForEmail(EmailTMPG.text.Replace("\u200B", ""));

            if (session)
            {
                Debug.Log($"Sent");
                MainMenuController.ShowLogInUI();
            }
            else
            {
                Debug.Log("No Sent");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
