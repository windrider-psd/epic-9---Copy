using System.Collections;
using UnityEngine;
using System;
using Assets.Source.Util;
using Assets.Source.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Admin
{
    public class HeroUnitFactory : MonoBehaviour
    {
        private static HeroUnitFactory _instance;

        public List<UnityTuple<HeroId, Sprite>> heroImages;

        public List<UnityTuple<HeroElement, Sprite>> heroElements;

        public static HeroUnitFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<HeroUnitFactory>();
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

        public Sprite GetHeroImage(HeroId id)
        {
            return heroImages.GetUnityTupleValue(id);
        }

        public Sprite GetElementIcon(HeroElement element)
        {
            return heroElements.GetUnityTupleValue(element);
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}