using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.Heroes;
using TMPro;
using Assets.Source.Admin;

namespace Assets.Source.UI
{
    public class HeroShowcaseInfoDisplay : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text heroName;
        [SerializeField]
        private Image heroElement;

        [SerializeField]
        private TMP_Text elementText;


        private Hero hero;
        
        public Hero Hero
        {
            get => hero;
            set
            {
                hero = value;
                if(hero != null)
                {
                    heroElement.enabled = true;
                    heroName.SetText(hero.Name);
                    heroElement.sprite = UIFactory.Instance.GetElementIcon(hero.Element);
                    elementText.SetText(hero.Element.ToString());
                }
                else
                {
                    heroName.SetText("");
                    heroElement.enabled = false;
                    elementText.SetText("");
                }
                
            }
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