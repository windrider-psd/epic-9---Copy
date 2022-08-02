using System.Collections.Generic;

namespace Assets.Source.Heroes
{
    [System.Serializable]
    public class HeroStat
    {
        private static readonly Dictionary<HeroStatId, int> maxValues = new()
        {
            { HeroStatId.CritChance, 100 },
            { HeroStatId.CritDamage, 300 }
        };

        private static readonly Dictionary<HeroId, Dictionary<HeroStatId, int>> statBaseValue = new()
        {
            {
                HeroId.MagicGirl,
                new()
                {
                    { HeroStatId.Attack, 1200 },
                    { HeroStatId.Armor, 600 },
                    { HeroStatId.HP, 5000 },
                    { HeroStatId.Speed, 110 },
                    { HeroStatId.CritChance, 15 },
                    { HeroStatId.CritDamage, 190 },
                    { HeroStatId.Eff, 20 },
                    { HeroStatId.Res, 0 },
                }
            },
            {
                HeroId.Aqua,
                new()
                {
                    { HeroStatId.Attack, 600 },
                    { HeroStatId.Armor, 1100 },
                    { HeroStatId.HP, 3000 },
                    { HeroStatId.Speed, 95 },
                    { HeroStatId.CritChance, 0 },
                    { HeroStatId.CritDamage, 150 },
                    { HeroStatId.Eff, 20 },
                    { HeroStatId.Res, 50 },
                }
            }
        };

        private static readonly Dictionary<HeroId, Dictionary<HeroStatId, int>> statGainData = new()
        {
            {
                HeroId.MagicGirl,
                new()
                {
                    { HeroStatId.Attack, 100 },
                    { HeroStatId.Armor, 30 },
                    { HeroStatId.HP, 200 },
                    { HeroStatId.Speed, 3 },
                    { HeroStatId.CritChance, 5 },
                    { HeroStatId.CritDamage, 10 },
                    { HeroStatId.Eff, 5 },
                    { HeroStatId.Res, 3 },
                }
            },
            {
                HeroId.Aqua,
                new()
                {
                    { HeroStatId.Attack, 100 },
                    { HeroStatId.Armor, 30 },
                    { HeroStatId.HP, 200 },
                    { HeroStatId.Speed, 3 },
                    { HeroStatId.CritChance, 5 },
                    { HeroStatId.CritDamage, 10 },
                    { HeroStatId.Eff, 5 },
                    { HeroStatId.Res, 3 },
                }
            }
        };

        private readonly Dictionary<HeroId, Dictionary<HeroStatId, Dictionary<int, int>>> levelCostData = new()
        {
            {
                HeroId.MagicGirl,
                new()
                {
                    {
                        HeroStatId.Attack,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Armor,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.HP,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.CritChance,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.CritDamage,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Eff,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Res,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    }
                }
            },
            {
                HeroId.Aqua,
                new()
                {
                    {
                        HeroStatId.Attack,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Armor,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.HP,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.CritChance,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.CritDamage,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Eff,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    },
                    {
                        HeroStatId.Res,
                        new()
                        {
                            { 1, 1 },
                            { 20, 2 },
                            { 30, 3 },
                            { 40, 4 },
                            { 50, 5 },
                            { 60, 6 },
                        }
                    }
                }
            }
        };

        private Dictionary<object, int> flatModifiers = new();
        private HeroId heroIdentifier;
        private HeroStatId id;
        private int level;
        private Dictionary<object, int> porcentageModifiers = new();

        public HeroStat(HeroStatId id, HeroId heroIdentifier, int level)
        {
            Id = id;
            HeroIdentifier = heroIdentifier;
            Level = level;
        }

        public int BaseStat { get => statBaseValue[HeroIdentifier][id]; }
        public HeroId HeroIdentifier { get => heroIdentifier; set => heroIdentifier = value; }
        public HeroStatId Id { get => id; set => id = value; }
        public int Level { get => level; set => level = value; }

        public int NextLevelCost
        {
            get
            {
                var keys = levelCostData[heroIdentifier][id];

                int highestKey = -1;

                foreach (var kv in keys)
                {
                    if (Level + 1 >= kv.Key)
                    {
                        if (kv.Key > highestKey)
                        {
                            highestKey = kv.Key;
                        }
                    }
                }
                return levelCostData[heroIdentifier][id][highestKey];
            }
        }

        public int PartialTotal { get => BaseStat + TotalFromLevels; }

        public int Total
        {
            get
            {
                int total = PartialTotal * (1 + TotalPorcentage / 100) + TotalFlat;

                if (maxValues.TryGetValue(id, out int limit))
                {
                    if (total > limit)
                    {
                        total = limit;
                    }
                }
                return total;
            }
        }

        public int TotalFromLevels { get => LevelStatGain(HeroIdentifier, id) * level; }

        private int TotalFlat
        {
            get
            {
                int total = 0;
                foreach (var kv in flatModifiers)
                {
                    total += kv.Value;
                }
                return total;
            }
        }

        private int TotalPorcentage
        {
            get
            {
                int total = 0;
                foreach (var kv in porcentageModifiers)
                {
                    total += kv.Value;
                }
                return total;
            }
        }

        public void AddFlatModifier(object source, int value)
        {
            flatModifiers[source] = value;
        }

        public void AddPorcentageModifier(object source, int value)
        {
            porcentageModifiers[source] = value;
        }

        public int LevelStatGain(HeroId hero, HeroStatId stat)
        {
            return statGainData[hero][stat];
        }

        public int LevelStatLevelCost(HeroId hero, HeroStatId stat)
        {
            return statGainData[hero][stat];
        }

        public void RemoveFlatModifier(object source, int value)
        {
            flatModifiers[source] = value;
        }

        public void RemovePorcentageModifier(object source, int value)
        {
            porcentageModifiers[source] = value;
        }
    }
}