using Assets.Source.Heroes;
using Assets.Source.Util;
using System;
using System.Collections.Generic;

namespace Assets.Source.Battle
{
    internal class BattleManager
    {
        private static BattleManager _instance;
        private double crDistance = 1000;
        private HashSet<HeroUnit> heroUnits = new();
        private Dictionary<HeroUnit, double> startingPoints;

        public int CurrrentTurn { get; private set; } = 1;
        internal HashSet<HeroUnit> HeroUnits { get => heroUnits; set => heroUnits = value; }

        public void AddUnit(HeroUnit heroUnit, double startingPorcentage)
        {
            double startingPoint = 100 * startingPorcentage / crDistance;
            HeroUnits.Add(heroUnit);
            startingPoints.Add(heroUnit, startingPoint);
        }

        public List<HeroUnit> CalculateNextTurns(int numberOfTurns)
        {
            List<HeroUnit> heroUnitsTurns = new();
            this.startingPoints.ShallowCopyDictonary(out Dictionary<HeroUnit, double> tmp);

            for (int i = 0; i < numberOfTurns; i++)
            {
                heroUnitsTurns.Add(GetTurn(ref tmp));
            }
            return heroUnitsTurns;
        }

        public void SetNextTurn()
        {
            GetTurn(ref this.startingPoints);
            CurrrentTurn++;
        }

        private HeroUnit GetTurn(ref Dictionary<HeroUnit, double> startingPoints)
        {
            // List<HeroUnit> unitsDistanceOverflow = new();
            List<HeroUnit> potentialTurnUnits = new();
            double lowestTime;
            double highestStartingPoint = DataUtil.GetHighestValueOfDictonary(startingPoints, out List<HeroUnit> unitsDistanceOverflow);

            if (highestStartingPoint >= crDistance)
            {
                potentialTurnUnits = unitsDistanceOverflow;
            }

            if (potentialTurnUnits.Count == 0)
            {
                Dictionary<HeroUnit, double> timeTable = new();

                foreach (var h in HeroUnits)
                {
                    double time = (crDistance - startingPoints[h]) / h.Hero.speed.Total;
                    timeTable[h] = time;
                }

                List<HeroUnit> lowestTimeUnits;
                lowestTime = DataUtil.GetLowestValueOfDictonary(timeTable, out lowestTimeUnits);
                HeroUnit selectedUnit = SelectRandomHero(potentialTurnUnits);

                Dictionary<HeroUnit, double> ntp = new();
                if (lowestTimeUnits.Count == 1)
                {
                    foreach (var kv in startingPoints)
                    {
                        if (kv.Key != selectedUnit)
                        {
                            ntp[kv.Key] = kv.Value + (kv.Key.Hero.speed.Total * lowestTime);
                        }
                        else
                        {
                            ntp[kv.Key] = 0;
                        }
                    }
                }
                else
                {
                    foreach (var kv in startingPoints)
                    {
                        if (kv.Key == selectedUnit)
                        {
                            ntp[kv.Key] = 0;
                        }
                        else if (lowestTimeUnits.Contains(kv.Key))
                        {
                            ntp[kv.Key] = kv.Value + (kv.Key.Hero.speed.Total * lowestTime);
                        }
                        else
                        {
                            ntp[kv.Key] = kv.Value;
                        }
                    }
                }
                return selectedUnit;
            }
            else
            {
                HeroUnit selectedUnit = SelectRandomHero(potentialTurnUnits);

                Dictionary<HeroUnit, double> ntp = new();
                if (potentialTurnUnits.Count == 1)
                {
                    foreach (var kv in startingPoints)
                    {
                        if (kv.Key == selectedUnit)
                        {
                            ntp[kv.Key] = 0;
                        }
                        else
                        {
                            ntp[kv.Key] = kv.Value;
                        }
                    }
                }

                foreach (var kv in ntp)
                {
                    startingPoints[kv.Key] = kv.Value;
                }
                return selectedUnit;
            }
        }

        private HeroUnit SelectRandomHero(List<HeroUnit> heroes)
        {
            Random random = new();
            int randomNumber = random.Next(0, heroes.Count);
            return heroes[randomNumber];
        }
    }
}