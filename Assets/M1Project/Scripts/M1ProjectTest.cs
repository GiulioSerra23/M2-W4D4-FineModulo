

using M1.ProjectTest;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] private Stats stats;

    [SerializeField] private Weapon weapon;

    [SerializeField] private Hero heroA;

    [SerializeField] private Hero heroB;

    private Hero attacker;   // <-- Variabili Hero temporanee non definite da far riempire in base al risultato di FirstToAttack()
    private Hero defender;

    private void Start()
    {
        //-------- Hero A: BaseStats, Weapon e BonusStats --------//

        heroA = new Hero("Oblique", 250, heroA.BaseStats, ELEMENT.DARK, ELEMENT.LIGHT, heroA.Weapon);

        heroA.BaseStats = new Stats(50, 15, 10, 70, 50, 60, 20);   // Stats(atk, def, res, spd, crt, aim, eva)

        heroA.Weapon = new Weapon("MoonShadow Daggers", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.DARK, heroA.Weapon.BonusStats);

        heroA.Weapon.BonusStats = new Stats(20, 10, 15, 15, 10, 20, 10);


        //-------- Hero B: BaseStats, Weapon e BonusStats --------//

        heroB = new Hero("Unctus", 250, heroB.BaseStats, ELEMENT.WATER, ELEMENT.ICE, heroB.Weapon);

        heroB.BaseStats = new Stats(50, 10, 15, 50, 50, 70, 10);   // Stats(atk, def, res, spd, crt, aim, eva)

        heroB.Weapon = new Weapon("Blue Orbs", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.WATER, heroB.Weapon.BonusStats);

        heroB.Weapon.BonusStats = new Stats(30, 15, 10, 10, 10, 10, 20);  
    }

    public void FirstToAttack(out Hero attacker, out Hero defender)                                  // <-- Funzione che calcola la speed dei due Hero e fa partire prima uno o l'altro
    {                                                                                                //     in base a chi la ha più alta, in caso fosse pari farà tirare un dado
        int totalSpeedHeroA = heroA.BaseStats._spd + heroA.Weapon.BonusStats._spd;                   //     con la funzione PlayWithDices(), l'Hero che fa il numero più alto partirà per primo
        int totalSpeedHeroB = heroB.BaseStats._spd + heroB.Weapon.BonusStats._spd;

        Debug.Log($"INIZIA IL COMBATTIMENTO!");

        if (totalSpeedHeroA > totalSpeedHeroB)
        {
            attacker = heroA;
            defender = heroB;
            Debug.Log($"La speed di {heroA.Name} è maggiore di quella di {heroB.Name}");
        }
        else if (totalSpeedHeroB > totalSpeedHeroA)
        {
            attacker = heroB;
            defender = heroA;
            Debug.Log($"La speed di {heroB.Name} è maggiore di quella di {heroA.Name}");
        }
        else
        {
            Debug.Log($"La speed di {heroA.Name} e {heroB.Name} è pari quindi si tirerà un dado:");

            if (GameFormulas.PlayWithDices(heroA, heroB))
            {
                attacker = heroA;
                defender = heroB;
            }
            else
            {
                attacker = heroB;
                defender = heroA;
            }
        }
    }

    public void Fight(Hero attacker, Hero defender)                                                   // <-- Funzione che fa combattere i due Hero, appena uno finisce il suo turno   
    {                                                                                                 //     i loro ruoli si switchano, continua così finchè uno dei due non muore
        while (attacker.IsAlive() && defender.IsAlive())
        {
            Debug.Log($"{attacker.Name} sta attaccando e {defender.Name} si sta difendendo");

            if (GameFormulas.HasHit(attacker.BaseStats, defender.BaseStats))
            {
                if (attacker.Weapon.Elem == defender.Weakness)
                {
                    Debug.Log("WEAKNESS!");
                }
                else if (attacker.Weapon.Elem == defender.Resistance)
                {
                    Debug.Log("RESIST!");
                }

                int dmgDealt;
                dmgDealt = GameFormulas.CalculateDamage(attacker, defender);
                Debug.Log($"{attacker.Name} ha inflitto {dmgDealt} danni a {defender.Name}");

                defender.TakeDamage(dmgDealt);
            }

            if (!defender.IsAlive())
            {
                Debug.Log($"{defender.Name} é morto!");
                Debug.Log($"COMBATTIMENTO TERMINATO {attacker.Name.ToUpper()} HA VINTO!");
                break;
            }
            else
            {
                (attacker, defender) = (defender, attacker);
            }
        }
    }

    private bool hasDecidedTurn = false;   // <-- Variabile bool sempre in false finchè FirstToAttack() non viene risolta, a quel punto diventa true e parte Fight()

    void Update()
    {
        if (!hasDecidedTurn)
        {
            FirstToAttack(out attacker, out defender);
            hasDecidedTurn = true;
        }
        else
        {
            Fight(attacker, defender);
        }        
    }
}
