using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Profession {
		private string name = "";
		private bool isAdvanced;
		private MainCharacteristics mainCharacteristicsProgression = new MainCharacteristics();
		private SideCharacteristics sideCharacteristicsProgression = new SideCharacteristics();
		private List<SkillProfile> skillProgression = new List<SkillProfile>();
        private List<Item> inventory = new List<Item>();
	}
}
