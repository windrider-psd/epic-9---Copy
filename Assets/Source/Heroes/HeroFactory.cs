using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Heroes
{
    public static class HeroFactory
    {

        public static Hero CreateHero(HeroId id)
        {
            Hero hero = null;

            if (id == HeroId.MagicGirl)
            {
                hero = new Hero("Megumin", HeroClass.Mage, HeroElement.Fire, id);
            }
            else if (id == HeroId.Aqua)
            {
                hero = new Hero("Aqua", HeroClass.Healer, HeroElement.Ice, id);
            }

            return hero;
            
        }

    }
}
