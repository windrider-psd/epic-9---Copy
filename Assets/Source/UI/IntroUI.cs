using Assets.source.DataManager;
using Assets.Source.UI.SceneControl;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.source.UI
{
    public class IntroUI : MonoBehaviour
    {
        public GameObject panel;
        public GameObject statusText;

        public GameObject input;
        public GameObject submitButton;


        private void Awake()
        {
            SetStatusText("");
            panel.SetActive(false);

        }


        async void Start()
        {
            //await GameController.Instance.DataManager.DeletePlayerData();

            SetStatusText("Fetching data");
            PlayerData playerData = await GameController.Instance.DataManager.GetPlayerData();

            if(playerData == null)
            {
                ShowPanel();
            }
            else
            {
                CreateSession();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetStatusText(string text)
        {
            this.statusText.GetComponent<TextMeshProUGUI>().SetText(text);
        }

        private  void CreateSession()
        {
            
            SetStatusText("Creating session");
            GameController.Instance.CreateSession();
            JoinIdleLobby();
        }

        private void ShowPanel()
        {
            SetStatusText("");
            panel.SetActive(true);
            submitButton.GetComponent<Button>().onClick.AddListener(SubmitNickname);
        }

        private void HidePanel()
        {
            panel.SetActive(false);
            submitButton.GetComponent<Button>().onClick.RemoveListener(SubmitNickname);
        }

        private async void SubmitNickname()
        {
            submitButton.GetComponent<Button>().interactable = false;
            string nickname = input.GetComponent<TMP_InputField>().text;

            
            await GameController.Instance.DataManager.CreatePlayerData(nickname);
            

            HidePanel();
            CreateSession();
        }

        private async void JoinIdleLobby()
        {
            await GameController.Instance.DataManager.SetUp();
            SetStatusText("Connecting to server");
            var result = await GameController.Instance.JoinIdleLobby();
            if (result)
            {
               SceneController.LoadMainMenu();
               //SetStatusText("Completed");
            }
            else
            {
                SetStatusText("Error conneting to the server");
            }
        }

    }
}