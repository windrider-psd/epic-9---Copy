using System;
using System.Collections.Generic;


namespace Assets.Source.Heroes
{
    public class HeroLayout
    {
        public static readonly int totalStatPoints = 100;

        private Dictionary<HeroStatId, int> levels = new()
        {
            { HeroStatId.Attack, 0 }
        };


    }
}