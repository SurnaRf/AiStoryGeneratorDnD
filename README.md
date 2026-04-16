# AI Story Generator 

## Overview

**AI Story Generator** is a mobile application built with **.NET MAUI**, which allows users to create unique fantasy heroes and generate dynamic stories using artificial intelligence.

The app combines:

* Character creation (name, race, class, alignment, world)
* RPG-style stat rolling system (D&D inspired)
* AI-generated stories (via Gemini API)
* AI-generated character portraits
* Cinematic hero descriptions

Each generated story is unique and influenced by the hero’s attributes, stats, and alignment.

---

## Features

### Hero creation

Users can customize:

* Name
* Race (Elf, Human, Orc, etc.)
* Class (Mage, Ranger, Paladin, etc.)
* Alignment (Lawful Good → Chaotic Evil)
* World (Fantasy settings)
* Custom lore input

---

### RPG Stats system

* Dice rolling (d4, d6, d8, d10, d12, d20)
* Automatic stat generation (4d6 drop lowest)
* Stats:

  * STR (Strength)
  * DEX (Dexterity)
  * CON (Constitution)
  * INT (Intelligence)
  * WIS (Wisdom)
  * CHA (Charisma)
* Derived stats:

  * HP (Health)
  * AC (Armor Class)
  * Initiative

Stats directly influence the generated story.

---

### AI story generation

The app uses **prompt engineering** to guide the AI.

The prompt includes:

* Hero attributes
* Generated stats
* Alignment behavior rules
* Custom user input
  
---

### AI image generation

* Generates a fantasy portrait of the hero
* Includes:

  * Visual interpretation of custom description
  * Environment based on selected world
  * Companions/creatures if described

---

### Hero description generator

* Creates a short cinematic description 
* Focuses on:

  * Personality
  * Strengths & weaknesses
  * Atmosphere

---

## Technologies used

* **.NET MAUI** – Cross-platform mobile development
* **C#** – Application logic
* **XAML** – UI design
* **Gemini API** – AI text generation
* **Stability API** – AI image generation

---


## Project structure

```
AiStoryGenerator/
│
├── Models/
│   └── HeroStats.cs
│
├── Services/
│   ├── GeminiService.cs
│   ├── ImageService.cs
│   └── DiceService.cs
│
├── Views/
│   ├── MainPage.xaml
│   └── MainPage.xaml.cs
│
└── Resources/
```

---

## How to run

1. Open the project in **Visual Studio**
2. Restore NuGet packages
3. Add your API keys (Gemini / Stability API) in the Services
4. Build and run on:

   * Android emulator/device
   * Windows (if configured)

---

## Screenshots

<img width="945" height="443" alt="image" src="https://github.com/user-attachments/assets/5e7a4c81-3bb6-4891-93d0-65e41e6abc37" />
<img width="945" height="171" alt="image" src="https://github.com/user-attachments/assets/2a75a241-5129-40d0-99e8-627d42cfdc81" />
<img width="945" height="144" alt="image" src="https://github.com/user-attachments/assets/e53594fc-1b43-4959-a43d-4864e7a84ff4" />
<img width="945" height="468" alt="image" src="https://github.com/user-attachments/assets/469c88ce-4f9a-4b0b-8535-e66bcfcd4fd4" />
<img width="945" height="473" alt="image" src="https://github.com/user-attachments/assets/68054b1c-2735-4e93-bdb8-ec6e4d26a6dd" />
<img width="470" height="696" alt="image" src="https://github.com/user-attachments/assets/dd0c6127-31fa-4be6-a3f2-ee2218c458f7" />


---

