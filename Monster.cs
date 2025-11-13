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
        public float _dp { get; set; }
        public float _ap { get; set; }
        public float _hp { get; set; }
        public float _s { get; set; }
        public float _maxHp { get; set; }
        public string _description { get; set; }

        protected Random random = new Random();

        public Monster()
        {
            _dp = 0;
            _ap = 0;
            _hp = 0;
            _s = 0;
            _maxHp = 0;
            _description = string.Empty;
        }

        public Monster(float hp, float ap, float dp, float s)
        {
            _dp = dp;
            _ap = ap;
            _hp = hp;
            _s = s;
            _maxHp = hp;
            _description = string.Empty;
        }

        public abstract void TakeDamage(float damage, Monster attacker);
        public abstract void Attack(Monster target);

        public bool IsAlive()
        {
            return (_hp > 0);
        }
    }
}
