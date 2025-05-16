// Assets/Scripts/Commands/ICommand.cs
namespace SolitaireGame.Commands
{
    /// <summary>
    /// Interface for commands supporting Execute and Undo.
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
