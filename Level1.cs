using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace A_BUS_FOR_THE_MOON
{

    public enum LevelState
    {
        Menu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10
    }
    public class Level1
    {

        // Game States

        public LevelState CurrentLevelState { get; set; } = LevelState.Level1;
        //THE MOON ARRIVAL
        public Texture2D MyMoon;
        public Vector2 MoonPosition;
        float MoonSpeed;



        public Texture2D MyBorder;
        public Texture2D MyLines;
        public Texture2D MyBorder2;
        public Texture2D MyBorder3;
        public Texture2D MyMagneticDiscSpeedText;
        // DOCK & STATIONS

        public Texture2D BigStationLeft;
        public Texture2D BigStationRight;
        public Texture2D BigStationDockLeft;
        public Texture2D BigStationDockRight;

        public Texture2D CentralStation;
        public Texture2D CentralStationDockLeft;
        public Texture2D CentralStationDockRight;

        public Texture2D SmallStationLeftOrRight;
        public Texture2D SmallStationDockLeftOrRight;

        public Texture2D DockDangerousLeftOrRight;

        public List<Assets> DocksAndStations = new List<Assets>();

        public bool DrawStation = false;
        public bool StationIsDrawn = false;

        // Bus variables
        public Texture2D MyBus;
        public Texture2D BusExplosion;

        public Vector2 BusPosition ;
        public float BusTargetX;
        public const int BusMinX = 576;
        public const int BusMaxX = 768;
        public List<Assets> BusExplosions = new List<Assets>();

        public bool IsBusExploding = false;
        public bool BusDestroyed = false;
        public Rectangle BusRect;


        // Affichages
        public int DistanceToMoon;
        private int FontSize = 25;


        // Lines

        public List<Assets> Lines = new List<Assets>();
        public float LinesSpeed;
        public float BordersSpeed;

        // MagneticDisc variables

        public Texture2D MyMagneticDiscText;
        public Texture2D MyMagneticDiscCollisionText;
        public List <Assets>MagneticDisc = new List<Assets>();
        public List <Assets>MagneticDiscUsed = new List<Assets>();
        public List <Assets>MagneticCollisions = new List<Assets>();
        public List <Assets>Borders = new List<Assets>();
        public float MagneticDiscCountDown;
        public float NewMagnetDiscCountDown;
        public float MagneticDiscSpeed;
        public int Col;
        public Random DiscRandom1;
        public Random DiscRandom2;
        public Random DiscRandom3;
        public Random DiscRandom4;
        public Random DiscRandom5;

        // Collisions


        public int IsCollidingDiscCount;
        public bool IsCollidingDisc;

        private Font FontInfos = LoadFont("assets/fonts/PixelOperatorSC-Bold.ttf");
        public Level1()
        {
            Col = 64;

            //Text

            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789éèàùçâêîôûëïüöÉÈÀÙÇÂÊÎÔÛËÏÜÖ' :-!?";
            int[] codepoints = chars.Select(c => (int)c).ToArray();
            FontInfos = LoadFontEx("assets/fonts/PixelOperatorSC-Bold.ttf", 70, codepoints, codepoints.Length);

            // Bordures & BackGround & Lines
            MyBorder = LoadTexture("assets/Border.png");
            MyBorder2 = LoadTexture("assets/Border2.png");
            MyBorder3 = LoadTexture("assets/Border3.png");
            MyLines = LoadTexture("assets/Line.png");

            Assets MyLine1 = new Assets("Line1", MyLines, 509, -720, 0, 0, 1, 0, false, false, false);
            Assets MyLine2 = new Assets("Line2", MyLines, 573, -720, 0, 0, 1, 0, false, false, false);
            Assets MyLine3 = new Assets("Line3", MyLines, 637, -720, 0, 0, 1, 0, false, false, false);
            Assets MyLine4 = new Assets("Line4", MyLines, 701, - 720, 0, 0, 1, 0, false, false, false);
            Assets MyLine5 = new Assets("Line5", MyLines, 765, -720, 0, 0, 1, 0, false, false, false);

            Lines.Add(MyLine1);
            Lines.Add(MyLine2);
            Lines.Add(MyLine3);
            Lines.Add(MyLine4);
            Lines.Add(MyLine5);

            Assets Border2 = new Assets("Border2", MyBorder2, 385, -720, 0, 0, 1, 0, false, false, false);
            Borders.Add(Border2);
            Assets Border3 = new Assets("Border3", MyBorder3, 831, -720, 0, 0, 1, 0, false, false, false);
            Borders.Add(Border3);

            BordersSpeed = 8;
            LinesSpeed = 10;


            // Bus variables
            MyBus = LoadTexture("assets/MyBus.png");
            BusExplosion = LoadTexture("assets/BusExplosion.png");
            BusPosition = new Vector2(512, 600);

            // MagneticDisc variables
            MyMagneticDiscText = LoadTexture("assets/MagneticDisc.png");
            MyMagneticDiscCollisionText = LoadTexture("assets/MagneticDiscColl.png");
            MyMagneticDiscSpeedText = LoadTexture("assets/MagneticDiscSpeedUp.png");

            MagneticDiscSpeed = 6;
            MagneticDiscCountDown = 25;
            NewMagnetDiscCountDown = 25;
            BordersSpeed = MagneticDiscSpeed;
            LinesSpeed = 8;

            IsCollidingDiscCount = 0;
            BusTargetX = BusPosition.X;
            DiscRandom1 = new Random();
            DiscRandom2 = new Random();
            DiscRandom3 = new Random();
            DiscRandom4 = new Random();
            DiscRandom5 = new Random();

            //Docks & Stations
            BigStationLeft = LoadTexture("assets/BigStationLeft.png");
            BigStationRight = LoadTexture("assets/BigStationRight.png");
            BigStationDockLeft = LoadTexture("assets/BigStationDockLeft.png");
            BigStationDockRight = LoadTexture("assets/BigStationDockRight.png");

            CentralStation = LoadTexture("assets/CentralStation.png");
            CentralStationDockLeft = LoadTexture("assets/CentralStationDockLeft.png");
            CentralStationDockRight = LoadTexture("assets/CentralStationDockRight.png");

            SmallStationLeftOrRight = LoadTexture("assets/SmallStationLeftOrRight.png");
            SmallStationDockLeftOrRight = LoadTexture("assets/SmallStationDockLeftOrRight.png");

            DockDangerousLeftOrRight = LoadTexture("assets/DockDangerousLeftOrRight.png");

            //Moon Variables
            MyMoon = LoadTexture("assets/Moon2.png");
            MoonPosition = new Vector2(-1860,-5200);
            MoonSpeed = MagneticDiscSpeed / 2;

            DistanceToMoon = 380000;

        }

        public void Update()
        {
            float DeltaTime = GetFrameTime();


            switch (CurrentLevelState)
            {
                case LevelState.Menu:
                    // Handle menu logic
                    break;
                case LevelState.Level1:

                    BusRect = new Rectangle(BusPosition.X - MyBus.Width / 2, BusPosition.Y, MyBus.Width, MyBus.Height);


                    //Apparition MAgneticDisc
                    int MyDiscRandom1 = DiscRandom1.Next(1, 100);
                    int MyDiscRandom2 = DiscRandom2.Next(1, 100);
                    int MyDiscRandom3 = DiscRandom3.Next(1, 100);
                    int MyDiscRandom4 = DiscRandom4.Next(1, 100);
                    int MyDiscRandom5 = DiscRandom5.Next(1, 100);

                    if (!BusDestroyed)
                    {
                        MagneticDiscCountDown--;
                        if (MagneticDiscCountDown <= 0)
                        {
                            if (MyDiscRandom1 <= 80)
                            {
                                Assets MyMagnetic = new Assets("MagneticDisc", MyMagneticDiscText, 480, -50, 0, MagneticDiscSpeed, 6, 500, false, false, false);
                                MagneticDisc.Add(MyMagnetic);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                                DistanceToMoon = DistanceToMoon - 1000;
                            }
                            if (MyDiscRandom1 > 80)
                            {
                                Assets MagneticDiscSpeed1 = new Assets("MagneticDiscSpeed", MyMagneticDiscSpeedText, 480, -50, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                                MagneticDisc.Add(MagneticDiscSpeed1);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                                DistanceToMoon = DistanceToMoon - 1000;
                            }
                            if (MyDiscRandom2 <= 80)
                            {

                                Assets MyMagnetic002 = new Assets("MagneticDisc002", MyMagneticDiscText, 544, -50, 0, MagneticDiscSpeed, 6, 500, false, false, false);
                                MagneticDisc.Add(MyMagnetic002);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom2 > 80)
                            {
                                Assets MagneticDiscSpeed2 = new Assets("MagneticDiscSpeed", MyMagneticDiscSpeedText, 544, -50, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                                MagneticDisc.Add(MagneticDiscSpeed2);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom3 <= 80)
                            {
                                Assets MyMagnetic003 = new Assets("MagneticDisc003", MyMagneticDiscText, 608, -50, 0, MagneticDiscSpeed, 6, 500, false, false, false);
                                MagneticDisc.Add(MyMagnetic003);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom3 > 80)
                            {
                                Assets MagneticDiscSpeed3 = new Assets("MagneticDiscSpeed", MyMagneticDiscSpeedText, 608, -50, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                                MagneticDisc.Add(MagneticDiscSpeed3); ;
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom4 <= 80)
                            {
                                Assets MyMagnetic004 = new Assets("MagneticDisc004", MyMagneticDiscText, 672, -50, 0, MagneticDiscSpeed, 6, 500, false, false, false);
                                MagneticDisc.Add(MyMagnetic004);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom4 > 80)
                            {
                                Assets MagneticDiscSpeed4 = new Assets("MagneticDiscSpeed", MyMagneticDiscSpeedText, 672, -50, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                                MagneticDisc.Add(MagneticDiscSpeed4);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom5 <= 80)
                            {
                                Assets MyMagnetic005 = new Assets("MagneticDisc005", MyMagneticDiscText, 736, -50, 0, MagneticDiscSpeed, 6, 500, false, false, false);
                                MagneticDisc.Add(MyMagnetic005);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                            if (MyDiscRandom5 > 80)
                            {
                                Assets MagneticDiscSpeed5 = new Assets("MagneticDiscSpeed", MyMagneticDiscSpeedText, 736, -50, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                                MagneticDisc.Add(MagneticDiscSpeed5);
                                MagneticDiscCountDown = NewMagnetDiscCountDown;
                            }
                        }
                    }


                    foreach (Assets myDisc in MagneticDisc.ToList())
                    {

                        // Vitesse de déplacement du MagneticDisc

                        myDisc.AssetY += MagneticDiscSpeed;

                        if (myDisc.AssetY > 720)
                        {
                            MagneticDisc.Remove(myDisc);
                        }

                        // Animation MagneticDisc

                        myDisc.AssetFrameTimer -= 200 * DeltaTime;
                        if (myDisc.AssetFrameTimer <= 0)
                        {
                            myDisc.AssetCurrentFrame++;
                            if (myDisc.AssetCurrentFrame >= myDisc.AssetFrameCount)
                            {
                                myDisc.AssetCurrentFrame = 0;
                            }
                            myDisc.AssetFrameTimer = myDisc.AssetNewFrameTimer;


                        }
                        myDisc.AssetSourceRec = new Rectangle(myDisc.AssetCurrentFrame * myDisc.AssetFrameWidth, 0, myDisc.AssetFrameWidth, myDisc.AssetFrameHeight);


                        /*//Collisions MagneticDisc & Bus                           

                        if (CheckCollisionRecs(myDisc.AssetRect, BusRect))
                        {
                            Assets MyMagneticCollision = new Assets("MagneticDiscCollision", MyMagneticDiscCollisionText, (int)BusPosition.X, (int)BusPosition.Y + 50, 0, 5, 8, 6, false, false, false);
                            MagneticCollisions.Add(MyMagneticCollision);
                            if (myDisc.AssetName == "MagneticDiscSpeed")
                            {
                                MagneticDiscSpeed = MagneticDiscSpeed + (MagneticDiscSpeed * DeltaTime);
                                NewMagnetDiscCountDown = NewMagnetDiscCountDown - (NewMagnetDiscCountDown * DeltaTime);
                                BordersSpeed = BordersSpeed + (BordersSpeed * DeltaTime);
                                LinesSpeed = LinesSpeed + (LinesSpeed * DeltaTime);
                            }
                            Assets MyMagneticUsed = new Assets("MagneticDiscUsed", MyMagneticDiscText, (int)myDisc.AssetX, (int)myDisc.AssetY + 45, 0, MagneticDiscSpeed, 6, 10, false, false, false);
                            MagneticDiscUsed.Add(MyMagneticUsed);
                            MagneticDisc.Remove(myDisc);
                        }*/
                    }

                    foreach (Assets myDiscColl in MagneticCollisions.ToList())
                    {

                        // Animation MagneticDisc

                        myDiscColl.AssetFrameTimer -= 200 * DeltaTime;
                        if (myDiscColl.AssetFrameTimer <= 0)
                        {
                            myDiscColl.AssetCurrentFrame++;
                            if (myDiscColl.AssetCurrentFrame >= myDiscColl.AssetFrameCount)
                            {
                                MagneticCollisions.Remove(myDiscColl);
                            }
                            myDiscColl.AssetFrameTimer = myDiscColl.AssetNewFrameTimer;


                        }
                        myDiscColl.AssetSourceRec = new Rectangle(myDiscColl.AssetCurrentFrame * myDiscColl.AssetFrameWidth, 0, myDiscColl.AssetFrameWidth, myDiscColl.AssetFrameHeight);

                        // Vitesse de déplacement du MagneticDisc

                        myDiscColl.AssetY += myDiscColl.AssetSpeed;

                    }

                    foreach (Assets MyDiscUsed in MagneticDiscUsed.ToList())
                    {
                        MyDiscUsed.AssetY += MagneticDiscSpeed;

                        if (MyDiscUsed.AssetY > 750)
                        {
                            MagneticDiscUsed.Remove(MyDiscUsed);
                        }

                        MyDiscUsed.AssetSourceRec = new Rectangle(MyDiscUsed.AssetCurrentFrame * MyDiscUsed.AssetFrameWidth, 0, MyDiscUsed.AssetFrameWidth, MyDiscUsed.AssetFrameHeight);

                    }

                    //Contrôle du bus 

                    if (IsKeyPressed(KeyboardKey.Right) && BusTargetX < BusMaxX)
                        BusTargetX += Col;

                    if (IsKeyPressed(KeyboardKey.Left) && BusTargetX >= BusMinX)
                        BusTargetX -= Col;

                    // déplacement fluide vers la cible, valable pour les deux sens
                    if (BusPosition.X < BusTargetX)
                    {
                        BusPosition.X += 9;
                        if (BusPosition.X > BusTargetX) BusPosition.X = BusTargetX;
                    }
                    else if (BusPosition.X > BusTargetX)
                    {
                        BusPosition.X -= 9;
                        if (BusPosition.X < BusTargetX) BusPosition.X = BusTargetX;
                    }



                    // Déplacement des Lines & Borders

                    foreach (Assets line in Lines)
                    {

                        line.AssetY += LinesSpeed;
                        if (line.AssetY > 0)
                        {
                            line.AssetY = -720;
                        }
                    }

                    foreach (Assets border in Borders)
                    {
                        border.AssetY += BordersSpeed;
                        if (border.AssetY > -1)
                        {
                            border.AssetY = -720;
                        }
                    }

                    // Docks & Stations

                    if (DistanceToMoon == 350000 || DistanceToMoon == 200000 || DistanceToMoon == 150000 || DistanceToMoon == 30000)
                        DrawStation = true;
                    StationIsDrawn = true;

                    if (DrawStation)
                    {
                        Assets MyCentralStation = new Assets("CentralStation", CentralStation, 544, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyCentralStation);

                        Assets MyCentralDockStationLeft = new Assets("CentralDockStationLeft", CentralStationDockLeft, 448, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyCentralDockStationLeft);

                        Assets MyCentralDockStationRight = new Assets("CentralDockStationRight", CentralStationDockRight, 800, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyCentralDockStationRight);

                        DrawStation = false;
                    }

                    if (DistanceToMoon == 250000 || DistanceToMoon == 100000)
                        DrawStation = true;

                    if (DrawStation)
                    {
                        Assets MyBigStationLeft = new Assets("MyBigStationLeft", BigStationLeft, 448, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyBigStationLeft);

                        Assets MyBigStationDockRight = new Assets("MyBigStationDockRight", BigStationDockRight, 736, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyBigStationDockRight);

                        DrawStation = false;
                    }

                    if (DistanceToMoon == 320000 || DistanceToMoon == 180000)
                        DrawStation = true;

                    if (DrawStation)
                    {
                        Assets MyBigStationRight = new Assets("MyBigStationRight", BigStationRight, 608, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyBigStationRight);

                        Assets MyBigStationDockLeft = new Assets("MyBigStationDockLeft", BigStationDockLeft, 448, -500, 0, MagneticDiscSpeed, 1, 1, false, false, false);
                        DocksAndStations.Add(MyBigStationDockLeft);

                        DrawStation = false;
                    }

                    foreach (Assets MyDockOrStation in DocksAndStations.ToList())
                    {
                        MyDockOrStation.AssetY += MagneticDiscSpeed;
                        if (MyDockOrStation.AssetY > 720)
                        {
                            DocksAndStations.Remove(MyDockOrStation);
                        }

                        /*//Collisions

                        Rectangle discRect = new Rectangle(MyDockOrStation.AssetX, MyDockOrStation.AssetY, MyDockOrStation.AssetFrameWidth, MyDockOrStation.AssetFrameHeight);

                        (CheckCollisionRecs(MyDockOrStation.AssetRect, BusRect) && !BusDestroyed)
                        {
                            IsBusExploding = true;
                            BusDestroyed = true;
                            /*MagneticDiscSpeed = 0;
                            BordersSpeed = 0;
                            LinesSpeed = 0;
                        }*/
                    }

                    if (IsBusExploding)
                    {
                        Assets MyBusExplosion = new Assets("BusExplosion", BusExplosion, (int)BusPosition.X, (int)BusPosition.Y, 0, 0, 13, 6, false, false, false);
                        BusExplosions.Add(MyBusExplosion);
                        IsBusExploding = false;
                    }

                    foreach (Assets MyBusExplosion in BusExplosions.ToList())
                    {
                        // Animation BusExplosion
                        MyBusExplosion.AssetFrameTimer -= 100 * DeltaTime;
                        if (MyBusExplosion.AssetFrameTimer <= 0)
                        {
                            MyBusExplosion.AssetCurrentFrame++;
                            if (MyBusExplosion.AssetCurrentFrame >= MyBusExplosion.AssetFrameCount - 1)
                            {
                                BusExplosions.Remove(MyBusExplosion);
                            }
                            MyBusExplosion.AssetFrameTimer = MyBusExplosion.AssetNewFrameTimer;
                        }
                        MyBusExplosion.AssetSourceRec = new Rectangle(MyBusExplosion.AssetCurrentFrame * MyBusExplosion.AssetFrameWidth, 0, MyBusExplosion.AssetFrameWidth, MyBusExplosion.AssetFrameHeight);
                    }

                    //THE MOON ARRIVAL

                    if(DistanceToMoon <= 350000)
                    {
                        MoonSpeed = MoonSpeed + (MoonSpeed *DeltaTime) /15;
                        MoonPosition.Y += MoonSpeed;

                        if (MoonSpeed >= MagneticDiscSpeed)
                        {
                            MoonSpeed = MagneticDiscSpeed;
                        }                       
                    }
                    break;

            
            
                case LevelState.Level2:
                    // Handle level 2 logic
                    break;
                case LevelState.Level3:
                    // Handle level 3 logic
                    break;
                case LevelState.Level4:
                    // Handle level 4 logic
                    break;
                case LevelState.Level5:
                    // Handle level 5 logic
                    break;
                case LevelState.Level6:
                    // Handle level 6 logic
                    break;
                case LevelState.Level7:
                    // Handle level 7 logic
                    break;
                case LevelState.Level8:
                    // Handle level 8 logic
                    break;
                case LevelState.Level9:
                    // Handle level 9 logic
                    break;
                case LevelState.Level10:
                    // Handle level 10 logic
                    break;
            }

        }
        public void Draw()
        {
            // Lines 
            /*foreach (Assets MyLine in Lines)
            {
                DrawTexture(MyLine.AssetTileSet, (int)MyLine.AssetX, (int)MyLine.AssetY, Color.White);
            }*/
                    
            switch (CurrentLevelState)
            {
                case LevelState.Menu:
                    // Handle menu drawing
                    break;
                case LevelState.Level1:

                    DrawTexture(MyMoon, (int)MoonPosition.X, (int)MoonPosition.Y, Color.White);

                    foreach (Assets myDisc in MagneticDisc)
                    {
                        //DrawTextureRec(MyMagneticDisc, MagneticDiscAnimationSourceRect, MagneticDiscPosition, Color.White);
                        DrawTexturePro(myDisc.AssetTileSet, myDisc.AssetSourceRec, new Rectangle(myDisc.AssetX, myDisc.AssetY, myDisc.AssetFrameWidth, myDisc.AssetFrameHeight), new Vector2(0, 0), 0, Color.White);
                        DrawRectangleLinesEx(myDisc.AssetRect, 2, Color.Red);
                    }
                    foreach (Assets myDiscColl in MagneticCollisions)
                    {
                        //DrawTextureRec(MyMagneticDisc, MagneticDiscAnimationSourceRect, MagneticDiscPosition, Color.White);
                        DrawTexturePro(myDiscColl.AssetTileSet, myDiscColl.AssetSourceRec, new Rectangle(myDiscColl.AssetX, myDiscColl.AssetY, myDiscColl.AssetFrameWidth, myDiscColl.AssetFrameHeight), new Vector2(myDiscColl.AssetFrameWidth / 2, myDiscColl.AssetFrameHeight), 0, Color.White);
                    }
                    foreach (Assets myDiscUsed in MagneticDiscUsed)
                    {
                        //DrawTextureRec(MyMagneticDisc, MagneticDiscAnimationSourceRect, MagneticDiscPosition, Color.White);
                        DrawTexturePro(myDiscUsed.AssetTileSet, myDiscUsed.AssetSourceRec, new Rectangle(myDiscUsed.AssetX, myDiscUsed.AssetY, myDiscUsed.AssetFrameWidth, myDiscUsed.AssetFrameHeight), new Vector2(0, myDiscUsed.AssetFrameHeight), 0, Color.White);
                    }

                    if (!BusDestroyed)
                        DrawTexturePro(MyBus, new Rectangle(0, 0, MyBus.Width, MyBus.Height), new Rectangle(BusPosition.X, BusPosition.Y, MyBus.Width, MyBus.Height), new Vector2(MyBus.Width / 2, 0), 0, Color.White);

                    // Borders & Lines
                    /*DrawTexture(MyBorder, 0, 0, Color.White);
                    DrawTexture(MyBorder, 816, 0, Color.White);*/

                    foreach (Assets MyBorder in Borders)
                    {
                        DrawTexture(MyBorder.AssetTileSet, (int)MyBorder.AssetX, (int)MyBorder.AssetY, Color.White);
                    }

                    // Docks & Stations

                    foreach (Assets MyDockOrStation in DocksAndStations)
                    {
                        DrawTexture(MyDockOrStation.AssetTileSet, (int)MyDockOrStation.AssetX, (int)MyDockOrStation.AssetY, Color.White);
                        DrawRectangleLinesEx(MyDockOrStation.AssetRect, 2, Color.Red);
                    }

                    foreach (Assets explo in BusExplosions)
                    {
                        DrawTexturePro(explo.AssetTileSet, explo.AssetSourceRec, new Rectangle(explo.AssetX, explo.AssetY, explo.AssetFrameWidth, explo.AssetFrameHeight), new Vector2(explo.AssetFrameWidth / 2, 30), 0, Color.White);
                    }


                    DrawTextEx(FontInfos, "Distance to the Moon", new Vector2(950, 50), FontSize, 0, Color.White);
                    DrawTextEx(FontInfos, "" + DistanceToMoon, new Vector2(1010, 100), FontSize + 10, 0, Color.Green);

                    DrawRectangleLinesEx(BusRect, 2, Color.White);
                    break;
                case LevelState.Level2:
                    // Handle level 2 drawing
                    break;
                case LevelState.Level3:
                    // Handle level 3 drawing
                    break;
                case LevelState.Level4:
                    // Handle level 4 drawing
                    break;
                case LevelState.Level5:
                    // Handle level 5 drawing
                    break;
                case LevelState.Level6:
                    // Handle level 6 drawing
                    break;
                case LevelState.Level7:
                    // Handle level 7 drawing
                    break;
                case LevelState.Level8:
                    // Handle level 8 drawing
                    break;
                case LevelState.Level9:
                    // Handle level 9 drawing
                    break;
                case LevelState.Level10:
                    // Handle level 10 drawing
                    break;
            }
            

        }
        public void Close()
        {
            // Close logic for Level 1
        }
    }
}
