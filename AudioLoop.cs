using System;
using UnityEngine;

namespace TomWill
{
    public static class AudioLoop 
    {
        /// <summary>
        /// This is the method to make looping at specific track session with intro first time
        /// Will be seen when the start loop time is not the start time of the clip
        /// </summary>
        /// <param name="source"> Is the Audio Source you want to loop with clip attached </param>
        /// <param name="startLoop"> Is the time where you want to start the loop in seconds </param>
        /// <param name="endLoop"> Is the time where the audio will get back to start loop time in seconds </param>
        public static void OnLoopWithIntro(ref AudioSource source, float startLoop, float endLoop)
        {
            try
            {
                if (source.loop) source.loop = false;

                float clipTime = source.time;
                float maxTime = source.clip.length;

                if (!source.isPlaying) source.Play();

                if (CheckTimeLoop(source, startLoop, endLoop))
                {
                    source.Stop();
                    return;
                }

                if (clipTime >= endLoop)
                {
                    source.time = startLoop;
                }
            } catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached");
            } catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null");
            }
        }

        /// <summary>
        /// This is the method to make looping at specific track session without intro music, 
        /// The audio will playing between start loop time and end loop time
        /// </summary>
        /// <param name="source"> Is the Audio Source you want to loop with clip attached </param>
        /// <param name="startLoop"> Is the time where you want to start the loop in seconds </param>
        /// <param name="endLoop"> Is the time where the audio will get back to start loop time in seconds </param>
        public static void OnLoop(ref AudioSource source, float startLoop, float endLoop)
        {
            try
            {
                if (source.loop) source.loop = false;

                float clipTime = source.time;
                float maxTime = source.clip.length;

                if (!source.isPlaying) source.Play();

                if (CheckTimeLoop(source, startLoop, endLoop))
                {
                    source.Stop();
                    return;
                }

                if (clipTime < startLoop)
                {
                    source.time = startLoop;
                }

                if (clipTime >= endLoop)
                {
                    source.time = startLoop;
                }
            }
            catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached");
            }
            catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null");
            }
        }

        /// <summary>
        /// This method will check the exception value for start loop time and end loop time
        /// - Max time is the length time of clip in seconds
        /// - Start Loop time is not allowed to be more than Max Time and End Loop Time
        /// - Start Loop time is not allowed to be same with End Loop Time
        /// - End Loop time is not allowed to be more than Max Time
        /// </summary>
        /// <param name="source"> Is the Audio Source you want to loop with clip attached </param>
        /// <param name="startLoop"> Is the time where you want to start the loop in seconds </param>
        /// <param name="endLoop"> Is the time where the audio will get back to start loop time in seconds </param>
        /// <returns> Returns true if the above conditions are met and returns false if none of the above conditions are met</returns>
        private static bool CheckTimeLoop(AudioSource source, float startLoop, float endLoop)
        {
            float maxTime = source.clip.length;
            if (maxTime < startLoop)
            {
                Debug.LogWarning("Start Loop Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nStart loop time is " + startLoop + " seconds");
                return true;
            }
            else if (startLoop > endLoop)
            {
                Debug.LogWarning("Start Loop Time is more than the End Loop time");
                Debug.LogWarning("End Loop time is " + endLoop + " seconds\nStart loop time is " + startLoop + " seconds");
                return true;
            }
            else if (maxTime < endLoop)
            {
                Debug.LogWarning("End Loop Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nEnd loop time is " + startLoop + " seconds");
                return true;
            }
            else if (startLoop == endLoop)
            {
                Debug.LogWarning("Start Loop Time has same value with End Loop time, will make this function is useless");
                Debug.LogWarning("End Loop time is " + endLoop + " seconds\nStart loop time is " + startLoop + " seconds");
                return true;
            }
            else return false;
        }

        /// <summary>
        /// This is the method to make exception play between start except time (exclude) and end except time (excude)
        /// Will do the looping other than the specified time range
        /// </summary>
        /// <param name="source"> Is the Audio Source you want to loop with clip attached </param>
        /// <param name="startExcept"> Is the time where you want to start the exception play (exclude) </param>
        /// <param name="endExcept"> Is the time where the audio will skip from the start time untill this time (exclude) </param>
        public static void PlayExcept(ref AudioSource source, float startExcept, float endExcept)
        {
            try
            {
                if (source.loop) source.loop = false;

                float clipTime = source.time;
                float maxTime = source.clip.length;

                if (!source.isPlaying)
                {
                    if (clipTime == maxTime)
                    {
                        source.time = 0;
                    }

                    source.Play();
                }

                if (CheckTimeExcept(source, startExcept, endExcept))
                {
                    source.Stop();
                    return;
                }

                if (clipTime >= startExcept && clipTime < endExcept)
                {
                    source.time = endExcept;
                }

            }
            catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached");
            }
            catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null");
            }
        }

        /// <summary>
        /// This method will check the exception value for start except time and end except time
        /// - Max time is the length time of clip in seconds
        /// - End Except time is not allowed to be same or more than Max Time
        /// - Start Except time is not allowed to be more than Max Time and End Except Time
        /// - Start Except time is not allowed to be same with End Except Time
        /// </summary>
        /// <param name="source"> Is the Audio Source you want to loop with clip attached </param>
        /// <param name="startExcept"> Is the time where you want to start the exception play (exclude) </param>
        /// <param name="endExcept"> Is the time where the audio will skip from the start time untill this time (exclude) </param>
        /// <returns> Returns true if the above conditions are met and returns false if none of the above conditions are met</returns>
        private static bool CheckTimeExcept(AudioSource source, float startExcept, float endExcept)
        {
            float maxTime = source.clip.length;
            if (maxTime < startExcept)
            {
                Debug.LogWarning("Start Except Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nStart Except time is " + startExcept + " seconds");
                return true;
            }
            else if (startExcept > endExcept)
            {
                Debug.LogWarning("Start Except Time is more than the End Except time");
                Debug.LogWarning("End Except time is " + endExcept + " seconds\nStart Except time is " + startExcept + " seconds");
                return true;
            }
            else if (maxTime < endExcept)
            {
                Debug.LogWarning("End Except Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nEnd except time is " + startExcept + " seconds");
                return true;
            }
            else if (startExcept == endExcept)
            {
                Debug.LogWarning("Start Except Time has same value with End Except time, will make this function is useless");
                Debug.LogWarning("End except time is " + endExcept + " seconds\nStart except time is " + startExcept + " seconds");
                return true;
            }
            else if (maxTime == endExcept)
            {
                Debug.LogWarning("End Except Time is not allowed to be same with the Max Time Clip");
                Debug.LogWarning("Max time is " + maxTime + " seconds\nEnd except time is " + endExcept + " seconds");
                return true;
            }
            else return false;
        }
    }
}