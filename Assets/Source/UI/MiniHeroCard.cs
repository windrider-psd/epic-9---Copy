using Assets.Source.Admin;
using Assets.Source.Heroes;
using Assets.Source.Util;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class MiniHeroCard : MonoBehaviour
    {
        public Button button;
        public List<Image> disableImages;
        public Image elementImage;
        public GenericEventSystem<MiniHeroCard, bool> onSelect = new();
        public GameObject portrait;
        public GameObject portraitImage;
        public GameObject selectedBorder;
        public TMP_Text heroname;

        [SerializeField]
        private bool disabled;

        private HeroId heroId;
        private bool selected;

        private bool seletionEnabled = true;

        public bool Disabled
        {
            get => disabled;
            set
            {
                foreach (var image in disableImages)
                {
                    image.enabled = value;
                }
                disabled = value;
            }
        }
        public HeroId HeroId
        {
            get => heroId;
            set
            {
                heroId = value;
                var hero = HeroFactory.CreateHero(value);
                elementImage.sprite = UIFactory.Instance.GetElementIcon(hero.Element);
               

                RectTransform rect = portraitImage.GetComponent<RectTransform>();
                var data = UIFactory.Instance.GetHeroMiniCardImageData(value);

                rect.SetTop(data.leftTop.y);
                rect.SetBottom(data.rightBottom.y);
                rect.SetRight(data.rightBottom.x);
                rect.SetLeft(data.leftTop.x);

                portraitImage.GetComponent<Image>().sprite = data.sprite;
                heroname.SetText(hero.Name);
            }
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selectedBorder.SetActive(value);
                selected = value;
            }
        }

        public bool SelectionEnabled
        {
            get
            {
                return seletionEnabled;
            }
            set
            {
                seletionEnabled = value;
            }
        }
        private void Start()
        {
            selectedBorder.SetActive(Selected);
            button.onClick.AddListener(() =>
            {
                if (SelectionEnabled)
                {
                    Selected = !Selected;
                    
                    
                    onSelect.TriggerListeners(this, Selected);
                    
                }
            });
        }

        private void Update()
        {
            if (Selected)
            {
                selectedBorder.transform.rotation *= Quaternion.Euler(0, 0, 50f * Time.deltaTime);
            }
        }
    }
}