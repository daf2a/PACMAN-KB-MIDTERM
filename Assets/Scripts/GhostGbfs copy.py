import sys
from collections import namedtuple
from queue import PriorityQueue

args = sys.argv

direction = [0, 0, 0, 0]
player = (args[1])
ghost = args[2]
direction[0] = args[3] #Right x+1
direction[1] = args[4] #Left x-1
direction[2] = args[5] #Up y+1
direction[3] = args[6] #Down y-1

frontier = PriorityQueue()

for i in range(4):
    newPath = [0, 0]
    newDir = [0, 0]

    if direction[i] == 1:
        newPath[i%2] = ghost[i%2] + pow(-1, i)
        newDir[i%2] = pow(-1, i)
        newPath[(i+1)%2] = ghost[(i+1)%2]

        #pytagorean distance
        heuristic = math.sqrt(pow(newPath[0] - player[0], 2) + pow(newPath[1] - player[1], 2))
        frontier.put(str('('+ str(newDir[0]) + ", " + str(newDir[1]) + ')'), heuristic)

print(frontier.get())


        



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