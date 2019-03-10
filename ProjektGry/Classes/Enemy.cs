using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ProjektGry
{
    class Enemy : Character
    {
        private System.Media.SoundPlayer GunShoot = new System.Media.SoundPlayer();
        private System.Media.SoundPlayer HittedSound = new System.Media.SoundPlayer();

        //WindowsMediaPlayer GunShoot = new WindowsMediaPlayer();
        //WindowsMediaPlayer HittedSound = new WindowsMediaPlayer();

        public ushort EnemyBulletSpeed { get; set; }
        public bool IsShooting { get; set; }
        public int TimeToDisapear = 200;
        public bool IsStartFormEnemy { get; set; }
        public int MaxHealthPoints { get; set; }
        bool DamageColor = false;
        static Random EnemyMoveSekRnd = new Random();
        public int _movingPhase = 0;
        int _actualMovingSek = 0;
        int _shootingPhase = 0;
        int _actualShootingSek = 2;
        int _actualWaitingSek = 2;

        Image[] ColorImageBox;

        public Enemy(int level, bool isActive)
        {
            IsStartFormEnemy = false;
            ColorImageBox = new Image[8];
            this.CharImageBox = new PictureBox();
            CharImageBox.Size = new Size(30, 35);
            CharImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            CharImageBox.BackColor = Color.Black;
            this.Level = level;
            this.IsMoving = true;
            this.IsShooting = false;
            this.IsActive = isActive;
            this.Points = (this.Level + 1) * 10;

            #region EnemyLevels
            switch (this.Level)
            {
                case 0:
                    SetColorOfEnemy(1);
                    this.Speed = 1;
                    this.EnemyBulletSpeed = 10;
                    this.HealthPoints = 10;
                    this.MaxHealthPoints = 10;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 6;
                    break;
                case 1:
                    SetColorOfEnemy(2);
                    this.Speed = 1;
                    this.EnemyBulletSpeed = 11;
                    this.HealthPoints = 20;
                    this.MaxHealthPoints = 20;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 5;
                    break;
                case 2:
                    SetColorOfEnemy(3);
                    this.Speed = 2;
                    this.EnemyBulletSpeed = 12;
                    this.HealthPoints = 30;
                    this.MaxHealthPoints = 30;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 4;
                    break;
                case 3:
                    SetColorOfEnemy(4);
                    this.Speed = 2;
                    this.EnemyBulletSpeed = 13;
                    this.HealthPoints = 40;
                    this.MaxHealthPoints = 40;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 3;
                    break;
                case 4:
                    SetColorOfEnemy(5);
                    this.Speed = 3;
                    this.EnemyBulletSpeed = 14;
                    this.HealthPoints = 50;
                    this.MaxHealthPoints = 50;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 2;
                    break;
                case 5:
                    SetColorOfEnemy(6);
                    this.Speed = 3;
                    this.EnemyBulletSpeed = 15;
                    this.HealthPoints = 60;
                    this.MaxHealthPoints = 60;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 1;
                    break;
                default:
                    SetColorOfEnemy(7);
                    this.Speed = 0;
                    this.EnemyBulletSpeed = 0;
                    this.HealthPoints = 1;
                    this.MaxHealthPoints = 1;
                    this.PowerOfGun = 10;
                    this.ReloadSek = 100;
                    break;
            }
            #endregion

        }
        public Enemy(byte speed, Point ImageBoxLocalization, byte colorOfEnemy) //StartMenu
        {
            IsStartFormEnemy = true;
            ColorImageBox = new Image[8];
            SetColorOfEnemyDefault(colorOfEnemy);
            //SetColorOfEnemy(colorOfEnemy);
            this.CharImageBox = new PictureBox();
            CharImageBox.Size = new Size(30, 35);
            CharImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            CharImageBox.BackColor = Color.Black;
            CharImageBox.Location = ImageBoxLocalization;
            this.MaxHealthPoints = 1;
            this.HealthPoints = 1;
            this.Speed = speed;
            this.IsMoving = true;
            this.IsShooting = false;
        }

        public Enemy(byte colorOfEnemy, PictureBox Pic) //HowToPlay
        {
            IsStartFormEnemy = true;
            ColorImageBox = new Image[8];
            SetColorOfEnemy(colorOfEnemy);
            this.CharImageBox = Pic;
            this.MaxHealthPoints = 1;
            this.HealthPoints = 1;
            this.Speed = 0;
            this.IsMoving = true;
            this.IsShooting = false;
        }

        private void SetColorOfEnemy(byte takenColor)
        {
            switch (takenColor)
            {
                case 1: //Kolor żółty 
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T1B_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T1B_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T1B_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T1B_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T1BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T1BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T1BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T1BH_Right.gif");
                    }
                    break;

                case 2: //Kolor zielony 
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T2B_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T2B_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T2B_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T2B_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2BH_Right.gif");
                    }
                    break;

                case 3: //Kolor czerwony 
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T3B_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T3B_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T3B_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T3B_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T3BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T3BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T3BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T3BH_Right.gif");
                    }
                    break;

                case 4: //Kolor niebieski
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T4B_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T4B_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T4B_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T4B_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T4BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T4BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T4BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T4BH_Right.gif");
                    }
                    break;

                case 5: //Kolor fioletowy 
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T5B_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T5B_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T5B_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T5B_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T5BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T5BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T5BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T5BH_Right.gif");
                    }
                    break;

                case 6: //Kolor błękitny
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T6B1_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T6B1_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T6B1_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T6B1_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T6BH_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T6BH_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T6BH_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T6BH_Right.gif");
                    }
                    break;
                case 7: //Kolor szary
                    {
                        ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Up.gif");
                        ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Down.gif");
                        ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Left.gif");
                        ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Right.gif");
                        ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                        ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                        ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                        ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                    }
                    break;
            }
        }
        
         private void SetColorOfEnemyDefault(byte takenColor)
         {
             switch (takenColor)
             {
                 case 1: //Kolor żółty 
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T1_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T1_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T1_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T1_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;

                 case 2: //Kolor zielony 
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;

                 case 3: //Kolor czerwony 
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T3_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T3_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T3_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T3_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;

                 case 4: //Kolor niebieski
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T4_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T4_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T4_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T4_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;

                 case 5: //Kolor fioletowy 
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T5_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T5_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T5_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T5_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;

                 case 6: //Kolor błękitny
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/T6_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/T6_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/T6_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/T6_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;
                 case 7: //Kolor szary
                     {
                         ColorImageBox[0] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Up.gif");
                         ColorImageBox[1] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Down.gif");
                         ColorImageBox[2] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Left.gif");
                         ColorImageBox[3] = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Right.gif");
                         ColorImageBox[4] = Image.FromFile("Assets/Textures/TanksColors/T2_Up.gif");
                         ColorImageBox[5] = Image.FromFile("Assets/Textures/TanksColors/T2_Down.gif");
                         ColorImageBox[6] = Image.FromFile("Assets/Textures/TanksColors/T2_Left.gif");
                         ColorImageBox[7] = Image.FromFile("Assets/Textures/TanksColors/T2_Right.gif");
                     }
                     break;
             }
         }
         
        public void Moving(Panel MainPanel)
        {
            if (this.IsMoving == true)
            {
                if (_actualMovingSek > _movingPhase)
                {
                    Direction();
                    _actualMovingSek = 0;
                }
                _actualMovingSek += 1;

                if (IsStartFormEnemy) MovingInStartForm(MainPanel);
                else MovingInGame(MainPanel);
            }
        }

        //Kolizje z Panelem
        private void MovingInStartForm(Panel MainPanel)
        {
            switch (this.Direct)
            {
                case DirectionsOfMoving.UP:
                    if (!(this.CharImageBox.Top <= MainPanel.Top - 60))
                    {
                        this.CharImageBox.Top -= this.Speed;
                    }
                    else
                    {
                        this.CharImageBox.Top += 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.DOWN:
                    if (!(this.CharImageBox.Bottom >= MainPanel.Bottom + 5))
                    {
                        this.CharImageBox.Top += this.Speed;
                    }
                    else
                    {
                        this.CharImageBox.Top -= 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.LEFT:
                    if (!(this.CharImageBox.Left <= MainPanel.Left - 30))
                    {
                        this.CharImageBox.Left -= this.Speed;
                    }
                    else
                    {
                        CharImageBox.Left += 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.RIGHT:
                    if (!(this.CharImageBox.Right >= MainPanel.Right + 30))
                    {
                        this.CharImageBox.Left += this.Speed;
                    }
                    else
                    {
                        CharImageBox.Left -= 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
            }
        }
        private void MovingInGame(Panel MainPanel)
        {
            switch (this.Direct)
            {
                case DirectionsOfMoving.UP:
                    if (!(this.CharImageBox.Top <= MainPanel.Top))
                    {
                        this.CharImageBox.Top -= this.Speed;
                    }
                    else
                    {
                        this.CharImageBox.Top += 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.DOWN:
                    if (!(this.CharImageBox.Bottom >= MainPanel.Bottom))
                    {
                        this.CharImageBox.Top += this.Speed;
                    }
                    else
                    {
                        this.CharImageBox.Top -= 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.LEFT:
                    if (!(this.CharImageBox.Left <= MainPanel.Left))
                    {
                        this.CharImageBox.Left -= this.Speed;
                    }
                    else
                    {
                        CharImageBox.Left += 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
                case DirectionsOfMoving.RIGHT:
                    if (!(this.CharImageBox.Right >= MainPanel.Right))
                    {
                        this.CharImageBox.Left += this.Speed;
                    }
                    else
                    {
                        CharImageBox.Left -= 5;
                        Direction(); //Zmiana kierunku po kolizji z panelem
                    }
                    break;
            }
        }
        //

        private void Direction()
        {
            Random EnemyDirection = new Random();

            _movingPhase = EnemyMoveSekRnd.Next(25, 75) + EnemyMoveSekRnd.Next(25, 75);
            switch (EnemyDirection.Next(1, 5))
            {

                case 1:
                    this.Direct = DirectionsOfMoving.UP;
                    if (DamageColor) this.CharImageBox.Image = ColorImageBox[4];
                    else this.CharImageBox.Image = ColorImageBox[0];
                    this.CharImageBox.Top -= this.Speed;
                    break;

                case 2:
                    this.Direct = DirectionsOfMoving.DOWN;
                    if (DamageColor) this.CharImageBox.Image = ColorImageBox[5];
                    else this.CharImageBox.Image = ColorImageBox[1];
                    this.CharImageBox.Top += this.Speed;
                    break;

                case 3:
                    this.Direct = DirectionsOfMoving.LEFT;
                    if (DamageColor) this.CharImageBox.Image = ColorImageBox[6];
                    else this.CharImageBox.Image = ColorImageBox[2];
                    this.CharImageBox.Left -= this.Speed;
                    break;

                case 4:
                    this.Direct = DirectionsOfMoving.RIGHT;
                    if (DamageColor) this.CharImageBox.Image = ColorImageBox[7];
                    else this.CharImageBox.Image = ColorImageBox[3];
                    this.CharImageBox.Left += this.Speed;
                    break;

                default:
                    break;
            }
        }

        public void BlockCollisionForEnemy(Block CollidingBlock)
        {
            if (CollidingBlock.IsCollidingWithTanks)
            {
                //this.IsMoving = false;
                switch (this.Direct)
                {
                    case DirectionsOfMoving.UP:
                        CharImageBox.Top += 5;
                        break;
                    case DirectionsOfMoving.DOWN:
                        CharImageBox.Top -= 5;
                        break;
                    case DirectionsOfMoving.LEFT:
                        CharImageBox.Left += 5;
                        break;
                    case DirectionsOfMoving.RIGHT:
                        CharImageBox.Left -= 5;
                        break;
                }
                this.Direction();
            }
        }

        private void Shooting(Panel MainPanel, PictureBox EnemyBullet)
        {
            if (this.IsShooting == true)
            {

                if (EnemyBullet.Visible == false)
                {

                    EnemyBullet.Visible = true;
                    this.IsShooting = false;
                    this.BulletMoving = (DirectionsOfBulletMoving)Direct;
                    this.ShootingSound();
                    switch (this.Direct)
                    {
                        case DirectionsOfMoving.UP:
                            EnemyBullet.Location = new Point(CharImageBox.Location.X + 11, CharImageBox.Location.Y + 14);
                            EnemyBullet.Size = new Size(4, 10);
                            //EnemyBullet.Top -= EnemyBulletSpeed;
                            EnemyBullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Up.gif");
                            break;

                        case DirectionsOfMoving.DOWN:
                            EnemyBullet.Location = new Point(CharImageBox.Location.X + 11, CharImageBox.Location.Y - 12);
                            EnemyBullet.Size = new Size(4, 10);
                            //EnemyBullet.Top += EnemyBulletSpeed;
                            EnemyBullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Down.gif");
                            break;

                        case DirectionsOfMoving.LEFT:
                            EnemyBullet.Location = new Point(CharImageBox.Location.X - 12, CharImageBox.Location.Y + 11);
                            EnemyBullet.Size = new Size(10, 4);
                            //EnemyBullet.Left -= EnemyBulletSpeed;
                            EnemyBullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Left.gif");
                            break;

                        case DirectionsOfMoving.RIGHT:
                            EnemyBullet.Location = new Point(CharImageBox.Location.X - 5, CharImageBox.Location.Y + 11);
                            EnemyBullet.Size = new Size(10, 4);
                            //EnemyBullet.Left += EnemyBulletSpeed;
                            EnemyBullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Right.gif");
                            break;
                    }


                }
            }
        }
        private void EnemyBulletMoving(Panel MainPanel, PictureBox EnemyBullet)
        {
            if (EnemyBullet.Visible == true)
            {
                switch (this.BulletMoving)
                {
                    case DirectionsOfBulletMoving.UP:
                        EnemyBullet.Top -= EnemyBulletSpeed;
                        break;

                    case DirectionsOfBulletMoving.DOWN:
                        EnemyBullet.Top += EnemyBulletSpeed;
                        break;

                    case DirectionsOfBulletMoving.LEFT:
                        EnemyBullet.Left -= EnemyBulletSpeed;
                        break;

                    case DirectionsOfBulletMoving.RIGHT:
                        EnemyBullet.Left += EnemyBulletSpeed;
                        break;
                }
            }
        }
        public void EnemyReload()
        {
            if (this.IsActive && this.HealthPoints > 0)
            {
                if (!this.IsShooting)
                {
                    if (_actualShootingSek > this.ReloadSek)
                    {

                        Random rand = new Random();
                        _shootingPhase = rand.Next(0, 5 - this.Level);
                        if (this._actualWaitingSek > this._shootingPhase) //Gdy przeładuje pocisk, czeka by strzelić
                        {
                            _actualShootingSek = 0;
                            _actualWaitingSek = 0;
                            this.IsShooting = true;
                        }
                        _actualWaitingSek++;
                    }
                    this._actualShootingSek++;


                }
            }

        }

        public override void GetHit(int ValueOfAttack)
        {
            base.GetHit(ValueOfAttack);

            if (this.HealthPoints <= this.MaxHealthPoints / 2)
            {
                DamageColor = true;
                if (this.IsMoving)
                {
                    if (this.CharImageBox.Image == ColorImageBox[0]) this.CharImageBox.Image = ColorImageBox[4];
                    if (this.CharImageBox.Image == ColorImageBox[1]) this.CharImageBox.Image = ColorImageBox[5];
                    if (this.CharImageBox.Image == ColorImageBox[2]) this.CharImageBox.Image = ColorImageBox[6];
                    if (this.CharImageBox.Image == ColorImageBox[3]) this.CharImageBox.Image = ColorImageBox[7];
                }
            }
        }
        private void DeadColors()
        {

        }
        private void EnemyDisapear(PictureBox EnemyBullet)
        {
            if (this.HealthPoints <= 0)
            {
                this.IsMoving = false;
                this.IsShooting = false;
                EnemyBullet.Location = new Point(-454, -534);
                switch (this.Direct)
                {
                    case DirectionsOfMoving.UP:
                        if (this.Level == 5) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Boss_Up.gif");
                        else if (this.Level == 3 || this.Level == 4) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_45_Up.gif");
                        else this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Up.gif");
                        break;
                    case DirectionsOfMoving.DOWN:
                        if (this.Level == 5) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Boss_Down.gif");
                        else if (this.Level == 3 || this.Level == 4) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_45_Up.gif");
                        else this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Down.gif");
                        break;
                    case DirectionsOfMoving.LEFT:
                        if (this.Level == 5) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Boss_Left.gif");
                        else if (this.Level == 3 || this.Level == 4) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_45_Up.gif");
                        else this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Left.gif");
                        break;
                    case DirectionsOfMoving.RIGHT:
                        if (this.Level == 5) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Boss_Right.gif");
                        else if (this.Level == 3 || this.Level == 4) this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_45_Up.gif");
                        else this.CharImageBox.Image = Image.FromFile("Assets/Textures/TanksColors/TDEAD_Right.gif");
                        break;
                    default:
                        break;
                }

                this.TimeToDisapear -= 1;
                if (this.TimeToDisapear <= 0)
                {
                    this.CharImageBox.Location = new Point(-1000, -1000);
                    this.IsActive = false;
                }
            }
        }

        public void EnemyCollideWithPlayerTank(Tank Player, Point TSpawn, Point ESpawn)
        {
            if (this.CharImageBox.Bounds.IntersectsWith(Player.CharImageBox.Bounds))
            {
                if (this.IsMoving)
                {
                    Player.GetHit(10);
                    if (Player.IsActive) Player.CharImageBox.Location = TSpawn;
                }
                if (Player.IsActive)
                {
                    this.GetHit(10);
                    if (this.IsMoving) this.CharImageBox.Location = ESpawn;
                }
            }
        }

        public void Deactivation()
        {
            this.CharImageBox.Hide();
            this.IsActive = false;
            this.IsMoving = false;
            this.CharImageBox.Location = new Point(-1000, -1000);

        }

        //Poruszanie i strzelanie w jednej metodzie
        public void EnemyMovingAndShooting(PictureBox EnemyBullet, Panel MainPanel)
        {
            if (this.IsActive)
            {
                this.Moving(MainPanel);
                this.Shooting(MainPanel, EnemyBullet);
                this.EnemyBulletMoving(MainPanel, EnemyBullet);
            }
            this.EnemyDisapear(EnemyBullet);
        }

    }
}
