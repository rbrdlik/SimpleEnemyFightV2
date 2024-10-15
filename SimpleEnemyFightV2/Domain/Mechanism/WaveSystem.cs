using SimpleEnemyFightV2.Domain.Entity;
using System;
using System.Collections.Generic;
// ReSharper disable All

namespace SimpleEnemyFightV2.Domain;

public class WaveSystem
{
    public static int waveNumber = 1;
    public static List<Enemy> currentEnemies = new List<Enemy>();

    public static void StartWave(int wave)
    {
        currentEnemies.Clear();
        Random rand = new Random();

        if (wave == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                Enemy enemy = GenerateRandomEnemy(rand);
                currentEnemies.Add(enemy);
                int x = rand.Next(1, 8);
                int y = rand.Next(1, 24);
                GameField.GameField.dungeon[x, y] = enemy.Style;
            }
        }
        else if (wave == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                Enemy enemy = GenerateRandomEnemy(rand);
                currentEnemies.Add(enemy);
                int x = rand.Next(9, 16);
                int y = rand.Next(1, 24);
                GameField.GameField.dungeon[x, y] = enemy.Style;
            }
            GameField.GameField.dungeon[8, 12] = "  ";
        }
        else if (wave == 3)
        {
            for (int i = 0; i < 2; i++)
            {
                Enemy enemy = GenerateRandomEnemy(rand);
                currentEnemies.Add(enemy);
                int x = rand.Next(17, 24);
                int y = rand.Next(1, 24);
                GameField.GameField.dungeon[x, y] = enemy.Style;
            }

            GameField.GameField.dungeon[16, 12] = "  "; 
            
            if (AreEnemiesDefeated())
            {
                Console.WriteLine("Všichni nepřátelé byli poraženi! Přichází Boss...");
                Boss.SpawnBoss();
            }
        }

    }

    private static Enemy GenerateRandomEnemy(Random rand)
    {
        int enemyType = rand.Next(1, 6);
        return enemyType switch
        {
            1 => Enemy.Factory.Glimrok(),
            2 => Enemy.Factory.Dredkin(),
            3 => Enemy.Factory.Murgrul(),
            4 => Enemy.Factory.Skarn(),
            5 => Enemy.Factory.Fethrok(),
            _ => Enemy.Factory.Glimrok(),
        };
    }

    public static bool AreEnemiesDefeated()
    {
        foreach (var enemy in currentEnemies)
        {
            if (enemy.Hp > 0)
            {
                return false;
            }
        }
        return true;
    }
    
    public static void SpawnChests()
    {
        Random rand = new Random();
        int chestsSpawnedInRoom = 0;
        
        for (int i = 1; i < 24; i++)
        {
            for (int j = 1; j < 24; j++)
            {
                if (rand.Next(0, 100) < 5 && chestsSpawnedInRoom < 3)
                {
                    if (GameField.GameField.dungeon[i, j] == "  ")
                    {
                        GameField.GameField.SpawnChest(i, j);
                        chestsSpawnedInRoom++;
                    }
                }
            }
        }
    }
}
