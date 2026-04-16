using AiStoryGenerator.Models;

namespace AiStoryGenerator.Services;

public class DiceService
{
	private readonly Random rng = new();

	public int Roll(int sides) => rng.Next(1, sides + 1);

	public int Roll4d6DropLowest()
	{
		var rolls = Enumerable.Range(0, 4)
			.Select(x => Roll(6))
			.OrderBy(x => x)
			.ToList();
		return rolls[1] + rolls[2] + rolls[3]; 
	}

	public HeroStats RollAllStats() => new()
	{
		Strength = Roll4d6DropLowest(),
		Dexterity = Roll4d6DropLowest(),
		Constitution = Roll4d6DropLowest(),
		Intelligence = Roll4d6DropLowest(),
		Wisdom = Roll4d6DropLowest(),
		Charisma = Roll4d6DropLowest(),
	};
}