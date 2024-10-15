// ReSharper disable All
namespace SimpleEnemyFightV2.Domain.Entity;

public class Player
{
    public string name;
    public int positionX = 1; 
    public int positionY = 1;
    public int BaseDamage { get; private set; }
    public int hp { get; set; }
    public int maxHP { get; set; }
    public Weapon EquippedWeapon { get; private set; }
    
    public Player(string name, int baseDamage, int hp)
    {
        this.name = name;
        this.BaseDamage = baseDamage;
        this.hp = hp;
        this.maxHP = hp;
    }

    public void MovePlayer(char cmd)
    {
        GameField.GameField.dungeon[positionX, positionY] = "  ";
        switch (cmd)
        {
            case 'W':
                if(GameField.GameField.dungeon[positionX - 1, positionY].Equals("  ")) positionX--;
                break;
            case 'S':
                if(GameField.GameField.dungeon[positionX + 1, positionY].Equals("  ")) positionX++;
                break;
            case 'A':
                if(GameField.GameField.dungeon[positionX, positionY - 1].Equals("  ")) positionY--;
                break;
            case 'D':
                if(GameField.GameField.dungeon[positionX, positionY + 1].Equals("  ")) positionY++;
                break;
            default:
                Console.WriteLine("Neplatný tah!");
                break;
        }
        GameField.GameField.dungeon[positionX, positionY] = name;
    }
    
    public void InteractWithChest()
    {
        if (GameField.GameField.IsChestAtPosition(positionX - 1, positionY) || 
            GameField.GameField.IsChestAtPosition(positionX + 1, positionY) ||
            GameField.GameField.IsChestAtPosition(positionX, positionY - 1) || 
            GameField.GameField.IsChestAtPosition(positionX, positionY + 1))
        {
            Console.WriteLine("Truhla byla otevřena!");
            Random rand = new Random();
            int itemType = rand.Next(0, 2);

            if (itemType == 0) 
            {
                int potionType = rand.Next(0, 2);
                PotionType potion = (PotionType)potionType;
                UsePotion(potion);
                Console.WriteLine($"Z truhly padl {potion} potion!");
            }
            else
            {
                Weapon weapon = WeaponFactory.GenerateRandomWeapon();
                EquipWeapon(weapon);
                Console.WriteLine($"Z truhly padla zbraň: {weapon.Name}!");
            }
            
            if (GameField.GameField.IsChestAtPosition(positionX - 1, positionY))
                GameField.GameField.dungeon[positionX - 1, positionY] = "  ";
            else if (GameField.GameField.IsChestAtPosition(positionX + 1, positionY))
                GameField.GameField.dungeon[positionX + 1, positionY] = "  ";
            else if (GameField.GameField.IsChestAtPosition(positionX, positionY - 1))
                GameField.GameField.dungeon[positionX, positionY - 1] = "  ";
            else if (GameField.GameField.IsChestAtPosition(positionX, positionY + 1))
                GameField.GameField.dungeon[positionX, positionY + 1] = "  ";
        }
        else
        {
            Console.WriteLine("Žádná truhla není v dosahu.");
        }
    }

    
    public void EquipWeapon(Weapon weapon)
    {
        EquippedWeapon = weapon;
        BaseDamage += weapon.BaseDamage;
    }

    public void UsePotion(PotionType potionType)
    {
        switch (potionType)
        {
            case PotionType.Health:
                hp = Math.Min(maxHP, hp + 20);
                break;
            case PotionType.Strength:
                BaseDamage += 5;
                break;
        }
    }

    public override string ToString()
    {
        double hpPercent = (double)hp / maxHP * 100.0; 
        int fullHpBar = 10;
        int filledBar = (int)Math.Round((hpPercent / 100) * fullHpBar);
        int emptyBar = fullHpBar - filledBar;
        filledBar = Math.Clamp(filledBar, 0, fullHpBar);
        emptyBar = fullHpBar - filledBar;

        string bar = new string('█', filledBar) + new string('░', emptyBar);
        return $"Životy: {bar} ({hp}/100 hp) (Damage: {BaseDamage})";
    }
}