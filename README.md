Tomato
======

Tomato is a library for emulating a DCPU-16.  Also included is Lettuce, which is a GUI front-end for Tomato.  Also also
included is Bacon, which is an SDP server built on top of Tomato.

Planned Things
--------------

* Allow the Lettuce debugger connect to SDP servers like Bacon
* Fix bugs in Tomato emulation core
* Live disassembly for Lettuce

How to Use
----------

If you wish to use any of this software on Linux or Mac, install Mono first.  It's usually called "mono-complete" on Linux.

**Lettuce**

Lettuce is probably what you want to use.  It looks like this:

![Lettuce](http://i.imgur.com/TuwIA.png)

It has lots of useful stuff.  If you start it up without command line arguments, you get this:

![Memory Configuration](http://i.imgur.com/JEO7P.png)

This optional (click Skip if you want to) step lets you load a binary file into the emulated memory.  Click browse and go
find one.  If the file is stored in little-endian, click that check box.  If you don't want to see this on startup, give
Lettuce a binary file in one of the arguments.  When you click OK, you see this:

![Hardware Configuration](http://i.imgur.com/0mtTK.png)

This window searches for all hardware provided by Tomato and by any plugins (note that plugins don't actually exist as of
this moment).  The ones pictured here are provided by Tomato.  Check any amount that you'd like.  If you don't wish to
see this screen, you can add hardware with -hw, or --connect, or -c from the command line.  If you use the command line,
you can also connect several of the same device.  When you hit OK, you see this:

![Debugger](http://i.imgur.com/TuwIA.png)

For every connected LEM 1802, a window will pop up.  Each window will associate itself with a keyboard as well, if present.
Any additional keyboards that don't have LEM 1802 devices to associate with will have their own window.  You can focus on
a window (i.e. click it) and type to send keys to that keyboard.  Right click the LEM1802 window if you want to take a 
screenshot of it.

Also opened is the debugger window.  On the right is the register view, which shows all registers and their current values.
You can also see if interrupt queueing is enabled, and how many interrupts are queued, and whether or not the device is on fire.
If you uncheck "Running" and stop the CPU, you're free to modify any of these values.  When stopped, Step Into (or F6 on the
keyboard) will execute one instruction.  Step over will execute one instruction, unless it's a JSR instruction, in which case
it will execute until it returns.

Below that is a list of all your connected devices.  Click one to see a little info about it.  Edit Device will open a window
with more device information and the ability to edit that information.

To the left is the memory view.  You can scroll through all the memory of the device, and you can edit it if the CPU is stopped.
Double click a cell to edit it, or right click and select "Edit Value".  Right click and select "Goto Address" to jump to a
different address in memory, or hit Ctrl+G.

Under that is the disassembler.  Breakpoint lines are highlighted in red, and PC is in yellow.  Double click a line to toggle the
breakpoint.  If you hover over a value like "C" or "[0x8000]", you'll see the actual runtime value.

Keyboard Shortcuts: F5 to Run/Stop, F6 to Step Into, F7 to Step Over, Ctrl+G to goto address in memory view.

**Bacon**

This doesn't do anything right now.  Eventually, there will be a Windows Service and a command line application around a SDP server
library called Bacon.

**Tomato**

If you're a .NET developer, you can use Tomato like a library.  Just add a reference to Tomato.dll, and you'll have a nice
DCPU emulation library at your disposal.  The core is the DCPU class, which has memory and registers and such.  Each device
inherits from the abstract Device class, and you can use DCPU.Connect(Device) to add them to the CPU.  Provided by Tomato are
GenericClock, GenericKeyboard, and LEM1802.