Description

This repository contains the source code and case study for a Solitaire game implementation. It serves as a developer test to evaluate code quality, architecture, and adherence to best practices.

File Structure

Solitaire_Developer_Case_Study_Updated.pdf: Project brief and requirements.

Card.cs: Domain model representing a single playing card.

CardData.cs: Definitions and metadata for card values and suits.

CardFactory.cs: Factory pattern for creating card instances.

DeckManager.cs: Logic for shuffling, drawing, and managing the deck.

CardStack.cs: Represents tableau, foundation, and waste stacks.

MoveCommand.cs: Implements command pattern for moves and undo/redo.

CommandBus.cs / EventBus.cs: Messaging layers for commands and events.

CardDropService.cs / ICardDropService.cs: Abstraction and implementation for drop-zone validation.

DropZone.cs: UI component defining valid drop areas.

CardView.cs: Visual representation of a card in the UI.

CardDragHandler.cs: Handles drag-and-drop interactions.

CardAnimator.cs: Manages card movement animations.

ObjectPool.cs: Generic pool for reusing card view objects.

UndoRedoUI.cs: UI controls for undoing and redoing moves.

GameManager.cs: Orchestrates game flow and state transitions.



Planned Improvements

As part of the next iteration, we aim to enhance the project in the following areas:

Modular Architecture: Restructure folders by feature (Core, UI, Services, Animations) to enforce separation of concerns.

Dependency Injection: Integrate a DI framework (e.g., Zenject) to manage service and controller lifetimes and facilitate testing.

Automated Testing: Add a separate test suite using NUnit or xUnit covering:

DeckManager shuffle and draw logic

MoveCommand execution and undo/redo behavior

CardStack rule validations

Event & Command Handling: Migrate to a robust event aggregator (e.g., Reactive Extensions) to improve decoupling and performance.

Performance Optimization: Make animations and asset loading asynchronous; profile ObjectPool usage to reduce garbage collection spikes.

Configuration via ScriptableObjects: Expose card metadata and animation settings to ScriptableObjects for designer-driven adjustments without code changes.

Logging & Diagnostics: Implement a logging framework to capture runtime events, errors, and performance metrics.

Coding Standards: Enforce StyleCop or EditorConfig rules and add XML documentation for all public APIs.

CI/CD Pipeline: Set up continuous integration (GitHub Actions or Azure DevOps) for automated builds, tests, and code quality checks.

UX Refinements: Improve drag-and-drop snapping logic and visual feedback for different screen resolutions.


AI-Driven Development Prompts

The following high-quality prompts were used to collaborate with ChatGPT in generating and refining core gameplay scripts. Each prompt follows prompt engineering best practices, providing context, roles, and explicit instructions.

CardStack.cs – Move Logic

System: You are a Unity C# expert with deep knowledge of Solitaire rules.
User: Implement a `MoveCards` method in `CardStack.cs` that moves one or more cards between tableau, foundation, and waste piles. Ensure color alternation and descending order are enforced, and return a boolean indicating success.

MoveCommand.cs & ICommand.cs – Command Pattern

System: You are a software architect skilled in the Command design pattern.
User: Define the `ICommand` interface and implement a `MoveCommand` class with `Execute()` and `Undo()` methods. Wire them into `CommandBus.cs` so that commands can be dispatched and undone/redone reliably.

DropZone.cs & CardDropService.cs – Validation Service

System: Act as an API designer for Unity game services.
User: In `CardDropService.cs`, create a method `IsValidDrop(Card card, DropZone zone)` that returns true only if the card fits the zone’s rules (e.g., alternating colors, correct sequence). Use `ICardDropService` for abstraction.


