namespace ViberBotApi
{
    /**
    ** BaseUrl for Viber is https://chatapi.viber.com/pa/
    **/
    public static class UrlResources
    {
        private const string knownBaseUrl = "https://chatapi.viber.com/pa/";
    
        private const string setWebhook = "set_webhook";
        private const string sendMessage = "send_message";
        private const string bradcastMessage = "broadcast_message";
        private const string getOnline = "get_online";
        private const string getUserDetails = "get_user_details";
        private const string getAccountInfo = "get_account_info";

        public static string SetWebhook(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, setWebhook);

        public static string SendMessage(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, sendMessage);

        public static string BradcastMessage(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, bradcastMessage);

        public static string GetOnline(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, getOnline);

        public static string GetUserDetails(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, getUserDetails);

        public static string GetAccountInfo(string baseUrl = knownBaseUrl) => ConcatSafeSeparator(baseUrl, getAccountInfo);

        private static string ConcatSafeSeparator(string head, string tail)
        {
            if (head.EndsWith('/'))
                return head + tail;
            else
                return head + '/' + tail;
        }
    }
}