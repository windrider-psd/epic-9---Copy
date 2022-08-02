using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class UISpritesAnimation : MonoBehaviour
    {
        private Animator animator;
        private Image image;
        private SpriteRenderer spriteRenderer;

        public RuntimeAnimatorController AnimatorController
        {
            get => animator.runtimeAnimatorController;
            set => animator.runtimeAnimatorController = value;
        }

        public Image Image { get => image; set => image = value; }

        private void OnSpriteChange(SpriteRenderer renderer)
        {
            Image.sprite = renderer.sprite;
        }

        private void Start()
        {
            Image = GetComponent<Image>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.RegisterSpriteChangeCallback(OnSpriteChange);
            animator = GetComponent<Animator>();
        }
    }
}