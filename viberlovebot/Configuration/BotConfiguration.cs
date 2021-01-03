namespace viberlovebot
{
    public class BotOptions
    {
        public string WelcomeMessage { get; set; }

        public SenderOptions Sender { get; set; }

        public class SenderOptions
        {
            public string Name { get; set; }
            public string Avatar { get; set; } = null;
        }
    }
}