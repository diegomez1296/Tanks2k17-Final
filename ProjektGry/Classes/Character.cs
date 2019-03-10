using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace ProjektGry
{
    public enum DirectionsOfMoving
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    };

    public enum DirectionsOfBulletMoving
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    };
    public abstract class Character
    {
        private System.Media.SoundPlayer GunShoot = new System.Media.SoundPlayer();
        private System.Media.SoundPlayer HittedSound = new System.Media.SoundPlayer();

        //WindowsMediaPlayer GunShoot = new WindowsMediaPlayer();
        //WindowsMediaPlayer HittedSound = new WindowsMediaPlayer();

        public bool isPlayer { get; set; }
        public PictureBox CharImageBox;
        public byte Speed { get; set; } //Max 3
        public short Armor { get; set; }
        public int HealthPoints { get; set; }
        public bool IsMoving { get; set; }
        public bool IsActive { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public ushort PowerOfGun { get; set; }
        public DirectionsOfMoving Direct = new DirectionsOfMoving();
        public DirectionsOfBulletMoving BulletMoving = new DirectionsOfBulletMoving();
        private int _reloadSek;
        public int ReloadSek { get; set; }
        /* {
             get { return _reloadSek; }
             set
             {
                 if (_reloadSek == 3)
                 {
                     _reloadSek = 3;
                 }
                 else
                 {
                     _reloadSek = value;
                 }
             }
         }*/

        public virtual void GetHit(int ValueOfAttack)
        {
            //HittedSound.URL = "Assets/Sounds/HittedSound.mp3";
            //HittedSound.settings.volume = 20;
            HittedSound.SoundLocation = "Assets/Sounds/HittedSound.wav"; //Nie może być mp3
            if (this.HealthPoints > 0)
            {
                HittedSound.Play();

                //HittedSound.controls.play();
            }

            this.HealthPoints = this.HealthPoints - (ValueOfAttack - Armor);
        }
        public void ShootingSound()
        {
            //GunShoot.URL = "Assets/Sounds/TankShoot.mp3";
            GunShoot.SoundLocation = "Assets/Sounds/TankShoot.wav";
            GunShoot.Play();
        }
        public void BlockCollision(Block CollidingBlock)
        {
            if (CollidingBlock.IsCollidingWithTanks)
            {
                this.IsMoving = false;
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
            }
        }

        

       


    }
}
