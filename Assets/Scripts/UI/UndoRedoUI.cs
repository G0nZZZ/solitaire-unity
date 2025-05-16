// Assets/Scripts/UI/UndoRedoUI.cs

using SolitaireGame.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace SolitaireGame.UI
{
    public class UndoRedoUI : MonoBehaviour
    {
        [SerializeField] Button _undoBtn, _redoBtn;

        void OnEnable()
        {
            _undoBtn.onClick.AddListener(CommandBus.Instance.Undo);
            _redoBtn.onClick.AddListener(CommandBus.Instance.Redo);
            CommandBus.Instance.OnStateChanged += UpdateButtons;
            UpdateButtons(CommandBus.Instance.CanUndo, CommandBus.Instance.CanRedo);
        }
        void OnDisable() => CommandBus.Instance.OnStateChanged -= UpdateButtons;

        void UpdateButtons(bool canUndo, bool canRedo)
        {
            _undoBtn.interactable = canUndo;
            _redoBtn.interactable = canRedo;
        }
    }
}
