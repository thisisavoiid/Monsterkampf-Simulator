namespace Monsterkampf_Simulator
{
    public class Ork : Monster
    {

        public Ork()
        {
            _description = "The mischievious Ork has the ability to inflict a thorn effect on its opponent, dealing back a certain damage!";
        }

        public override void TakeDamage(float damage, Monster attacker)
        {
            this._hp -= damage;

            if (this._hp < 0)
            {
                this._hp = 0;
            }

            float hpRatio = this._maxHp / this._hp;
            attacker.TakeDamage((float)Math.Round(hpRatio * 10), this);
            GUIHandler.AddInfoBoardEntry($"{this.GetType().Name} took {damage} damage!");
        }

        public override void Attack(Monster target)
        {
            base.Attack(target);

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
            _description = "Hier muss eine Beschreibung hin, die die Fähigkeit des Monsters erklärt!";
        }

        public override void TakeDamage(float damage, Monster attacker)
        {
            this._hp -= damage;
            if (this._hp < 0)
            {
                this._hp = 0;
            }
            GUIHandler.AddInfoBoardEntry($"{this.GetType().Name} took {damage} damage!");
        }

        public override void Attack(Monster target)
        {
            base.Attack(target);

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
            _description = "Hier muss eine Beschreibung hin, die die Fähigkeit des Monsters erklärt!";

        }
        public override void TakeDamage(float damage, Monster attacker)
        {
            this._hp -= damage;
            if (this._hp < 0)
            {
                this._hp = 0;
            }
            GUIHandler.AddInfoBoardEntry($"{this.GetType().Name} took {damage} damage!");
        }

        public override void Attack(Monster target)
        {
            base.Attack(target);

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
        // Monster attributes
        public float _dp { get; set; }
        public float _ap { get; set; }
        public float _hp { get; set; }
        public float _s { get; set; }
        public float _maxHp { get; set; }
        public string _description { get; set; }

        internal Monster()
        {
            _dp = 0;
            _ap = 0;
            _hp = 0;
            _s = 0;
            _maxHp = 0;
            _description = string.Empty;
        }

        internal Monster(float hp, float ap, float dp, float s)
        {
            _dp = dp;
            _ap = ap;
            _hp = hp;
            _s = s;
            _maxHp = hp;
        }

        public abstract void TakeDamage(float damage, Monster attacker);
        public virtual void Attack(Monster target)
        {
            GUIHandler.AddInfoBoardEntry($"{this.GetType().Name} attacked {target.GetType().Name}!");
        }

        public bool IsAlive()
        {
            return (_hp > 0);
        }
    }
}
