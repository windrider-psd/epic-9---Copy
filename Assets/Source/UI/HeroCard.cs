using Assets.source.DataManager;
using Assets.Source.Admin;
using Assets.Source.Heroes;
using Assets.Source.UI.SceneControl;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

using Assets.Source.Util;
using System;

namespace Assets.Source.UI
{
    public class HeroCard : MonoBehaviour
    {
        private Hero hero;
        public TMP_Text heroName;
        public Image heroImage;
        public Button button;
        public Image heroElement;

        public GenericEventSystem<HeroCard> onClick = new();

        public Hero Hero
        {
            get => hero;
            set
            {
                
                //heroImage.material.SetTexture("_Main_Tex", HeroUnitFactory.Instance.GetHeroImage(value.Id));
                heroImage.sprite = HeroUnitFactory.Instance.GetHeroImage(value.Id);
                heroName.SetText(value.Name);
                heroElement.sprite = UIFactory.Instance.GetElementIcon(value.Element);
                hero = value;
            }
        }

        public void AddClickListener(Action<HeroCard> action)
        {

        }
        public void RemoveClickListener()
        {

        }
        public void Start()
        {

            button.onClick.AddListener(() =>
            {
                onClick.TriggerListeners(this);
            });
        }


    }
}