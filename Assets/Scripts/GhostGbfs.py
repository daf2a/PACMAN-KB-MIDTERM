import sys
from collections import namedtuple

# Get the command-line arguments
args = sys.argv

# Check that at least one argument was passed
# if len(args) > 1:
#     # Get the input variable
#     input_variable = args[1]

#     # Print the input to the console
#     print("Received input: " + input_variable)
# else:
#     print("No input variable was provided.")

# Pair = namedtuple('Pair', ['x', 'y'])
# ghost = Pair(args[1], args[2])
# player = Pair(args[3], args[4])

player = args[1]
ghost = args[2]
directionRight = args[3]
directionLeft = args[4]
directionUp = args[5]
directionDown = args[6]

string = "Ghost: " + str(ghost) + " Player: " + str(player) + " Direction: " + str(directionRight)
print(string)