// ReSharper disable All

using SimpleEnemyFightV2.Domain.Entity;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SimpleEnemyFightV2.Domain.GameField;

public class GameField
{
    public static string[,] dungeon;
    static int dungeonSize = 25;

    public static void CreateDungeon()
    {
        dungeon = new string[dungeonSize, dungeonSize];
        
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                if (i == 0 || i == dungeonSize - 1 || j == 0 || j == dungeonSize - 1)
                {
                    dungeon[i, j] = "🧱"; 
                }
                else if (i == 8 || i == 16)
                {
                    dungeon[i, j] = "🧱"; 
                }
                else
                {
                    dungeon[i, j] = "  "; 
                }
            }
        }
        
        dungeon[8, 12] = "🔒";
        dungeon[16, 12] = "🔒";
    }

    public static void DrawPlayer(Player player)
    {
        dungeon[player.positionX, player.positionY] = player.name;
    }

    public static void SpawnChest(int x, int y)
    {
        GameField.dungeon[x, y] = "📦";
    }

    public static bool IsChestAtPosition(int x, int y)
    {
        return GameField.dungeon[x, y] == "📦";
    }
    
    public static void DrawDungeon()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                Console.Write(dungeon[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}