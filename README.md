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
<img width="1919" height="960" alt="Screenshot 2026-04-16 200932" src="https://github.com/user-attachments/assets/4bab1701-3797-4664-9ae1-b4cba34888ed" />
<img width="1915" height="949" alt="Screenshot 2026-04-16 200952" src="https://github.com/user-attachments/assets/1251a2b7-1e09-486f-9d12-d9ea526b9261" />
<img width="1852" height="354" alt="Screenshot 2026-04-16 201008" src="https://github.com/user-attachments/assets/1a54598c-05a3-4ac0-af4f-9f2fe857ab96" />
<img width="1850" height="331" alt="Screenshot 2026-04-16 201020" src="https://github.com/user-attachments/assets/6e3fb478-f870-4e51-a6a9-7dc636cc031e" />
<img width="1777" height="270" alt="Screenshot 2026-04-16 201035" src="https://github.com/user-attachments/assets/0a8c698f-8c76-48ee-821f-bf24780cba72" />
<img width="1846" height="334" alt="Screenshot 2026-04-16 201046" src="https://github.com/user-attachments/assets/b29ba461-67a2-404d-8a6e-457c379d9a61" />
<img width="1249" height="586" alt="Screenshot 2026-04-16 203906" src="https://github.com/user-attachments/assets/069c2c32-be66-4d06-91cf-d3edf295fd7e" />
<img width="470" height="696" alt="Screenshot 2026-04-16 221453" src="https://github.com/user-attachments/assets/845c06cd-828b-4f68-9cef-8e31bee76b7a" />


---

