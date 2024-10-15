using SimpleEnemyFightV2.Domain.Entity;
// ReSharper disable All

namespace SimpleEnemyFightV2.Domain
{
    public class Boss : Enemy
    {
        public bool phaseTwo = false;
        public static bool bossAlive = true; 
        public int maxHp;

        public Boss() : base("Boss", "👹", 15, 200)
        {
            maxHp = Hp;
        }

        public void Attack(Player player)
        {
            if (!phaseTwo && Hp <= 100)
            {
                Console.WriteLine("Boss se rozzuřil a jeho síla se zvýšila!");
                BaseDmg += 5;
                phaseTwo = true;
            }

            if (Hp <= 0)
            {
                bossAlive = false;
            }
            
            player.hp -= BaseDmg;
            Console.WriteLine($"Boss tě udeřil za {BaseDmg} dmg!");
        }
        public static void SpawnBoss()
        {
            Boss boss = new Boss();
            GameField.GameField.dungeon[12, 12] = boss.Style;
            WaveSystem.currentEnemies.Add(boss);
        }

        public void EnterPhaseTwo()
        {
            phaseTwo = true;
            BaseDmg += 5;  
            Console.WriteLine($"{Name} se rozzlobil! Nyní způsobuje více škod (damage +5)!");
        }
        
    }
}