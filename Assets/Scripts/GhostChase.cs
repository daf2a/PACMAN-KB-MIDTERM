using UnityEngine;
using System.Diagnostics;
using System;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        GameManager gameManager = FindObjectOfType<GameManager>();
        int framesRendered = gameManager.framesRendered;

        // Do nothing while the ghost is frightened
        // and one execeution per second
        // UnityEngine.Debug.Log("Frames : " + framesRendered);
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            int[] directions = {0, 0, 0, 0};
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                if(availableDirection.x == 1 && availableDirection.y == 0) directions[0] = 1;
                else if(availableDirection.x == -1 && availableDirection.y == 0) directions[1] = 1;
                else if(availableDirection.x == 0 && availableDirection.y == 1) directions[2] = 1;
                else if(availableDirection.x == 0 && availableDirection.y == -1) directions[3] = 1;
            }
            Vector3 targetPosition = ghost.target.position;
            Vector3 ghostPosition = transform.position;
            
            // Run API every 2 second
            if(framesRendered % 2 == 0 && node.availableDirections.Count > 2){
                UnityEngine.Debug.Log("Frames : " + framesRendered);
                gameManager.SetFramesRendered(1);
                // Set the path to your Python executable and script
                string pythonExecutable = "C:/Users/asus/AppData/Local/Microsoft/WindowsApps/python.exe";
                string pythonScript = "D:/Kuliah/(2023) S4-KB/PACMAN-KB-MIDTERM/Assets/Scripts/GhostGbfs.py";

                // Create a new process to run the Python script
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = pythonExecutable;
                startInfo.Arguments = "\"" + pythonScript + "\" \"" + targetPosition + "\" \"" + ghostPosition + "\" \"" + directions[0] + "\" \"" + directions[1] + "\" \"" + directions[2] + "\" \"" + directions[3] + "\"";
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;

                Process process = new Process();
                process.StartInfo = startInfo;

                // Start the process and read the output
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                string[] parts = output.Split(' ');

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                Vector2 result = new Vector2(x, y);

                UnityEngine.Debug.Log("result :" + result + " ghost : " + ghost.name);
                ghost.movement.SetDirection(result);

            } else {
                // Do nothing while the ghost is frightened
                if (node != null && enabled && !ghost.frightened.enabled)
                {
                    // Pick a random available direction
                    int index = UnityEngine.Random.Range(0, node.availableDirections.Count);

                    // Prefer not to go back the same direction so increment the index to
                    // the next available direction
                    if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction)
                    {
                        index++;

                        // Wrap the index back around if overflowed
                        if (index >= node.availableDirections.Count) {
                            index = 0;
                        }
                    }
                    ghost.movement.SetDirection(node.availableDirections[index]);
                }
            }

            // Print the output to the console
            // UnityEngine.Debug.Log("Output: " + output);
            // Print log debug direction each ghost
            // arc direction
            // string arc = "";
            // if(direction[0] == 1 && direction[1] == 0) arc = "right";
            // else if(direction[0] == -1 && direction[1] == 0) arc = "left";
            // else if(direction[0] == 0 && direction[1] == 1) arc = "up";
            // else if(direction[0] == 0 && direction[1] == -1) arc = "down";

            //Debug.Log("GhostChase::OnTriggerEnter2D " + ghost.name + " direction: " + arc);

        } 
    }

}

#pragma warning disable CS0436
