using System;
using UnityEngine;

namespace Assets.Source.Heroes
{
    [System.Serializable]
    public class Hero
    {
        public static readonly int MaxStatPoints = 150;

        public HeroStat armor;

        public HeroStat attack;

        public HeroStat critHitChance;

        public HeroStat critHitDamage;

        public HeroStat effectiveness;

        [SerializeField]
        public HeroElement element;

        [SerializeField]
        public HeroClass heroClass;

        public HeroStat hitPoints;

        public HeroStat resistance;

        public HeroStat speed;

        [SerializeField]
        private string heroName;

        [SerializeField]
        private HeroId id;

        [SerializeField]
        private string idv4;


        public Hero(string name, HeroClass heroclass, HeroElement element, HeroId id)
        {
            Name = name;
            Element = element;
            Id = id;
            HeroClass = heroClass;
            hitPoints = new(HeroStatId.HP, Id, 0);
            attack = new(HeroStatId.Attack, Id, 0);
            armor = new(HeroStatId.Armor, Id, 0);
            speed = new(HeroStatId.Speed, Id, 0);
            critHitChance = new(HeroStatId.CritChance, Id, 0);
            critHitDamage = new(HeroStatId.CritDamage, Id, 0);
            effectiveness = new(HeroStatId.Eff, Id, 0);
            resistance = new(HeroStatId.Res, Id, 0);
            Idv4 = Guid.NewGuid().ToString();
        }

        public Hero()
        {
        }

        public HeroElement Element { get => element; set => element = value; }
        public HeroId Id { get => id; set => id = value; }
        public string Name { get => heroName; set => heroName = value; }
        public string Idv4 { get => idv4; set => idv4 = value; }
        public HeroClass HeroClass { get => heroClass; set => heroClass = value; }

        public override bool Equals(object obj)
        {
            return obj is Hero hero &&
                   Idv4 == hero.Idv4;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Idv4);
        }
    }



}