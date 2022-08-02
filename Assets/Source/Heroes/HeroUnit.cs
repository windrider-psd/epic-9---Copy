using System.Collections;
using UnityEngine;

namespace Assets.Source.Heroes
{
    public class HeroUnit : MonoBehaviour
    {

        public int Id { get; set; }
        public Hero Hero { get; set; }

        public Vector2 Position { get; set; }

        public bool alive;

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