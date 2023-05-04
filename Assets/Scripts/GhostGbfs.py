import sys
from collections import namedtuple

# Get the command-line arguments
args = sys.argv

# Create a named tuple to store the data
player = args[1]
ghost = args[2]
available_directions = []

i = 0
while (i < 4):
    available_directions.append(args[i+3])
    i += 1

# RIGHT = 0
# LEFT = 1
# UP = 2
# DOWN = 3

# Get euclidean distance between ghost and player for each direction
# RIGHT
if (available_directions[0] == "1"):
    min_distance = (ghost[0] - player[0])**2 + (ghost[1] - player[1])**2
    min_direction = 0

# LEFT
if (available_directions[1] == "1"):
    distance = (ghost[0] - player[0])**2 + (ghost[1] - player[1])**2
    if (distance < min_distance):
        min_direction = 1
        min_distance = distance

# UP
if (available_directions[2] == "1"):
    distance = (ghost[0] - player[0])**2 + (ghost[1] - player[1])**2
    if (distance < min_distance):
        min_direction = 2
        min_distance = distance

# DOWN
if (available_directions[3] == "1"):
    distance = (ghost[0] - player[0])**2 + (ghost[1] - player[1])**2
    if (distance < min_distance):
        min_direction = 3
        min_distance = distance

# Return the direction with the minimum distance
print(min_direction)
