using Assets.Source.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Battle
{
    public abstract class Modifier
    {
        protected abstract void ApplyModifier(HeroUnit heroMono);
        protected abstract void RemoveModifier(HeroUnit heroMono);
    }
}
