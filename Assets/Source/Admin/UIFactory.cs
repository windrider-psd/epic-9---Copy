using System.Collections;
using UnityEngine;
using System;
using Assets.Source.Util;
using Assets.Source.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Admin
{
    public class UIFactory : MonoBehaviour
    {

        public List<UnityTuple<HeroElement, Sprite>> heroElements;

        private static UIFactory _instance;

        public List<UnityTuple<HeroId, MiniCardImageData>> portraitImageData;

        public List<UnityTuple<HeroId, HeroShowcaseData>> heroShowcaseImage;


        public static UIFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<UIFactory>();
                return _instance;
            }
        }

        public Sprite GetElementIcon(HeroElement element)
        {
            return heroElements.GetUnityTupleValue(element);
        }

        public MiniCardImageData GetHeroMiniCardImageData(HeroId id)
        {
            return portraitImageData.GetUnityTupleValue(id);
        }

        public HeroShowcaseData GetHeroShowcaseImage(HeroId id)
        {
            return heroShowcaseImage.GetUnityTupleValue(id);
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

        [Serializable]
        public struct MiniCardImageData
        {   
            public Vector2 leftTop;//right top
            public Vector2 rightBottom;//left bottm
            public Vector3 scale;
            public Sprite sprite;
        }

        [Serializable]
        public struct HeroShowcaseData
        {
            public RuntimeAnimatorController controller;
            public Vector3 size;
        }
    }
}