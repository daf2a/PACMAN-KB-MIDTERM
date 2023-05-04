import sys
import math
from collections import namedtuple
import heapq

TILE_SIZE = 256

args = sys.argv

direction = []
player = tuple(float(x) for x in args[1][1:-1].split(','))
ghost = tuple(float(x) for x in args[2][1:-1].split(','))

i = 0
while i < 4:
    direction.append(int(args[i+3]))
    i += 1

# # Data Dump
# direction = [0, 0, 0, 0]
# player = (1.50, -8.53, -5.00)
# ghost = (2.15, 2.50, 0.00)
# direction[0] = 1 #Right x+1 0 1 
# direction[1] = 0 #Left x-1 0 -1
# direction[2] = 1 #Up y+1 1 1
# direction[3] = 1 #Down y-1 1 -1

frontier = []

# cara if else
availableDirections = []
if(direction[0] == 1):
    availableDirections.append((1, 0))
if(direction[1] == 1):
    availableDirections.append((-1, 0))
if(direction[2] == 1):
    availableDirections.append((0, 1))
if(direction[3] == 1):
    availableDirections.append((0, -1))

for i in range(len(availableDirections)):
    newPath = [0, 0]
    newDir = [0, 0]

    newPath[0] = ghost[0] + availableDirections[i][0] * TILE_SIZE
    newPath[1] = ghost[1] + availableDirections[i][1] * TILE_SIZE
    newDir[0] = availableDirections[i][0]
    newDir[1] = availableDirections[i][1]

    #pytagorean distance
    heuristic = math.sqrt((newPath[0] - player[0])**2 + (newPath[1] - player[1])**2)
    heapq.heappush(frontier, (heuristic, str(str(newDir[0]) + " " + str(newDir[1]) )))

string = str(heapq.heappop(frontier)[1])
print(string)


## Cara for loop
# for i in range(4):
#     newPath = [0, 0]
#     newDir = [0, 0]

#     if direction[i] == 1:
#         newPath[int(i/2)] = ghost[int(i/2)] + pow(-1, i)
#         newDir[int(i/2)] = pow(-1, i)

#         newPath[int(((i/2)+1)%2)] = ghost[int(((i/2)+1)%2)]

#         #pytagorean distance
#         heuristic = math.sqrt(pow(newPath[0] - player[0], 2) + pow(newPath[1] - player[1], 2))
#         heapq.heappush(frontier, (heuristic, str(str(newDir[0]) + " " + str(newDir[1]) )))

# string = str(heapq.heappop(frontier)[1])
# print(string)


## Dump
# string = str(ghost) + ", " + str(player) + ", " + str(direction[0]) + ", " + str(direction[1]) + ", " + str(direction[2]) + ", " + str(direction[3])
# print(string)
        

#             string = "Ghost: " + str(ghost) + " Player: " + str(player) + " Direction: " + str(directionRight)
#             print(string)

#             Vector2 direction = Vector2.zero;
#             float minDistance = float.MaxValue;

#             // Find the available direction that moves closet to pacman
#             foreach (Vector2 availableDirection in node.availableDirections)
#             {
#                 // If the distance in this direction is less than the current
#                 // min distance then this direction becomes the new closest
#                 Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
#                 float distance = (ghost.target.position - newPosition).sqrMagnitude;

#                 if (distance < minDistance)
#                 {
#                     direction = availableDirection;
#                     minDistance = distance;
#                 }
#             }

#             ghost.movement.SetDirection(direction);