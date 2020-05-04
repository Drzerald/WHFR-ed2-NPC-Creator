using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
    class Weapon : Item
    {
        public bool usesStrength;
        public int damage;
        public bool isRanged;
        public int range;
        public int maxRange;
        public string WeaponClass;
        public string WeaponTrait;
    }
}
