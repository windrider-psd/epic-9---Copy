using System.Collections;
using UnityEngine;
using System;
using Assets.Source.Util;
using Assets.Source.Heroes;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.UI;

namespace Assets.Source.Admin
{
    public class UIBuilder : MonoBehaviour
    {

        public GameObject miniHeroCardSelectorPrefab;
        public GameObject overflowPanelPrefab;

        private static UIBuilder _instance;
        
        private Canvas Canvas
        {
            get => FindObjectOfType<Canvas>();
        }

        public static UIBuilder Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<UIBuilder>(true);
                return _instance;
            }
        }


        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            if (_instance != this)
            {
                Destroy(this);
            }
           
        }

        
        private void Start()
        {
        }

        public void CallMiniHeroCardSelector(Action<bool, HeroId> callback)
        {
            var p = Instantiate(overflowPanelPrefab, Canvas.transform);
            var selector = Instantiate(miniHeroCardSelectorPrefab, p.transform).GetComponent<MiniHeroSelector>();

            foreach (HeroId id in Enum.GetValues(typeof(HeroId)))
            {
                selector.AddCard(id);
            }
            
            selector.onCancel.AddListener(() =>
            {
                callback.Invoke(false, default);
                Destroy(p);
            });
            selector.onSelectConfirm.AddListener(id =>
            {
                callback.Invoke(true, id);
                Destroy(p);
            });
        }



    }
}
