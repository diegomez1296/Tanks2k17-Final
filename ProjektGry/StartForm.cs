using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using System.Diagnostics;

namespace ProjektGry
{
    public partial class StartForm : Form
    {
        WindowsMediaPlayer Background_Music = new WindowsMediaPlayer();

        Enemy StartEnemy1, StartEnemy2, StartEnemy3, StartEnemy4, StartEnemy5, StartEnemy6;
        Enemy HTPEnemy1, HTPEnemy2, HTPEnemy3, HTPEnemy4, HTPEnemy5, HTPEnemy6;
        Tank P1, P2;
        Bullet BulletMethods = new Bullet();
        PictureBox Title = new PictureBox();
        Panel PrzyciskiRamka = new Panel();
        Button Tab0 = new Button(); //Dla pierwszego taba

        public static string Player1_Nick = "Tank1";
        public static string Player2_Nick = "Tank2";
        readonly ushort MaxTankColorChoice = 6; // maksymalny indeks dla koloru czolgu, zmieniamy gdy dodajemy nowy kolor czolgu.
        public static ushort Tank1ColorChoice = 1;
        public static ushort Tank2ColorChoice = 3;
        public static bool FriendlyFireOption = false;
        public static int GameMode = 0; //0 - DM, 1-TDM
        public static int Selected_Map = 0;
        public static bool ActualPlayer2OptionActive;
        public static int ActualBackgroundMusic;

        public int MusicBack = 0;
        public int MusicGame = 0;
        public int Volume = 0;
        
        public bool SaveOptions = false;

        public static ushort ActualInfo = 0;
        const ushort MaxInfo = 7;

        public bool P1Fire, P2Fire;
        public static int P1Reload = 100, P2Reload = 100;

        public static int DifficultyLevelOption = 2;

        public static byte Ranking_Radio_Option = 0;
        public static string FileLocalization = "";

        public static bool HTP_FirstClick = false;

        //AddonMap
        AddonMap AMap = null;

        public StartForm()
        {
            StartEnemy1 = new Enemy(2, new Point(0, 200), 1);
            StartEnemy2 = new Enemy(2, new Point(520, 200), 2);
            StartEnemy3 = new Enemy(2, new Point(0, 353), 3);
            StartEnemy4 = new Enemy(2, new Point(520, 353), 4);
            StartEnemy5 = new Enemy(2, new Point(0, 517), 5);
            StartEnemy6 = new Enemy(2, new Point(520, 517), 6);

            InitializeComponent();

            #region WczytanieOpcji
            try
            {

                using (StreamReader streamR = new StreamReader(("Assets/Files/config.txt"), true))
                {
                    string Option;
                    string[] OptionS;
                    ushort AktOpt = 0;
                    while ((Option = streamR.ReadLine()) != null)
                    {
                        OptionS = Option.Split(' ');
                        switch (AktOpt)
                        {
                            case 0:
                                MusicBack = int.Parse(OptionS[1]);
                                AktOpt++;
                                break;
                            case 1:
                                MusicGame = int.Parse(OptionS[1]);
                                AktOpt++;
                                break;
                            case 2:
                                Background_Music.settings.volume = int.Parse(OptionS[1]);
                                AktOpt++;
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            catch (Exception)
            {
                MusicBack = 0;
                MusicGame = 0;
                Background_Music.settings.volume = 0;

            }
            MusicBackground(MusicBack);
            ActualBackgroundMusic = MusicBack;
            comboBox_BackMusic.SelectedIndex = ActualBackgroundMusic;
            #endregion

            Tab0.TabIndex = 1;
            Tab0.Location = new Point(-1000, -1000);
            this.Controls.Add(Tab0);

            Title.Size = new Size(500, 153);
            Title.Location = new Point(50, 40);
            Title.BackgroundImage = Image.FromFile("Assets/Textures/TANKS.gif");
            this.Controls.Add(Title);

            PrzyciskiRamka.Size = new Size(200, 317);
            PrzyciskiRamka.Location = new Point(200, 253);
            PrzyciskiRamka.BackgroundImage = Image.FromFile("Assets/Buttons/ramka.gif");
            this.Controls.Add(PrzyciskiRamka);

            B_NewGame.BackgroundImage = Image.FromFile("Assets/Buttons/NEWGAME2.gif");
            B_Ranking.BackgroundImage = Image.FromFile("Assets/Buttons/RANKING2.gif");
            B_HTP.BackgroundImage = Image.FromFile("Assets/Buttons/HELP2.gif");
            B_Options.BackgroundImage = Image.FromFile("Assets/Buttons/OPTIONS2.gif");
            B_Exit.BackgroundImage = Image.FromFile("Assets/Buttons/EXIT2.gif");

            StartPanel.Size = new Size(520, 337);
            StartPanel.Location = new Point(40, 253);
            StartPanel.BringToFront();

            #region Dodawanie Enemy do StartForma
            this.Controls.Add(StartEnemy1.CharImageBox);
            this.Controls.Add(StartEnemy2.CharImageBox);
            this.Controls.Add(StartEnemy3.CharImageBox);
            this.Controls.Add(StartEnemy4.CharImageBox);
            this.Controls.Add(StartEnemy5.CharImageBox);
            this.Controls.Add(StartEnemy6.CharImageBox);
            #endregion

        }

        private void StartFormT_Tick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NickP1.Text) && !String.IsNullOrWhiteSpace(NickP2.Text))
            {
                B_START.Enabled = true;
            }
            else
            {
                B_START.Enabled = false;
            }
            P1Reload++;
            P2Reload++;

            StartEnemy1.Moving(StartPanel);
            StartEnemy2.Moving(StartPanel);
            StartEnemy3.Moving(StartPanel);
            StartEnemy4.Moving(StartPanel);
            StartEnemy5.Moving(StartPanel);
            StartEnemy6.Moving(StartPanel);

            if (HTP_PlayGround_Panel.Visible)
            {
                if (P2.IsMoving) P2.MovingInOption(HTP_PlayGround_Panel);
                if (P2Fire == true) CreateBullet(P2, P2Bullet);
                BulletMethods.BulletHideWithEnemy(P2, P2Bullet, P1);
                BulletMethods.BulletHideWithMainPanel(P2, P2Bullet, HTP_PlayGround_Panel);

                if (P1.IsMoving) P1.MovingInOption(HTP_PlayGround_Panel);
                if (P1Fire == true) CreateBullet(P1, P1Bullet);
                BulletMethods.BulletHideWithEnemy(P1, P1Bullet, P2);
                BulletMethods.BulletHideWithMainPanel(P1, P1Bullet, HTP_PlayGround_Panel);

            }
            
            if (HowToPlay_Enemies_Panel.Visible)
            {
                HTPEnemy1.Moving(HowToPlay_Enemies_Panel);
                HTPEnemy2.Moving(HowToPlay_Enemies_Panel);
                HTPEnemy3.Moving(HowToPlay_Enemies_Panel);
                HTPEnemy4.Moving(HowToPlay_Enemies_Panel);
                HTPEnemy5.Moving(HowToPlay_Enemies_Panel);
                HTPEnemy6.Moving(HowToPlay_Enemies_Panel);
            }

        }
      
        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Buttons_ChangeColors

        private void B_NewGame_MouseHover(object sender, EventArgs e)
        {
            B_NewGame.BackgroundImage = Image.FromFile("Assets/Buttons/NEWGAME1.gif");
        }

        private void B_Ranking_MouseHover(object sender, EventArgs e)
        {
            B_Ranking.BackgroundImage = Image.FromFile("Assets/Buttons/RANKING1.gif");
        }

        private void B_Credits_MouseHover(object sender, EventArgs e)
        {
            B_HTP.BackgroundImage = Image.FromFile("Assets/Buttons/HELP1.gif");
        }

        private void B_Options_MouseHover(object sender, EventArgs e)
        {
            B_Options.BackgroundImage = Image.FromFile("Assets/Buttons/OPTIONS1.gif");
        }

        private void B_Exit_MouseHover(object sender, EventArgs e)
        {
            B_Exit.BackgroundImage = Image.FromFile("Assets/Buttons/EXIT1.gif");
        }

        //****

        private void B_NewGame_MouseLeave(object sender, EventArgs e)
        {
            B_NewGame.BackgroundImage = Image.FromFile("Assets/Buttons/NEWGAME2.gif");
        }

        private void B_Ranking_MouseLeave(object sender, EventArgs e)
        {
            B_Ranking.BackgroundImage = Image.FromFile("Assets/Buttons/RANKING2.gif");
        }

        private void B_Credits_MouseLeave(object sender, EventArgs e)
        {
            B_HTP.BackgroundImage = Image.FromFile("Assets/Buttons/HELP2.gif");
        }

        private void B_Options_MouseLeave(object sender, EventArgs e)
        {
            B_Options.BackgroundImage = Image.FromFile("Assets/Buttons/OPTIONS2.gif");
        }

        private void B_Exit_MouseLeave(object sender, EventArgs e)
        {
            B_Exit.BackgroundImage = Image.FromFile("Assets/Buttons/EXIT2.gif");
        }

        #endregion

        #region Tanks_ChangeColor
        private void T1LeftB_Click(object sender, EventArgs e)
        {
            if (Tank1ColorChoice == 1)
            {
                Tank1ColorChoice = MaxTankColorChoice;
            }
            else
            {
                Tank1ColorChoice--;
            }
            TankColorSwitch();
        }

        private void T1RightB_Click(object sender, EventArgs e)
        {
            if (Tank1ColorChoice == MaxTankColorChoice)
            {
                Tank1ColorChoice = 1;
            }
            else
            {
                Tank1ColorChoice++;
            }
            TankColorSwitch();
        }

        private void T2LeftB_Click(object sender, EventArgs e)
        {
            if (Tank2ColorChoice == 1)
            {
                Tank2ColorChoice = MaxTankColorChoice;
            }
            else
            {
                Tank2ColorChoice--;
            }
            TankColorSwitch();


        }

        private void T2RightB_Click(object sender, EventArgs e)
        {
            if (Tank2ColorChoice == MaxTankColorChoice)
            {
                Tank2ColorChoice = 1;
            }
            else
            {
                Tank2ColorChoice++;
            }
            TankColorSwitch();
        }

        private void TankColorSwitch()
        {
            switch (Tank1ColorChoice)
            {
                case 1: // ZÓŁTY
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T1_Up.gif");
                        break;
                    }
                case 2: //ZIELONY
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                        break;
                    }
                case 3: // CZERWONY
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T3_Up.gif");
                        break;
                    }
                case 4: // NIEBIESKI
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T4_Up.gif");
                        break;
                    }
                case 5: //FIOLETOWY
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T5_Up.gif");
                        break;
                    }
                case 6: // BŁĘKITNY
                    {
                        Tank1Color.Image = Image.FromFile("Assets/Textures/TanksColors/T6_Up.gif");
                        break;
                    }
                default:
                    break;
            }


            switch (Tank2ColorChoice)
            {
                case 1: // ZÓŁTY
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T1_Up.gif");
                        break;
                    }
                case 2: //ZIELONY
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                        break;
                    }
                case 3: // CZERWONY
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T3_Up.gif");
                        break;
                    }
                case 4: // NIEBIESKI
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T4_Up.gif");
                        break;
                    }
                case 5: //FIOLETOWY
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T5_Up.gif");
                        break;
                    }
                case 6: // BŁĘKITNY
                    {
                        Tank2Color.Image = Image.FromFile("Assets/Textures/TanksColors/T6_Up.gif");
                        break;
                    }
                default:
                    break;
            }
        }

        #endregion

        #region Przyciski_NewGame
        private void SetFirstColorTank(ushort TankColorChoice, PictureBox TankColor)
        {
            switch (TankColorChoice)
            {
                case 1:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T1_Up.gif");
                    break;
                case 2:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                    break;
                case 3:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T3_Up.gif");
                    break;
                case 4:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T4_Up.gif");
                    break;
                case 5:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T5_Up.gif");
                    break;
                case 6:
                    TankColor.Image = Image.FromFile("Assets/Textures/TanksColors/T6_Up.gif");
                    break;
            }
        }

        private void B_NewGame_Click(object sender, EventArgs e)
        {
            StartPanel.BackgroundImage = Image.FromFile("Assets/Buttons/tło_pod_wybor_mapy520x317.gif");
            StartPanel.Visible = true;
            NickP1.Text = Player1_Nick;
            NickP2.Text = Player2_Nick;
            SetFirstColorTank(Tank1ColorChoice, Tank1Color);
            SetFirstColorTank(Tank2ColorChoice, Tank2Color);
            C_Map.SelectedIndex = Selected_Map;

            if (ActualPlayer2OptionActive)
            {
                Player2ActiveGame.Checked = true;
                if (GameMode == 0)
                {
                    DM_radiob.Checked = true;
                    TDM_radiob.Checked = false;
                }
                else if (GameMode == 1)
                {
                    DM_radiob.Checked = false;
                    TDM_radiob.Checked = true;
                    if (!FriendlyFireOption) checkB_FriendlyFire.Checked = false;
                    else checkB_FriendlyFire.Checked = true;
                }
            }
            else
            {
                Player2ActiveGame.Checked = false;
            }

            T1LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo2.gif");
            T1RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo2.gif");
            T2LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo2.gif");
            T2RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo2.gif");

            BackToMainB.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            B_START.Image = Image.FromFile("Assets/Buttons/START2.gif");

            DiffLvlBack_Button.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            DiffLvl_TrackBar.Value = DifficultyLevelOption;
            DiffLvl_Labels_Options(DifficultyLevelOption);          
        }
        private void B_START_Click(object sender, EventArgs e)
        {
            if (Player2ActiveGame.Checked)
            {
                ActualPlayer2OptionActive = true;
            }
            else
            {
                ActualPlayer2OptionActive = false;
            }

            Background_Music.controls.stop();
            Player1_Nick = NickP1.Text;
            Player2_Nick = NickP2.Text;
            FriendlyFireOption = checkB_FriendlyFire.Checked;
            Form1 form1 = new Form1(NickP1.Text, NickP2.Enabled, NickP2.Text, C_Map.SelectedIndex, Tank1ColorChoice, Tank2ColorChoice, GameMode, checkB_FriendlyFire.Checked, DiffLvl_TrackBar.Value, MusicGame, Background_Music.settings.volume, AMap);
            //AddonMap
            if (C_Map.SelectedIndex == 4)
            {
                C_Map.SelectedIndex = 0;
                Selected_Map = C_Map.SelectedIndex;
            }


            //

            form1.Show();
            this.Hide();
        }
        private void BackToMainB_Click(object sender, EventArgs e)
        {
            StartPanel.Visible = false;
        }
        private void C_Map_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (C_Map.SelectedIndex)
            {
                /* case 0: TestArea
                     {
                         CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/Map0.jpg");
                         break;
                     }*/
                case 0:
                    {
                        CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/Map1.jpg");
                        break;
                    }
                case 1:
                    {
                        CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/Map2.jpg");
                        break;
                    }
                case 2:
                    {
                        CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/Map3.jpg");
                        break;
                    }
                case 3:
                    {
                        CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/Map4.jpg");
                        break;
                    }
                case 4:
                    {
                        if (openFileDialog_LoadMap.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            System.IO.StreamReader sr = new
                               System.IO.StreamReader(openFileDialog_LoadMap.FileName);

                            string AddonMap_Option;
                            string[] AddonMap_OptionSplit;
                            string AddonMap_PathSplitt;

                            

                            Color AMap_Background;
                            int AMap_BonusChange;
                            Point[] AMap_Tanks_Spawn = new Point[8];
                            List<Block> AddonMap_BlockList = new List<Block>();

                            AMap_Background = Color.FromArgb(int.Parse(sr.ReadLine()));
                            AMap_BonusChange = int.Parse(sr.ReadLine());
                            for (int i = 0; i < AMap_Tanks_Spawn.Length; i++)
                            {
                                string[] AddonMap_SpawnSplit = sr.ReadLine().Split(';');
                                int SpawnX = int.Parse(AddonMap_SpawnSplit[0]);
                                int SpawnY = int.Parse(AddonMap_SpawnSplit[1]);
                                AMap_Tanks_Spawn[i] = new Point(SpawnX, SpawnY);

                            }

                            while ((AddonMap_Option = sr.ReadLine()) != null)
                            {
                                bool isBlock = true;
                                AddonMap_OptionSplit = AddonMap_Option.Split(';');

                                Block am_tmp = new Block();

                                switch (AddonMap_OptionSplit[0])
                                {
                                    case "DEFAULT":
                                        isBlock = false;
                                        break;
                                    case "DESTROYABLE":
                                        am_tmp.IsCollidingWithBullet = true;
                                        am_tmp.IsCollidingWithTanks = true;
                                        am_tmp.IsDestroyable = true;
                                        am_tmp.IsWater = false;
                                        am_tmp.Bloczek.BackColor = AMap_Background;
                                        break;
                                    case "UNDESTROYABLE":
                                        am_tmp.IsCollidingWithBullet = true;
                                        am_tmp.IsCollidingWithTanks = true;
                                        am_tmp.IsDestroyable = false;
                                        am_tmp.IsWater = false;
                                        am_tmp.Bloczek.BackColor = AMap_Background;
                                        break;
                                    case "LIQUID":
                                        am_tmp.IsCollidingWithBullet = false;
                                        am_tmp.IsCollidingWithTanks = true;
                                        am_tmp.IsDestroyable = false;
                                        am_tmp.IsWater = true;
                                        break;
                                    case "GREEN":
                                        am_tmp.IsCollidingWithBullet = false;
                                        am_tmp.IsCollidingWithTanks = false;
                                        am_tmp.IsDestroyable = false;
                                        am_tmp.IsWater = false;
                                        break;
                                }
                                if (isBlock)
                                {
                                    AddonMap_PathSplitt = AddonMap_OptionSplit[1].Replace('\\', '/');

                                    am_tmp.Bloczek.Image = Image.FromFile(AddonMap_PathSplitt);
                                    am_tmp.Bloczek.Location = new Point(int.Parse(AddonMap_OptionSplit[2])-5, int.Parse(AddonMap_OptionSplit[3])-5);

                                    AddonMap_BlockList.Add(am_tmp);
                                }
                             }
                            AMap = new AddonMap(AMap_Background, AMap_BonusChange, AMap_Tanks_Spawn, AddonMap_BlockList);

                        }
                        CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/ScreenX.png");
                        break;
                    }
                default:
                    CurrentMapImgBox.Image = Image.FromFile("Assets/MapScreens/ScreenX.png");
                    break;

                    
            }
            Selected_Map = C_Map.SelectedIndex;
        }
        private void Player2ActiveGame_CheckedChanged(object sender, EventArgs e)
        {
            NickP2.Enabled = !NickP2.Enabled;
            Tank2Color.Visible = !Tank2Color.Visible;

            DM_radiob.Visible = !DM_radiob.Visible;
            TDM_radiob.Visible = !TDM_radiob.Visible;

            if (DM_radiob.Checked && Player2ActiveGame.Checked)
            {
                PB_Player1.BackColor = Color.LimeGreen;
                PB_Player2.BackColor = Color.DarkRed;
            }
            else if (TDM_radiob.Checked && Player2ActiveGame.Checked)
            {
                PB_Player1.BackColor = Color.LimeGreen;
                PB_Player2.BackColor = Color.LimeGreen;
            }
            else
            {
                PB_Player1.BackColor = Color.Transparent;
                PB_Player2.BackColor = Color.Transparent;
            }

        }

        private void DM_radiob_CheckedChanged(object sender, EventArgs e)
        {
            GameMode = 0;
            PB_Player2.BackColor = Color.DarkRed;
            checkB_FriendlyFire.Visible = false;         
        }

        private void TDM_radiob_CheckedChanged(object sender, EventArgs e)
        {
            GameMode = 1;
            PB_Player2.BackColor = Color.LimeGreen;
            checkB_FriendlyFire.Visible = true;
        }

        private void DifficultyLvl_Button_Click(object sender, EventArgs e)
        {
            DiffLvl_Panel.Location = new Point(0, 0);
            DiffLvl_Panel.Show();
            DiffOptionColor_panel.Hide();
        }

        private void DiffLvlBack_Button_Click(object sender, EventArgs e)
        {
            DiffLvl_Panel.Hide();
            DiffOptionColor_panel.Show();
        }

        private void Regular_DiffLvl_Labels()
        {
            DiffLabel_VeryHard.Font = new Font("High Tower Text", 15.75F, FontStyle.Regular);
            DiffLabel_Hard.Font = new Font("High Tower Text", 15.75F, FontStyle.Regular);
            DiffLabel_Normal.Font = new Font("High Tower Text", 15.75F, FontStyle.Regular);
            DiffLabel_Easy.Font = new Font("High Tower Text", 15.75F, FontStyle.Regular);
            DiffLabel_VeryEasy.Font = new Font("High Tower Text", 15.75F, FontStyle.Regular);
        }

        private void DiffLvl_Labels_Options(int Value)
        {
            switch (Value)
            {
                case 0:
                    DifficultyLevelOption = 0;
                    Regular_DiffLvl_Labels();
                    DiffLabel_VeryEasy.Font = new Font("High Tower Text", 15.75F, FontStyle.Bold);
                    DiffLvl_Value_Label.Text = "Very Easy";
                    DiffLvl_Value_Label.ForeColor = Color.LimeGreen;
                    DiffOptionColor_panel.BackColor = Color.LimeGreen;
                    break;
                case 1:
                    DifficultyLevelOption = 1;
                    Regular_DiffLvl_Labels();
                    DiffLabel_Easy.Font = new Font("High Tower Text", 15.75F, FontStyle.Bold);
                    DiffLvl_Value_Label.Text = "Easy";
                    DiffLvl_Value_Label.ForeColor = Color.GreenYellow;
                    DiffOptionColor_panel.BackColor = Color.GreenYellow;
                    break;
                case 2:
                    DifficultyLevelOption = 2;
                    Regular_DiffLvl_Labels();
                    DiffLabel_Normal.Font = new Font("High Tower Text", 15.75F, FontStyle.Bold);
                    DiffLvl_Value_Label.Text = "Normal";
                    DiffLvl_Value_Label.ForeColor = Color.Yellow;
                    DiffOptionColor_panel.BackColor = Color.Yellow;
                    break;
                case 3:
                    DifficultyLevelOption = 3;
                    Regular_DiffLvl_Labels();
                    DiffLabel_Hard.Font = new Font("High Tower Text", 15.75F, FontStyle.Bold);
                    DiffLvl_Value_Label.Text = "Hard";
                    DiffLvl_Value_Label.ForeColor = Color.OrangeRed;
                    DiffOptionColor_panel.BackColor = Color.OrangeRed;
                    break;
                case 4:
                    DifficultyLevelOption = 4;
                    Regular_DiffLvl_Labels();
                    DiffLabel_VeryHard.Font = new Font("High Tower Text", 15.75F, FontStyle.Bold);
                    DiffLvl_Value_Label.Text = "Very Hard";
                    DiffLvl_Value_Label.ForeColor = Color.Red;
                    DiffOptionColor_panel.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
        }

        private void DiffLvl_TrackBar_Scroll(object sender, EventArgs e)
        {
            DiffLvl_Labels_Options(DiffLvl_TrackBar.Value);
        }

        #endregion

        #region Przyciski_START_COLORS
        private void T1LeftB_MouseHover(object sender, EventArgs e)
        {
            T1LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo1.gif");
            InfoLabel.Text = "Choose appearance for Player1's Tank";
        }

        private void T1LeftB_MouseLeave(object sender, EventArgs e)
        {
            T1LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo2.gif");
            Clr();
        }

        private void T1RightB_MouseHover(object sender, EventArgs e)
        {
            T1RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo1.gif");
            InfoLabel.Text = "Choose appearance for Player1's Tank";
        }

        private void T1RightB_MouseLeave(object sender, EventArgs e)
        {
            T1RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo2.gif");
            Clr();
        }

        private void T2LeftB_MouseHover(object sender, EventArgs e)
        {
            T2LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo1.gif");
            InfoLabel.Text = "Choose appearance for Player2's Tank";
        }

        private void T2LeftB_MouseLeave(object sender, EventArgs e)
        {
            T2LeftB.Image = Image.FromFile("Assets/Buttons/strzalka lewo2.gif");
            Clr();
        }

        private void T2RightB_MouseHover(object sender, EventArgs e)
        {
            T2RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo1.gif");
            InfoLabel.Text = "Choose appearance for Player2's Tank";
        }

        private void T2RightB_MouseLeave(object sender, EventArgs e)
        {
            T2RightB.Image = Image.FromFile("Assets/Buttons/strzalka prawo2.gif");
            Clr();
        }

        private void BackToMainB_MouseHover(object sender, EventArgs e)
        {
            BackToMainB.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
            InfoLabel.Text = "Return to Main Menu";
        }

        private void BackToMainB_MouseLeave(object sender, EventArgs e)
        {
            BackToMainB.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            Clr();
        }

        private void B_START_MouseHover(object sender, EventArgs e)
        {
            B_START.Image = Image.FromFile("Assets/Buttons/START1.gif");
            InfoLabel.Text = "Start the Game";
        }

        private void B_START_MouseLeave(object sender, EventArgs e)
        {
            B_START.Image = Image.FromFile("Assets/Buttons/START2.gif");
            Clr();
        }

        private void DiffLvlBack_Button_MouseHover(object sender, EventArgs e)
        {
            DiffLvlBack_Button.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
        }

        private void DiffLvlBack_Button_MouseLeave(object sender, EventArgs e)
        {
            DiffLvlBack_Button.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
        }

        private void DifficultyLvl_Button_MouseHover(object sender, EventArgs e)
        {

            InfoLabel.Text = $"Difficulty Level: {DiffLvl_Value_Label.Text}";

        }

        private void DifficultyLvl_Button_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        #endregion

        #region Przyciski_Ranking + KOLORY


        private void Ranking_Fonts()
        {
            Radio_Single_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Regular);
            Radio_DM_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Regular);
            Radio_TDM_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Regular);
            switch (Ranking_Radio_Option)
            {
                case 0:
                    Radio_Single_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Bold);
                    Radio_Single_Ranking.Checked = true;
                    break;
                case 1:
                    Radio_DM_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Bold);
                    Radio_DM_Ranking.Checked = true;
                    break;
                case 2:
                    Radio_TDM_Ranking.Font = new Font("High Tower Text", 12, FontStyle.Bold);
                    Radio_TDM_Ranking.Checked = true;
                    break;
                default:
                    break;
            }
        }
        private void Ranking_Records_Change()
        {
            Ranking_Fonts();
            switch (Ranking_Radio_Option)
            {
                case 0:
                    FileLocalization = "Assets/Files/Scores_Single.txt";
                    break;
                case 1:
                    FileLocalization = "Assets/Files/Scores_Multi_DM.txt";
                    break;
                case 2:
                    FileLocalization = "Assets/Files/Scores_Multi_TDM.txt";
                    break;
                default:
                    FileLocalization = "Assets/Files/Scores_Single.txt";
                    break;
            }

            RecordListBox.Items.Clear();
            RecordListBox.Show();
            EmptyFIleLabel.Hide();
            try
            {

                using (StreamReader streamR = new StreamReader((FileLocalization), true))
                {
                    string Record;
                    string[] RecordS;
                    string rank = "";
                    while ((Record = streamR.ReadLine()) != null)
                    {
                        RecordS = Record.Split('֎');
                        if (Ranking_Radio_Option == 2)
                            rank = rank + RecordS[0] + "    " + RecordS[1] + "    " + "&" + "    " + RecordS[2] + "    " + RecordS[3];
                        else
                            rank = rank + RecordS[0] + "    " + RecordS[1];
                        RecordListBox.Items.Add(rank);
                        rank = "";
                    }
                    if (RecordListBox.Items.Count == 0)
                    {

                        //dodanie labela na ekran, pusty listbox
                        EmptyFIleLabel.Show();
                        EmptyFIleLabel.Text = "Brak rekordów do wyświetlenia!";
                        RecordListBox.Hide();
                    }
                }
                RecordsPanel.Size = new Size(520, 317);
                RecordsPanel.Location = new Point(40, 253);
                ClearScores.Image = Image.FromFile("Assets/Buttons/CLEAR_RECORDS2.gif");
                RecordBackButton.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
                RecordsPanel.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ups, brak pliku z rekordami :(. \nWyłącz grę i uruchom ją jeszcze raz, aby stworzyć plik.", "Error detected");
                try
                {
                    File.Create(FileLocalization);
                }
                catch (Exception)
                {
                    MessageBox.Show("Miałeś wyłączyć grę!", "Wykonuj polecenia!");
                }

            }
        }

        private void B_Ranking_Click(object sender, EventArgs e)
        {

            Ranking_Records_Change();
        }

        private void Radio_Single_Ranking_Click(object sender, EventArgs e)
        {
            Ranking_Radio_Option = 0;
            Ranking_Records_Change();
        }

        private void Radio_DM_Ranking_Click(object sender, EventArgs e)
        {
            Ranking_Radio_Option = 1;
            Ranking_Records_Change();
        }

        private void Radio_TDM_Ranking_Click(object sender, EventArgs e)
        {
            Ranking_Radio_Option = 2;
            Ranking_Records_Change();
        }

        private void RecordBackButton_Click(object sender, EventArgs e)
        {
            RecordsPanel.Visible = false;
        }
        private void ClearScores_Click(object sender, EventArgs e)
        {
            DialogResult CheckDevision = MessageBox.Show("Jesteś pewny, że chcesz usunąć wszystkie rekordy?",
                      "Hmm?", MessageBoxButtons.YesNo);
            switch (CheckDevision)
            {
                case DialogResult.Yes:
                    {
                        File.WriteAllText(FileLocalization, string.Empty);
                        RecordListBox.Items.Clear();
                        EmptyFIleLabel.Show();
                        EmptyFIleLabel.Text = "Pomyślnie usunięto wszystkie rekordy!";
                        RecordListBox.Hide();
                    }
                    break;
                case DialogResult.No: break;
            }
        }

        private void ClearScores_MouseHover(object sender, EventArgs e)
        {
            ClearScores.Image = Image.FromFile("Assets/Buttons/CLEAR_RECORDS1.gif");
        }

        private void ClearScores_MouseLeave(object sender, EventArgs e)
        {
            ClearScores.Image = Image.FromFile("Assets/Buttons/CLEAR_RECORDS2.gif");
        }

        private void RecordBackButton_MouseHover(object sender, EventArgs e)
        {
            RecordBackButton.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
        }

        private void RecordBackButton_MouseLeave(object sender, EventArgs e)
        {
            RecordBackButton.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
        }


        #endregion

        #region HowToPlay

        #region Przyciski_HTP + ZMIANA KOLORU

        private void ClearMenuStripOptions()
        {
            HowToPlay_Control_Panel.Hide();
            HowToPlay_Enemies_Panel.Hide();
            HowToPlay_Bonuses_Panel.Hide();
            HowToPlay_Credits_Panel.Hide();

            Controls_ToolStripMenuItem.BackColor = Color.Transparent;
            Enemies_ToolStripMenuItem.BackColor = Color.Transparent;
            Bonuses_ToolStripMenuItem.BackColor = Color.Transparent;
            Authors_ToolStripMenuItem.BackColor = Color.Transparent;

            Controls_ToolStripMenuItem.Font = new Font("Segoe UI", 9,FontStyle.Regular);
            Enemies_ToolStripMenuItem.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            Bonuses_ToolStripMenuItem.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            Authors_ToolStripMenuItem.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        }
        
        private void B_HTP_Click(object sender, EventArgs e)
        {
            Panel_HTP.Location = new Point(40, 253);
            HTP_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Enemies_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Bonuses_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Credits_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            ClearMenuStripOptions();
            Controls_ToolStripMenuItem.BackColor = Color.Gray;
            Controls_ToolStripMenuItem.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            HowToPlay_Control_Panel.Location = new Point(0, 24);
            Panel_HTP.Show();
            HowToPlay_Control_Panel.Show();

            Random rand = new Random();
            try
            {
                HTP_PlayGround_Panel.Controls.Remove(P1.CharImageBox);
                HTP_PlayGround_Panel.Controls.Remove(P2.CharImageBox);

            }
            catch (Exception)
            {


            }
            P1 = new Tank(ushort.Parse((rand.Next(1, 7).ToString())));
            P2 = new Tank(ushort.Parse((rand.Next(1, 7).ToString())));

            HTP_PlayGround_Panel.Controls.Add(P1.CharImageBox);
            HTP_PlayGround_Panel.Controls.Add(P2.CharImageBox);

            P1.CharImageBox.Location = new Point(435, 120);
            P2.CharImageBox.Location = new Point(5, 5);
        }
        private void Options_ToolStripMenuItems(ToolStripMenuItem Item, Panel HowToPlay_Panel)
        {
            ClearMenuStripOptions();
            Item.BackColor = Color.Gray;
            Item.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            HowToPlay_Panel.Location = new Point(0, 24);
            HowToPlay_Panel.Show();
        }
        private void Controls_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_ToolStripMenuItems(Controls_ToolStripMenuItem, HowToPlay_Control_Panel);
        }

        private void Enemies_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_ToolStripMenuItems(Enemies_ToolStripMenuItem, HowToPlay_Enemies_Panel);
            try
            {
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy1.CharImageBox);
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy2.CharImageBox);
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy3.CharImageBox);
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy4.CharImageBox);
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy5.CharImageBox);
                HowToPlay_Enemies_Panel.Controls.Remove(HTPEnemy6.CharImageBox);
            }
            catch (Exception)
            {

               
            }
            HTPEnemy1 = new Enemy(1, EnemiesTable_Pic1);
            HTPEnemy2 = new Enemy(2, EnemiesTable_Pic2);
            HTPEnemy3 = new Enemy(3, EnemiesTable_Pic3);
            HTPEnemy4 = new Enemy(4, EnemiesTable_Pic4);
            HTPEnemy5 = new Enemy(5, EnemiesTable_Pic5);
            HTPEnemy6 = new Enemy(6, EnemiesTable_Pic6);

        }

        private void Bonuses_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_ToolStripMenuItems(Bonuses_ToolStripMenuItem, HowToPlay_Bonuses_Panel);
        }

        private void Credits_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options_ToolStripMenuItems(Authors_ToolStripMenuItem, HowToPlay_Credits_Panel);
        }

        private void Authors_Label_Click(object sender, EventArgs e)
        {
            StartPanel.Hide();
            RecordsPanel.Hide();
            PanelOptions.Hide();

            if (!HTP_FirstClick)
            {
                B_HTP_Click(sender, e);
                HTP_FirstClick = true;
            }
            Panel_HTP.Show();
            Options_ToolStripMenuItems(Authors_ToolStripMenuItem, HowToPlay_Credits_Panel);
        }

        private void Authors_Label_MouseHover(object sender, EventArgs e)
        {
            Authors_Label.Font = new Font("Tempus Sans ITC", 12, FontStyle.Underline);
            Cursor = Cursors.Hand;
        }

        private void Authors_Label_MouseLeave(object sender, EventArgs e)
        {
            Authors_Label.Font = new Font("Tempus Sans ITC", 12, FontStyle.Regular);
            Cursor = Cursors.Default;
        }


        private void ClrAuthors()
        {
            Authors_Info1_Label.Text = "Thanks for the Playing!";
            Authors_Info2_Label.Text = "                      ~Tanks 2017 Team";
            Cursor = Cursors.Default;
            Pawel_Panel.BackColor = Color.Transparent;
            Lukasz_Panel.BackColor = Color.Transparent;
            Mateusz_Panel.BackColor = Color.Transparent;

        }
        private void Pawel_MouseHover(object sender, EventArgs e)
        {
            Authors_Info1_Label.Text = "Graphic Designer";
            Authors_Info2_Label.Text = "Paweł Minda";
            Pawel_Panel.BackColor = Color.Gold;
            Cursor = Cursors.Hand;
        }

        private void Lukasz_MouseHover(object sender, EventArgs e)
        {
            Authors_Info1_Label.Text = "Game Designer";
            Authors_Info2_Label.Text = "Łukasz Rydziński";
            Cursor = Cursors.Hand;
            Lukasz_Panel.BackColor = Color.Gold;
        }

        private void Mateusz_MouseHover(object sender, EventArgs e)
        {
            Authors_Info1_Label.Text = "Engine Programmer";
            Authors_Info2_Label.Text = "Mateusz Wedeł";
            Cursor = Cursors.Hand;
            Mateusz_Panel.BackColor = Color.Gold;
        }
        private void Pawel_MouseLeave(object sender, EventArgs e)
        {
            ClrAuthors();
        }

        private void Lukasz_MouseLeave(object sender, EventArgs e)
        {
            ClrAuthors();
        }

        private void Mateusz_MouseLeave(object sender, EventArgs e)
        {
            ClrAuthors();
        }

        private void Pawel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.facebook.com/profile.php?id=100001728866242");
            }
            catch { }
        }


        private void Lukasz_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.facebook.com/rlukasz1996");
            }
            catch { }
        }

        private void Mateusz_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.facebook.com/profile.php?id=100001745611361");
            }
            catch { }
        }

        private void Authors_Info2_Label_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.rlukasz1996.cba.pl");
            }
            catch { }
        }

        private void Authors_Info2_Label_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Authors_Info2_Label_MouseLeave(object sender, EventArgs e)
        {
            ClrAuthors();
        }

        public void HTP_Back_Click(object sender, EventArgs e)
        {
            ClearMenuStripOptions();
            Panel_HTP.Hide();
        }

        public void HTP_Back_MouseHover(object sender, EventArgs e)
        {
            HTP_Back.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
            HTP_Enemies_Back.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
            HTP_Bonuses_Back.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
            HTP_Credits_Back.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
        }

        public void HTP_Back_MouseLeave(object sender, EventArgs e)
        {
            HTP_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Enemies_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Bonuses_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            HTP_Credits_Back.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
        }

        private void CreateBullet(Tank Player, PictureBox Bullet)
        {          
            BulletMethods.CreateBullet(Player, Bullet);            
        }
        private void ClearHTP_Colors()
        {
            HowToPlay_W.BackColor = Color.Transparent;
            HowToPlay_S.BackColor = Color.Transparent;
            HowToPlay_A.BackColor = Color.Transparent;
            HowToPlay_D.BackColor = Color.Transparent;
            HowToPlay_Up.BackColor = Color.Transparent;
            HowToPlay_Down.BackColor = Color.Transparent;
            HowToPlay_Left.BackColor = Color.Transparent;
            HowToPlay_Right.BackColor = Color.Transparent;
            HowToPlay_Caps.BackColor = Color.Transparent;
            HowToPlay_Num.BackColor = Color.Transparent;
        }

        #region Player1
        private void HowToPlay_Up_MouseHover(object sender, EventArgs e)
        {
            P1.IsMoving = true;
            P1.Direct = DirectionsOfMoving.UP;
            P1.CharImageBox.Image = Image.FromFile(P1.MovingImage[0]);
            HowToPlay_Up.BackColor = Color.Gray;
        }

        private void HowToPlay_Up_MouseLeave(object sender, EventArgs e)
        {
            P1.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_Left_MouseHover(object sender, EventArgs e)
        {
            P1.IsMoving = true;
            P1.Direct = DirectionsOfMoving.LEFT;
            P1.CharImageBox.Image = Image.FromFile(P1.MovingImage[2]);
            HowToPlay_Left.BackColor = Color.Gray;
        }

        private void HowToPlay_Left_MouseLeave(object sender, EventArgs e)
        {
            P1.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_Down_MouseHover(object sender, EventArgs e)
        {
            P1.IsMoving = true;
            P1.Direct = DirectionsOfMoving.DOWN;
            P1.CharImageBox.Image = Image.FromFile(P1.MovingImage[1]);
            HowToPlay_Down.BackColor = Color.Gray;
        }

        private void HowToPlay_Down_MouseLeave(object sender, EventArgs e)
        {
            P1.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_Right_MouseHover(object sender, EventArgs e)
        {
            P1.IsMoving = true;
            P1.Direct = DirectionsOfMoving.RIGHT;
            P1.CharImageBox.Image = Image.FromFile(P1.MovingImage[3]);
            HowToPlay_Right.BackColor = Color.Gray;
        }

        private void HowToPlay_Right_MouseLeave(object sender, EventArgs e)
        {
            P1.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_Num_MouseHover(object sender, EventArgs e)
        {
            HowToPlay_Num.BackColor = Color.Gray;
        }

        private void HowToPlay_Num_MouseLeave(object sender, EventArgs e)
        {
            ClearHTP_Colors();
        }
        private void HowToPlay_Num_Click(object sender, EventArgs e)
        {
            if (P1Reload >= 50)
            {
                P1.ShootingSound();
                P1Bullet.Location = new Point(P1.CharImageBox.Location.X + 11, P1.CharImageBox.Location.Y + 14);
                P1Bullet.BringToFront();
                P1.BulletMoving = (DirectionsOfBulletMoving)P1.Direct;
                P1Fire = true;
                P1Reload = 0;
            }
        }

        #endregion

        #region Player2
        private void HowToPlay_W_MouseHover(object sender, EventArgs e)
        {
            P2.IsMoving = true;
            P2.Direct = DirectionsOfMoving.UP;
            P2.CharImageBox.Image = Image.FromFile(P2.MovingImage[0]);
            HowToPlay_W.BackColor = Color.Gray;
        }

        private void HowToPlay_A_MouseHover(object sender, EventArgs e)
        {
            P2.IsMoving = true;
            P2.Direct = DirectionsOfMoving.LEFT;
            P2.CharImageBox.Image = Image.FromFile(P2.MovingImage[2]);
            HowToPlay_A.BackColor = Color.Gray;
        }

        private void HowToPlay_S_MouseHover(object sender, EventArgs e)
        {
            P2.IsMoving = true;
            P2.Direct = DirectionsOfMoving.DOWN;
            P2.CharImageBox.Image = Image.FromFile(P2.MovingImage[1]);
            HowToPlay_S.BackColor = Color.Gray;
        }

        private void HowToPlay_D_MouseHover(object sender, EventArgs e)
        {
            P2.IsMoving = true;
            P2.Direct = DirectionsOfMoving.RIGHT;
            P2.CharImageBox.Image = Image.FromFile(P2.MovingImage[3]);
            HowToPlay_D.BackColor = Color.Gray;
        }


        private void HowToPlay_Caps_MouseHover(object sender, EventArgs e)
        {
            HowToPlay_Caps.BackColor = Color.Gray;
        }

        private void HowToPlay_Caps_MouseLeave(object sender, EventArgs e)
        {
            ClearHTP_Colors();
        }

        private void HowToPlay_W_MouseLeave(object sender, EventArgs e)
        {
            P2.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_A_MouseLeave(object sender, EventArgs e)
        {
            P2.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_S_MouseLeave(object sender, EventArgs e)
        {
            P2.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_D_MouseLeave(object sender, EventArgs e)
        {
            P2.IsMoving = false;
            ClearHTP_Colors();
        }

        private void HowToPlay_Caps_Click(object sender, EventArgs e)
        {
            if (P2Reload >= 50)
            {
                P2.ShootingSound();
                P2Bullet.Location = new Point(P2.CharImageBox.Location.X + 11, P2.CharImageBox.Location.Y + 14);
                P2Bullet.BringToFront();
                P2.BulletMoving = (DirectionsOfBulletMoving)P2.Direct;
                P2Fire = true;
                P2Reload = 0;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Przyciski_Options + KOLORY
        private void B_Options_Click(object sender, EventArgs e)
        {
            PanelOptions.Show();
            PanelOptions.Location = new Point(40, 253);

            Volume_Value.Text = Background_Music.settings.volume.ToString();
            trackBar1.Value = Background_Music.settings.volume;
            Save_btn.Image = Image.FromFile("Assets/Buttons/SAVE_CHANGES2.gif");
            BackMusic_btn.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
            SaveOptions = false;
            //comboBox_BackMusic.SelectedIndex = ActualBackgroundMusic;
            comboBox_GameMusic.SelectedIndex = MusicGame;

        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (comboBox_BackMusic.SelectedIndex != -1 || comboBox_GameMusic.SelectedIndex != -1)
            {
                System.IO.File.WriteAllText("Assets/Files/config.txt", string.Empty);
                using (StreamWriter streamW = new StreamWriter(("Assets/Files/config.txt"), true))
                {

                    streamW.WriteLine($"Background_Music: {comboBox_BackMusic.SelectedIndex}");
                    streamW.WriteLine($"Game_Music: {comboBox_GameMusic.SelectedIndex}");
                    streamW.WriteLine($"Volume: {Background_Music.settings.volume}");
                }
                SaveOptions = true;
            }
            else
            {
                MessageBox.Show("Error, unable to save!");
            }

        }

        private void Save_btn_MouseHover(object sender, EventArgs e)
        {
            Save_btn.Image = Image.FromFile("Assets/Buttons/SAVE_CHANGES1.gif");
        }

        private void Save_btn_MouseLeave(object sender, EventArgs e)
        {
            if (!SaveOptions)
                Save_btn.Image = Image.FromFile("Assets/Buttons/SAVE_CHANGES2.gif");
        }

        private void BackMusic_btn_MouseHover(object sender, EventArgs e)
        {
            BackMusic_btn.Image = Image.FromFile("Assets/Buttons/BACK1.gif");
        }

        private void BackMusic_btn_MouseLeave(object sender, EventArgs e)
        {
            BackMusic_btn.Image = Image.FromFile("Assets/Buttons/BACK2.gif");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Background_Music.settings.volume = trackBar1.Value;
            Volume_Value.Text = Background_Music.settings.volume.ToString();
        }

        private void BackMusic_btn_Click(object sender, EventArgs e)
        {
            PanelOptions.Hide();
        }

        private void comboBox_BackMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            MusicBackground(comboBox_BackMusic.SelectedIndex);
        }

        private void comboBox_GameMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            MusicGame = comboBox_GameMusic.SelectedIndex;
        }

        #endregion

        private void B_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void LoadMusic(string URL)
        {
            Background_Music.URL = URL;
            Background_Music.controls.play();
            Background_Music.settings.setMode("loop", true);
        }
        private void MusicBackground(int choose)
        {
            switch (choose)
            {
                case 0:
                    {
                        Background_Music.controls.stop();
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
            ActualBackgroundMusic = choose;
        }

        #region InfoLabel

        private void Clr()
        {
            string Information = "";

            switch (ActualInfo)
            {
                case 0:
                    Information = "How to control a tank? See 'HELP' in the MENU";
                    break;
                case 1:
                    Information = "Destroy the blocks to get bonuses for upgrading the tank";
                    break;
                case 2:
                    Information = "Clashing with a hostile tank will cause loss of life and points";
                    break;
                case 3:
                    Information = "While dealing damage, the appearance of enemy tanks changes";
                    break;
                case 4:
                    Information = "Rocks can not be destroyed - use them as a cover";
                    break;
                case 5:
                    Information = "Water reservoir can be used for distance combat";
                    break;
                case 6:
                    Information = "Watch out for your opponents hiding in the bush";
                    break;
                case 7:
                    Information = "Don't rush! Time doesn't affect the score";
                    break;
                default:
                    break;
            }
            ActualInfo++;
            if (ActualInfo > MaxInfo) ActualInfo = 0;

            InfoLabel.Text = Information;
        }
        //
        private void PB_Player1_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Nickname for Player 1";
        }

        private void PB_Player1_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void NickP1_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Nickname for Player 1";
        }

        private void NickP1_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }
        //
        private void PB_Player2_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Nickname for Player 2";
        }

        private void PB_Player2_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void NickP2_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Nickname for Player 2";
        }

        private void NickP2_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }
        //
        private void PB_Map_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Map";
        }

        private void PB_Map_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void C_Map_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose Map";
        }


        private void C_Map_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void Player2ActiveGame_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Check if you wish play with friend";
        }

        private void Player2ActiveGame_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void Tank1Color_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose appearance for Player1's Tank";
        }

        private void Tank1Color_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void Tank2Color_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Choose appearance for Player2's Tank";
        }

        private void Tank2Color_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void DM_radiob_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Deathmatch. Player vs All";
        }

        private void DM_radiob_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void TDM_radiob_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "Team Deathmatch. Player1 and Player2 vs All";
        }

        private void TDM_radiob_MouseLeave(object sender, EventArgs e)
        {
            Clr();
        }

        private void checkB_FriendlyFire_MouseHover(object sender, EventArgs e)
        {
            InfoLabel.Text = "If checked, Players can destroy each other";
        }

        private void checkB_FriendlyFire_Leave(object sender, EventArgs e)
        {
            Clr();
        }

        #endregion
    }
}