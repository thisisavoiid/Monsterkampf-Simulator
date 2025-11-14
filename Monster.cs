using static System.Net.Mime.MediaTypeNames;

namespace Monsterkampf_Simulator
{
    public class Ork : Monster
    {

        public Ork()
        {
            _description = "The Ork is a brutal fighter who reflects part of the damage back at attackers, dealing more as his health drops.";
        }

        public override void TakeDamage(float damage, Monster attacker)
        {
            this._hp -= damage;

            if (this._hp < 0)
            {
                this._hp = 0;
            }

            // This monster reflects some of the damage it takes back to the attacker. The lower HP it has, the more damage it reflects.
            if (this._hp > 0)
            {
                float hpRatio = this._maxHp / this._hp;
                attacker.TakeDamage((float)Math.Round(hpRatio * 10), this);

                InfoBoard.AddEntry(
                    new InfoBoardAction
                    {
                        content = $"{this.GetType().Name} inflicted {(float)Math.Round(hpRatio * 10)} damage by thorns!",
                        fgColor = ConsoleColor.DarkYellow,
                    }
                );
            }

            InfoBoard.AddEntry(
                new InfoBoardAction
                {
                    content = $"{this.GetType().Name} took {damage} damage!",
                    fgColor = ConsoleColor.DarkRed,
                }
            );

        }

        public override void Attack(Monster target)
        {

            Thread.Sleep((int)Math.Round(_s));

            float damage = (_ap - target._dp);

            if (damage <= 0)
            {
                damage = 0;
            }

            target.TakeDamage(damage, this);
        }
    }


    public class Troll : Monster
    {
        public Troll()
        {
            _description = "The Troll is a resilient magic tank with a 33% chance to reduce incoming damage by 50%, shrinking down attacks unpredictably.";
        }

        public override void TakeDamage(float damage, Monster attacker)
        {
            int randomInt = random.Next(1, 4);

            // This monster has a ~33% chance of taking 50% of the incoming damage.
            if (randomInt == 1 && damage > 0)
            {
                damage = damage - damage / 2;

                InfoBoard.AddEntry(
                    new InfoBoardAction
                    {
                        content = $"{this.GetType().Name} tanked 50% damage!",
                        fgColor = ConsoleColor.DarkYellow,
                    }
                );
            }

            this._hp -= damage;

            if (this._hp < 0)
            {
                this._hp = 0;
            }

            InfoBoard.AddEntry(
                new InfoBoardAction
                {
                    content = $"{this.GetType().Name} took {damage} damage!",
                    fgColor = ConsoleColor.DarkRed,
                }
            );
        }

        public override void Attack(Monster target)
        {

            Thread.Sleep((int)Math.Round(_s));

            float damage = (_ap - target._dp);

            if (damage <= 0)
            {
                damage = 0;
            }

            target.TakeDamage(damage, this);
        }
    }


    public class Goblin : Monster
    {

        public Goblin()
        {
            _description = "The Goblin is a tricky survivor who has a 20% chance to regain health when hit, trading attack speed for extra survivability.";

        }
        public override void TakeDamage(float damage, Monster attacker)
        {
            int randomInt = random.Next(1, 6);

            this._hp -= damage;

            // This monster has a 20% chance to regain health upon damage taken. Slower attack speed = More HP regain.
            if (randomInt == 1 && damage > 0)
            {
                float hpRegained = Math.Min(this._maxHp, this._s / 50);
                this._hp += hpRegained;

                InfoBoard.AddEntry(
                    new InfoBoardAction
                    {
                        content = $"{this.GetType().Name} evaded part of the damage and regained {hpRegained} HP!",
                        fgColor = ConsoleColor.DarkYellow,
                    }
                );
            }

            if (this._hp < 0)
            {
                this._hp = 0;
            }

            InfoBoard.AddEntry(
                new InfoBoardAction
                {
                    content = $"{this.GetType().Name} took {damage} damage!",
                    fgColor = ConsoleColor.DarkRed,
                }
            );
        }

        public override void Attack(Monster target)
        {

            Thread.Sleep((int)Math.Round(_s));

            float damage = (_ap - target._dp);

            if (damage <= 0)
            {
                damage = 0;
            }

            target.TakeDamage(damage, this);
        }
    }

    public abstract class Monster
    {
        public float _dp { get; set; } // The amount of defense the monster can use.
        public float _ap { get; set; } // The amount of attack damage the monster can deal.
        public float _hp { get; set; } // The amount of current health points of the monster.
        public float _s { get; set; } // The attack speed of the monster. Used inside the attack threads to allow for asynchronous game events.
        public float _maxHp { get; set; } // Max HP of the monster. Used for the statbar display.
        public string _description { get; set; } // Description of the monster. Used for the display in the main menu.

        protected Random random = new Random(); // Instance of a random object to allow for random events.

        /// <summary>
        /// Creates a default monster object.
        /// </summary>
        public Monster()
        {
            _dp = 0;
            _ap = 0;
            _hp = 0;
            _s = 0;
            _maxHp = 0;
            _description = string.Empty;
        }

        /// <summary>
        /// Creates a new monster with passed values.
        /// </summary>
        /// <param name="hp">
        /// The amount of health points of the monster.
        /// </param>
        /// <param name="ap">
        /// The amount of attack damage the monster can deal.
        /// </param>
        /// <param name="dp">
        /// The amount of defense the monster can use.
        /// </param>
        /// <param name="s">
        /// The attack speed of the monster.
        /// </param>
        public Monster(float hp, float ap, float dp, float s)
        {
            _dp = dp;
            _ap = ap;
            _hp = hp;
            _s = s;
            _maxHp = hp;
            _description = string.Empty;
        }

        /// <summary>
        /// Makes the monster take damage. 
        /// Is used to force a reaction to an attack.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage to be taken.
        /// </param>
        /// <param name="attacker">
        /// A reference of the monster which caused the attack.
        /// </param>
        public abstract void TakeDamage(float damage, Monster attacker);

        /// <summary>
        /// Makes the monster attack another monster.
        /// </summary>
        /// <param name="target">
        /// The target monster to be attacked.
        /// </param>
        public abstract void Attack(Monster target);

        /// <summary>
        /// Checks whether the monster is currently alive or not.
        /// </summary>
        /// <returns>
        /// True if the monster is alive, False if it isn't.
        /// </returns>
        public bool IsAlive()
        {
            return (_hp > 0);
        }
    }
}
