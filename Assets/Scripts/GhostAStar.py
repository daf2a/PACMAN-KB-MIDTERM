import sys
import math
from collections import namedtuple
from queue import PriorityQueue
import heapq

TILE_SIZE = 256

args = sys.argv

# get player position from argument
player = tuple(float(x) for x in args[1][1:-1].split(','))

# get ghost position from argument
ghost = tuple(float(x) for x in args[2][1:-1].split(','))

# iterate to the end of the argument to get available path positions
availablePath = []
for i in range(3, len(args)):
    availablePath.append(tuple(float(x) for x in args[i][1:-1].split(',')))

# get the shortest available path from ghost to player using A* algorithm
frontier = PriorityQueue()
frontier.put(ghost, 0)
came_from = {}
cost_so_far = {}
came_from[ghost] = None
cost_so_far[ghost] = 0

while not frontier.empty():
    current = frontier.get()

    if current == player:
        break

    for next in availablePath:
        new_cost = cost_so_far[current] + TILE_SIZE
        if next not in cost_so_far or new_cost < cost_so_far[next]:
            cost_so_far[next] = new_cost
            priority = new_cost + math.sqrt((player[0] - next[0])**2 + (player[1] - next[1])**2)
            frontier.put(next, priority)
            came_from[next] = current

# get the shortest path from ghost to player
path = []
current = player
while current != ghost:
    path.append(current)
    current = came_from[current]

# get the direction from ghost to player
direction = []
for i in range(len(path)):
    direction.append((path[i][0] - path[i+1][0], path[i][1] - path[i+1][1]))

# print the direction
print(direction[0][0], direction[0][1])

