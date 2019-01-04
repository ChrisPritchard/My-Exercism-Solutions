using System;
using System.Linq;

public class DndCharacter
{
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Constitution { get; private set; }
    public int Intelligence { get; private set; }
    public int Wisdom { get; private set; }
    public int Charisma { get; private set; }
    public int Hitpoints { get; private set; }

    private static Random random = new Random();

    private static Func<int> d6 = () => random.Next(1, 7);

    public static int Modifier(int score) => (int)Math.Floor((double)(score - 10) / 2);

    public static int Ability() => new [] { d6(), d6(), d6(), d6() }.OrderBy(id => id).Skip(1).Sum();

    public static DndCharacter Generate()
    {
        var character = new DndCharacter
        {
            Strength = Ability(),
            Dexterity = Ability(),
            Constitution = Ability(),
            Intelligence = Ability(),
            Wisdom = Ability(),
            Charisma = Ability(),
        };
        character.Hitpoints = 10 + Modifier(character.Constitution);
        return character;
    }
}
