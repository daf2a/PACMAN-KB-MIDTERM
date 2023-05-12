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

            // Run API every 60 frames rendered or is in intersection
            if (framesRendered % 60 == 0 || availableDirection.Length > 2) {
                // Set the path to your Python executable and script
                string pythonExecutable = "C:/Users/asus/AppData/Local/Microsoft/WindowsApps/python.exe";
                string pythonScript = "D:/Kuliah/(2023) S4-KB/PACMAN-KB-MIDTERM/Assets/Scripts/GhostGbfs.py";

                // // GET TIME NOW BEFORE RUNNING SCRIPT
                DateTime now = DateTime.UtcNow;
                TimeSpan timeSinceEpoch = now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                long epoch_before = (long)timeSinceEpoch.TotalSeconds;
                UnityEngine.Debug.Log("Before running script: " + epoch_before);

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

                UnityEngine.Debug.Log("result :" + result);
                ghost.movement.SetDirection(result);

                // // GET TIME NOW AFTER RUNNING SCRIPT
                DateTime now2 = DateTime.UtcNow;
                TimeSpan timeSinceEpoch2 = now2 - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                long epoch_after = (long)timeSinceEpoch2.TotalSeconds;
                UnityEngine.Debug.Log("After running script: " + epoch_after);

                // CALCULATE LATENCY
                long latency = epoch_after - epoch_before;
                UnityEngine.Debug.Log("Latency: " + latency);
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
