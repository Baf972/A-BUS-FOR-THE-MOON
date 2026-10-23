using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace A_BUS_FOR_THE_MOON
{
    public class Assets
    {

        public Texture2D AssetTileSet { get; set; }
        public Rectangle AssetSourceRec { get; set; }
        public Rectangle AssetRect => new Rectangle(AssetX, AssetY, AssetFrameWidth, AssetFrameHeight);
        public float AssetX { get; set; }
        public float AssetY { get; set; }
        public float AssetR { get; set; }
        public float AssetSpeedR { get; set; }
        public float AssetSpeed { get; set; }
        public float AssetSpeedX { get; set; }
        public float AssetVelocity { get; set; }
        public int AssetWidth { get; set; }
        public int AssetHeight { get; set; }
        public int AssetFrameWidth { get; set; }
        public int AssetFrameHeight { get; set; }
        public int AssetFrameCount { get; set; }
        public float AssetFrameTimer { get; set; }
        public int AssetCurrentFrame { get; set; }
        public float AssetTimer { get; set; }
        public int AssetNewFrameTimer { get; set; }
        public bool AssetIsFlipped { get; set; }
        public bool AssetActiv { get; set; }
        public bool AssetAnimated { get; set; }
        public string? AssetName { get; set; }

        public static Random MyRandom { get; } = new Random();



        public Assets(string name, Texture2D assetTileSet, int assetX, int assetY, int assetSpeedR, float assetSpeed, int assetFrameCount, int assetNewFrameTimer, bool assetActive, bool assetAnimated, bool assetIsFlipped)
        {
            AssetName = name;
            AssetTileSet = assetTileSet;
            AssetX = assetX;
            AssetY = assetY;
            AssetSpeedR = assetSpeedR;
            AssetSpeed = assetSpeed;
            AssetFrameCount = assetFrameCount;
            AssetFrameWidth = assetTileSet.Width / assetFrameCount;
            AssetFrameHeight = assetTileSet.Height;
            AssetNewFrameTimer = assetNewFrameTimer;
            AssetIsFlipped = assetIsFlipped;
            AssetActiv = assetActive;
            AssetAnimated = assetAnimated;            
            AssetVelocity = 500;

        }



    }
}
