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
    public class AddonMap
    {
        public Color Background;
        public int BonusChange;
        public Point[] Tanks_Spawn = new Point[8];
        public List<Block> AddonMap_Blocks = new List<Block>();

        public AddonMap(Color background, int bonusChange, Point[] tanks_Spawn, List<Block> addonMap_BlocksList)
        {
            this.Background = background;
            this.BonusChange = bonusChange;
            this.Tanks_Spawn = tanks_Spawn;
            this.AddonMap_Blocks = addonMap_BlocksList;
        }

        
    }
}
