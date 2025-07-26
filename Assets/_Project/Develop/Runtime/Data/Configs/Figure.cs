using _Project.Develop.Runtime.Core.Enums;
using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [System.Serializable]
    public class Figure
    {
        public FigureType Type;
        public Sprite Sprite;
        public Sprite FrameSprite;
        public Color Color = Color.white;
    }
}