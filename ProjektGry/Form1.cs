using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WMPLib;
using System.Diagnostics;

namespace ProjektGry
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer BackgroundMusic = new WindowsMediaPlayer();

        public bool CheatsON = false;
        Label CheatLabel = new Label();
        public int TimeOfGame = 0;
        public List<Block> Blocks;
        public List<Bonus> Bonuses;

        private ushort numberOfSpeedBonuses;
        private ushort numberOfLessReloadTimeBonuses;
        private ushort numberOfHpBonuses;
        private ushort numberOfBulletSpeedBonuses;

        public int PercentForBonus;
        public static Point T1Spawn, T2Spawn, E1Spawn, E2Spawn, E3Spawn, E4Spawn, E5Spawn, E6Spawn;
        Tank Player1, Player2;
        Enemy Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6;
        Bullet BulletMethods = new Bullet();
        PictureBox Bullet, Bullet2, EnemyBullet1, EnemyBullet2, EnemyBullet3, EnemyBullet4, EnemyBullet5, EnemyBullet6;

        Panel GameOver_Panel = new Panel();
        Label GameOver_WinLoss_Label = new Label(), GameOver_Time_Label = new Label(), GameOver_TimeValue = new Label(), GameOver_Points_Label = new Label(), GameOver_Nick1 = new Label(), GameOver_Nick2 = new Label(), GameOver_Nick1PointsValue = new Label(), GameOver_Nick2PointsValue = new Label();
        Button GameOver_Close = new Button();

        bool FriendlyFire;
        int GameMode;
        public int Diff_Level;
        bool Victory = false;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    Player1.IsMoving = false;
                    break;
                case Keys.W:
                case Keys.A:
                case Keys.D:
                case Keys.S:
                    Player2.IsMoving = false;
                    break;
                default:
                    break;
            }
        }

        public void Map1() //Zielone Pola
        {           
            T1Spawn = new Point(565, 560);
            T2Spawn = new Point(5, 0);
            E1Spawn = new Point(5, 560);
            E3Spawn = new Point(565, 0);
            E2Spawn = new Point(45, 520);
            E4Spawn = new Point(525, 40);
            E5Spawn = new Point(45, 560);
            E6Spawn = new Point(525, 0);


            //Bonusy
            PercentForBonus = 30;
            numberOfSpeedBonuses = 2;
            numberOfLessReloadTimeBonuses = 2;
            numberOfHpBonuses = 1;
            numberOfBulletSpeedBonuses = 2;

            //Bonusy
            BonusCreator(Color.Green, true, true, true, true);

            const int numberOfDestroyalbeBox = 112;
            const int numberOfUnDestroyableBox = 20;
            const int numberOfWaterBox = 7;
            const int numberOfGrassBox = 72;
            // MapNumber = 0;
            //[Ctrl] + [K], [C] ->  Zakomentuj zaznaczone
            //[Ctrl] + [K], [U] ->  Odkomentuj zaznaczone

            #region TworzenieBloczkow

            //BLOKI DO NISZCZENIA
            BlocksCreator(numberOfDestroyalbeBox, "Assets/Textures/Default_Blocks/RedBlock.gif", "Assets/Textures/BackGrounds/T_BackGreen.jpg", true, true, true, new Size(40, 40), false);

            //BLOKI NIENISZCZACE
            BlocksCreator(numberOfUnDestroyableBox, "Assets/Textures/Default_Blocks/MetalBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, true, new Size(40, 40), false);

            //BLOK WODA
            BlocksCreator(numberOfWaterBox, "Assets/Textures/Default_Blocks/WaterBlock.gif", "Assets/Textures/BackGrounds/T_BackGreen.jpg", false, true, false, new Size(40, 40), true);

            //BLOK TRAWA
            BlocksCreator(numberOfGrassBox, "Assets/Textures/Default_Blocks/GreenBlock.gif", "Assets/Textures/BackGrounds/T_BackGreen.jpg", false, false, false, new Size(40, 40), false);

            #endregion

            LoadBlocks("Assets/MapScr/Map1.txt");
        }

        public void Map2() //Dzika Rzeka
        {
            T1Spawn = new Point(565, 480);
            T2Spawn = new Point(5, 80);

            E1Spawn = new Point(565, 80);
            E2Spawn = new Point(5, 480);
            E3Spawn = new Point(245, 200);
            E4Spawn = new Point(325, 360);
            E5Spawn = new Point(325, 200);
            E6Spawn = new Point(245, 360);

            //Bonusy
            PercentForBonus = 80;
            numberOfSpeedBonuses = 4;
            numberOfLessReloadTimeBonuses = 4;
            numberOfHpBonuses = 2;
            numberOfBulletSpeedBonuses = 4;

            //Bonusy
            BonusCreator(Color.Yellow, true, true, true, true);


            const int numberOfDestroyalbeBox = 20;
            const int numberOfUnDestroyalbeBox = 12;
            const int numberOfWaterBox = 62;
            const int numberOfGrassBox = 24;

            #region TworzenieBloczkow


            //BLOKI NIENISZCZACE
            //BlocksCreator(numberOfUnDestroyalbeBox, "Assets/Textures/Default_Blocks/MetalBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, true, new Size(40, 40), false);
            BlocksCreator2(numberOfUnDestroyalbeBox, "Assets/Textures/Default_Blocks/MetalBlock.gif", Color.Yellow, false, true, true, false);

            //BLOK WODA
            BlocksCreator(numberOfWaterBox, "Assets/Textures/Default_Blocks/WaterBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, false, new Size(40, 40), true);

            //BLOK TRAWA
            BlocksCreator(numberOfGrassBox, "Assets/Textures/Default_Blocks/GreenBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, false, false, new Size(40, 40), false);
            //BlocksCreator(numberOfWaterBox, "Assets/Textures/Default_Blocks/WaterBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, false, new Size(40, 40), true);
            //BLOKI DO NISZCZENIA
            BlocksCreator(numberOfDestroyalbeBox, "Assets/Textures/Default_Blocks/RedBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", true, true, true, new Size(40, 40), false);

            #endregion

            LoadBlocks("Assets/MapScr/Map2.txt");
        }

        public void Map3() //Wulkan
        {
            T1Spawn = new Point(485, 80);
            T2Spawn = new Point(85, 80);

            E1Spawn = new Point(85, 520);
            E2Spawn = new Point(485, 520);
            E3Spawn = new Point(45, 480);
            E4Spawn = new Point(525, 560);
            E5Spawn = new Point(125, 440);
            E6Spawn = new Point(485, 440);

            //Bonusy
            PercentForBonus = 50;
            numberOfSpeedBonuses = 4;
            numberOfLessReloadTimeBonuses = 4;
            numberOfHpBonuses = 2;
            numberOfBulletSpeedBonuses = 4;

            //Bonusy
            BonusCreator(Color.Black, true, true, true, true);

            const int numberOfLawaBox = 37;
            const int numberOfUnDestroyableBoxRED = 32;
            const int numberOfUnDestroyableBoxBLACK = 20;
            const int numberOfDestroyalbeBoxRed = 44;
            const int numberOfDestroyalbeBoxBlack = 32;

            #region TworzenieBloczkow

            //BLOK Lawa
            BlocksCreator(numberOfLawaBox, "Assets/Textures/Volcano_Blocks/Lawa.gif", "Assets/Textures/Volcano_Blocks/Lawa.gif", false, true, false, new Size(40, 40), true);

            //BLOKI NIENISZCZACE_Red
            BlocksCreator(numberOfUnDestroyableBoxRED, "Assets/Textures/Default_Blocks/MetalBlock.gif", "Assets/Textures/Volcano_Blocks/Lawa.gif", false, true, true, new Size(40, 40), false);

            //BLOKI NIENISZCZACE_Black
            BlocksCreator(numberOfUnDestroyableBoxBLACK, "Assets/Textures/Default_Blocks/MetalBlock.gif", "Assets/Textures/Volcano_Blocks/MagmaBlack.gif", false, true, true, new Size(40, 40), false);


            //BLOKI DO NISZCZENIA
            BlocksCreator(numberOfDestroyalbeBoxRed, "Assets/Textures/Volcano_Blocks/MagmaRed.gif", "Assets/Textures/Volcano_Blocks/MagmaRed.gif", true, true, true, new Size(40, 40), false);

            //BLOKI DO NISZCZENIA
            BlocksCreator(numberOfDestroyalbeBoxBlack, "Assets/Textures/Volcano_Blocks/MagmaBlack.gif", "Assets/Textures/Volcano_Blocks/MagmaRed.gif", true, true, true, new Size(40, 40), false);

            #endregion

            LoadBlocks("Assets/MapScr/Map3.txt");
        }

        public void Map4() //Twierdza
        {
            T1Spawn = new Point(405, 520);
            T2Spawn = new Point(205, 520);

            E1Spawn = new Point(245, 40);
            E2Spawn = new Point(445, 80);
            E3Spawn = new Point(325, 40);
            E4Spawn = new Point(125, 80);
            E5Spawn = new Point(525, 120);
            E6Spawn = new Point(45, 120);

            //Bonusy
            PercentForBonus = 65;
            numberOfSpeedBonuses = 4;
            numberOfLessReloadTimeBonuses = 4;
            numberOfHpBonuses = 2;
            numberOfBulletSpeedBonuses = 4;

            //Bonusy
            BonusCreator(Color.Gold, true, true, true, true);

            const int numberOfWaterBox = 24;
            const int numberOfUnDestroyalbeBoxGreen = 12;
            const int numberOfGrassBox1 = 6;
            const int numberOfUnDestroyalbeBoxYellow = 12;
            const int numberOfDestroyalbeBox = 39;
            const int numberOfGrassBox = 63;

            #region TworzenieBloczkow
            //BLOK WODA
            BlocksCreator(numberOfWaterBox, "Assets/Textures/Default_Blocks/WaterBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, false, new Size(40, 40), true);

            //BLOKI NIENISZCZACE_Green
            BlocksCreator(numberOfUnDestroyalbeBoxGreen, "Assets/Textures/Default_Blocks/MetalBlock.gif", "Assets/Textures/Default_Blocks/GreenBlock.gif", false, true, true, new Size(40, 40), false);

            //BLOK TRAWA
            BlocksCreator(numberOfGrassBox1, "Assets/Textures/Default_Blocks/GreenBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, false, false, new Size(40, 40), false);

            //BLOKI NIENISZCZACE_Yellow
            BlocksCreator2(numberOfUnDestroyalbeBoxYellow, "Assets/Textures/Default_Blocks/MetalBlock.gif", Color.Gold, false, true, true, false);

            //BLOKI DO NISZCZENIA
            BlocksCreator(numberOfDestroyalbeBox, "Assets/Textures/Default_Blocks/RedBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", true, true, true, new Size(40, 40), false);

            //BLOK TRAWA
            BlocksCreator(numberOfGrassBox, "Assets/Textures/Default_Blocks/GreenBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, false, false, new Size(40, 40), false);
            //BlocksCreator(numberOfWaterBox, "Assets/Textures/Default_Blocks/WaterBlock.gif", "Assets/Textures/BackGrounds/T_BackSand.gif", false, true, false, new Size(40, 40), true);

            #endregion

            LoadBlocks("Assets/MapScr/Map4.txt");
        }


        public void AddonMap(AddonMap AMap)
        {
            T1Spawn = AMap.Tanks_Spawn[0];
            if (T1Spawn.X == -1000) T1Spawn = new Point(0, 0);
            T2Spawn = AMap.Tanks_Spawn[1];
            if (T2Spawn.X == -1000) T2Spawn = new Point(560, 560);

            E1Spawn = AMap.Tanks_Spawn[2];
            if (E1Spawn.X == -1000) Enemy1.Deactivation();
            E2Spawn = AMap.Tanks_Spawn[3];
            if (E2Spawn.X == -1000) Enemy2.Deactivation();
            E3Spawn = AMap.Tanks_Spawn[4];
            if (E3Spawn.X == -1000) Enemy3.Deactivation();
            E4Spawn = AMap.Tanks_Spawn[5];
            if (E4Spawn.X == -1000) Enemy4.Deactivation();
            E5Spawn = AMap.Tanks_Spawn[6];
            if (E5Spawn.X == -1000) Enemy5.Deactivation();
            E6Spawn = AMap.Tanks_Spawn[7];
            if (E6Spawn.X == -1000) Enemy6.Deactivation();

            //Bonusy
            PercentForBonus = AMap.BonusChange;
            numberOfSpeedBonuses = 50;
            numberOfLessReloadTimeBonuses = 50;
            numberOfHpBonuses = 50;
            numberOfBulletSpeedBonuses = 50;

            //Bonusy
            BonusCreator(AMap.Background, true, true, true, true);

           // AMap.AddonMap_Blocks[10].Bloczek.Image = Image.FromFile("Assets/Textures/Default_Blocks/RedBlock.gif");
            Blocks = AMap.AddonMap_Blocks;
            for (int i = 0; i < Blocks.Count(); i++)
            {
                this.Controls.Add(Blocks[i].Bloczek);
            }
           // Blocks[10].Bloczek.Image = Image.FromFile("Assets/Textures/Default_Blocks/RedBlock.gif");


        }

        public void Test_Area()
        {
            T1Spawn = new Point(565, 560);
            T2Spawn = new Point(5, 0);

            E1Spawn = new Point(240, 240);
            E2Spawn = new Point(300, 300);
            E3Spawn = new Point(400, 400);
            E4Spawn = new Point(240, 240);
            E5Spawn = new Point(300, 300);
            E6Spawn = new Point(400, 400);
        }

        public Form1(string NickP1, bool Player2ActiveInGame, string NickP2, int MapNumber, ushort Tank1Color, ushort Tank2Color, int GameModeOption, bool FriendlyFireOption, int DifficultyLevel, int GameMusic, int Volume, AddonMap AMap)
        {
            #region Add_Bullets
            Bullet = new PictureBox();
            Bullet2 = new PictureBox();
            EnemyBullet1 = new PictureBox();
            EnemyBullet2 = new PictureBox();
            EnemyBullet3 = new PictureBox();
            EnemyBullet4 = new PictureBox();
            EnemyBullet5 = new PictureBox();
            EnemyBullet6 = new PictureBox();

            BulletMethods.MakeBulletForStartGame(Bullet);
            BulletMethods.MakeBulletForStartGame(Bullet2);

            BulletMethods.MakeBulletForStartGame(EnemyBullet1);
            BulletMethods.MakeBulletForStartGame(EnemyBullet2);
            BulletMethods.MakeBulletForStartGame(EnemyBullet3);
            BulletMethods.MakeBulletForStartGame(EnemyBullet4);
            BulletMethods.MakeBulletForStartGame(EnemyBullet5);
            BulletMethods.MakeBulletForStartGame(EnemyBullet6);
            #endregion

            //GameMode: 0 - DM, 1-TDM
            FriendlyFire = FriendlyFireOption;
            GameMode = GameModeOption;
            if (GameMode == 0) FriendlyFire = true;

            #region DiffLvl_Options

            Diff_Level = DifficultyLevel;

            switch (DifficultyLevel)
            {
                case 0:
                    Enemy1 = new Enemy(0, true);
                    Enemy2 = new Enemy(0, true);
                    Enemy3 = new Enemy(1, true);
                    Enemy4 = new Enemy(1, true);
                    Enemy5 = new Enemy(2, true);
                    Enemy6 = new Enemy(2, true);
                    break;
                case 1:
                    Enemy1 = new Enemy(0, true);
                    Enemy2 = new Enemy(1, true);
                    Enemy3 = new Enemy(1, true);
                    Enemy4 = new Enemy(2, true);
                    Enemy5 = new Enemy(2, true);
                    Enemy6 = new Enemy(3, true);
                    break;
                case 2:
                    Enemy1 = new Enemy(1, true);
                    Enemy2 = new Enemy(2, true);
                    Enemy3 = new Enemy(2, true);
                    Enemy4 = new Enemy(3, true);
                    Enemy5 = new Enemy(3, true);
                    Enemy6 = new Enemy(4, true);
                    break;
                case 3:
                    Enemy1 = new Enemy(2, true);
                    Enemy2 = new Enemy(3, true);
                    Enemy3 = new Enemy(3, true);
                    Enemy4 = new Enemy(4, true);
                    Enemy5 = new Enemy(4, true);
                    Enemy6 = new Enemy(5, true);
                    break;
                case 4:
                    Enemy1 = new Enemy(3, true);
                    Enemy2 = new Enemy(5, true);
                    Enemy3 = new Enemy(4, true);
                    Enemy4 = new Enemy(4, true);
                    Enemy5 = new Enemy(3, true);
                    Enemy6 = new Enemy(5, true);
                    break;
                default:
                    break;
            }

            #endregion

            Player1 = new Tank(NickP1, 2, 30, 10, 6, Tank1Color);
            Player2 = new Tank(NickP2, 2, 30, 10, 6, Tank2Color);

            Blocks = new List<Block>();

            Bonuses = new List<Bonus>();

            #region AddObjectToMap
            this.Controls.Add(Player1.CharImageBox);
            this.Controls.Add(Player2.CharImageBox);

            this.Controls.Add(Enemy1.CharImageBox);
            this.Controls.Add(Enemy2.CharImageBox);
            this.Controls.Add(Enemy3.CharImageBox);
            this.Controls.Add(Enemy4.CharImageBox);
            this.Controls.Add(Enemy5.CharImageBox);
            this.Controls.Add(Enemy6.CharImageBox);

            this.Controls.Add(Bullet);
            this.Controls.Add(Bullet2);

            this.Controls.Add(EnemyBullet1);
            this.Controls.Add(EnemyBullet2);
            this.Controls.Add(EnemyBullet3);
            this.Controls.Add(EnemyBullet4);
            this.Controls.Add(EnemyBullet5);
            this.Controls.Add(EnemyBullet6);
            #endregion

            switch (MapNumber)
            {
                case 0:
                    Map1();
                    break;
                case 1:
                    Map2();
                    break;
                case 2:
                    Map3();
                    break;
                case 3:
                    Map4();
                    break;
                //AddonMap
                case 4:
                    if(AMap != null)
                    {
                        AddonMap(AMap);
                    }             
                    else
                    {
                        Test_Area();
                        MapNumber = 5;
                    }
                        
                    break;
                    
                case 5:
                    Test_Area();
                    break;
                default:
                    break;
            }

            InitializeComponent();

            #region Player2ActiveInGameOptions
            if (!Player2ActiveInGame)
            {
                Player2.CharImageBox.Location = new Point(-200, -200);
                //Player2.CharImageBox.Hide();
                T2Spawn = new Point(-200, -200);
                Player2.IsActive = false;

                Tank1_Nick.Location = new Point(225, 600);
                Tank1_Nick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                Player1HP10.Location = new Point(225, 630);
                Player1HP20.Location = new Point(265, 630);
                Player1HP30.Location = new Point(305, 630);
                Player1HP40.Location = new Point(345, 630);
                BulletReload.Location = new Point(185, 610);

                //
                Enemy1.Deactivation();
                E1Spawn = new Point(-1000, -1000);
                Enemy3.Deactivation();
                E3Spawn = new Point(-1000, -1000);
                // Enemy5.Deactivation();
                // E5Spawn = new Point(-1000, -1000);

            }
            #endregion


            #region GameMusic
            switch (GameMusic)
            {
                case 0:
                    {
                        BackgroundMusic.controls.stop();
                        break;
                    }

                case 1:
                    {
                        LoadMusic("Assets/Music/Menu.mp3");
                        break;
                    }
                case 2:
                    {
                        LoadMusic("Assets/Music/Eye of the Tiger.mp3");
                        break;
                    }
                case 3:
                    {
                        LoadMusic("Assets/Music/Stayin Alive.mp3");
                        break;
                    }
                case 4:
                    {
                        LoadMusic("Assets/Music/The Last Stand.mp3");
                        break;
                    }
                default:
                    LoadMusic("Assets/Music/Menu.mp3");
                    break;
            }


            BackgroundMusic.settings.volume = Volume;
            #endregion

            Tank1_Nick.Text = NickP1;
            Tank2_Nick.Text = NickP2;

            for (int i = 0; i < Blocks.Count; i++)
            {
               Blocks[i].Bloczek.SendToBack();
            }
            MainPanel.SendToBack();

            for (int i = 0; i < Blocks.Count; i++)
            {
                if (!Blocks[i].IsWater) Blocks[i].Bloczek.BringToFront();
            }

            #region MapFinalOptions

            switch (MapNumber)
            {

                case 0:
                    MainPanel.BackColor = Color.Green;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;

                    Player1.CharImageBox.BackColor = Color.Green;
                    Player2.CharImageBox.BackColor = Color.Green;
                    Enemy1.CharImageBox.BackColor = Color.Green;
                    Enemy2.CharImageBox.BackColor = Color.Green;
                    Enemy3.CharImageBox.BackColor = Color.Green;
                    Enemy4.CharImageBox.BackColor = Color.Green;
                    Enemy5.CharImageBox.BackColor = Color.Green;
                    Enemy6.CharImageBox.BackColor = Color.Green;
                    break;
                case 1:
                    MainPanel.BackColor = Color.Yellow;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;

                    Player1.CharImageBox.BackColor = Color.Yellow;
                    Player2.CharImageBox.BackColor = Color.Yellow;
                    Enemy1.CharImageBox.BackColor = Color.Yellow;
                    Enemy2.CharImageBox.BackColor = Color.Yellow;
                    Enemy3.CharImageBox.BackColor = Color.Yellow;
                    Enemy4.CharImageBox.BackColor = Color.Yellow;
                    Enemy5.CharImageBox.BackColor = Color.Yellow;
                    Enemy6.CharImageBox.BackColor = Color.Yellow;

                    //for (int i = 0; i < Blocks.Count; i++)
                    //{
                    //    if (Blocks[i].IsWater) Blocks[i].Bloczek.SendToBack();

                    //}
                    break;
                case 2:
                    //MainPanel.BackgroundImage = Image.FromFile("Assets/MapScr/Map3Back.gif");
                    MainPanel.BackColor = Color.Black;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;
                    break;
                case 3:
                    MainPanel.BackColor = Color.Gold;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;

                    Player1.CharImageBox.BackColor = Color.Gold;
                    Player2.CharImageBox.BackColor = Color.Gold;
                    Enemy1.CharImageBox.BackColor = Color.Gold;
                    Enemy2.CharImageBox.BackColor = Color.Gold;
                    Enemy3.CharImageBox.BackColor = Color.Gold;
                    Enemy4.CharImageBox.BackColor = Color.Gold;
                    Enemy5.CharImageBox.BackColor = Color.Gold;
                    Enemy6.CharImageBox.BackColor = Color.Gold;

                    break;
                case 4: //AddonMap
                    MainPanel.BackColor = AMap.Background;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;

                    Player1.CharImageBox.BackColor = AMap.Background;
                    Player2.CharImageBox.BackColor = AMap.Background;
                    Enemy1.CharImageBox.BackColor = AMap.Background;
                    Enemy2.CharImageBox.BackColor = AMap.Background;
                    Enemy3.CharImageBox.BackColor = AMap.Background;
                    Enemy4.CharImageBox.BackColor = AMap.Background;
                    Enemy5.CharImageBox.BackColor = AMap.Background;
                    Enemy6.CharImageBox.BackColor = AMap.Background;
                    break;
                case 5:
                    MainPanel.BackColor = Color.Black;
                    Player1.CharImageBox.Location = T1Spawn;
                    Player2.CharImageBox.Location = T2Spawn;
                    Enemy1.CharImageBox.Location = E1Spawn;
                    Enemy2.CharImageBox.Location = E2Spawn;
                    Enemy3.CharImageBox.Location = E3Spawn;
                    Enemy4.CharImageBox.Location = E4Spawn;
                    Enemy5.CharImageBox.Location = E5Spawn;
                    Enemy6.CharImageBox.Location = E6Spawn;


                    //MainPanel.BackgroundImage = Image.FromFile("Assets/GreenBlockBackground.png");
                    break;
                default:
                    MainPanel.BackColor = Color.DarkBlue;
                    Player1.CharImageBox.Location = new Point(565, 560);
                    Player2.CharImageBox.Location = new Point(5, 0);

                    break;
            }
            #endregion


            #region GameOverComponent
            GameOver_Panel.Size = new Size(400, 250);
            GameOver_Panel.Location = new Point(640, 60);
            GameOver_Panel.BackgroundImage = Image.FromFile("Assets/Buttons/tło_pod_wybor_mapy520x317.gif");
            GameOver_Panel.Visible = false;
            this.Controls.Add(GameOver_Panel);

            GameOver_WinLoss_Label.Size = new Size(400, 50);
            GameOver_WinLoss_Label.TextAlign = ContentAlignment.MiddleCenter;
            GameOver_WinLoss_Label.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_WinLoss_Label);
            GameOver_WinLoss_Label.Location = new Point(0, 10);
            GameOver_WinLoss_Label.Font = new Font("High Tower Text", 22, FontStyle.Bold);

            GameOver_Time_Label.Size = new Size(270, 30);
            GameOver_Time_Label.TextAlign = ContentAlignment.MiddleRight;
            GameOver_Time_Label.BackColor = Color.Transparent;
            GameOver_Time_Label.Text = "Time of Game: ";
            GameOver_Panel.Controls.Add(GameOver_Time_Label);
            GameOver_Time_Label.Location = new Point(0, 80);
            GameOver_Time_Label.Font = new Font("High Tower Text", 18, FontStyle.Bold);

            GameOver_TimeValue.Size = new Size(130, 30);
            GameOver_TimeValue.TextAlign = ContentAlignment.MiddleLeft;
            GameOver_TimeValue.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_TimeValue);
            GameOver_TimeValue.Location = new Point(270, 80);
            GameOver_TimeValue.Font = new Font("Tempus Sans ITC", 16, FontStyle.Italic);

            GameOver_Points_Label.Size = new Size(400, 30);
            GameOver_Points_Label.TextAlign = ContentAlignment.MiddleCenter;
            GameOver_Points_Label.BackColor = Color.Transparent;
            GameOver_Points_Label.Text = "Points:";
            GameOver_Panel.Controls.Add(GameOver_Points_Label);
            GameOver_Points_Label.Location = new Point(0, 130);
            GameOver_Points_Label.Font = new Font("High Tower Text", 18, FontStyle.Bold);

            GameOver_Nick1.Size = new Size(300, 30);
            GameOver_Nick1.Visible = false;
            GameOver_Nick1.TextAlign = ContentAlignment.MiddleLeft;
            GameOver_Nick1.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_Nick1);
            GameOver_Nick1.Location = new Point(0, 160);
            GameOver_Nick1.Font = new Font("Tempus Sans ITC", 18, FontStyle.Regular);

            GameOver_Nick1PointsValue.Size = new Size(100, 30);
            GameOver_Nick1PointsValue.Visible = false;
            GameOver_Nick1PointsValue.TextAlign = ContentAlignment.MiddleRight;
            GameOver_Nick1PointsValue.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_Nick1PointsValue);
            GameOver_Nick1PointsValue.Location = new Point(300, 160);
            GameOver_Nick1PointsValue.Font = new Font("Tempus Sans ITC", 18, FontStyle.Italic);

            GameOver_Nick2.Size = new Size(300, 30);
            GameOver_Nick2.Visible = false;
            GameOver_Nick2.TextAlign = ContentAlignment.MiddleLeft;
            GameOver_Nick2.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_Nick2);
            GameOver_Nick2.Location = new Point(0, 190);
            GameOver_Nick2.Font = new Font("Tempus Sans ITC", 18, FontStyle.Regular);

            GameOver_Nick2PointsValue.Size = new Size(100, 30);
            GameOver_Nick2PointsValue.Visible = false;
            GameOver_Nick2PointsValue.TextAlign = ContentAlignment.MiddleRight;
            GameOver_Nick2PointsValue.BackColor = Color.Transparent;
            GameOver_Panel.Controls.Add(GameOver_Nick2PointsValue);
            GameOver_Nick2PointsValue.Location = new Point(300, 190);
            GameOver_Nick2PointsValue.Font = new Font("Tempus Sans ITC", 18, FontStyle.Italic);

            GameOver_Close.Text = "OK";
            GameOver_Close.Size = new Size(70, 35);
            GameOver_Close.TextAlign = ContentAlignment.MiddleCenter;
            GameOver_Close.Font = new Font("Tempus Sans ITC", 16, FontStyle.Bold);
            GameOver_Close.ForeColor = Color.White;
            GameOver_Close.BackColor = Color.Gray;
            //GameOver_Close.FlatStyle = FlatStyle.Popup;
            GameOver_Panel.Controls.Add(GameOver_Close);
            GameOver_Close.Location = new Point(165, 210);
            GameOver_Close.BringToFront();
            this.GameOver_Close.Click += new System.EventHandler(this.GameOver_Close_Click);

            #endregion

            #region CheatLabel
            //CheatLabel.Size = new Size(82, 17);
            CheatLabel.AutoSize = true;
            CheatLabel.Text = "Cheat Mode";
            CheatLabel.Location = new Point(4, 4);
            CheatLabel.Font = new Font("Tempus Sans ITC", 10, FontStyle.Bold);
            CheatLabel.ForeColor = Color.DarkBlue;
            CheatLabel.BackColor = SystemColors.ControlDark;
            CheatLabel.Visible = false;
            this.Controls.Add(CheatLabel);
            #endregion
        }
        private void LoadMusic(string URL)
        {
            BackgroundMusic.URL = URL;
            BackgroundMusic.controls.play();
            BackgroundMusic.settings.setMode("loop", true);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            #region KeysToControl
            if (Player1.IsActive)
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        Player1.IsMoving = true;
                        Player1.Direct = DirectionsOfMoving.RIGHT;
                        Player1.CharImageBox.Image = Image.FromFile(Player1.MovingImage[3]);
                        break;
                    case Keys.Left:
                        Player1.IsMoving = true;
                        Player1.Direct = DirectionsOfMoving.LEFT;
                        Player1.CharImageBox.Image = Image.FromFile(Player1.MovingImage[2]);
                        break;
                    case Keys.Up:
                        Player1.IsMoving = true;
                        Player1.Direct = DirectionsOfMoving.UP;
                        Player1.CharImageBox.Image = Image.FromFile(Player1.MovingImage[0]);
                        break;
                    case Keys.Down:
                        Player1.IsMoving = true;
                        Player1.Direct = DirectionsOfMoving.DOWN;
                        Player1.CharImageBox.Image = Image.FromFile(Player1.MovingImage[1]);
                        break;

                    case Keys.OemQuestion:
                    case Keys.NumPad0:
                        if (!Player1.FireHero)
                        {
                            Player1.ShootingSound();
                            Player1.FireHero = true;
                            Player1.Fire_sek = 0;
                            Bullet.Location = new Point(Player1.CharImageBox.Location.X + 11, Player1.CharImageBox.Location.Y + 14);
                            Player1.BulletMoving = (DirectionsOfBulletMoving)Player1.Direct;
                        }
                        break;
                }
            }
            if (Player2.IsActive)
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        Player2.IsMoving = true;
                        Player2.Direct = DirectionsOfMoving.RIGHT;
                        Player2.CharImageBox.Image = Image.FromFile(Player2.MovingImage[3]);
                        break;
                    case Keys.W:
                        Player2.IsMoving = true;
                        Player2.Direct = DirectionsOfMoving.UP;
                        Player2.CharImageBox.Image = Image.FromFile(Player2.MovingImage[0]);
                        break;
                    case Keys.S:
                        Player2.IsMoving = true;
                        Player2.Direct = DirectionsOfMoving.DOWN;
                        Player2.CharImageBox.Image = Image.FromFile(Player2.MovingImage[1]);
                        break;
                    case Keys.A:
                        Player2.IsMoving = true;
                        Player2.Direct = DirectionsOfMoving.LEFT;
                        Player2.CharImageBox.Image = Image.FromFile(Player2.MovingImage[2]);
                        break;


                    case Keys.G:
                    case Keys.CapsLock:
                        if (!Player2.FireHero)
                        {
                            Player2.ShootingSound();
                            Player2.FireHero = true;
                            Player2.Fire_sek = 0;
                            Bullet2.Location = new Point(Player2.CharImageBox.Location.X + 11, Player2.CharImageBox.Location.Y + 14);
                            Player2.BulletMoving = (DirectionsOfBulletMoving)Player2.Direct;
                        }
                        break;
                }
            }
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    MainTime.Enabled = false;
                    TimeReload.Enabled = false;
                    BackgroundMusic.controls.stop();
                    StartForm startform = new StartForm();
                    startform.Show();
                    this.Hide();
                    break;
                case Keys.F2:
                    CheatsON = !CheatsON;
                    CheatLabel.Visible = !CheatLabel.Visible;
                    CheatLabel.BringToFront();
                    break;
            }
            if (CheatsON)
            {
                switch (e.KeyCode)
                {
                    //CHEATS
                    case Keys.NumPad7:
                        Player1.Speed = 6;
                        Player2.Speed = 6;
                        CheatLabel.Text = "Speed Cheat";
                        Player1.Points -= 1000;
                        Player2.Points -= 1000;
                        break;
                    case Keys.NumPad8:
                        Player1.ReloadSek = 2;
                        Player2.ReloadSek = 2;
                        CheatLabel.Text = "Reload Cheat";
                        Player1.Points -= 1000;
                        Player2.Points -= 1000;
                        break;
                    case Keys.NumPad9:
                        Player1.HealthPoints += 100;
                        Player2.HealthPoints += 100;
                        CheatLabel.Text = "HP Cheat";
                        Player1.Points -= 1000;
                        Player2.Points -= 1000;
                        break;
                }
            }
            #endregion
        }
        private void MainTime_Tick(object sender, EventArgs e)
        {
            TickUpdate();
        }
        private void TimeReload_Tick(object sender, EventArgs e)
        {
            TimeOfGame++;

            Player1.TimeReload();
            Player2.TimeReload();

            EnemysReloading();
        }

        //Przeładowanie Enemy
        private void EnemysReloading()
        {
            Enemy1.EnemyReload();
            Enemy2.EnemyReload();
            Enemy3.EnemyReload();
            Enemy4.EnemyReload();
            Enemy5.EnemyReload();
            Enemy6.EnemyReload();
        }
        //kolizje z ekranem
        private void BulletCollideWithMainPanel()
        {
            BulletMethods.BulletHideWithMainPanel(Player1, Bullet, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Player2, Bullet2, MainPanel);

            BulletMethods.BulletHideWithMainPanel(Enemy1, EnemyBullet1, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Enemy2, EnemyBullet2, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Enemy3, EnemyBullet3, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Enemy4, EnemyBullet4, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Enemy5, EnemyBullet5, MainPanel);
            BulletMethods.BulletHideWithMainPanel(Enemy6, EnemyBullet6, MainPanel);
        }
        //Kolizja naboju z blokiem       
        private void BulletCollideWithBlok()
        {
            BulletMethods.BulletAndBlock(Bullet, Blocks, Bonuses, PercentForBonus, false);
            BulletMethods.BulletAndBlock(Bullet2, Blocks, Bonuses, PercentForBonus, false);

            BulletMethods.BulletAndBlock(EnemyBullet1, Blocks, Bonuses, PercentForBonus, true);
            BulletMethods.BulletAndBlock(EnemyBullet2, Blocks, Bonuses, PercentForBonus, true);
            BulletMethods.BulletAndBlock(EnemyBullet3, Blocks, Bonuses, PercentForBonus, true);
            BulletMethods.BulletAndBlock(EnemyBullet4, Blocks, Bonuses, PercentForBonus, true);
            BulletMethods.BulletAndBlock(EnemyBullet5, Blocks, Bonuses, PercentForBonus, true);
            BulletMethods.BulletAndBlock(EnemyBullet6, Blocks, Bonuses, PercentForBonus, true);

        }
        //Kolizja naboju z czołgiem
        private void BulletCollideWithTank()
        {
            //Hero1
            if (Bullet.Bounds.IntersectsWith(Player2.CharImageBox.Bounds))
            {
                if (FriendlyFire) Player2.GetHit(Player1.PowerOfGun);
                Bullet.Hide();
                Bullet.Location = new Point(-454, -534);
            }

            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy1);
            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy2);
            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy3);
            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy4);
            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy5);
            BulletMethods.BulletHideWithEnemy(Player1, Bullet, Enemy6);

            //Hero2
            if (Bullet2.Bounds.IntersectsWith(Player1.CharImageBox.Bounds))
            {
                if (FriendlyFire) Player1.GetHit(Player2.PowerOfGun);
                Bullet2.Hide();
                Bullet2.Location = new Point(-454, -534);
            }

            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy1);
            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy2);
            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy3);
            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy4);
            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy5);
            BulletMethods.BulletHideWithEnemy(Player2, Bullet2, Enemy6);

            //Enemy1
            BulletMethods.BulletHideWithEnemy(Enemy1, EnemyBullet1, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy1, EnemyBullet1, Player2);
            //Enemy2
            BulletMethods.BulletHideWithEnemy(Enemy2, EnemyBullet2, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy2, EnemyBullet2, Player2);

            //Enemy3
            BulletMethods.BulletHideWithEnemy(Enemy3, EnemyBullet3, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy3, EnemyBullet3, Player2);

            //Enemy4
            BulletMethods.BulletHideWithEnemy(Enemy4, EnemyBullet4, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy4, EnemyBullet4, Player2);

            //Enemy5
            BulletMethods.BulletHideWithEnemy(Enemy5, EnemyBullet5, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy5, EnemyBullet5, Player2);

            //Enemy6
            BulletMethods.BulletHideWithEnemy(Enemy6, EnemyBullet6, Player1);
            BulletMethods.BulletHideWithEnemy(Enemy6, EnemyBullet6, Player2);
        }
        //Kolizja Czołgu z blokiem
        private void CheckHeroCollideWithBlok()
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Player1.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Player1.BlockCollision(Blocks[i]);
                if (Player2.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Player2.BlockCollision(Blocks[i]);

                if (Enemy1.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy1.BlockCollisionForEnemy(Blocks[i]);
                if (Enemy2.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy2.BlockCollisionForEnemy(Blocks[i]);
                if (Enemy3.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy3.BlockCollisionForEnemy(Blocks[i]);
                if (Enemy4.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy4.BlockCollisionForEnemy(Blocks[i]);
                if (Enemy5.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy5.BlockCollisionForEnemy(Blocks[i]);
                if (Enemy6.CharImageBox.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds)) Enemy6.BlockCollisionForEnemy(Blocks[i]);

            }
        }

        //Kolizja 2 czolgów
        private void CheckHeroCollideWithHero2()
        {
            if (Player1.CharImageBox.Bounds.IntersectsWith(Player2.CharImageBox.Bounds))
            {
                if (Player2.IsActive)
                {
                    if (FriendlyFire) Player1.GetHit(10);
                    if (Player1.IsActive) Player1.CharImageBox.Location = T1Spawn;
                }

                if (Player1.IsActive)
                {
                    if (FriendlyFire) Player2.GetHit(10);
                    if (Player2.IsActive) Player2.CharImageBox.Location = T2Spawn;
                }
            }

            //Enemy1 with Hero1 and Hero2

            Enemy1.EnemyCollideWithPlayerTank(Player1, T1Spawn, E1Spawn);
            Enemy1.EnemyCollideWithPlayerTank(Player2, T2Spawn, E1Spawn);

            //Enemy2 with Hero1 and Hero2
            Enemy2.EnemyCollideWithPlayerTank(Player1, T1Spawn, E2Spawn);
            Enemy2.EnemyCollideWithPlayerTank(Player2, T2Spawn, E2Spawn);

            //Enemy3 with Hero1 and Hero2

            Enemy3.EnemyCollideWithPlayerTank(Player1, T1Spawn, E3Spawn);
            Enemy3.EnemyCollideWithPlayerTank(Player2, T2Spawn, E3Spawn);

            //Enemy4 with Hero1 and Hero2
            Enemy4.EnemyCollideWithPlayerTank(Player1, T1Spawn, E4Spawn);
            Enemy4.EnemyCollideWithPlayerTank(Player2, T2Spawn, E4Spawn);

            //Enemy5 with Hero1 and Hero2

            Enemy5.EnemyCollideWithPlayerTank(Player1, T1Spawn, E5Spawn);
            Enemy5.EnemyCollideWithPlayerTank(Player2, T2Spawn, E5Spawn);

            //Enemy6 with Hero1 and Hero2
            Enemy6.EnemyCollideWithPlayerTank(Player1, T1Spawn, E6Spawn);
            Enemy6.EnemyCollideWithPlayerTank(Player2, T2Spawn, E6Spawn);


        }
        //Spawn i poruszanie sie bullet  
        private void SpawnMovingBullet() //For Players
        {
            BulletMethods.SpawnMovingBullet(Player1, Bullet);
            BulletMethods.SpawnMovingBullet(Player2, Bullet2);
        }
        //Poruszanie sie
        private void HeroMoving()
        {
            if (Player1.IsMoving) Player1.Moving(MainPanel);
            if (Player2.IsMoving) Player2.Moving(MainPanel);
        }
        //Bonusy   
        private void BonusCollideWithTanks()
        {
            for (int i = 0; i < Bonuses.Count; i++)
            {
                if (Bonuses[i].BonusImg.Bounds.IntersectsWith(Player1.CharImageBox.Bounds))
                {
                    Bonuses[i].AddValueAsBonus(Player1);
                    Bonuses[i].BonusImg.Location = new Point(-1000, -1000);
                }

                //Player2

                if (Bonuses[i].BonusImg.Bounds.IntersectsWith(Player2.CharImageBox.Bounds))
                {
                    Bonuses[i].AddValueAsBonus(Player2);
                    Bonuses[i].BonusImg.Location = new Point(-1000, -1000);
                }
            }
        }

        //Tworzenie Map
        private void BonusCreator(Color BackColor, bool CreateSpeed, bool CreateReload, bool CreateHP, bool CreateBulletSpeed)
        {
            if (CreateSpeed)
            {
                //Speed Bonus
                for (int i = 0; i < numberOfSpeedBonuses; i++)
                {
                    Bonus TempBonus = new Bonus(BonusType.Speed, "Assets/Textures/Bonuses/bonus_speed.gif", new Size(30, 30), new Point(-1000, -1000));
                    TempBonus.BonusImg.BackColor = BackColor;
                    this.Controls.Add(TempBonus.BonusImg);
                    Bonuses.Add(TempBonus);
                }
            }
            if (CreateReload)
            {
                //ReloadTime Bonus
                for (int i = 0; i < numberOfLessReloadTimeBonuses; i++)
                {
                    Bonus TempBonus = new Bonus(BonusType.ReloadSek, "Assets/Textures/Bonuses/bonus_reload.gif", new Size(30, 30), new Point(-1000, -1000));
                    TempBonus.BonusImg.BackColor = BackColor;
                    this.Controls.Add(TempBonus.BonusImg);
                    Bonuses.Add(TempBonus);
                }
            }
            if (CreateHP)
            {
                //HP Bonus
                for (int i = 0; i < numberOfHpBonuses; i++)
                {
                    Bonus TempBonus = new Bonus(BonusType.HP, "Assets/Textures/Bonuses/bonus_hp.gif", new Size(30, 30), new Point(-1000, -1000));
                    TempBonus.BonusImg.BackColor = BackColor;
                    this.Controls.Add(TempBonus.BonusImg);
                    Bonuses.Add(TempBonus);
                }
            }
            if (CreateBulletSpeed)
            {
                //BulletSpeed Bonus
                for (int i = 0; i < numberOfBulletSpeedBonuses; i++)
                {
                    Bonus TempBonus = new Bonus(BonusType.BulletSpeed, "Assets/Textures/Bonuses/bonus_bulletspeed.gif", new Size(30, 30), new Point(-1000, -1000));
                    TempBonus.BonusImg.BackColor = BackColor;
                    this.Controls.Add(TempBonus.BonusImg);
                    Bonuses.Add(TempBonus);
                }
            }
        }
        private void LoadBlocks(string FileWithLocalizationBlocks)
        {
            using (StreamReader streamR = new StreamReader((FileWithLocalizationBlocks), true))
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    string Record = streamR.ReadLine();
                    string[] RecordS;
                    RecordS = Record.Split(' ');
                    Blocks[i].Bloczek.Location = new Point(int.Parse(RecordS[0]), int.Parse(RecordS[1]));
                }
            }
        }
        private void BlocksCreator(int numberOfBlocks, string ImageFromAddress, string ImageBackgroundAddress, bool isDestroyable, bool isCollidingtanks, bool isCollidingBullet, Size sizeOfImage, bool isWater)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                Block tempBlock = new Block(ImageFromAddress, ImageBackgroundAddress, isDestroyable, isCollidingtanks, isCollidingBullet, sizeOfImage, isWater);
                this.Controls.Add(tempBlock.Bloczek);
                Blocks.Add(tempBlock);
            }
        }
        private void BlocksCreator2(int numberOfBlocks, string ImageFromAddress, Color color, bool isDestroyable, bool isCollidingtanks, bool isCollidingBullet, bool isWater)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                Block tempBlock = new Block(ImageFromAddress, color, isDestroyable, isCollidingtanks, isCollidingBullet, isWater);
                this.Controls.Add(tempBlock.Bloczek);
                Blocks.Add(tempBlock);
            }
        }

        //Sprawdzanie wygranej
        private void GameOver_Close_Click(object sender, EventArgs e)
        {
            BackgroundMusic.controls.stop();
            StartForm startform = new StartForm();
            startform.Show();
            this.Hide();
        }
        private void GameOverPanel(int choose)
        {
            GameOver_Panel.Location = new Point(100, 60);
            GameOver_Panel.Show();
            GameOver_Panel.BringToFront();
            GameOver_TimeValue.Text = TimeOfGame.ToString();

            switch (choose)
            {
                case 1: //jesli wygral P1
                    GameOver_WinLoss_Label.Text = "VICTORY";
                    GameOver_WinLoss_Label.ForeColor = Color.LimeGreen;
                    GameOver_Nick1.Show();
                    GameOver_Nick1PointsValue.Show();
                    GameOver_Nick1.Text = Tank1_Nick.Text;
                    GameOver_Nick1PointsValue.Text = Player1.Points.ToString();
                    break;
                case 2: // jesli wygral P2
                    GameOver_WinLoss_Label.Text = "VICTORY";
                    GameOver_WinLoss_Label.ForeColor = Color.LimeGreen;
                    GameOver_Nick1.Show();
                    GameOver_Nick1PointsValue.Show();
                    GameOver_Nick1.Text = Tank2_Nick.Text;
                    GameOver_Nick1PointsValue.Text = Player2.Points.ToString();
                    break;
                case 3: //Remis
                    GameOver_WinLoss_Label.Text = "DRAW";
                    GameOver_WinLoss_Label.ForeColor = Color.White;
                    GameOver_Points_Label.Text = "Game Over";
                    break;
                case 4:
                    GameOver_WinLoss_Label.Text = "DEFEAT";
                    GameOver_WinLoss_Label.ForeColor = Color.DarkRed;
                    GameOver_Points_Label.Text = "Game Over";
                    break;
                case 5:
                    GameOver_WinLoss_Label.Text = "VICTORY";
                    GameOver_WinLoss_Label.ForeColor = Color.LimeGreen;
                    GameOver_Nick1.Show();
                    GameOver_Nick1PointsValue.Show();
                    GameOver_Nick2.Show();
                    GameOver_Nick2PointsValue.Show();
                    GameOver_Nick1.Text = Tank1_Nick.Text;
                    GameOver_Nick1PointsValue.Text = Player1.Points.ToString();
                    GameOver_Nick2.Text = Tank2_Nick.Text;
                    GameOver_Nick2PointsValue.Text = Player2.Points.ToString();
                    break;
                default:
                    break;
            }                       
        }
        private void StopGame(int choose)
        {
            MainTime.Enabled = false;
            TimeReload.Enabled = false;
            GameOverPanel(choose);
            if (Victory)
            {
                if (T2Spawn.X == -200)
                {
                    FileUpdate_Single(Tank1_Nick.Text, Player1.Points);
                }
                else if (GameMode == 0 && T2Spawn.X != -200)
                {
                    if (Player1.IsActive) FileUpdate_Multi_DM(Tank1_Nick.Text, Player1.Points);
                    if (Player2.IsActive) FileUpdate_Multi_DM(Tank2_Nick.Text, Player2.Points);
                }
                else if (GameMode == 1)
                {
                    FileUpdate_Multi_TDM(Tank1_Nick.Text, Player1.Points, Tank2_Nick.Text, Player2.Points);
                }
            }
        }
        private void EndGameCheck()
        {

            Player1.CheckActivity();
            Player2.CheckActivity();

            if (GameMode == 0) //DM
            {

                if (!Enemy1.IsActive && !Enemy2.IsActive && !Enemy3.IsActive && !Enemy4.IsActive && !Enemy5.IsActive && !Enemy6.IsActive)
                {
                    if (Player1.IsActive && !Player2.IsActive) //Jesli wygral Player1
                    {
                        Victory = true;
                        Player1.HP_EndPoints(Diff_Level);
                        StopGame(1);
                    }

                    if (!Player1.IsActive && Player2.IsActive)
                    {
                        Victory = true;
                        Player2.HP_EndPoints(Diff_Level);
                        StopGame(2);
                    }
                    if (!Player1.IsActive && !Player2.IsActive) //Jesli Remis
                    {
                        StopGame(3);
                    }
                }
                else if (!Player1.IsActive && !Player2.IsActive) //Jesli Przegrana
                {
                    StopGame(4);
                }

            }

            if (GameMode == 1) //TDM
            {
                //Jesli gracze przegrali
                if (!Player1.IsActive && !Player2.IsActive)
                {
                    StopGame(4);
                }
                //Jesli gracze wygrali
                if (!Enemy1.IsActive && !Enemy2.IsActive && !Enemy3.IsActive && !Enemy4.IsActive && !Enemy5.IsActive && !Enemy6.IsActive)
                {
                    if (Player1.IsActive || Player2.IsActive)
                    {
                        Victory = true;
                        Player1.HP_EndPoints(Diff_Level);
                        Player2.HP_EndPoints(Diff_Level);
                        StopGame(5);
                    }
                }
            }

        }

        //Aktualizacja ilosci HP i serduszek
        private void HP_Hide(PictureBox HP40, bool HP40_Hide, PictureBox HP30, bool HP30_Hide, PictureBox HP20, bool HP20_Hide, PictureBox HP10, bool HP10_Hide)
        {
            if (HP40_Hide) HP40.Hide();
            else HP40.Show();

            if (HP30_Hide) HP30.Hide();
            else HP30.Show();

            if (HP20_Hide) HP20.Hide();
            else HP20.Show();

            if (HP10_Hide) HP10.Hide();
            else HP10.Show();
        }
        private void PlayerGUI(Tank Player, Label Tank_Nick, PictureBox HP10, PictureBox HP20, PictureBox HP30, PictureBox HP40, PictureBox BulletReload)
        {
            if (Player.IsActive)
            {
                if (Player.HealthPoints == 40)
                {
                    HP_Hide(HP40, false, HP30, false, HP20, false, HP10, false);
                }
                if (Player.HealthPoints == 30)
                {
                    HP_Hide(HP40, true, HP30, false, HP20, false, HP10, false);
                }
                if (Player.HealthPoints == 20)
                {
                    HP_Hide(HP40, true, HP30, true, HP20, false, HP10, false);
                }
                if (Player.HealthPoints == 10)
                {
                    HP_Hide(HP40, true, HP30, true, HP20, true, HP10, false);
                }
                if (Player.HealthPoints <= 0)
                {
                    HP_Hide(HP40, true, HP30, true, HP20, true, HP10, true);
                }

                if (Player.Fire_sek == 0)
                {
                    if (Player.HeroBulletStart == true) BulletReload.Image = Image.FromFile("Assets/Textures/Bullets/BulletFull.gif");
                    else BulletReload.Image = Image.FromFile("Assets/Textures/Bullets/BulletOut.gif");

                }
                if (Player.Fire_sek == (Player.ReloadSek) / 2)
                {
                    BulletReload.Image = Image.FromFile("Assets/Textures/Bullets/BulletHalf.gif");
                }
                if (Player.Fire_sek == Player.ReloadSek)
                {
                    BulletReload.Image = Image.FromFile("Assets/Textures/Bullets/BulletFull.gif");
                }
            }
            else
            {
                Tank_Nick.Hide();
                HP_Hide(HP40, true, HP30, true, HP20, true, HP10, true);
                BulletReload.Hide();
            }
        }
        private void UpdatePictures()
        {
            PlayerGUI(Player1, Tank1_Nick, Player1HP10, Player1HP20, Player1HP30, Player1HP40, BulletReload);
            PlayerGUI(Player2, Tank2_Nick, Player2HP10, Player2HP20, Player2HP30, Player2HP40, Bullet2Reload);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            BackgroundMusic.controls.stop();
        }

        //Sprawdzenie wszystkich metod co każdy tick zegara
        private void TickUpdate()
        {
            //***************************************
            #region HeroMovingAndColliding
            HeroMoving();
            CheckHeroCollideWithBlok();
            CheckHeroCollideWithHero2();
            #endregion
            //***************************************
            #region EnemyMovngAndShooting
            Enemy1.EnemyMovingAndShooting(EnemyBullet1, MainPanel);
            Enemy2.EnemyMovingAndShooting(EnemyBullet2, MainPanel);
            Enemy3.EnemyMovingAndShooting(EnemyBullet3, MainPanel);
            Enemy4.EnemyMovingAndShooting(EnemyBullet4, MainPanel);
            Enemy5.EnemyMovingAndShooting(EnemyBullet5, MainPanel);
            Enemy6.EnemyMovingAndShooting(EnemyBullet6, MainPanel);
            #endregion
            //***************************************
            #region BulletMovingAndColliding
            SpawnMovingBullet();
            BulletCollideWithBlok();
            BulletCollideWithMainPanel();
            BulletCollideWithTank();
            #endregion
            //***************************************
            BonusCollideWithTanks();
            //***************************************
            UpdatePictures();
            //***************************************
            EndGameCheck();
            //***************************************
        }

        //Zapis wyników do pliku
        private void File_Record(string PlayerName, int Points, string PlayerName2, int Points2, string FileLocalization)
        {
            try
            {
                List<Record> FileRec = new List<Record>();
                using (StreamReader streamR = new StreamReader((FileLocalization)))
                {
                    string Record;
                    string[] RecordS;
                    while ((Record = streamR.ReadLine()) != null)
                    {
                        RecordS = Record.Split('֎');
                        if (PlayerName2 == "" && Points2 == -1)
                            FileRec.Add(new Record(RecordS[0], int.Parse(RecordS[1])));
                        else
                            FileRec.Add(new Record(RecordS[0], int.Parse(RecordS[1]), RecordS[2], int.Parse(RecordS[3])));
                    }
                }
                if (PlayerName2 == "" && Points2 == -1)
                    FileRec.Add(new Record(PlayerName, Points));
                else
                    FileRec.Add(new Record(PlayerName, Points, PlayerName2, Points2));

                var SortedFileRec = from item in FileRec select item;

                if (PlayerName2 == "" && Points2 == -1)
                {

                    SortedFileRec =
                    from item in FileRec
                    orderby item.Points descending
                    select item;
                }
                else
                {
                    SortedFileRec =
                    from item in FileRec
                    orderby item.Points + item.Points2 descending
                    select item;
                }

                FileRec = SortedFileRec.ToList();
                System.IO.File.WriteAllText(FileLocalization, string.Empty);
                using (StreamWriter streamW = new StreamWriter((FileLocalization), true))
                {
                    for (int i = 0; i < FileRec.Count; i++)
                    {
                        if (i == 10)
                        {
                            break;
                        }
                        if (PlayerName2 == "" && Points2 == -1) streamW.WriteLine(FileRec[i].Name + "֎" + FileRec[i].Points);
                        else streamW.WriteLine($"{FileRec[i].Name}֎{FileRec[i].Points}֎{FileRec[i].Name2}֎{FileRec[i].Points2}");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ups, coś poszło nie tak. Na pewno masz wszystkie pliki?", "Nie dodano nowego rekordu");
            }
        }
        private void FileUpdate_Single(string PlayerName, int Points)
        {
            File_Record(PlayerName, Points, "", -1, "Assets/Files/Scores_Single.txt");
        }
        private void FileUpdate_Multi_DM(string PlayerName, int Points)
        {
            File_Record(PlayerName, Points, "", -1, "Assets/Files/Scores_Multi_DM.txt");
        }
        private void FileUpdate_Multi_TDM(string PlayerName, int Points, string PlayerName2, int Points2)
        {
            File_Record(PlayerName, Points, PlayerName2, Points2, "Assets/Files/Scores_Multi_TDM.txt");
        }
    }
}