namespace SimpleEnemyFightV2.Domain.Entity
{
    public class Weapon
    {
        public string Name { get; }
        public int BaseDamage { get; }

        public Weapon(string name, int baseDamage)
        {
            Name = name;
            BaseDamage = baseDamage;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class WeaponFactory
    {
        public static Weapon Sword() => new Weapon("Sword", 5);
        public static Weapon Axe() => new Weapon("Axe", 7);
        public static Weapon Bow() => new Weapon("Bow", 3);
        
        public static Weapon GenerateRandomWeapon()
        {
            Random rand = new Random();
            int weaponType = rand.Next(1, 4);

            return weaponType switch
            {
                1 => Sword(),
                2 => Axe(),
                3 => Bow(),
                _ => Sword()
            };
        }
    }
}