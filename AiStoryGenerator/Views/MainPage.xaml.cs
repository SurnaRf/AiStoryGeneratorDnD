using AiStoryGenerator.Models;
using AiStoryGenerator.Services;

namespace AiStoryGenerator.Views;

public partial class MainPage : ContentPage
{
	GeminiService gemini = new GeminiService();
	ImageService imageService = new ImageService();
	DiceService _dice = new DiceService();
	HeroStats _currentStats;

	private async void OnDiceClicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		int sides = int.Parse(btn.CommandParameter.ToString());

		await btn.ScaleTo(1.15, 60);
		await btn.ScaleTo(1.0, 60);

		int result = _dice.Roll(sides);
		DiceResultLabel.Text = $"d{sides} rolled — you got {result}";
	}

	private async void OnRollAllClicked(object sender, EventArgs e)
	{
		RollAllBtn.IsEnabled = false;

		for (int i = 0; i < 12; i++)
		{
			StatSTR.Text = new Random().Next(3, 19).ToString();
			StatDEX.Text = new Random().Next(3, 19).ToString();
			StatCON.Text = new Random().Next(3, 19).ToString();
			StatINT.Text = new Random().Next(3, 19).ToString();
			StatWIS.Text = new Random().Next(3, 19).ToString();
			StatCHA.Text = new Random().Next(3, 19).ToString();
			await Task.Delay(55);
		}

		_currentStats = _dice.RollAllStats();
		SetStat(StatSTR, ModSTR, _currentStats.Strength);
		SetStat(StatDEX, ModDEX, _currentStats.Dexterity);
		SetStat(StatCON, ModCON, _currentStats.Constitution);
		SetStat(StatINT, ModINT, _currentStats.Intelligence);
		SetStat(StatWIS, ModWIS, _currentStats.Wisdom);
		SetStat(StatCHA, ModCHA, _currentStats.Charisma);

		StatHP.Text = _currentStats.HP.ToString();
		StatAC.Text = _currentStats.AC.ToString();
		StatInit.Text = _currentStats.Initiative;
		DerivedStats.IsVisible = true;

		DiceResultLabel.Text = "Stats forged — values of 16+ are your strengths";
		RollAllBtn.Text = "◈  Reroll Stats  ◈";
		RollAllBtn.IsEnabled = true;
	}

	private void SetStat(Label valueLabel, Label modLabel, int score)
	{
		valueLabel.Text = score.ToString();
		valueLabel.TextColor = score >= 16
			? Color.FromArgb("#C8DEB4")
			: Color.FromArgb("#D4C5A9");

		int mod = (int)Math.Floor((score - 10) / 2.0);
		modLabel.Text = mod >= 0 ? $"+{mod}" : $"{mod}";
		modLabel.TextColor = mod > 0
			? Color.FromArgb("#8FA87C")
			: mod < 0 ? Color.FromArgb("#8B5A5A")
			: Color.FromArgb("#7A8E6E");
	}

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnGenerateClicked(object sender, EventArgs e)
	{
		string name = NameEntry.Text;
		string race = RacePicker.SelectedItem?.ToString();
		string heroClass = ClassPicker.SelectedItem?.ToString();
		string world = WorldPicker.SelectedItem?.ToString();
		string custom = CustomPrompt.Text;
		string alignment = AlignmentPicker.SelectedItem?.ToString();

		if (string.IsNullOrWhiteSpace(name) ||
			race == null ||
			heroClass == null ||
			world == null || alignment == null)
		{
			await DisplayAlert("Error", "Please fill all hero fields.", "OK");
			return;
		}

		StoryEditor.Text = "✨ Generating magical story...";

		string statsBlock = _currentStats != null
			? $"STR {_currentStats.Strength}, DEX {_currentStats.Dexterity}, " +
			  $"CON {_currentStats.Constitution}, INT {_currentStats.Intelligence}, " +
			  $"WIS {_currentStats.Wisdom}, CHA {_currentStats.Charisma}"
			: "stats not rolled";

		try
		{
			string prompt = $"""
            You are a creative fantasy RPG storyteller.
            Create a short magical adventure.
            Hero Name: {name}
            Race: {race}
            Class: {heroClass}
            World: {world}
            Hero Stats: {statsBlock}
            Extra description: {custom}
            Alignment: {alignment}

            Let the stats subtly influence the story:
            - High STR (15+): the hero is physically dominant
            - High DEX (15+): the hero is agile and quick
            - High INT (15+): the hero is clever and strategic
            - High WIS (15+): the hero is perceptive and wise
            - High CHA (15+): the hero is persuasive and charming
            - Low stats (8 or below) can be a weakness or flaw in the story

            Let alignment influence the hero's behavior:
            - Lawful: follows rules, honor, discipline
            - Chaotic: unpredictable, rebellious
            - Good: compassionate, heroic
            - Evil: selfish, manipulative, cruel
            - Neutral: balanced or indifferent

            Include:
            - A short story
            - One quest
            - One dialogue
            """;

			var story = await gemini.GenerateStory(prompt);

			if (story.Contains("Gemini error") || string.IsNullOrWhiteSpace(story))
			{
				story = $"""
				Hero {name}, a {race} {heroClass}, begins an epic journey in {world}.

				Quest:
				Defeat a dark creature threatening the land.

				Dialogue:
				"I will not fail," said {name}, gripping their weapon tightly.
				""";
			}

			StoryEditor.Text = story;

			var imagePrompt = $"""
							Epic fantasy character portrait, ultra detailed, cinematic.

							MAIN HERO:
							{name}, a {race} {heroClass}, alignment: {alignment}

							APPEARANCE:
							{custom}

							VISUAL DETAILS:
							- Highly detailed face, sharp features
							- Realistic anatomy and proportions
							- Expressive eyes, dramatic emotion
							- Detailed armor or clothing matching class
							- Magical effects depending on class (glow, aura, particles)

							SCENE:
							Environment from {world}, rich atmosphere, depth, fog, lighting

							STYLE:
							- Epic fantasy concept art
							- Cinematic lighting, volumetric light
							- Depth of field, sharp focus
							- 4k, ultra detailed, masterpiece quality
							- Artstation quality, AAA game character design

							IMPORTANT:
							- Show full or half body hero
							- Include all elements from description
							- No blurry image, no low quality, no distortion
							""";

			var imageBase64 = await imageService.GenerateCharacterImage(imagePrompt, world);

			if (imageBase64 != null)
			{
				byte[] imageBytes = Convert.FromBase64String(imageBase64);
				CharacterImage.Source =
					ImageSource.FromStream(() => new MemoryStream(imageBytes));
				CharacterImage.IsVisible = true;
			}
			else
			{
				await DisplayAlert("Image error", "Image could not be generated.", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void OnDescribeHeroClicked(object sender, EventArgs e)
	{
		if (_currentStats == null)
		{
			await DisplayAlert("Error", "Roll stats first!", "OK");
			return;
		}

		string name = NameEntry.Text;
		string race = RacePicker.SelectedItem?.ToString();
		string heroClass = ClassPicker.SelectedItem?.ToString();
		string alignment = AlignmentPicker.SelectedItem?.ToString();

		string statsBlock =
			$"STR {_currentStats.Strength}, DEX {_currentStats.Dexterity}, " +
			$"CON {_currentStats.Constitution}, INT {_currentStats.Intelligence}, " +
			$"WIS {_currentStats.Wisdom}, CHA {_currentStats.Charisma}";

		DescriptionEditor.Text = "✨ Summoning hero description...";

		string prompt = $"""
						You are a fantasy RPG narrator.

						Describe this hero in 3-5 cinematic sentences:

						Name: {name}
						Race: {race}
						Class: {heroClass}
						Stats: {statsBlock}
						Alignment: {alignment}

						Focus on personality, strengths and weaknesses.
						Make it feel epic and atmospheric.
						""";

		try
		{
			var description = await gemini.GenerateStory(prompt);

			if (description.Contains("error") || string.IsNullOrWhiteSpace(description))
			{
				description =
					$"{name} is a {race} {heroClass}, forged by fate. " +
					$"With strengths in battle and hidden weaknesses, they walk a dangerous path.";
			}

			DescriptionEditor.Text = description;
		}
		catch (Exception ex)
		{
			DescriptionEditor.Text = "Failed to generate description.";
		}
	}
}