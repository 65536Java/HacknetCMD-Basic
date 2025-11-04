using System.Media;

namespace Audio
{
    public class AudioUtil
    {
        public static SoundPlayer GetSoundPlayer(string Dir, string path) {
            SoundPlayer player = new SoundPlayer();
            // 設置 WAV 檔案的路徑
            player.SoundLocation = System.IO.Path.Combine(Dir, path); // 請替換成您的 WAV 檔案路徑
            return player;
        }
    }
}