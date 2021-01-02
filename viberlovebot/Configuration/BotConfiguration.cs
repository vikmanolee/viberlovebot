namespace viberlovebot
{
    public class BotConfiguration
    {
        public string WelcomeMessage { get; set; }

        public SenderConfiguration Sender { get; set; }

        public class SenderConfiguration
        {
            public string Name { get; set; }
            public string Avatar { get; set; } = null;
        }
    }
}