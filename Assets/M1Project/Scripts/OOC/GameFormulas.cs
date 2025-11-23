


using UnityEngine;

namespace M1.ProjectTest
{
    public static class GameFormulas
    {
        public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
        {
            return attackElement == defender.Weakness ? true : false;   // <-- Ho usato l'operatore ternario dentro la funzione perchè non volevo fare una singola riga troppo lunga
        }

        public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
        {
            return attackElement == defender.Resistance ? true : false;
        }

        public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
        {
            float elementalVote;

            if (HasElementAdvantage(attackElement, defender))
            {
                elementalVote = 1.5f;
            }
            else if (HasElementDisadvantage(attackElement, defender))
            {
                elementalVote = 0.5f;
            }
            else
            {
                elementalVote = 1f;
            }

            return elementalVote;
        }

        public static bool HasHit(Stats attacker, Stats defender)
        {
            int hitChance = attacker._aim - defender._eva;
            int randomNumber = Random.Range(0, 100);

            if (randomNumber > hitChance)
            {
                Debug.Log("MISS!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCrit(int critValue)
        {
            int randomNumber = Random.Range(0, 100);

            if (randomNumber < critValue)
            {
                Debug.Log("CRIT!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int CalculateDamage(Hero attacker, Hero defender)
        {
            Stats totalStatsAttacker = attacker.BaseStats;
            Stats totalStatsDefender = defender.BaseStats;

            if (attacker.Weapon.DmgType != Weapon.DAMAGE_TYPE.HAND)   // <-- If che controlla se il DmgType dell'arma è diverso da "HAND" in quel caso fa la somma delle Stats
            {
                totalStatsAttacker = Stats.Sum(attacker.BaseStats, attacker.Weapon.BonusStats);
            }

            if (defender.Weapon.DmgType != Weapon.DAMAGE_TYPE.HAND)
            {
                totalStatsDefender = Stats.Sum(defender.BaseStats, defender.Weapon.BonusStats);
            }


            int defenseType = 0;
            switch (attacker.Weapon.DmgType)
            {
                case Weapon.DAMAGE_TYPE.MAGICAL:
                    defenseType = totalStatsDefender._res;
                    break;

                case Weapon.DAMAGE_TYPE.PHYSICAL:
                    defenseType = totalStatsDefender._def;
                    break;
                    
                case Weapon.DAMAGE_TYPE.HAND:
                    defenseType = totalStatsDefender._def;
                    break;                
            }

            float baseDamage = totalStatsAttacker._atk - defenseType;
            baseDamage *= EvaluateElementalModifier(attacker.Weapon.Elem, defender);

            if (IsCrit(totalStatsAttacker._crt))
            {
                baseDamage *= 2;
            }

            Mathf.RoundToInt(baseDamage);

            return Mathf.Max(0, (int)baseDamage);
        }

        public static bool PlayWithDices(Hero heroA, Hero heroB)   // <-- Funzione che tira due dadi finchè uno dei due è maggiore dell'altro
        {
            int firstDice;
            int secondDice;

            do
            {
                firstDice = Random.Range(1, 7);   // <-- D6: 1-6
                secondDice = Random.Range(1, 7);

                Debug.Log($"Dal dado di {heroA.Name} è uscito {firstDice} e dal dado di {heroB.Name} è uscito {secondDice}");

                if (firstDice == secondDice)
                {
                    Debug.Log($"Parità! Si ritirano i dadi:");
                }

            } while (firstDice == secondDice);

            return firstDice > secondDice ? true : false;
        }
    }
}

