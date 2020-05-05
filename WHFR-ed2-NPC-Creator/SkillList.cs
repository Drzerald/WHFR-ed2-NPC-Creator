using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class SkillList {

		public enum skillId {
			Brzuchomowstwo = 1,
			Charakteryzacja,
			Czytanie,
			CzytanieZWarg,
			Dowodzenie,
			Gadanina,
			Hazard,
			Hipnoza,
			Jezdziectwo,
			Leczenie,
			MocnaGlowa,
			Nawigacja,
			OpiekaNadZwierzetami,
			Oswajanie,
			OtwieranieZamkow,
			Plotkowanie,
			Plywanie,
			Powożenie,
			Przekonywanie,
			Przeszukiwanie,
			Skradanie,
			SplatanieMagii,
			Spostrzegawczosc,
			SztukaPrzetrwania,
			Sledzenie,
			Targowanie,
			Torturowanie,
			Tresura,
			Tropienie,
			Ukrywanie,
			Unik,
			WarzenieTrucizn,
			Wioslarstwo,
			Wspinaczka,
			Wycena,
			WykrywanieMagii,
			ZastawianiePulapek,
			Zastraszanie,
			ZwinnePalce,
			Zeglarstwo
		}

		//Skills Pointers
		public Skill Brzuchomowstwo;
		public Skill Charakteryzacja;
		public Skill Czytanie;
		public Skill CzytanieZWarg;
		public Skill Dowodzenie;
		public Skill Gadanina;
		public Skill Hazard;
		public Skill Hipnoza;
		public Skill Jezdziectwo;
		public Skill Leczenie;
		public Skill MocnaGlowa;
		public Skill Nawigacja;
		public Skill OpiekaNadZwierzetami;
		public Skill Oswajanie;
		public Skill OtwieranieZamkow;
		public Skill Plotkowanie;
		public Skill Plywanie;
		public Skill Powożenie;
		public Skill Przekonywanie;
		public Skill Przeszukiwanie;
		public Skill Skradanie;
		public Skill SplatanieMagii;
		public Skill Spostrzegawczosc;
		public Skill SztukaPrzetrwania;
		public Skill Sledzenie;
		public Skill Targowanie;
		public Skill Torturowanie;
		public Skill Tresura;
		public Skill Tropienie;
		public Skill Ukrywanie;
		public Skill Unik;
		public Skill WarzenieTrucizn;
		public Skill Wioslarstwo;
		public Skill Wspinaczka;
		public Skill Wycena;
		public Skill WykrywanieMagii;
		public Skill ZastawianiePulapek;
		public Skill Zastraszanie;
		public Skill ZwinnePalce;
		public Skill Zeglarstwo;




		public SkillList() {
			Brzuchomowstwo = new Skill(0);
			Charakteryzacja = new Skill(1);
			Czytanie = new Skill(2);
			CzytanieZWarg = new Skill(3);
			Dowodzenie = new Skill(4);
			Gadanina = new Skill(5);
			Hazard = new Skill(6);
			Hipnoza = new Skill(7);
			Jezdziectwo = new Skill(8);
			Leczenie = new Skill(9);
			MocnaGlowa = new Skill(10);
			Nawigacja = new Skill(11);
			OpiekaNadZwierzetami = new Skill(12);
			Oswajanie = new Skill(13);
			OtwieranieZamkow = new Skill(14);
			Plotkowanie = new Skill(15);
			Plywanie = new Skill(16);
			Powożenie = new Skill(17);
			Przekonywanie = new Skill(18);
			Przeszukiwanie = new Skill(19);
			Skradanie = new Skill(20);
			SplatanieMagii = new Skill(21);
			Spostrzegawczosc = new Skill(22);
			SztukaPrzetrwania = new Skill(23);
			Sledzenie = new Skill(24);
			Targowanie = new Skill(25);
			Torturowanie = new Skill(26);
			Tresura = new Skill(27);
			Tropienie = new Skill(28);
			Ukrywanie = new Skill(29);
			Unik = new Skill(30);
			WarzenieTrucizn = new Skill(31);
			Wioslarstwo = new Skill(32);
			Wspinaczka = new Skill(33);
			Wycena = new Skill(34);
			WykrywanieMagii = new Skill(35);
			ZastawianiePulapek = new Skill(36);
			Zastraszanie = new Skill(37);
			ZwinnePalce = new Skill(38);
			Zeglarstwo = new Skill(39);
		}




	private List<Skill> skills = new List<Skill>();

		public List<Skill> Skills{
			get{ return skills; }
		}

		public void advanceSkill(string skillName) {
			foreach (Skill skill in skills) {
				if(skill.ToString() == skillName) {
					skill.Advance();
				}
			}
		}

		public void addSkill(Skill skillAdded) {
			foreach(Skill skill in skills) {
				if(skill.Name == skillAdded.Name) {
					return;
				}
			}
			skills.Add(skillAdded);
		}

	}
}
