using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ProjektGry
{

    public class Tank : Character
    {
        //private System.Media.SoundPlayer GunShoot = new System.Media.SoundPlayer();
        //private System.Media.SoundPlayer HittedSound = new System.Media.SoundPlayer();
        public string Nickname { get; set; }
        public string[]  MovingImage { get; set; } //0 - UP, 1 - DOWN, 2 - Left, 3 - Right
        public int BulletSpeed { get; set; }
        public int Fire_sek { get; set; }
        public bool HeroBulletStart { get; set; }
        public bool FireHero { get; set; }
       
        public Tank(string nickname, byte speed, int healthPoints, ushort powerOfGun, ushort reloadsek, ushort ColorOfTank)
        {
            isPlayer = true;
            MovingImage = new string[4];
            GetImage(ColorOfTank);
            this.CharImageBox = new PictureBox();
            CharImageBox.Image = Image.FromFile(MovingImage[0]);
            CharImageBox.Size = new Size(30, 35);
            CharImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            CharImageBox.BackColor = Color.Black;
            //CharImageBox.BackgroundImage = Image.FromFile(BackgroundColor);

            this.FireHero = false;
            this.HeroBulletStart = true;
            this.BulletSpeed = 10;

            this.IsActive = true;
            this.Points = 0;
            this.Level = -1;

            Nickname = nickname;
            this.Speed = speed;
            this.HealthPoints = healthPoints;
            PowerOfGun = powerOfGun;
            ReloadSek = reloadsek;

            
        }
        public Tank(ushort ColorOfTank) //ControlTank
        {
            isPlayer = true;
            MovingImage = new string[4];
            this.Speed = 1;
            this.ReloadSek = 2;
            this.HealthPoints = 999999999;
            GetImage(ColorOfTank);
            this.CharImageBox = new PictureBox();
            CharImageBox.Image = Image.FromFile(MovingImage[0]);
            CharImageBox.Size = new Size(30, 35);
            CharImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            CharImageBox.BackColor = Color.Black;
            this.Level = -1;
            BulletSpeed = 10;

        }

        public void Moving(Panel MainPanel)
        {
            switch (this.Direct)
            {
                case DirectionsOfMoving.UP:
                    if (!(CharImageBox.Top <= MainPanel.Top))
                        CharImageBox.Top -= this.Speed;
                    break;
                case DirectionsOfMoving.DOWN:
                    if (!(CharImageBox.Bottom >= MainPanel.Bottom-5))
                        CharImageBox.Top += this.Speed;
                    break;
                case DirectionsOfMoving.LEFT:
                    if (!(CharImageBox.Left <= MainPanel.Left))
                        CharImageBox.Left -= this.Speed;
                    break;
                case DirectionsOfMoving.RIGHT:
                    if (!(CharImageBox.Right >= MainPanel.Right-5))
                        CharImageBox.Left += this.Speed;
                    break;
            }
        }
        public void TimeReload()
        {
            if (this.FireHero)
            {
                this.Fire_sek += 1;
                if (this.Fire_sek >= this.ReloadSek) this.FireHero = false;
            }
        }


        public void MovingInOption(Panel MainPanel)
        {
            switch (this.Direct)
            {
                case DirectionsOfMoving.UP:
                    if (!(CharImageBox.Top <= MainPanel.Top - 15))
                        CharImageBox.Top -= this.Speed;
                    break;
                case DirectionsOfMoving.DOWN:
                    if (!(CharImageBox.Bottom >= MainPanel.Bottom - 15))
                        CharImageBox.Top += this.Speed;
                    break;
                case DirectionsOfMoving.LEFT:
                    if (!(CharImageBox.Left <= MainPanel.Left -25))
                        CharImageBox.Left -= this.Speed;
                    break;
                case DirectionsOfMoving.RIGHT:
                    if (!(CharImageBox.Right >= MainPanel.Right-25))
                        CharImageBox.Left += this.Speed;
                    break;
            }
        }
        public void GetImage(ushort choice)
        {
            switch (choice)
            {
                case 1: //Kolor żółty 
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T1_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T1_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T1_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T1_Right.gif";
                    }
                    break;

                case 2: //Kolor zielony 
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T2_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T2_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T2_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T2_Right.gif";
                    }
                    break;

                case 3: //Kolor czerwony 
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T3_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T3_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T3_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T3_Right.gif";
                    }
                    break;

                case 4: //Kolor niebieski
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T4_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T4_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T4_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T4_Right.gif";
                    }
                    break;

                case 5: //Kolor fioletowy 
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T5_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T5_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T5_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T5_Right.gif";
                    }
                    break;

                case 6: //Kolor błękitny
                    {
                        MovingImage[0] = "Assets/Textures/TanksColors/T6_Up.gif";
                        MovingImage[1] = "Assets/Textures/TanksColors/T6_Down.gif";
                        MovingImage[2] = "Assets/Textures/TanksColors/T6_Left.gif";
                        MovingImage[3] = "Assets/Textures/TanksColors/T6_Right.gif";
                    }
                    break;
            }
        }

        public void CheckActivity()
        {
            if (this.HealthPoints <= 0)
            {
                this.IsActive = false;
                this.IsMoving = false;
                DeadColors();
            }
        }
        private void DeadColors()
        {
            switch (this.Direct)
            {
                case DirectionsOfMoving.UP:
                    this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Up.gif");
                    break;
                case DirectionsOfMoving.DOWN:
                    this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Down.gif");
                    break;
                case DirectionsOfMoving.LEFT:
                    this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Left.gif");
                    break;
                case DirectionsOfMoving.RIGHT:
                    this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Right.gif");
                    break;
                default:
                    break;
            }
        }

        public void HP_EndPoints(int Diff)
        {
            if (this.HealthPoints == 40) this.Points += 40;
            if (this.HealthPoints == 30) this.Points += 30;
            if (this.HealthPoints == 20) this.Points += 20;
            if (this.HealthPoints == 10) this.Points += 10;
            this.Points += (Diff+1)*10;

        }
        
    }
}
