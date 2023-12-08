using FixMath.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public static class TimeUtil
    {
        /// <summary>
        /// 把时间转换成帧数
        /// </summary>
        /// <param name="frameRate"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeToFrame(int frameRate, Fix64 time)
        {
            return Fix64.ToInt32(frameRate * time);
        }
    }

