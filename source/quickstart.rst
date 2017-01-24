===========
Quick Start
===========

The following platforms are supported:

* .NET 4.6 and up.
* .NET Core / NetStandard in due course.

.. code-block:: c#

    // Create an in-memory store
    var store = new InMemoryStreamStore();

    // Create a new message
    var newStreamMessage = new NewStreamMessage(Guid.NewGuid(), "my-message-type", "{ \"name\": \"foo\" }");

    // Append to a stream
    await store.AppendToStream("stream-1", ExpectedVersion.NoStream, newStreamMessage, ct);

    // Read the stream
    var page = await store.ReadStreamForwards("stream-1", StreamVersion.Start, 10, ct);

    Console.WriteLine(page.Status);
    Console.WriteLine(page.Messages[0].Type);