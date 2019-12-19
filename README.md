# AudioLoop-Unity
V.1.0

Trying to make this audio function, maybe will helps your problem. 

Export Package : [TW Audio](http://bit.ly/TW-Audio)

## Documentation

---
### OnLoopWithIntro(ref AudioSource source, float startLoop, float endLoop)
Is a method that loops audio track time at the sepecific time that we specify but will play from the first track time for the first time and ignore it for the second loops and next. Will loops the track time between startLoop time and endLoop time after playing the intro. 

**source** : is a reference parameter will modify directly the AudioSource we are use for outputing the Audio. The AudioSource must be have the clip before using this method.

**startLoop** : is the start time when we will start the audio to loops in seconds. The valid value is 0 seconds until less then end loop time's value. 

**endLoop** : is the end time the audio will loops in seconds and back to start time to loops again. The valid value is more than start loop time's value until the clip length in seconds. 

> NOTE : If The Start Loop Time is from beginning of the track time audio, this method will be useless. It's better to use OnLoop method rather than OnLoopWithIntro when the start time is from beginning

> Example : startLoop = 4 , endLoop = 36. Will loops between 4 second to 36 seconds on the track time after playing from the first track time.

> ![On Loop With Intro](https://user-images.githubusercontent.com/47166058/71138139-6059d380-2202-11ea-8a72-5badbe7ecb58.gif)

---  
  
 ### OnLoop(ref AudioSource source, float startLoop, float endLoop)
Is a method that loops audio track time only at the sepecific time that we specify. Will loops the track time between startLoop time and endLoop time

**source** : is a reference parameter will modify directly the AudioSource we are use for outputing the Audio. The AudioSource must be have the clip before using this method. 

**startLoop** : is the start time when we will start the audio to loops in seconds. The valid value is 0 seconds until less then end loop time's value.

**endLoop** : is the end time the audio will loops in seconds and back to start time to loops again. The valid value is more than start loop time's value until the clip length in seconds.

> Example : startLoop = 4 , endLoop = 36. Will only loops between 4 second to 36 seconds on the track time.

> ![On Loop](https://user-images.githubusercontent.com/47166058/71139059-09a1c900-2205-11ea-8b32-23b2ef12d4a8.gif)

---

 ### PlayExcept(ref AudioSource source, float startExcept, float endExcept)
Is a method that loops audio track time with ignoring the range time that we specify. Will loops the track time until the startExcept time and immediately passed it up and assign the track time playing to endExcept time to continue paying. So the track time after startExcept time and before the endExcept time will not plays.

**source** : is a reference parameter will modify directly the AudioSource we are use for outputing the Audio. The AudioSource must be have the clip before using this method. 

**startExcept** : is the start time when we will start the audio to skip in seconds. The valid value is 0 seconds until less then end loop time's value.

**endExcept** : is the end time of audio skipped, the track time before endExcept and after the startExcept time will not plays. The valid value is more than start loop time's value until less then the clip length in seconds.

> Example : startLoop = 4 , endLoop = 10. Will playing the audio until 4 seconds of the track time and skip it until 10 seconds of the track time then will loops again.

> ![Play Except](https://user-images.githubusercontent.com/47166058/71139698-d3fddf80-2206-11ea-8608-bdf137d7f6e4.gif)
