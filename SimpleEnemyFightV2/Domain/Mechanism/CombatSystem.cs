using SimpleEnemyFightV2.Domain.Entity;
using SimpleEnemyFightV2.Domain.GameField;
// ReSharper disable All
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SimpleEnemyFightV2.Domain
{
    public static class CombatSystem
    {
        private static Enemy currentEnemy;

        public static void PlayerAttack(Player player)
        {
            Enemy targetEnemy = null;
            int targetX = 0, targetY = 0;
            
            if (IsEnemyAtPosition(player.positionX - 1, player.positionY, out targetEnemy))
            {
                targetX = player.positionX - 1;
                targetY = player.positionY;
            }
            else if (IsEnemyAtPosition(player.positionX + 1, player.positionY, out targetEnemy))
            {
                targetX = player.positionX + 1;
                targetY = player.positionY;
            }
            else if (IsEnemyAtPosition(player.positionX, player.positionY - 1, out targetEnemy))
            {
                targetX = player.positionX;
                targetY = player.positionY - 1;
            }
            else if (IsEnemyAtPosition(player.positionX, player.positionY + 1, out targetEnemy))
            {
                targetX = player.positionX;
                targetY = player.positionY + 1;
            }
            
            if (targetEnemy != null)
            {
                currentEnemy = targetEnemy;
                targetEnemy.Hp -= player.BaseDamage;
                Console.WriteLine($"Útok! Nepřítel {targetEnemy.Name} dostal {player.BaseDamage} dmg!");
                
                if (targetEnemy is Boss boss)
                {
                    BossFight(player, boss);
                }
                else
                {
                    if (targetEnemy.Hp > 0)
                    {
                        player.hp -= targetEnemy.BaseDmg;
                        Console.WriteLine($"{targetEnemy.Name} tě udeřil za {targetEnemy.BaseDmg} dmg!");
                    }
                    else
                    {
                        Console.WriteLine($"Nepřítel {targetEnemy.Name} byl poražen!");
                        GameField.GameField.dungeon[targetX, targetY] = "  ";
                        currentEnemy = null;
                    }
                }
            }
            else
            {
                Console.WriteLine("Žádný nepřítel není v dosahu útoku!");
            }
        }
        
        private static void BossFight(Player player, Boss boss)
        {
            if (boss.Hp > 0)
            {
                if (!boss.phaseTwo && boss.Hp <= boss.maxHp / 2)
                {
                    boss.EnterPhaseTwo();
                }
                
                boss.Attack(player);
                if (boss.Hp <= 0)
                {
                    Console.WriteLine("Boss byl poražen! Vyhrál jsi hru!");
                    currentEnemy = null;
                    Environment.Exit(0);
                }
            }
        }

        public static Enemy GetCurrentEnemy()
        {
            return currentEnemy;
        }

        private static bool IsEnemyAtPosition(int x, int y, out Enemy enemy)
        {
            enemy = null;
            
            foreach (var e in WaveSystem.currentEnemies)
            {
                if (GameField.GameField.dungeon[x, y] == e.Style && e.Hp > 0)
                {
                    enemy = e;
                    return true;
                }
            }

            return false;
        }
    }
}
