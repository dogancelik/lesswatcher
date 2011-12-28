# Introduction
Welcome to readme file of lesswatcher.
**lesswatcher** is a directory watcher and less parser (with the help of dotless library) made specifically for Windows system.
It is coded in C# with .NET 4 Framework.

## Building
You can build lesswatcher by opening the solution file, then going `Build > Build Solution` from the menu.

### Requirements:
To build lesswatcher you must have these:

* Visual Studio
* dotless library ([Get the latest version](http://www.dotlesscss.org/))
* ILmerge ([Install](http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=17630))

### Building from Command Line

If you want to build lesswatcher from command line, you can use `Build.bat` helper tool.
**Important:** Start `Build.bat` from Visual Studio Command Line, not the regular ones.

## Notes (important)

* lesswatcher has a post-build event which will trigger ILmerge from PATH. So please make sure to add ILmerge to PATH variable. Otherwise you will get errors.
* dotless library is in `incl` directory, if you want to update it, just download the new version from the official site and replace it with the current library.
* ILmerge is actually optional but it is required for combining both the binary and the library into one executable file. You can remove the post-build event from the project settings but when you do this, you must move both files(exe and dll file) to desired directory everytime you need to move it.

## Bug reports
Before making a bug report, please know the source of the problem. If it's library related problem, please report it to dotless developers.