using System;
using System.Collections.Generic;
using SimpleEnemyFightV2.Domain;
using SimpleEnemyFightV2.Domain.Entity;
using SimpleEnemyFightV2.Domain.GameField;
// ReSharper disable All

namespace SimpleEnemyFightV2
{
    class Program
    {
        public static bool isChestOpen = false;
        static void Main(string[] args)
        {
            bool isGameRunning = true;
            GameField.CreateDungeon();
            Player player = new Player("\ud83e\uddd1", 10, 500);
            WaveSystem.StartWave(WaveSystem.waveNumber);
            WaveSystem.SpawnChests();
            
            while (isGameRunning)
            {
                GameField.DrawPlayer(player);
                GameField.DrawDungeon();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"WAVE {WaveSystem.waveNumber}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--- Hráč ---");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(player);
                Console.WriteLine();
                Enemy currentEnemy = CombatSystem.GetCurrentEnemy();
                if (currentEnemy != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"--- Nepřítel: {currentEnemy.Name} ---");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(currentEnemy);
                }
                Console.WriteLine();
                Console.Write("Ovladač (pohyb: W/A/S/D, interakce: E/F): ");
                char cmd = Char.ToUpper(Console.ReadKey().KeyChar);
                if (cmd == 'F')
                {
                    CombatSystem.PlayerAttack(player);
                }
                if (cmd == 'E')
                {
                    ChestSystem.TryOpenChest(player);
                }

                player.MovePlayer(cmd);
                Console.Clear();

                if (player.hp <= 0)
                {
                    Console.WriteLine("Byl jsi poražen! Prohrál jsi");
                    isGameRunning = false;
                }
                
                if (WaveSystem.AreEnemiesDefeated())
                {
                    if (WaveSystem.waveNumber == 3 && Boss.bossAlive)
                    {
                        Console.WriteLine("Boss byl poražen! Vyhrál jsi hru!");
                        isGameRunning = false;
                    }
                    else
                    {
                        WaveSystem.waveNumber++;
                        if (WaveSystem.waveNumber > 3)
                        {
                            Console.WriteLine("Všechny vlny byly poraženy! Přichází Boss");
                            Boss.SpawnBoss();
                        }
                        else
                        {
                            Console.WriteLine($"Další wave {WaveSystem.waveNumber}");
                            WaveSystem.StartWave(WaveSystem.waveNumber);
                        }
                    }
                }
            }
        }
    }
}