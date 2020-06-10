using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rakib
{
    public class AnalyticsManager
    {
        public void Initialize()
        {
//            GameAnalytics.Initialize();
        }
        public void LevelLoaded(int level)
        {
            
        }

        /// <summary>
        /// Called when level is started.
        /// </summary>
        /// <param name="level"> -1 indicates that game isn't level based</param>
        public void LevelStarted(int level = -1)
        {
//            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, level.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">-1 indicates that game isn't level based</param>
        /// <param name="score">0 indicates that there is no score based system</param>
        public void LevelComplete(int level = -1, int score = 0)
        {
//            if(level == -1)
//                GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, level.ToString());
//            else
//                GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, level.ToString(), score);

           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">-1 indicates that game isn't level based</param>
        /// <param name="score">0 indicates that there is no score based system</param>
        public void LevelFail(int level = -1, int score = 0)
        {
//            if(level == -1)
//                GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, level.ToString());
         //            else
         //                GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, level.ToString(), score);
                 }

        
    }
}