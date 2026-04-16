namespace AiStoryGenerator.Models;

public class HeroStats
{
	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Constitution { get; set; }
	public int Intelligence { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }

	public int GetModifier(int score) => (int)Math.Floor((score - 10) / 2.0);
	public string FormatMod(int score)
	{
		int m = GetModifier(score);
		return m >= 0 ? $"+{m}" : $"{m}";
	}

	public int HP => 8 + GetModifier(Constitution);
	public int AC => 10 + GetModifier(Dexterity);
	public string Initiative => FormatMod(Dexterity);
}