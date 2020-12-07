﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CommandAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            using (var context = new CommandContext())
            {
                #region Read
                        Console.WriteLine("Retrieving  all commands on the db");
                        List<Command> commandlist = context.command.OrderBy(c => c.id).ToList();
                        Console.WriteLine("Done! commands: ");
                        int cmdAmount = commandlist.Count;
                        for (int i = 0; i < cmdAmount; i++)
                        {
                            Console.WriteLine("Id: "+commandlist[i].id +"; CommandLine: "+commandlist[i].ds_commandLine);   
                        }

                        Console.WriteLine("Counting the amount of commands in db");
                        Console.WriteLine("Done! amount : " + cmdAmount); 

                    #endregion
                #region Create
                        Console.WriteLine("Adding a new command to the db...");
                        var newCommand = new Command
                        {
                            ds_howTo = "Open the cmd and execute it with enter",
                            nm_platform ="Windows git",
                            ds_commandLine = "git add ."
                        };
                        context.Add(newCommand);
                        Console.WriteLine("Added " + newCommand +"!");
                        
                    #endregion
                Console.WriteLine("Saving changes...");
                try{
                    context.SaveChanges();
                    Console.Write("Changes saved!");                        
                }
                catch (Exception e){
                    Console.WriteLine("Concurrency conflict");
                    Console.WriteLine(e.ToString().Substring(0,255)+"...");
                }
                #region Read (here for testing, not going to be implemented)
                    Console.WriteLine("Retrieving  all commands on the db");
                    List<Command> commandlist2 = context.command.OrderBy(c => c.id).ToList();
                    Console.WriteLine("Done! commands: ");
                    int cmdAmount2 = commandlist2.Count;
                    for (int i = 0; i < cmdAmount2; i++)
                    {
                            Console.WriteLine("Id: "+commandlist2[i].id +"; CommandLine: "+commandlist2[i].ds_commandLine);   
                    }
                    Console.WriteLine("Counting the amount of commands in db");
                    Console.WriteLine("Done! amount : " + cmdAmount2); 
                #endregion
                //CreateWebHostBuilder(args).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
