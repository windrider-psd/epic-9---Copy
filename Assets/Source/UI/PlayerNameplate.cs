using Assets.source.DataManager;
using Assets.Source.UI.SceneControl;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.source.UI
{
    public class PlayerNameplate : MonoBehaviour
    {
        private PlayerData playerData;
        public TMP_Text playerName;

        [SerializeField]
        public bool loadPlayerData;


        public PlayerData PlayerData 
        { 
            
            get => playerData; 
            set
            { 
                playerName.SetText(value.Name);

                playerData = value;
            } 
        }

        public async void Start()
        {
            playerName = GetComponentInChildren<TMP_Text>();

            if (loadPlayerData)
            {
                PlayerData = await GameController.Instance.DataManager.GetPlayerData();
            }

        }

    }
}