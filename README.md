**Key Features of the Game**

🟩 Grid-Based Area: At the start of the game, a well-organized playing area based on grids is created. Additionally, the snake is also generated when the game starts.

Snake Movement:

• 🐍 Body Management: The snake's body is managed by a list called bodyList, and the head and tail positions are updated with each movement.

• ↔️ Position Updates: Body parts simulate movement by swapping the first and last elements of the list.

🎨 Dynamic Body Sprites: Body sprites are adjusted according to the difference between previous and current directions, with appropriate corner sprites assigned at turns.

🍎 Apple Destruction and Spawn: When an apple is eaten, it disappears, and an apple-sized object spawns at the first element of the body list. The eating simulation is carried out with an animation moving through the snake's body. This animation is realized using DoScale, and the spawned object gradually shrinks and disappears.

🏆 Scoring and PlayerPrefs: Tracks the player's progress and saves high scores.
