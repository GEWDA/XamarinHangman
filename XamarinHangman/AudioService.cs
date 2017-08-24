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
        private MediaPlayer player;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            Log.Info("myDebug","Starting Audio Service");
            Play();
            // Create your application here
        }

        public override void UnbindService(IServiceConnection conn)
        {
            player.Pause();
            player.Stop();
            player.Release();
            base.UnbindService(conn);
            StopSelf();
        }

        //public void Stop()
        //{
        //    player.Pause();
        //    player.Stop();
        //    player.Release();
        //    StopSelf();
        //}
        private void Play()
        {
            player = MediaPlayer.Create(this,Resource.Raw.Blood_Is_Pumpin);
            player.Start();
        }
    }
}