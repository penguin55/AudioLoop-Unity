// AudioLoop version 1.1
using System;
using UnityEngine;

namespace TomWill
{ 
    public class AudioLoop
    {
        public enum LoopType { LOOP_WITH_INTRO, SPECIFIC_LOOP, LOOP_EXCEPTION, NONE }
        private LoopType currentLoopType = LoopType.NONE;

        private AudioLoopCallbacks callbacks;
        private AudioSource source;

        private float start;
        private float end;

        /// <summary>
        /// Set the audio source to define where the audio source being affected by this AudioLoop function
        /// </summary>
        /// <param name="source">Is the audio source will be modify to loop the music</param>
        public void SetAudioSource(ref AudioSource source)
        {
            this.source = source;
            if (callbacks == null) callbacks = source.transform.gameObject.AddComponent<AudioLoopCallbacks>();
        }

        /// <summary>
        /// Playing audio with loop option based on given start and end time to loops
        /// </summary>
        /// <param name="loopType">
        ///     Loop option, to loop the audio with different option
        ///     <br> - LOOP_WITH_INTRO : Is the loop option to make looping at specific track session with intro first time </br>
        ///     <br> - SPECIFIC_LOOP : Is the loop option to make looping at specific track session without intro at the first play </br>
        ///     <br> - LOOP_EXCEPTION : Is the loop option that loop the entire audio clip except the given value of start time until end time </br>
        ///     <br> - NONE : Is the none option, doesn't use the Audio Loop option, but use the AudioSource option. </br>
        ///     <br></br>
        ///     <br>Note : Intro is the first track time on audio clip before the start value.</br>
        /// </param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void PlayLoops(LoopType loopType, float start, float end)
        {
            this.start = start;
            this.end = end;

            if (currentLoopType != loopType)
            {
                currentLoopType = loopType;

                switch (currentLoopType)
                {
                    case LoopType.LOOP_WITH_INTRO:
                        callbacks.SetCallbacks(OnLoopWithIntro_Callbacks);
                        break;
                    case LoopType.SPECIFIC_LOOP:
                        callbacks.SetCallbacks(OnLoop_Callbacks);
                        break;
                    case LoopType.LOOP_EXCEPTION:
                        callbacks.SetCallbacks(PlayExcept_Callbacks);
                        break;
                }
            }
            
        }

        /// <summary>
        /// This is the method to make looping at specific track session with intro first time
        /// Will be seen when the start loop time is not the start time of the clip
        /// </summary>
        private void OnLoopWithIntro_Callbacks()
        {
            try
            {
                if (source.loop) source.loop = false;

                float clipTime = source.time;
                float maxTime = source.clip.length;

                if (!source.isPlaying) source.Play();

                if (CheckTimeLoop())
                {
                    source.Stop();
                    return;
                }

                if (clipTime >= end)
                {
                    source.time = start;
                }
            }
            catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached" + er.Message);
                callbacks.Unsubscribe(OnLoopWithIntro_Callbacks);
            }
            catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null" + erRefference.Message);
                callbacks.Unsubscribe(OnLoopWithIntro_Callbacks);
            }
        }

        /// <summary>
        /// This is the method to make looping at specific track session without intro music, 
        /// The audio will playing between start loop time and end loop time
        /// </summary>
        public void OnLoop_Callbacks()
        {
            try
            {
                if (source.loop) source.loop = false;

                float clipTime = source.time;
                float maxTime = source.clip.length;

                if (!source.isPlaying) source.Play();

                if (CheckTimeLoop())
                {
                    source.Stop();
                    return;
                }

                if (clipTime < start)
                {
                    source.time = start;
                }

                if (clipTime >= end)
                {
                    source.time = start;
                }
            }
            catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached : " + er.Message);
                callbacks.Unsubscribe(OnLoop_Callbacks);
            }
            catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null : " + erRefference.Message);
                callbacks.Unsubscribe(OnLoop_Callbacks);
            }
        }

        /// <summary>
        /// This method will check the exception value for start loop time and end loop time
        /// <br> - Max time is the length time of clip in seconds </br>
        /// <br> - Start Loop time is not allowed to be more than Max Time and End Loop Time </br>
        /// <br> - Start Loop time is not allowed to be same with End Loop Time </br>
        /// <br> - End Loop time is not allowed to be more than Max Time </br>
        /// </summary>
        /// <returns> Returns true if the above conditions are met and returns false if none of the above conditions are met</returns>
        private bool CheckTimeLoop()
        {
            float maxTime = source.clip.length;
            if (maxTime < start)
            {
                Debug.LogWarning("Start Loop Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nStart loop time is " + start + " seconds");
                return true;
            }
            else if (start > end)
            {
                Debug.LogWarning("Start Loop Time is more than the End Loop time");
                Debug.LogWarning("End Loop time is " + end + " seconds\nStart loop time is " + start + " seconds");
                return true;
            }
            else if (maxTime < end)
            {
                Debug.LogWarning("End Loop Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nEnd loop time is " + start + " seconds");
                return true;
            }
            else if (start == end)
            {
                Debug.LogWarning("Start Loop Time has same value with End Loop time, will make this function is useless");
                Debug.LogWarning("End Loop time is " + end + " seconds\nStart loop time is " + start + " seconds");
                return true;
            }
            else return false;
        }

        /// <summary>
        /// This is the method to make exception play between start except time (exclude) and end except time (excude)
        /// Will do the looping other than the specified time range
        /// </summary>
        public void PlayExcept_Callbacks()
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

                if (CheckTimeExcept())
                {
                    source.Stop();
                    return;
                }

                if (clipTime >= start && clipTime < end)
                {
                    source.time = end;
                }

            }
            catch (NullReferenceException er)
            {
                Debug.LogWarning("The Audio Source is dont have clip attached" + er.Message);
                callbacks.Unsubscribe(PlayExcept_Callbacks);
            }
            catch (UnassignedReferenceException erRefference)
            {
                Debug.LogWarning("The Audio Source is null" + erRefference.Message);
                callbacks.Unsubscribe(PlayExcept_Callbacks);
            }
        }

        /// <summary>
        /// This method will check the exception value for start except time and end except time
        /// <br>- Max time is the length time of clip in seconds </br>
        /// <br>- End Except time is not allowed to be same or more than Max Time </br>
        /// <br>- Start Except time is not allowed to be more than Max Time and End Except Time </br>
        /// <br>- Start Except time is not allowed to be same with End Except Time </br>
        /// </summary>
        /// <returns> Returns true if the above conditions are met and returns false if none of the above conditions are met</returns>
        private bool CheckTimeExcept()
        {
            float maxTime = source.clip.length;
            if (maxTime < start)
            {
                Debug.LogWarning("Start Except Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nStart Except time is " + start + " seconds");
                return true;
            }
            else if (start > end)
            {
                Debug.LogWarning("Start Except Time is more than the End Except time");
                Debug.LogWarning("End Except time is " + end + " seconds\nStart Except time is " + start + " seconds");
                return true;
            }
            else if (maxTime < end)
            {
                Debug.LogWarning("End Except Time is more than the maximum time in Clip played");
                Debug.LogWarning("You are playing " + source.clip.name + "(" + source.clip.length + "seconds)\nEnd except time is " + start + " seconds");
                return true;
            }
            else if (start == end)
            {
                Debug.LogWarning("Start Except Time has same value with End Except time, will make this function is useless");
                Debug.LogWarning("End except time is " + end + " seconds\nStart except time is " + start + " seconds");
                return true;
            }
            else if (maxTime == end)
            {
                Debug.LogWarning("End Except Time is not allowed to be same with the Max Time Clip");
                Debug.LogWarning("Max time is " + maxTime + " seconds\nEnd except time is " + end + " seconds");
                return true;
            }
            else return false;
        }
    }
}