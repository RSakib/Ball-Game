# Ball Demo

## Play The Demo
![image](https://user-images.githubusercontent.com/8354680/164341115-a884b939-f109-4db8-8f56-bbaac2f53252.png)
You can play the demo in your browser at this [link](https://rsakib.itch.io/ball-game)

## Table of Contents
+ [About](#about)


## About
I had a bit of free time in the beginning of my new college quarter and I had just gained better knowledge of Vector math. That knowledge combined with a want to keep my C programming skills up and a need to update my Unity skills led to this whole project.

## Goals
I had goals going into this project for what I wanted to learn or do with this project.
These goals were to:
+ Make Responsive Player Movement and Controls
+ Code in Fast Momentum
+ Create a Dynamic Camera system
+ Have a Non-Photo Realistic (NPR) style for the visuals

## Player Movement and Controls
I based my character movement off the orientation of the Camera. For example, pressing forward will make the ball go up on the screen, regardless of the actual position of the ball. All the controls were done via Unity's newer input system, which allows for easy remapping of all player functions. This new system let me implement a gamepad input map within less than an 5 minutes, which would not be possible by hard coding the inputs. With these two concepts for my player movement, I implemented 4 degrees of movement, a jump, and a stomp within this Demo.

## Momentum
I did't state this before, but I tied the player controller directly to Unity's rigidbody system in order to make easier dynamic movement within the Demo. With rigidbody, the movement definitely was happening, but it was not really that fun. Everything felt to floaty or slow even with gravity acting down onto the player. To spruce this up, I made sure that gradual forces would effect the player as they either fell or went down a slope in order to simulate momentum, similar to what is found in the Sonic the Hedgehog games. The process implementing this took a majority of dev time as it took a lot of error checking and Vector Math. You can read more about the details in the annotated file over in Ball-Game/Assets/Ball Scripts/TestBallControl.cs.

## Camera System
This was much simpler than the other two as I simply used a Cinemachine rig that I had to tweak in order to provide good coverage of the player. I spruced it up a little by tuning the camera to expand its FOV for when the player break a speed threshold.

## Visuals
This was the simplest part as I am very experienced within the 3D tool of Blender. I wanted the visual to look cartoony so I add a 3D effected by adding a second mesh to every object in the scene that acts as the line art to every object. I could have gone deeper into Unity Shader code as well, but I just needed to finish this game within a week.

## Conclusion
I learned a lot, and I can be a lot faster for the next project. That's it.
