using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
/// <summary>
/// Handles authentication.
/// </summary>
public class LoginPanel : MonoBehaviour
{
	[SerializeField]
	private InputField userNameInput;
	[SerializeField]
	private InputField passwordInput;
	[SerializeField]
	private Button loginButton;
	[SerializeField]
	private Button registerButton;
	[SerializeField]
	private Text errorMessageText;

	void Awake()
	{
		PlayFabSettings.TitleId = "";///Removed to avoid injections...
		loginButton.onClick.AddListener(Login);
		registerButton.onClick.AddListener(Register);
	}

	private void Login()
	{
		BlockInput();
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest { Username = userNameInput.text, Password = passwordInput.text };
		PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginError);
		//request.SetUserName(userNameInput.text);
		//request.SetPassword(passwordInput.text);
		//request.Send(OnLoginSuccess, OnLoginError);
	}

	private void OnLoginSuccess(LoginResult response)
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void OnLoginError(PlayFabError response)
	{
		UnblockInput();
		errorMessageText.text = response.GenerateErrorReport();
	}

	private void Register()
	{
		BlockInput();
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest { RequireBothUsernameAndEmail = false, DisplayName = userNameInput.text, Username = userNameInput.text, Password = passwordInput.text };
		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegistrationSuccess, OnRegistrationError);
	}

	private void OnRegistrationSuccess(RegisterPlayFabUserResult response)
	{
		Login();
	}

	private void OnRegistrationError(PlayFabError response)
	{
		UnblockInput();
		errorMessageText.text = response.GenerateErrorReport();
	}

	private void BlockInput()
	{
		userNameInput.interactable = false;
		passwordInput.interactable = false;
		loginButton.interactable = false;
		registerButton.interactable = false;
	}

	private void UnblockInput()
	{
		userNameInput.interactable = true;
		passwordInput.interactable = true;
		loginButton.interactable = true;
		registerButton.interactable = true;
	}
}