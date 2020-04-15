# AudioLoop-Unity
V.1.1

Trying to make this audio function, maybe will helps your problem with loops the audio track with specific time. 

Export Package : [TW Audio](https://bit.ly/TW_Audio_1-1) 

## How To Use

If we want to play the audio when the start method is executed.

```c#
using UnityEngine;
using System.Collections;
using TomWill;

public class Example : MonoBehaviour {

    public AudioSource audioSource;
    private AudioLoop audioLoop = new AudioLoop();
    
    private void Start(){
        audioLoop.SetAudioSource(ref audioSource);
        audioLoop.PlayLoops(AudioLoop.LoopType.LOOP_WITH_INTRO, 4 , 10);
    }
}
```


We can also change the AudioLoop setting when the game is in runtime. We can call it anytime, and the Audio will loops based on given option. Can be placed to Update method too (When you want to change the option every single frame updated).

```c#
using UnityEngine;
using System.Collections;
using TomWill;

public class Example : MonoBehaviour {

    public AudioSource audioSource;
    private AudioLoop audioLoop = new AudioLoop();
    
    private void Update(){
    
        ... // any game logic
        
        PlayAudio(AudioLoop.LoopType.LOOP_WITH_INTRO, 5 , 15);
        
        ... // any game logic
        
    }
    
    public void PlayAudio(AudioLoop.LoopType loopType, float start, float end){
        audioLoop.SetAudioSource(ref audioSource);
        audioLoop.PlayLoops(loopType, start , end);
    }

}
```


## Documentation

### PlayLoops(LoopType loopType, float start, float end)
Is a method that play the audio with loop option based on given start and end time to loops

**loopType** Loop option, to loop the audio with different option
- **LOOP_WITH_INTRO :** Is the loop option to make looping at specific track session with intro first time 
- **SPECIFIC_LOOP :** Is the loop option to make looping at specific track session without intro at the first play 
- **LOOP_EXCEPTION :** Is the loop option that loop the entire audio clip except the given value of start time until end time.
- **NONE :** Is the none option, doesn't use the Audio Loop option, but use the AudioSource option. 

**start** Is the time where the start time starts

**end** Is the end time to loop or end time to except play

---
### LOOP_WITH_INTRO
Is a loop type that execute the OnPlayWithIntro() method, this loop type will loops the audio track time at the sepecific time that we specify but will play from the first track time for the first time play and ignore it for the second loops and next. Will loops the track time between _start_ time and _end_ time after playing the intro. 

> NOTE : If The Start Loop Time is from beginning of the track time audio, this method will be useless. It's better to use **SPECIFIC_LOOP** type rather than **LOOP_WITH_INTRO** when the start time is from beginning

> Example : start = 4 , end = 36. Will loops between 4 second to 36 seconds on the track time after playing from the first track time.

> ![On Loop With Intro](https://user-images.githubusercontent.com/47166058/71138139-6059d380-2202-11ea-8a72-5badbe7ecb58.gif)

---  
  
 ### SPECIFIC_LOOP
Is a loop type that execute the OnPlay() method, this loop type will loops the audio track time only at the sepecific time that we specify. Will loops the track time between _start_ time and _end_ time

> Example : start = 4 , end = 36. Will only loops between 4 second to 36 seconds on the track time.

> ![On Loop](https://user-images.githubusercontent.com/47166058/71139059-09a1c900-2205-11ea-8b32-23b2ef12d4a8.gif)

---

 ### LOOP_EXCEPTION
Is a loop type that execute the PlayExcept() method, this loop type will loops the audio track time with ignoring the range time that we specify. Will loops the track time from the first track time until the _start_ time and immediately passed it up and assign the track time playing to _end_ time to continue playing. So the track time after _start_ time and before the _end_ time will not plays.

> Example : start = 4 , end = 10. Will playing the audio from the first tract time until 4 seconds of the track time and skip it until 10 seconds of the track time then will loops again.

> ![Play Except](https://user-images.githubusercontent.com/47166058/71139698-d3fddf80-2206-11ea-8608-bdf137d7f6e4.gif)

### NONE
Is a default type that will not use anything inside AudioLoop class function.
