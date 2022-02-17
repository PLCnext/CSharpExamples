# Threads

[Threads.cs](Threads.cs)

Minimal required:

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2019.0-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2019.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

This is an example how to run background threads with PLCnext, outside of the realtime application.
Nevertheless it must be implemented cooperative.

>**ATTENTION: Please see descrition for threads.**

Background threads have a lower priority than the all of the PLC threads which means that thery
have a priority lower than the "DEFAULT" thread of the PLC project. This means it can have
the .Net-Priority 1 or 0.

The main use of background threads is to separate long lasting tasks from the realtime supervised
PLC threads. Therefor the tasks are transfered to the low prior thread which sets a "done" flag
to signalize that a task was done.
   





