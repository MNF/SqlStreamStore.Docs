===========
Quick Start
===========

The following platforms are supported:

* .NET 4.6 and up.
* .NET Core / NetStandard in due course.

This example show the simplest way to append a message to a stream and read it
back again.

.. code-block:: c#

    // Create an in-memory store
    var store = new InMemoryStreamStore();

    // Create a new message
    var message = JsonConvert.SerializeObject(new { Name = "foo" });
    var messageId = Guid.NewGuid();
    var newStreamMessage = new NewStreamMessage(messageId, "my-message-type", message);

    // Append to a stream
    await store.AppendToStream("stream-1", ExpectedVersion.NoStream, newStreamMessage, ct);

    // Read the stream
    var page = await store.ReadStreamForwards("stream-1", StreamVersion.Start, 10, ct);

    // Loading the message content is async (but prefetched by default)
    string messageJson = await page.Messages[0].GetJsonData(ct);

    Console.WriteLine(messageJson); // "foo"