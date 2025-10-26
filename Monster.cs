using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Monsterkampf_Simulator
{
    public class Monster
    {
        // Monster attributes
        public float DP; // Defense points (damage reduction)
        public float AP; // Attack power (damage)
        public float HP; // Health points
        public float S;  // Speed (used as attack cooldown in milliseconds)
        public float maxHP; 

        // Monster class/type (e.g., Ork, Troll)
        internal Program.MonsterClass Class;

        // Default constructor, creates a monster with default values
        internal Monster()
        {
            DP = 0;
            AP = 0;
            HP = 0;
            S = 0;
            maxHP = 0;
            Class = Program.MonsterClass.Ork; // Default monster type
        }

        // Overloaded constructor, creates a monster with custom values
        internal Monster(float hp, float ap, float dp, float s, Program.MonsterClass monster_type)
        {
            DP = dp;
            AP = ap;
            HP = hp;
            S = s;
            Class = monster_type;
            maxHP = hp;
        }

        // Prints info about the newly created monster (called optionally)
        public void NewMonsterCreatedFeedback()
        {
            DebugPrinter.Print(
                level: DebugPrinter.DebugLevel.Info,
                message: $"Monster of Type {Class} created successfully. Values have been set to: AP: {AP} DP: {DP} HP: {HP} S: {S}"
            );
        }

        // Method to attack another monster
        public void Attack(Monster target)
        {
            // Wait for a duration based on attack speed before attacking again
            Thread.Sleep((int)Math.Round(S));

            // Calculate damage dealt to target
            float damage = (AP - target.DP);

            // Ensure damage is not negative to avoid breaking the game
            if (damage <= 0)
            {
                DebugPrinter.Print(
                    level: DebugPrinter.DebugLevel.Warning,
                    message: $"With the current configuration of {Class}, the attack damage is {damage} which could break the game. Damage has been defaulted to 0."
                );
                damage = 0;
            }

            // Reduce target's HP by the calculated damage
            target.HP -= damage;

            /* Log target's updated HP
            DebugPrinter.Print(
                level: DebugPrinter.DebugLevel.Info,
                message: $"(Round: {Program.currentRound}) {target.Class} Monster HP has been refreshed: {target.HP}"
            );

            // Log attack cooldown info
            DebugPrinter.Print(
                level: DebugPrinter.DebugLevel.Info,
                message: $"Waiting for {this.Class} to be able to attack again: {this.S}ms Cooldown initialized."
            );
            */
        }
    }
}
