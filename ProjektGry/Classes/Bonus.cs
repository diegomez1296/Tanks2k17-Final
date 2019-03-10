using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ProjektGry
{

    public enum BonusType
    {
        HP,
        ReloadSek,
        Speed,
        BulletSpeed
    };

    public class Bonus
    {
        public PictureBox BonusImg;
        public BonusType Type = new BonusType();
        public int TimeToDisappear { get; set; }
        public int PercentToShow { get; set; }

        public Bonus(BonusType type, string imageAddress, Size sizeOfBonus, Point location)
        {
            this.Type = type;
            this.BonusImg = new PictureBox();
            this.BonusImg.Image = Image.FromFile(imageAddress);
            //this.BonusImg.BackgroundImage = Image.FromFile(backgroundAddress);
            this.BonusImg.Size = sizeOfBonus;
            this.BonusImg.Location = location;
        }

        public void AddValueAsBonus(Tank Player)
        {
            switch (this.Type)
            {
                case BonusType.HP:
                    if (Player.HealthPoints < 40)
                    {
                        Player.HealthPoints += 10;
                    }
                    break;
                case BonusType.ReloadSek:
                    if (Player.ReloadSek > 2)
                    {
                        Player.ReloadSek--;
                    }
                    break;
                case BonusType.Speed:
                    if (Player.Speed < 6)
                    {
                        Player.Speed++;
                    }
                    break;
                case BonusType.BulletSpeed:
                    if (Player.BulletSpeed < 16)
                    {
                        Player.BulletSpeed+=2;
                    }
                    break;
                default:
                    break;
            }
           // Player.Points += 10;
        }
    }
}

