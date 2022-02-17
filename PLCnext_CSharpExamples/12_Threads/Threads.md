# Threads

[Threads.cs](Threads.cs)

Minimal required:

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2019.0-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2019.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

This is a minimal example how to run threads with PLCnext, outside of the realtime application. Nevertheless it must be implemented cooperative.

>**ATTENTION: Error due to changed priority:**
You can select a priority between 0 and 99.
To not change the structure of the real-time Threads,
Phoenix Contact recommends priority 0. Otherwise,
the stability of the firmware cannot be guaranteed.
To perform time-critical tasks programs are provided in ESM tasks.

The implementation of threads in PLCnext is similar to the .NET standard
as you can see in the first example. Different to .NET is the priority handling.
The standard .NET priorities can be used as usual and used to priortize the C# threads.
In C++ Phoenix Contact recommends to use the default priority of 0.
All threads with this priority are handled in a round robin scheduling schema.
The priorities from 1-99 are all FIFO and a higher priority task interrupts a lower one.

In C# all default priority values of the enum `ThreadPriority` are mapped to
explicit priorities of the underlying Linux sub system.
The default priority `ThreadPriority.Normal` is mapped to a system priority
of 2. Only `ThreadPriority.Lowest` (mapped priority 0) is not in conflict
with our realtime system.
For priorities higher than 4, it is possible to cast integer like in the
example: `StaticCaller.Priority = (ThreadPriority)setPrio;`


**Process Priorities**
>|  eCLR Process | .Net-Priority | Enumeration                       |
 |---------------|---------------|-----------------------------------|
 |  eCLR Process | 32            | ProcessPriorityClass.Normal       |
 |               | 64            | ProcessPriorityClass.Idle         |
 |               | 128           | ProcessPriorityClass.High         |
 |               | 256           | ProcessPriorityClass.Realtime     |
 |               | 16384         | ProcessPriorityClass.BelowNormal  |
 |               | 32768         | ProcessPriorityClass.AboveNormal  |

Changing process priorities is currently not supported for eCLR!

**Thread Priorities**
>| IEC Threads                 | .Net-Priority | Enumeration                       |
 |-----------------------------|---------------|-----------------------------------|
 | Default-Task                | 2             | ThreadPriority.Normal             |
 | Lowest priority cyclic task | 19            | ThreadPriority.Highest + 15       |
 | Higest priority cyclic task | 34            | ThreadPriority.Highest + 30       |

> If threads with priorities higher or equal to Default-Task priority should be created then
receive attention that a PLC task will be supperessed. PLC tasks are usually created with a
task watchdog and such a task may create a out of realtime exception fault if suppressed.
  





