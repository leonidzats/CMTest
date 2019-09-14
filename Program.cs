using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Netflix.App;
using Netflix.Models;
using Netflix.App.Api;
using Hanssens.Net;
using System.Linq;

namespace dotnetproj
{
    class Program
    {
        static IServiceProvider BootstrapServices(string[] args){
            bool is_persistent = args.Contains("--persistToLocal");

            var coolection = new ServiceCollection()
            .AddSingleton<LocalStorage>()
            .AddSingleton<LocalStorageRepository>();

            if (is_persistent)
                coolection.AddSingleton<IUserRepository,LocalStorageRepository>();
            else
                coolection.AddSingleton<IUserRepository,InMemoryRepository>();
            
            coolection.AddSingleton<INetflixApi,NetflixApi>()
            .AddSingleton<NetflixApp>();
           return coolection.BuildServiceProvider();
        }
        static bool handleCommand(NetflixApp app,string command,ref string user_id){
            bool stop_execution = false;
            switch(command){    
                case "e":
                case "E":
                    
                    stop_execution = true;
                    break;
                case "s":
                case "S":
                    
                    Console.WriteLine("swtching" );
                    Console.Write("user name: ");
                    user_id = Console.ReadLine();
                    app.login(user_id);
                    
                    break;
                case "h":
                case "H":
                    Console.WriteLine($"user : {user_id}");
                    var history_list = app.GetUserHistory(user_id);
                    foreach(var entry in history_list){
                        Console.WriteLine("\t"+entry);
                    }
                    break;
                case "c":
                case "C":
                    Console.WriteLine("(1 – TV Show, 2 – Movie, 3 – Any)" );
                    var content_coice = Console.ReadKey().KeyChar;
                    while (content_coice != '1' &&
                            content_coice != '2' &&
                            content_coice != '3') {
                                content_coice = Console.ReadKey().KeyChar;
                            } 
                    Console.WriteLine();

                    
                    Watchable now_watching = app.WatchSomething(user_id,content_coice.ToString());
                    var watching_done = 'n';
                    while(watching_done != 'Y'){
                        Console.WriteLine($"now watching : {now_watching}" );
                        Thread.Sleep(10*1000);
                        Console.WriteLine("R u done watching? (Y/n)" );
                        watching_done = Console.ReadKey().KeyChar;
                    }
                    Console.WriteLine();
                    Console.WriteLine($"thanks for watching :  {now_watching.title}" );
                    Console.WriteLine("please rank the content you just watched (0-10) :" );
                    float rank;
                    while(!float.TryParse(Console.ReadLine(), out rank))
                    {
                        Console.Clear();
                        Console.WriteLine("You entered an invalid rank" );
                        Console.WriteLine("please rank the content you just watched (0-10):" );
                    }
                    app.UpdateContentRank(user_id,now_watching,rank);
                    Console.WriteLine("thank you for your input!!" );
                    break;

            }
            return stop_execution;
        }
        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = BootstrapServices(args);
                var app = serviceProvider.GetService<NetflixApp>();
                bool stop = false;
                Console.Write("user name: ");
                var user_id = Console.ReadLine();
                app.login(user_id);
                
                while (!stop){
                    try
                    {
                        Console.WriteLine("content (c) , history (h) , exit (e) , switch user(s): ");
                        var command = Console.ReadKey().KeyChar.ToString();
                        Console.WriteLine();
                        stop = handleCommand(app,command,ref user_id);    
                    }
                    catch (System.Exception inner_ex)
                    {
                        Console.WriteLine($"an error has occured : {inner_ex.Message}");
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        Console.WriteLine();
                        Console.Clear();
                    }
                }
                Console.WriteLine("bye bye!");    
            }
            catch (System.Exception global_ex)
            {
                Console.WriteLine($"a fatal error has occured : {global_ex.Message}");
                Console.WriteLine("press any key to quit");
                Console.ReadKey();
            }
            
        }
    }
}
