using UnityEngine;
using System.Diagnostics;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Set the path to your Python executable and script
        string pythonExecutable = "C:/Users/asus/AppData/Local/Microsoft/WindowsApps/python.exe";
        string pythonScript = "D:/Kuliah/(2023) S4-KB/PACMAN-KB-MIDTERM/Assets/Scripts/GhostGbfs.py";       

        // Do nothing while the ghost is frightened
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

            // Print the output to the console
            UnityEngine.Debug.Log("Output: " + output);

            string[] parts = output.Split(' ');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            Vector2 result = new Vector2(x, y);

            UnityEngine.Debug.Log("result :" + result);
            ghost.movement.SetDirection(result);
            // Print log debug direction each ghost
            // arc direction
            string arc = "";
            if(direction[0] == 1 && direction[1] == 0) arc = "right";
            else if(direction[0] == -1 && direction[1] == 0) arc = "left";
            else if(direction[0] == 0 && direction[1] == 1) arc = "up";
            else if(direction[0] == 0 && direction[1] == -1) arc = "down";

            //Debug.Log("GhostChase::OnTriggerEnter2D " + ghost.name + " direction: " + arc);
        }
    }

}
