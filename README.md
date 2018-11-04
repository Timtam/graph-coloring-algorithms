# graph-coloring-algorithms
Several approaches to solving the np-hard graph coloring problem
## Building
### Windows
Simply open the Visual Studio project file under Visual Studio 2017 or higher and build it.
MSBuild should be working in the same environment as well.
### Linux
Mono can be used to compile this application as well. A shell script managing this task can be found in this repository as well.
Simply run the build.sh file (apply appropriate permissions before if necessary).
## Running
On Windows, simply execute the resulting executable. Linux will require the usage of mono to do so, via mono graph-coloring.exe.
## Parameters
This standalone executable requires some parameters to run. The only parameter requires to be either a file path to a DIMACS graph file, which will then be interpreted accordingly, or a number which will be interpreted as the number of nodes which will be generated in a randomly generated graph.
Either result will be processed by various graph-coloring algorithms afterwards, currently they are:
- local search