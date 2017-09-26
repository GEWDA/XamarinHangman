using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Content.Res;
using Android.Util;

namespace XamarinHangman
{
    [Service(Exported=false)]
    public class AudioService : Service
    {
        private class SongReseter : Java.Lang.Object, MediaPlayer.IOnCompletionListener
        {
            public void OnCompletion(MediaPlayer mp)
            {
                mp.Start();
            }
        }

        private int song;//stores an id, so technically is an int
        private MediaPlayer player;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            Log.Info("myDebug","Starting Audio Service");
            Random randSong = new Random();
            song = randSong.Next(4);
            Log.Info("myDebug", "selecting song #" + song.ToString());
            song = song==1? Resource.Raw.Blood_Is_Pumpin : song==2? Resource.Raw.Sandstorm : Resource.Raw.Final_Countdown;
            Play();
        }

        public override void OnDestroy()
        {
            player.Pause();
            player.Stop();
            player.Release();
            base.OnDestroy();
        }
        private void Play()
        {
            player = MediaPlayer.Create(this,song);
            player.SetOnCompletionListener(new SongReseter());
            player.Start();
        }
    }
}