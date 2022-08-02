using Assets.Source.Admin;
using Assets.Source.Heroes;
using Assets.Source.UI.SceneControl;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class PlayerHeroesUI : MonoBehaviour
    {
        public Button createHeroButton;
        public HeroCardSelector heroCardSelector;
        public List<Hero> heroes;
        public Image showcaseImage;
        public UISpritesAnimation uiSpritesAnimation;
        public HeroShowcaseInfoDisplay infoDisplay;
        public Button mainMenuButton;

        private void LoadShowcase(HeroCard card)
        {
            var data = UIFactory.Instance.GetHeroShowcaseImage(card.Hero.Id);
            
            uiSpritesAnimation.AnimatorController = data.controller;

            
            uiSpritesAnimation.gameObject.GetComponent<RectTransform>().localScale = new Vector3(data.size.x, data.size.y, data.size.z);
            uiSpritesAnimation.Image.enabled = true;

            infoDisplay.Hero = card.Hero;


        }

        private void OnCreateHeroButtonClicked()
        {
            UIBuilder.Instance.CallMiniHeroCardSelector(async (bool success, HeroId id) =>
            {
                if (success)
                {
                    Hero hero = HeroFactory.CreateHero(id);

                    await GameController.Instance.DataManager.AddPlayerHero(hero);

                    heroes.Add(hero);
                    var card = heroCardSelector.AddCard(hero);
                    heroCardSelector.SelectedCard = card;
                }
            });
        }

        private void SetUpHeroCardSelector()
        {
            heroCardSelector.onSelectedCardChanged.AddListener(LoadShowcase);
            foreach (var hero in heroes)
            {
                heroCardSelector.AddCard(hero);
            }
            if (heroes.Count > 0)
            {
                heroCardSelector.SelectTopCard();
            }
        }

        // Use this for initialization
        private async void Start()
        {
            uiSpritesAnimation.Image.enabled = false;
            heroCardSelector = FindObjectOfType<HeroCardSelector>();

            createHeroButton.onClick.AddListener(OnCreateHeroButtonClicked);

            heroes = await GameController.Instance.DataManager.GetPlayerHeroes();

            SetUpHeroCardSelector();

            mainMenuButton.onClick.AddListener(() =>
            {
                SceneController.LoadMainMenu();
            });

        }
    }
}