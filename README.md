# Procont — Windows Process Control & Monitor

**A C# WinForms application to list, inspect, and terminate running Windows processes using the System.Diagnostics namespace.**

![Language](https://img.shields.io/badge/language-C%23-blue)
![Platform](https://img.shields.io/badge/platform-.NET%20WinForms-green)
![IDE](https://img.shields.io/badge/IDE-Visual%20Studio%202008-9cf)

---

## Overview

Procont (Process Control) is a university project from **January 2009** (Sakarya University, Computer Science) that provides a graphical interface for **monitoring and managing Windows processes**. It uses `System.Diagnostics.Process` to enumerate all running processes and displays them in a hierarchical `TreeView` with detailed information.

---

## Features

- **📋 List all running processes** — uses `Process.GetProcesses()` to enumerate the process table
- **🌳 TreeView hierarchy** — each process shown as a root node with expandable child nodes
- **📊 Detailed process info** — displays:
  - Process name
  - Base priority
  - Process ID (PID)
  - Virtual memory size
- **🔪 Kill process** — terminate any selected process via `Process.Kill()`
- **🔄 Auto-refresh** — process list is refreshed after each kill operation with a small safety delay

---

## How It Works

### Process Enumeration

```csharp
pList = Process.GetProcesses(); // Gets all running processes
```

The `pListYenile()` method:
1. Sleeps for 100ms (`Thread.Sleep(100)`) — this small delay ensures killed processes are no longer reported by the OS before refreshing the list
2. Clears the `TreeView`
3. Gets all processes via `Process.GetProcesses()`
4. For each process, adds a root node with the process name
5. Adds child nodes for priority, PID, and virtual memory

### Process Termination

When the **Kill** button is clicked:

```csharp
pList[treeView1.SelectedNode.Index].Kill(); // Terminate the selected process
pListYenile(); // Refresh the list
```

The refresh happens immediately after kill to update the UI.

### TreeView Structure

```
ProcessName
├── Öncelik : <BasePriority>
├── ID : <ProcessId>
└── Sanal Bellek : <VirtualMemorySize64>
```

---

## Screenshot

```
📋 Process Control (Procont)
┌──────────────────────────────────────┐
│ 🔘 Process List                      │
│  ├── chrome                          │
│  │   ├── Öncelik : 8                 │
│  │   ├── ID : 1234                   │
│  │   └── Sanal Bellek : 245000000    │
│  ├── explorer                        │
│  │   ├── Öncelik : 8                 │
│  │   ├── ID : 5678                   │
│  │   └── Sanal Bellek : 180000000    │
│  └── ...                             │
│                                      │
│  [ 🔴 Kill Selected ]  [ 🔄 Refresh ]│
└──────────────────────────────────────┘
```

---

## Project Structure

```
Procont/
├── Procont/
│   ├── Form1.cs          # Main form with process logic (52 lines)
│   ├── Form1.Designer.cs # WinForms designer code
│   ├── Form1.resx        # Form resources
│   ├── Program.cs        # Application entry point
│   ├── Procont.csproj    # Project file
│   └── Properties/       # Assembly info
└── Procont.sln           # Visual Studio solution file
```

---

## Building & Running

1. **Prerequisites**: .NET Framework 2.0+, Visual Studio 2008+
2. Open `Procont.sln` in Visual Studio
3. Build and run (F5)
4. **Warning**: Killing system-critical processes may cause instability. Use with caution.

---

## Concepts Demonstrated

| Concept | Implementation |
|---------|---------------|
| **Process Enumeration** | `Process.GetProcesses()` to list all running processes |
| **System.Diagnostics** | Core .NET namespace for process and system information |
| **TreeView UI** | Hierarchical display of process info in WinForms |
| **Process Termination** | `Process.Kill()` to end processes |
| **Thread Synchronization** | `Thread.Sleep(100)` ensures clean refresh after kill |
| **Error Handling** | Try-catch around kill operations to handle permission-denied scenarios |

---

## License

Academic project — provided as-is for educational purposes.
