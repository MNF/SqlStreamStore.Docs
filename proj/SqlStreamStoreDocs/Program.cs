namespace SqlStreamStoreDocs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SqlStreamStore;
    using SqlStreamStore.Streams;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(QuickStart).GetAwaiter().GetResult();
            Console.ReadLine();
        }

        public static async Task QuickStart()
        {
            var ct = CancellationToken.None;

            // Create an in-memory store
            var store = new InMemoryStreamStore();

            // Create a new message
            var message = JsonConvert.SerializeObject(new { Name = "foo" });
            var newStreamMessage = new NewStreamMessage(Guid.NewGuid(), "my-message-type", message);

            // Append to a stream
            await store.AppendToStream("stream-1", ExpectedVersion.NoStream, newStreamMessage, ct);

            // Read the stream
            var page = await store.ReadStreamForwards("stream-1", StreamVersion.Start, 10, ct);

            Console.WriteLine(page.StreamId);           // stream-1
            Console.WriteLine(page.Status);             // Success
            Console.WriteLine(page.IsEnd);              // True
            Console.WriteLine(page.FromStreamVersion);  // 0
            Console.WriteLine(page.LastStreamVersion);  // 0
            Console.WriteLine(page.NextStreamVersion);  // 1
            Console.WriteLine(page.ReadDirection);      // Forward
            Console.WriteLine(page.Messages.Length);    // 1

            Console.WriteLine(page.Messages[0].StreamId);       // stream-1
            Console.WriteLine(page.Messages[0].CreatedUtc);     // The date time
            Console.WriteLine(page.Messages[0].MessageId);      // A guid
            Console.WriteLine(page.Messages[0].Position);       // 0
            Console.WriteLine(page.Messages[0].StreamVersion);  // 0
            Console.WriteLine(page.Messages[0].Type);           // my-message-type

            // Loading the message content is async
            dynamic messageRead = await page.Messages[0].GetJsonDataAs<dynamic>(ct);

            Console.WriteLine(messageRead.Name); // "foo"

        }
    }
}