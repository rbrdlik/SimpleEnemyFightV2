// ReSharper disable All
namespace SimpleEnemyFightV2.Domain.Entity;

public class Enemy
{
    public string Name;
    public string Style;
    public int BaseDmg;
    public int Hp;
    
    public Enemy(string name, string style, int baseDmg, int hp)
    {
        Name = name;
        Style = style;
        BaseDmg = baseDmg;
        Hp = hp;
    }
    public static class Factory
    {
        public static Enemy Glimrok()
        {
            return new Enemy("Glimrok", "\ud83e\udddf", 5, 75);
        }
        public static Enemy Dredkin()
        {
            return new Enemy("Dredkin", "\ud83d\udc79", 7, 100);
        }
        public static Enemy Murgrul()
        {
            return new Enemy("Murgrul", "\ud83e\uddd9", 12, 110);
        }
        public static Enemy Skarn()
        {
            return new Enemy("Skarn", "\ud83e\udddd", 15, 120);
        }
        public static Enemy Fethrok()
        {
            return new Enemy("Fethrok", "\ud83d\udc7a", 18, 130);
        }
    }
    public override string ToString()
    {
        double hpPercent = (double)Hp / 100 * 100.0;
        int fullHpBar = 10;
        int filledBar = (int)Math.Round((hpPercent / 100) * fullHpBar);
        int emptyBar = fullHpBar - filledBar;
        filledBar = Math.Clamp(filledBar, 0, fullHpBar);
        emptyBar = fullHpBar - filledBar;

        string bar = new string('█', filledBar) + new string('░', emptyBar);
        return $"Životy: {bar} ({Hp}/100 hp) (Damage: {BaseDmg})";
    }
}