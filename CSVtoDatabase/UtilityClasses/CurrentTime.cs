using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

using GalaSoft.MvvmLight;

namespace CSVtoDatabase.UtilityClasses
{
    public class CurrentTime : ViewModelBase
    {
        private DateTime _currentTime;

        public CurrentTime()
        {
            _currentTime = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan( 0, 0, 1 );
            timer.Tick += new EventHandler( TimerTick );
            timer.Start();
        }

        public DateTime Now { get => _currentTime; set => Set( ref _currentTime, value ); }

        private void TimerTick( object sender, EventArgs e )
        {
            Now = DateTime.Now;
        }
    }
}