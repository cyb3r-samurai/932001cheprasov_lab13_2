namespace lab13_2.Models
{
    public class QuestModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Operation { get; set; }

        public static QuestModel randQust
        {
            get
            {
                var random = new Random();
                return new QuestModel
                {
                    X = random.Next(10),
                    Y = random.Next(10) + 1,
                    Operation = random.Next(4) switch
                    {
                        0 => "+",
                        1 => "-",
                        2 => "*",
                        3 => "/"
                    }

                };
            }
        }
        public int getAnswer()
        {
            return Operation switch
            {
                "+" => X + Y,
                "-" => X - Y,
                "*" => X * Y,
                "/" => X / Y,
            };

        }
        public Nullable<int> UserAnswer { get; set; }
        public bool AnswerIsCorrect
        {
            get { return UserAnswer is { } a && a == this.getAnswer(); }
        }
        public string output()
        {
            return Operation switch
            {
                "+" => $"{X} + {Y} = ",
                "-" => $"{X} - {Y} = ",
                "*" => $"{X} * {Y} = ",
                "/" => $"{X} / {Y} = ",

            };
        }
    }
}
