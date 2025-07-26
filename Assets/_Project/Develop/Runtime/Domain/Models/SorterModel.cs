using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Data.Configs;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class SorterModel
    {
        private readonly Dictionary<FigureType, SlotData> _figuresColors;

        public SorterModel(GameConfig gameConfig)
        {
            _figuresColors = gameConfig.Figures.ToDictionary(f => f.Type,
                f => new SlotData(f.Sprite, f.Color, f.FrameSprite));
        }

        public SlotData GetSlotSprites(FigureType type)
        {
            return _figuresColors[type];
        }
    }

    public class SlotData
    {
        public Sprite Sprite;
        public Color SpriteColor;
        public Sprite FrameSprite;

        public SlotData(Sprite sprite, Color spriteColor, Sprite frameSprite)
        {
            Sprite = sprite;
            SpriteColor = spriteColor;
            FrameSprite = frameSprite;
        }
    }
}