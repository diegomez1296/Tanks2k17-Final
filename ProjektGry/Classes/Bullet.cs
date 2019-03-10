using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektGry
{
    public class Bullet
    {
        public void MakeBulletForStartGame(PictureBox Bullet)
        {
            Bullet.Size = new Size(4, 10);
            Bullet.SizeMode = PictureBoxSizeMode.Zoom;
            Bullet.Location = new Point(-656, -656);
            Bullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Up.gif");
        }

        public void CreateBullet(Tank Player, PictureBox Bullet)
        {
            Bullet.Visible = true;
            Bullet.Enabled = true;
            switch (Player.BulletMoving)
            {
                case DirectionsOfBulletMoving.UP:
                    Bullet.Size = new Size(4, 10);
                    Bullet.Top -= Player.BulletSpeed;
                    Bullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Up.gif");
                    break;

                case DirectionsOfBulletMoving.DOWN:
                    Bullet.Size = new Size(4, 10);
                    Bullet.Top += Player.BulletSpeed;
                    Bullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Down.gif");
                    break;

                case DirectionsOfBulletMoving.LEFT:
                    Bullet.Size = new Size(10, 4);
                    Bullet.Left -= Player.BulletSpeed;
                    Bullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Left.gif");
                    break;

                case DirectionsOfBulletMoving.RIGHT:
                    Bullet.Size = new Size(10, 4);
                    Bullet.Left += Player.BulletSpeed;
                    Bullet.Image = Image.FromFile("Assets/Textures/Bullets/Bullet_Right.gif");
                    break;
            }
        }
        public void SpawnMovingBullet(Tank Player, PictureBox Bullet)
        {
            if (Player.FireHero == true)
            {
                if (Player.HeroBulletStart == true) Player.HeroBulletStart = false;
                CreateBullet(Player, Bullet);
            }
        }

        public void BulletAndBlock(PictureBox Bullet, List<Block> Blocks, List<Bonus> Bonuses, int PercentForBonus, bool AI_Bullet) //AI_Bullet <- jeśli bullet należy do Enemy - nie losujemy bonusu
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Bullet.Bounds.IntersectsWith(Blocks[i].Bloczek.Bounds))
                {
                    Blocks[i].BlockParametrsChanging(Bullet, Bonuses, PercentForBonus, AI_Bullet);
                }
            }
        }

        public void BulletHideWithEnemy(Character Player, PictureBox PlayerBullet, Character Opponent)
        {
            if (PlayerBullet.Bounds.IntersectsWith(Opponent.CharImageBox.Bounds))
            {
                Opponent.GetHit(Player.PowerOfGun);
                if (Player.isPlayer) Player.Points += ((Opponent.Level + 1) * 10);
                PlayerBullet.Hide();
                PlayerBullet.Location = new Point(-454, -534);
            }
        }

        public void BulletHideWithMainPanel(Character NPC, PictureBox Bullet, Panel MainPanel)
        {
            if (NPC.IsActive)
            {
                if (Bullet.Top <= MainPanel.Top || Bullet.Left <= MainPanel.Left || Bullet.Right >= MainPanel.Right || Bullet.Bottom >= MainPanel.Bottom)
                {
                    Bullet.Hide();
                    Bullet.Location = new Point(-454, -534);
                    Bullet.Enabled = false;
                }
            }
        }

    }
}
