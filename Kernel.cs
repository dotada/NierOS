using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.IO;
using MediaToolkit;
using VideoLibrary;


namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        CosmosVFS fse = new Sys.FileSystem.CosmosVFS();
        protected override void BeforeRun()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fse);
            Console.WriteLine("NierOS booted successfully.");
            var ver = "1.22474487139";
            Console.WriteLine("Version:" + ver);
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            if (input == "help"){
                Console.WriteLine("help: Shows this menu");
                Console.WriteLine("space: shows available space");
                Console.WriteLine("type: shows filesystem type");
                Console.WriteLine("ls: shows files in current directory");
                Console.WriteLine("mkdir name: Creates a directory with the specified name");
                Console.WriteLine("shutdown (-r): shuts down the OS");
                Console.WriteLine("cd name: Changes active directory to name");
                Console.WriteLine("pwd: Print current working directory");
            }
            if (input == "space")
            {
                long available_space = Sys.FileSystem.VFS.VFSManager.GetAvailableFreeSpace("0:\\");
                Console.WriteLine("Available free space: " + available_space);
            }
            if (input == "type")
            {
                string fs_type = Sys.FileSystem.VFS.VFSManager.GetFileSystemType("0:\\");
                Console.WriteLine("File system type: " + fs_type);
            }
            if (input == "ls")
            {
                var current_diir = Directory.GetCurrentDirectory();
                if (current_diir == "")
                {
                    var current_dieir = "0:\\";
                    DirectoryInfo dire = new DirectoryInfo(current_dieir);
                    foreach (DirectoryInfo d in dire.GetDirectories())
                    {
                        Console.WriteLine("{0, -30}\t directory", d.Name);
                    }
                    foreach (FileInfo f in dire.GetFiles())
                    {
                        Console.WriteLine("{0, -30}\t file", f.Name);
                    }
                }
                else
                {
                    DirectoryInfo diree = new DirectoryInfo("0:\\" + current_diir.Replace(" ", ""));
                    
                    foreach (DirectoryInfo de in diree.GetDirectories())
                    {
                        Console.WriteLine("{0, -30}\t directory", de.Name);
                    }
                    foreach (FileInfo fe in diree.GetFiles())
                    {
                        Console.WriteLine("{0, -30}\t file", fe.Name);
                    }
                }
            }
            if (input.Contains("mkdir"))
            {
                var args = input.Trim();
                var args2 = args.Split("mkdir");
                var currentdir = Directory.GetCurrentDirectory();
                if (currentdir == "") {
                    Sys.FileSystem.VFS.VFSManager.CreateDirectory("0:\\" + args2[1].Replace(" ", ""));
                }
                else
                {
                    var currentdire = Directory.GetCurrentDirectory().Replace(" ", "0:\\");
                    Directory.CreateDirectory(currentdire + "\\" + args2[1].Replace(" ", ""));
                }
            }

            if (input == "shutdown")
            {
                Sys.Power.Shutdown();
            }
            if (input == "shutdown -r")
            {
                Sys.Power.Reboot();
            }
            if (input.Contains("cd"))
            {
                var args = input.Trim();
                var args2 = args.Split("cd");
                Directory.SetCurrentDirectory(args2[1]);
            }
            if (input == "pwd")
            {
                var direx = Directory.GetCurrentDirectory();
                Console.WriteLine(direx.Replace(" ", "0:\\"));
            }
        }
    }
}
