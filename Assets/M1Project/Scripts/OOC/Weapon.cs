

using UnityEngine;

namespace M1.ProjectTest
{
    [System.Serializable]
    public class Weapon
    {
        public enum DAMAGE_TYPE { HAND, MAGICAL, PHYSICAL }   // <-- Ho aggiunto il tipo di danno "HAND" nel caso in cui l'Hero dovesse combattere a mani nude

        [SerializeField] private string _name;

        [SerializeField] private DAMAGE_TYPE _dmgType;

        [SerializeField] private ELEMENT _elem;

        [SerializeField] private Stats _bonusStats;

        public Weapon(string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats)
        {
            Name = name;
            DmgType = dmgType;
            Elem = elem;
            BonusStats = bonusStats;
        }

        //------------------------- Property and Set(),Get() -------------------------//
        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Debug.LogError($"Il nome non può essere vuoto!");
                    return;
                }

                _name = value;
            }
        }

        public DAMAGE_TYPE DmgType { get => _dmgType; set => _dmgType = value; }

        public ELEMENT Elem { get => _elem; set => _elem = value; }

        public Stats BonusStats { get => _bonusStats; set => _bonusStats = value; }
    }
}
