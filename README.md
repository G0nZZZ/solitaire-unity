Solitaire Undo Move Feature Prototype

This Unity project implements a simplified Solitaire-style card movement system with a basic Undo/Redo feature to revert the last move.

What I Built

Drag-and-Drop Card Movement: Players can drag cards between stacks (initial deck and drop zones), with visual updates and sorting order adjustments.

Undo/Redo System: Integrated a CommandBus with ICommand implementations (MoveCommand) to support undoing and redoing the most recent card move. Buttons in the UI (UndoRedoUI) automatically enable/disable based on available history.

Event Sourcing Backend (optional extension): Provided an IEventStore (FileEventStore) for persisting game events (CardMovedEvent, CardFlippedEvent, etc.) to a file, demonstrating how the game state could be reconstructed from event history.

The code is organized into clear namespaces and folders:

Core: Card, CardStack, domain models.

Commands: ICommand, MoveCommand for encapsulating actions.

Infrastructure: CommandBus and optional EventBus for decoupled command execution and event sourcing.

UI: CardView, CardDragHandler, DropZone, and UndoRedoUI for visuals and input handling.

Managers & Services: DeckManager for shuffling and initializing deck, CardFactory & ObjectPool for efficient GameObject reuse, and CardDropService for drop logic.

What I Would Improve with More Time

Full Game Rules & Win Conditions: Extend beyond simple drag-and-drop to enforce Solitaire rules (alternating colors, descending ranks, foundation piles, etc.) and detect win/loss states.

Persistent Undo History: Integrate the event store fully to reload undo history across sessions, allowing multi-level undo/redo beyond a single command.

Animations & Feedback: Add smooth card slide animations, snap-to-grid behavior, and highlight valid drop targets.

AI-Assisted Testing & Documentation: Generate unit tests for command behaviors and event sourcing, plus inline code documentation via AI tools.

Mobile-Friendly UI: Adapt touch controls, scalable layouts, and responsive UI elements for mobile devices.

AI Assistance

Throughout this prototype, I leveraged AI tools to accelerate development:

ChatGPT (OpenAI)

Used detailed system and user role prompts to elicit best practices for architecture and code clarity.

Iteratively refined JSON serialization and event sourcing code snippets with targeted follow-up prompts.

Provided context-aware boilerplate for ICommand implementations and namespace imports.

Offered inline suggestions for UI component wiring and object pooling patterns.

Example Prompt Engineering:

System: "You are an expert Unity developer and software architect."
User: "Design a modular C# command bus for Unity using the Command pattern. Include interface definitions, concrete command classes, and undo/redo logic, with inline comments explaining each step."

System: "You are a seasoned developer familiar with event sourcing."
User: "Write C# code to serialize Unity ScriptableObject events to JSON lines in a file store, and demonstrate deserialization back into event objects. Include error handling and performance considerations."

