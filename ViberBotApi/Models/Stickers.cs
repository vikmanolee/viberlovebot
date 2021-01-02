using System;

namespace ViberBotApi.Models
{
    public static class Stickers
    {
        private static int[] _stickerIds = new int[] {
            114426,
            114417,
            40141,
            40100,
            40101,
            40106,
            40108,
            40109,
            40110,
            40111,
            40112,
            40130,
            40124
        };

        public static int Random
        {
            get
            {
                var random = new Random();
                return _stickerIds[random.Next(_stickerIds.Length)];
            }
        }
    }
}