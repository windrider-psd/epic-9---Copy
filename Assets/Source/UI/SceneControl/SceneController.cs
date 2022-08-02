using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Source.UI.SceneControl
{
    abstract internal class SceneController
    {
        private static SceneId sceneId = SceneId.Intro;

        public static SceneId SceneId { get => sceneId; private set => sceneId = value; }


        public static void LoadMainMenu()
        {
            sceneId = SceneId.MainMenu;
            SceneManager.LoadScene("MainMenu");
        }

        public static void LoadPlayerHeroes()
        {
            sceneId = SceneId.PlayerHeores;
            SceneManager.LoadScene("PlayerHeroes");
        }


    }
}
