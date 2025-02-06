namespace Barber.Colocho.App.Helpers
{
    public class GenerateColors
    {
        public static string GetColor()
        {
            var list = new List<string>()
            {
                "#D9CCE5",
                "#E3E3E3",
                "#D0E4E3",
                "#FFDCD0",
                "#FDE5B7",
                "#FF5555",
                "#FFC833",
                "#FF8800",
                "#6678ff",
                "#274C71",
                "#909E56",
                "#FF6B15",
                "#009EA9"
            };
            var rand = new Random();
            var number = rand.Next(1, list.Count);
            string color = list[number];
            return color;
        }
    }
}
