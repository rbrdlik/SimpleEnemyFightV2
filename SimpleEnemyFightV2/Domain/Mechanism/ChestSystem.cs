using SimpleEnemyFightV2.Domain.Entity;
// ReSharper disable All

namespace SimpleEnemyFightV2.Domain;

public class ChestSystem
{
    public static void TryOpenChest(Player player)
    {
        int[,] directions = { {-1, 0}, {1, 0}, {0, -1}, {0, 1} };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = player.positionX + directions[i, 0];
            int newY = player.positionY + directions[i, 1];
            
            if (GameField.GameField.dungeon[newX, newY] == "📦")
            {
                OpenChest(player, newX, newY);
                return;
            }
        }
        
        Console.WriteLine("Žádná bedna není v dosahu!");
    }

    public static void OpenChest(Player player, int x, int y)
    {
        Random rand = new Random();
        int item = rand.Next(1, 3);

        if (item == 1)
        {
            PotionType potion = (PotionType)rand.Next(0, 2);
            player.UsePotion(potion);
            Console.WriteLine($"Našel jsi potion: {potion}!");

            if (potion == PotionType.Health)
            {
                Console.WriteLine($"Tvůj život byl zvýšen o 20 bodů. Aktuální životy: {player.hp}/{player.maxHP}");
            }
            else if (potion == PotionType.Strength)
            {
                Console.WriteLine($"Tvoje síla byla zvýšena o 5. Aktuální síla: {player.BaseDamage}");
            }
        }
        else if (item == 2)
        {
            Weapon weapon = WeaponFactory.GenerateRandomWeapon();
            player.EquipWeapon(weapon);
            Console.WriteLine($"Našel jsi zbraň: {weapon.Name}! Zvýšení síly o {weapon.BaseDamage}.");
        }
        
        GameField.GameField.dungeon[x, y] = "  ";
    }
}