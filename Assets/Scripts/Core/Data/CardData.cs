// Assets/Core/Data/CardData.cs
using SolitaireGame.Core;
using UnityEngine;

[CreateAssetMenu(menuName = "Solitaire/Card Data")]
public class CardData : ScriptableObject
{
    public Sprite FaceSprite;
    public Sprite BackSprite;
    public Card.SuitType Suit;
    public Card.RankType Rank;
}