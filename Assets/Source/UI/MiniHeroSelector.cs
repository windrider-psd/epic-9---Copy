using Assets.Source.Heroes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.Util;
namespace Assets.Source.UI
{
    public class MiniHeroSelector : MonoBehaviour
    {

        public Button cancelButton;
        public Button selectButton;

        public GameObject miniHeroCardPrefab;
        public GameObject content;


        public GenericEventSystem onCancel = new();
        public GenericEventSystem<HeroId> onSelectConfirm = new();



        public List<MiniHeroCard> Cards
        {
            get
            {
                return new (content.GetComponentsInChildren<MiniHeroCard>());
            }
        }

        private MiniHeroCard selectedCard;
        

        public MiniHeroCard AddCard(HeroId id)
        {
            var go = Instantiate(miniHeroCardPrefab, content.transform);
            var card = go.GetComponent<MiniHeroCard>();
            card.HeroId = id;
            card.onSelect.AddListener(OnSelected);
            return card;
        }
        public MiniHeroCard RemoveCard(HeroId id)
        {
            return null;
        }
        private void OnSelected(MiniHeroCard card, bool value)
        {
            if (value)
            {
                if (selectedCard == null)
                {
                    selectedCard = card;
                }
                else if (selectedCard.Equals(card))
                {
                    selectedCard = null;

                }
                else
                {
                    selectedCard.Selected = false;
                    selectedCard = card;
                }

                selectButton.interactable = selectedCard != null;
            }
            else
            {
                selectedCard = null;
                selectButton.interactable = false;
            }
           


        }

        private void Start()
        {
            selectButton.onClick.AddListener(() =>
            {
                onSelectConfirm.TriggerListeners(this.selectedCard.HeroId);
            });
            cancelButton.onClick.AddListener(() =>
            {
                onCancel.TriggerListeners();
            });
            

        }

    }
}