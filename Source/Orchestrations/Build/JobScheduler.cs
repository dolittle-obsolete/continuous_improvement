/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Orchestrations.Build
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    public class JobScheduler : IPerformer<Context>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Perform(Context context)
        {
            // Cleanup non-active Build jobs
            // If any build jobs are running that we are not tracking - start tracking them

            //while (!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(10);
            var config = new KubernetesClientConfiguration { Host = "http://127.0.0.1:8001" };
            var client = new Kubernetes(config);

            var @namespace = "dolittle";
            

            var metadata = new V1ObjectMeta
            {
                Name = Guid.NewGuid().ToString(),
            };

            var job = new V1Job
            {
                Metadata = metadata,
                Spec = new V1JobSpec
                {
                    Template = new V1PodTemplateSpec
                    {
                        Metadata = metadata,
                        Spec = new V1PodSpec 
                        {
                            Containers = new [] {
                                new V1Container {
                                    Name = "build",
                                    Image = "dolittlebuild/dotnet",
                                    ImagePullPolicy = "IfNotPresent",
                                    Env = new [] {
                                        new V1EnvVar("REPOSITORY","https://github.com/dolittle/DotNET.Fundamentals.git"),
                                        new V1EnvVar("COMMIT","beb7544a44dff9283ba2f1d5c3cc8a567dfffa6c")
                                    }
                                }
                            },
                            RestartPolicy = "Never"
                        }
                    }
                }
            };

            Console.WriteLine("Starting job");
            var resetEvent = new ManualResetEventSlim(false);

            Task.Run(async () => {
                var status = await client.CreateNamespacedJobAsync(job, @namespace);
                for(;;) 
                {
                    Thread.Sleep(500);
                    status = await client.ReadNamespacedJobStatusAsync(metadata.Name, @namespace);
                    Console.Write($".");
                    if( (status.Status.Active ?? 0) == 0 ) 
                    {
                        Console.WriteLine("\nDone");
                        // Cleanup
                        resetEvent.Set();
                        break;
                    }
                    
                    
                }
            });

            resetEvent.Wait();           
        }
    }
}