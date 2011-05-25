using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TrainSimulator.Model;

namespace TrainSimulator.Ctr
{
    public class Dispatcher
    {
        public delegate void PaintDelegate();

        private PaintDelegate paint;
        private bool doRun = false;

        private RailRoad railroad;
        private List<TrainSet> trains;

        public Dispatcher(RailRoad railroad, List<TrainSet> trains)
        {
            this.railroad = railroad;
            this.trains = trains;
        }

        public void onPaint(PaintDelegate paint)
        {
            this.paint = paint;
        }

        public void run()
        {
            doRun = true;
            while (doRun)
            {
                foreach (TrainSet train in trains)
                    foreach (TrainCart cart in train.cartList)
                        cart.moveCart(5);
                paint.Invoke();
                Thread.Sleep(100);
            }
        }

        public void stop()
        {
            doRun = false;
        }
    }
}
