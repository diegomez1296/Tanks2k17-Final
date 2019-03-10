using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ProjektGry
{
    public class Block
    {
        public PictureBox Bloczek;
        public bool IsDestroyable { get; set; }
        public bool IsCollidingWithTanks { get; set; }
        public bool IsCollidingWithBullet { get; set; }
        public bool IsWater { get; set; }

        public Block(string ImageFromAddress, string ImageBackgroundAddress, bool isDestroyable, bool isCollidingtanks, bool isCollidingBullet, Size sizeOfImage, bool isWater)
        {
            Bloczek = new PictureBox();
            Bloczek.Image = Image.FromFile(ImageFromAddress);
            Bloczek.BackgroundImage = Image.FromFile(ImageBackgroundAddress);
            Bloczek.Size = sizeOfImage;
            IsDestroyable = isDestroyable;
            IsCollidingWithTanks = isCollidingtanks;
            IsCollidingWithBullet = isCollidingBullet;
            IsWater = isWater;
        }
        //Block without BackGround texture
        public Block(string ImageFromAddress, Color color, bool isDestroyable, bool isCollidingtanks, bool isCollidingBullet, bool isWater)
        {
            Bloczek = new PictureBox();
            Bloczek.Image = Image.FromFile(ImageFromAddress);
            Bloczek.BackColor = color;
            Bloczek.Size = new Size (40,40);
            IsDestroyable = isDestroyable;
            IsCollidingWithTanks = isCollidingtanks;
            IsCollidingWithBullet = isCollidingBullet;
            IsWater = isWater;
        }

        //AddonMap
        public Block()
        {
            Bloczek = new PictureBox();
            Bloczek.Size = new Size(40, 40);
        }

        public override string ToString()
        {
            return $"Block:  Image: {this.Bloczek.Image}  ImageLocation: {this.Bloczek.ImageLocation}";
        }


        public void BlockParametrsChanging(PictureBox Bullet, List<Bonus> Bonuses, int PercentForBonus, bool AI_Bullet)
        {
            if (this.IsCollidingWithBullet && this.IsDestroyable) // BloczekDestroyable
            {
                //Zmiana wartosci boolowskich
                this.IsCollidingWithBullet = false;
                this.IsDestroyable = false;
                this.IsCollidingWithTanks = false;
                Bullet.Visible = false;
                Bullet.Enabled = false;
                this.Bloczek.Visible = false;


                if (!AI_Bullet)
                {
                    //Tworzenie nowego bonusu
                    Random BonusRand = new Random();
                    Bullet.Location = new Point(-454, -534);
                    bool BonusLocatOk = true;
                    int CountWhile = 0;
                    int BloczekLocatX = this.Bloczek.Location.X;
                    int BloczekLocatY = this.Bloczek.Location.Y;

                    int I = BonusRand.Next(0, Bonuses.Count);
                    if (BonusRand.Next(1, 100) <= PercentForBonus)
                    {
                        do
                        {
                            if (Bonuses[I].BonusImg.Location.X == -1000)
                            {
                                Bonuses[I].BonusImg.Location = new Point(BloczekLocatX + 5, BloczekLocatY + 5);
                                break;
                            }
                            else
                            {
                                BonusLocatOk = false;
                                CountWhile++;
                                if (CountWhile >= 10)
                                {
                                    break;
                                }
                            }
                        } while (BonusLocatOk);
                    }
                }
                this.Bloczek.Location = new Point(1000, 1000);

            }

            else if (this.IsCollidingWithBullet && !this.IsDestroyable) // BloczekNotDestroyable
            {
                Bullet.Visible = false;
                Bullet.Enabled = false;
                Bullet.Location = new Point(1000, 1000);
            }
        }
    }
}
