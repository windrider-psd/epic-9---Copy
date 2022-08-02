using Assets.Source.Heroes;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class HeroCardSelector : MonoBehaviour
    {
        public RectTransform content;
        public GameObject heroCardPrefab;
        private EventTrigger.Entry beginDragEntry;

        [SerializeField]
        private bool dragging;

        private EventTrigger.Entry endDragEntry;
        private EventTrigger eventTrigger;

        [SerializeField]
        private bool lerping;

        [SerializeField]
        private float lerpSpeed;

        [SerializeField]
        private HeroCard lerpTarget;

        [SerializeField]
        private float lerpThreshold;

        public GenericEventSystem<HeroCard> onSelectedCardChanged = new();
        private RectTransform rectTransform;
        private ScrollRect scrollRect;

        [SerializeField]
        private HeroCard selectedCard;

        [SerializeField]
        private float selectionVelocityThreshold;

        [SerializeField]
        private RectTransform view;

        public List<HeroCard> Cards
        {
            get
            {
                return new(content.GetComponentsInChildren<HeroCard>());
            }
        }

        public double ContentCenter
        {
            get
            {
                return content.sizeDelta.y / 2;
            }
        }

        public bool Dragging
        {
            get => dragging;
            set
            {
                if (value)
                {
                    Lerping = false; lerpTarget = null;
                }
                dragging = value;
            }
        }

        public bool Lerping
        {
            get => lerping;
            set
            {
                if (value)
                {
                    dragging = false;
                    scrollRect.velocity = new Vector2(0, 0);
                }
                else
                {
                    /*lerpTarget = null;*/
                }
                lerping = value;
            }
        }

        public HeroCard SelectedCard
        {
            get => selectedCard;
            set
            {
                var tmp = selectedCard;

                selectedCard = value;
                LerpToCard(value);

                if (tmp != selectedCard)
                {
                    onSelectedCardChanged.TriggerListeners(value);
                }
            }
        }

        public float Velocity
        {
            get
            {
                return Math.Abs(scrollRect.velocity.y);
            }
        }

        public double ViewOffset
        {
            get
            {
                //double contentCenter = content.sizeDelta.y / 2;
                return view.localPosition.y;
            }
        }

        private HeroCard ClosestCardToCenter
        {
            get
            {
                return Cards[ClosestCardToCenterIndex];
            }
        }

        private int ClosestCardToCenterIndex
        {
            get
            {
                var cards = Cards;
                if (cards.Count > 0)
                {
                    List<double> distances = new();
                    foreach (var card in cards)
                    {
                        distances.Add(CardDistanceFromContentCenter(card));
                    }
                    double min = distances.Min();

                    int minIndex = distances.FindIndex(match => { return match == min; });

                    return minIndex;
                }
                else
                {
                    return -1;
                }
            }
        }

        #region callbacks

        public void OnClickedCard(HeroCard card)
        {
            SelectedCard = card;
            //LerpToCard(card);
        }

        public void OnValueChanged(Vector2 value)
        {
        }

        #endregion callbacks

        #region drag

        public void StartDragging(BaseEventData data)
        {
            Dragging = true;
        }

        public void StopDragging(BaseEventData data)
        {
            Dragging = false;
        }

        #endregion drag

        #region unity

        public void Start()
        {
            scrollRect = GetComponent<ScrollRect>();
            rectTransform = GetComponent<RectTransform>();
            eventTrigger = GetComponent<EventTrigger>();

            view = scrollRect.viewport;

            beginDragEntry = new();
            beginDragEntry.eventID = EventTriggerType.BeginDrag;

            endDragEntry = new();
            endDragEntry.eventID = EventTriggerType.EndDrag;

            beginDragEntry.callback.AddListener((data) =>
            {
                Dragging = true;
            });
            endDragEntry.callback.AddListener((data) =>
            {
                Dragging = false;
            });

            eventTrigger.triggers.Add(beginDragEntry);
            eventTrigger.triggers.Add(endDragEntry);

            scrollRect.onValueChanged.AddListener(OnValueChanged);

            OrderCards();
        }

        public void Update()
        {
            DoLerp();

            if (!Lerping && !Dragging && lerpTarget == null && Velocity <= selectionVelocityThreshold)
            {
                if(Cards.Count > 0)
                {
                    SelectedCard = ClosestCardToCenter;
                }
            }
        }

        #endregion unity

        #region scroll

        public void CenterAtCard(int index)
        {
            if(Cards.Count > 0)
            {
                content.localPosition = new Vector2(content.localPosition.x, (float)ContentPositionToCenter(index));
            }
            
        }

        public void CenterAtCard(HeroCard card)
        {
            var cs = Cards;
            for (int i = 0; i < cs.Count; i++)
            {
                if (cs[i] == card)
                {
                    CenterAtCard(i);
                }
            }
        }

        public void LerpToCard(HeroCard card)
        {
            Lerping = true;
            lerpTarget = card;
        }

        public void SetScrollTop()
        {
            CenterAtCard(0);
        }

        public void SelectTopCard()
        {
            SelectedCard = Cards[0];
        }

        private void DoLerp()
        {
            if (Lerping)
            {
                //if ((view.position - lerpTarget.transform.position).sqrMagnitude <= lerpThreshold * lerpThreshold)
                if (CardDistanceFromContentCenter(lerpTarget) <= lerpThreshold)
                {
                    CenterAtCard(lerpTarget);
                    Lerping = false;
                }
                else
                {
                    double position = ContentPositionToCenter(lerpTarget);
                    float newY = Mathf.Lerp(content.localPosition.y, (float)position, Time.deltaTime * lerpSpeed);
                    content.localPosition = new Vector2(content.localPosition.x, newY);
                }
            }
        }

        #endregion scroll

        #region add/remove

        public HeroCard AddCard(Hero hero)
        {
            GameObject go = Instantiate(heroCardPrefab, content);
            var card = go.GetComponent<HeroCard>();
            card.Hero = hero;

            card.onClick.AddListener(OnClickedCard);

            OrderCards();
            return card;
        }

        public void RemoveCard(Hero hero)
        {
            var cards = content.GetComponents<HeroCard>();

            HeroCard removedCard = null;

            foreach (var card in cards)
            {
                if (card.Hero == hero)
                {
                    removedCard = card;
                    break;
                }
            }
            if (removedCard != null)
            {
                Destroy(removedCard.gameObject);
            }
            OrderCards();
        }

        #endregion add/remove

        #region utility

        private double CardDistanceFromContentCenter(HeroCard card)
        {
            return Math.Abs(ContentPositionToCenter(card) - content.localPosition.y);
        }

        private double ContentPositionToCenter(int cardIndex)
        {
            double cardSize = heroCardPrefab.GetComponent<RectTransform>().sizeDelta.y;

            float spacing = content.GetComponent<VerticalLayoutGroup>().spacing;

            int reverseIndex = Cards.ReverseIndex(cardIndex);

            return ContentCenter - cardSize / 2 - reverseIndex * (cardSize + spacing);
        }

        private double ContentPositionToCenter(HeroCard card)
        {
            var cs = Cards;
            for (int i = 0; i < cs.Count; i++)
            {
                if (cs[i] == card)
                {
                    return ContentPositionToCenter(i);
                }
            }
            return 0;
        }

        #endregion utility

        private void OrderCards()
        {
            List<HeroCard> heroCards = Cards;

            heroCards = heroCards.OrderBy((card) => card.Hero.Name).ToList();

            int index = 0;
            foreach (var card in heroCards)
            {
                card.gameObject.transform.SetSiblingIndex(index++);
            }
        }
    }
}