// Assets/Scripts/Infrastructure/CommandBus.cs
using System.Collections.Generic;
using System;
using SolitaireGame.Commands;


namespace SolitaireGame.Infrastructure
{
    public class CommandBus
    {
        private static CommandBus _instance;
        public static CommandBus Instance => _instance ??= new CommandBus();

        private readonly Stack<ICommand> _undo = new Stack<ICommand>();
        private readonly Stack<ICommand> _redo = new Stack<ICommand>();

        public event Action<bool, bool> OnStateChanged;

        
        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            _undo.Push(cmd);
            _redo.Clear();
            OnStateChanged?.Invoke(_undo.Count > 0, _redo.Count > 0);
        }

        public void Undo()
        {
            if (_undo.Count == 0) return;
            var cmd = _undo.Pop(); cmd.Undo(); _redo.Push(cmd);
            OnStateChanged?.Invoke(_undo.Count > 0, _redo.Count > 0);
        }

        public void Redo()
        {
            if (_redo.Count == 0) return;
            var cmd = _redo.Pop(); cmd.Execute(); _undo.Push(cmd);
            OnStateChanged?.Invoke(_undo.Count > 0, _redo.Count > 0);
        }

        public bool CanUndo => _undo.Count > 0;
        public bool CanRedo => _redo.Count > 0;
    }
}
