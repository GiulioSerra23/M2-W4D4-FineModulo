

using UnityEngine;

namespace M1.ProjectTest
{
    [System.Serializable]
    public class Hero
    {
        [SerializeField] private string _name;

        [SerializeField] private int _hp;

        [SerializeField] private Stats _baseStats;

        [SerializeField] private ELEMENT _resistance;

        [SerializeField] private ELEMENT _weakness;

        [SerializeField] private Weapon _weapon;


        public Hero() { }  // Costruttore per le variabili temporanee

        public Hero(string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon)
        {
            Name = name;
            SetHp(hp);
            BaseStats = baseStats;
            Resistance = resistance;
            Weakness = weakness;
            Weapon = weapon;
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

        public void SetHp(int hp) => _hp = Mathf.Max(0, hp);

        public Stats BaseStats { get => _baseStats; set => _baseStats = value; }

        public ELEMENT Resistance { get => _resistance; set => _resistance = value; }

        public ELEMENT Weakness { get => _weakness; set => _weakness = value; }

        public Weapon Weapon { get => _weapon; set => _weapon = value; }


        //-------------------------------- Functions() --------------------------------//

        public void AddHp(int amount) => SetHp(_hp + amount);

        public void TakeDamage(int damage) => AddHp(-damage);

        public bool IsAlive() => _hp > 0 ? true : false;
    }
}
