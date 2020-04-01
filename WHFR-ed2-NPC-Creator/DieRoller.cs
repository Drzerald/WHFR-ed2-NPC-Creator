using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
    class DieRoller
    {
		Random rnd = new Random();

		public int rollDie(int numberOfRolls, int dieNumber) {
			int x = 0;
			for (int i = 0; i < numberOfRolls; i++) {
				x += (int)Math.Floor(rnd.NextDouble() * dieNumber) + 1;
			}
			return x;
		}

		public int rollD100(int numberOfRolls = 1) {
			return rollDie(numberOfRolls, 100);
		}

		public int rollD10(int numberOfRolls = 1) {
			return rollDie(numberOfRolls, 10);
		}

	}
}
