using Assets.source.DataManager;
using Assets.Source.UI.SceneControl;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.source.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button exitButton;
        public Button heroesButton;

        


        private void Awake()
        {
            heroesButton.onClick.AddListener(OpenHeores);
            
            exitButton.onClick.AddListener(ExitGame);
        }


        private void ExitGame()
        {
            Application.Quit();
        }
        private void OpenHeores()
        {
            SceneController.LoadPlayerHeroes();
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}