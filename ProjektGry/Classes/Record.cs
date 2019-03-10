namespace ProjektGry
{
    public class Record
    {
        public int TimeOfWinning { get; set; }
        public int Points { get; set; }
        public int Points2 { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }

        public Record(string name, int points /*int time*/)
        {
            //TimeOfWinning = time;
            Points = points;
            Name = name;
        }
        public Record(string name, int points, string name2, int points2 /*int time*/)
        {
            //TimeOfWinning = time;
            Name = name;
            Name2 = name2;
            Points = points;
            Points2 = points2;
            
        }
    }
}
