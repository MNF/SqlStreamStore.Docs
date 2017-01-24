.. SQL Stream Store documentation master file, created by
   sphinx-quickstart on Fri Jan 20 07:04:53 2017.
   You can adapt this file completely to your liking, but it should at least
   contain the root `toctree` directive.

============================================
Welcome to SQL Stream Store's documentation!
============================================

SQL Stream Store is *library* to help to you build stream orientated (event sourced)
applications that run on relational databases. It is built based on lessons
learned from years of building such applications and, previously, from maintaining
`NEventStore`_.

.. list-table:: Packages
   :header-rows: 1

   * - Package
     - Install
   * - SqlStreamStore (Memory)
     - .. image:: https://img.shields.io/nuget/v/SqlStreamStore.svg
          :target: https://www.nuget.org/packages/SqlStreamStore
   * - MSSql
     - .. image:: https://img.shields.io/nuget/v/SqlStreamStore.MsSql.svg
          :target: https://www.nuget.org/packages/SqlStreamStore.MsSql
   * - Postgres
     - *under development*
   * - MySql
     - `Up for grabs <https://github.com/damianh/SqlStreamStore/issues/29>`_
   * - SQLite
     - `Up for grabs <https://github.com/damianh/SqlStreamStore/issues/28>`_
   * - HTTP Wrapper API
     - *under development*

Key Design Considerations
=========================

 - Designed to only ever support RDMBS\SQL implementations. There will never be
   support for NoSQL stores. If you want to use a NoSQL store, use EventStore_.
 - Subscriptions (/projections) are always eventually consistent with respect to
   stream appends.
 - The stream is the consistency and transaction boundary. No support for 
   ambient transactions ensures that one cannot update more than one stream
   transactionally. If your model requires you to do that, consider NEventStore_.
 - JSON only message and metadatada payloads. Actually, technically, string only.
   Feedback from NEventStore time has shown most people used JSON serializer
   and when they didn't, they regret not being able to splunk the DB using
   off-the-shelf database management tools. Other serialization formats bring
   questionable value when used with an RDMBS. 
 - No object serialization in API. This is to encourage consideration of weak 
   typing / serialization and different approaches to event / message / stream
   versioning.
 - Allow hard deletion. Sometimes data must be deleted and, like SQL's
   ``DELETE``, it is expected the user knows when and when not to use it.
 - High API compatibility with `EventStore_`'s API. 

SQL Stream Store and EventStore
===============================

EventStore_ is a full featured database server specifically designed to manage
streams and much more. SQL Stream Store is a library over general purpose
relational databases. Both are easy to get started with. The primary reason you
might choose SQL Stream Store is that if you deploy software in operational
enviroments where deploying EventStore_ is just not viable.

However, one of the goals of this project is to encourage users to work in a
certain way such that a future migration to EventStore_ should be a relatively
painless process.

Performance
===========

SQL Stream Store has policy of correctness first, then performance. It is
your responsibility to understand the needs of your application and profile
is characteristics to see if SQL Stream Store is suitable for your needs.

If you do need high performance streams/ messages / events, you probably shouldn't
be doing so via a relational database anyway :)

.. toctree::
   :maxdepth: 2
   :caption: Contents:

   quickstart
   appending
   reading
   metadata
   deleting
   logging

.. _NEventStore: http://neventstore.org
.. _EventStore: https://geteventstore.org