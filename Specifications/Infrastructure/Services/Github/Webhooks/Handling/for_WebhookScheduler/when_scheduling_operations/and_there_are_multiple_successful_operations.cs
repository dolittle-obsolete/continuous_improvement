// /*---------------------------------------------------------------------------------------------
//  *  Copyright (c) Dolittle. All rights reserved.
//  *  Licensed under the MIT License. See LICENSE in the project root for license information.
//  * --------------------------------------------------------------------------------------------*/

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Machine.Specifications;

// namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.when_scheduling_operations
// {
//     public class and_there_are_multiple_successful_operations : given.a_webhook_scheduler_handler
//     {
//         static List<int> values;
//         static List<given.Payload> payloads;
//         static List<int> results;

//         Establish context = () => 
//         {
//             values = Enumerable.Range(0,10).ToList();
//             results = new List<int>();
//             payloads = new List<given.Payload>();

//             values.ForEach(v => payloads.Add(new given.Payload(v)));

//             processor.Setup(_ => _.Process(Moq.It.IsAny<WebhookOperation>()))//.Callback<WebhookOperation>((_) => {Console.WriteLine($"{((given.Payload)_.Payload).Value}");results.Add(((given.Payload)_.Payload).Value);})
//                 .Returns(async(WebhookOperation _) => {
//                     await Task.Delay(10);
//                     var payload = (given.Payload)_.Payload;
//                     results.Add(payload.Number);
//                     await Task.FromResult(payload.Number);
//                 });
//         };

//         Because of = () => 
//         {
//             payloads.ForEach(_ => 
//             {
//                 var operation = BuildWebhookOperation(_);
//                 scheduler.QueueWebhookEventForHandling(operation.Handler,operation.Payload);
//             });
//         };

//         It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = async () => 
//         {
//             await Task.Delay(100);
//             results.ShouldEqual(values);
//         };
//     }
// }