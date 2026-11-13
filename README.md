# Endless Driving Game

A 3D endless driving game I made in Unity with C#. You drive down a road that never ends, dodge traffic, and try to get the highest score you can.

I built this to get better at Unity and C#, mostly by working on the systems that make a game like this run.

<img width="1909" height="956" alt="image" src="https://github.com/user-attachments/assets/f548b11f-df75-4932-88c5-f1d9dc050fc7" />

## What's in it

- Car movement and handling with Unity physics
- Endless road that generates itself from random sections, with buildings on the sides
- Random traffic, with AI cars that drive on their own
- Object pooling, so roads, cars, and props get reused instead of constantly created and destroyed
- Collisions that wreck the car when you hit something
- Distance tracking
- Menus, UI, and sound effects
- More than one car to pick from

## How the endless road works

The road is built from section prefabs, each 26 units long. When the game starts, `EndlessLevelHandler` creates a pool of 20 sections, cycling through the prefabs I made, and switches them all off. It then takes 10 of them at random from the pool and lines them up along the Z axis to make the starting road.

Nothing gets created or destroyed while you drive. Every 0.1 seconds, a coroutine checks each section. If one has fallen more than a section length behind the player, it's switched off and sent back to the pool. A random inactive section is picked, moved to the front of the road, and switched on. Since the pool holds 20 sections and only 10 are ever active, there's always a free one available.

To hide sections popping into view, each one spawns 10 units below the ground. `EndlessSectionHandler` checks how far the section is from the player and smoothly lifts it up to ground level as the car gets closer, so the road rises into place instead of appearing out of nowhere.

## How the car works

The car runs on a Unity Rigidbody, handled in a script called `CarHandler`. It reads a simple input (left/right for steering, up/down for gas and brake) and turns it into forces.

- **Gas and brake:** gas pushes the car forward until it hits a top speed. Brakes only work while the car is moving forward, and the car can't reverse.
- **Steering:** sideways force scales with forward speed, so you can't turn while standing still. Sideways speed is capped so the car doesn't slide around, and it centers itself when you let go.
- **Sound:** engine pitch follows speed through an animation curve, and the skid sound fades in when you brake hard at speed.
- **Crashes:** on impact, the car blows apart (`ExplodeHandler`) using its current speed, plays a crash sound that gets louder the faster you were going, and the game drops into slow motion for a moment before returning to normal.

The same script drives the traffic cars. It checks the object's tag to decide if it's the player or an AI car, and AI cars only explode when they hit the player, not each other or the scenery.

## Run it yourself

1. Install Unity [6.6, check ProjectSettings/ProjectVersion.txt]
2. Clone the repo
   ```
   git clone https://github.com/Abdullahkamran009/Endless-Driving-Game.git
   ```
3. Open the project folder in Unity Hub
4. Open the main scene [Main Menu] and press Play

## Built with

- Unity [6.6]
- C#
- Git and GitHub

## Credits

The 3D city and Halloween models are free packs from [KayKit](https://kaylousberg.itch.io/). Everything else, including the code, is mine.

## What's next

I want to put this on a phone, so these are planned:

- [ ] Touch controls (tilt or on-screen buttons)
- [ ] Adjust the UI and performance for mobile
- [ ] Build and test on Android
- [ ] Publish it

The car takes its input through one method, `SetInput(Vector2)`, so nothing in it cares where the input comes from. That's why adding touch controls for mobile should be simple.

I'll update this list as I get through it.
