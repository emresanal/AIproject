# CS 461 Project - Tank Game - Tier 1

Group 4\
Eda Ayan\
İrem Tekin\
Kaan Emre Sanal\
Umut Ada Yürüten\
Gökay Turhanoğlu

## Running Instructions
For Search Agents:

1) Place the red circle and the player on a location on the map. The coordinates of both must be increments of 0.25 (e.g (15.5, 6.25, 0.0)). The Z coordinate must be zero.
2) On the player, enable the search agent script you want and disable all other scripts except "Box Collider 2D", and "Sprite Renderer" (The preset object "Player" has all scripts configured on it.)
3) Press play and watch the player find the red circle on the map.

For Adversarial Search Agents:
1) Place the red circle and the players on a location on the map. The coordinates of both must be increments of 0.25 (e.g (13.0, 6.75, 0.0)). The Z coordinate must be zero.
2) On the player, enable the adversarial search agent script you want and disable all other scripts except "Script 1", "Box Collider 2D", and "Sprite Renderer"(The preset object "Player(2)" has all scripts configured on it.)
3) On the enemy player, enable "Random Movement Script" and disable all other scripts except "Script 1", "Box Collider 2D", and "Sprite Renderer"(The preset object "Player(3)" has all scripts configured on it.)
4) The enemy will be moving randomly while your player will be trying to seek and destroy its enemy. Both tanks will shoot if the enemy is in their sightline.

## Warning
Placing two tanks far away will reduce their effectiveness unless the search depth is increased. Increasing the search depth increases the processsing time significantly and may cause Unity to freeze or crash. Search depth can be configured by changing the "private static int max_depth = 2;" in the desired adversarial search script.

## Bonus
Turning "Player Movement" and "Shooting" scripts (and turning off anything else) will let you control the tank manually by using WASD and arrow keys for movement and LCTRL and MOUSE1 for shooting.
