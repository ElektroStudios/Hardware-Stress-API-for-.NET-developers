<!-- Common Project Tags:
command-line 
console-applications 
desktop-app 
desktop-application 
dotnet 
dotnet-core 
netcore 
netframework 
netframework48 
tool 
tools 
vbnet 
visualstudio 
windows 
windows-app 
windows-application 
windows-applications 
 -->

# Hardware Stress API for .NET

### A .NET class library written in VB.NET, that provides a mechanism for exerting pressure on hardware resources, such as CPU, RAM, or disk drives.

------------------

## 📝 Requirements

- Visual Studio.

## 🤖 Getting Started

Download the latest release by clicking [here](https://github.com/ElektroStudios/Hardware-Stress-API-for-.NET-developers/releases/latest).

### Usage

Usage is very simple, there are 3 classes: **CpuStress**, **DiskStress** and **MemoryStress**, which provides an **Allocate()** method to start stressing resources, and a **Deallocate()** method to stop it.

#### CPU Stress

```vbnet
Using cpuStress As New CpuStress()
    Dim percentage As Single = 20.5F 20.50%

    Console.WriteLine("Allocating CPU usage percentage...")
    cpuStress.Allocate(percentage)
    Thread.Sleep(TimeSpan.FromSeconds(5))
    Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
    Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
    Console.WriteLine()

    Console.WriteLine("Deallocating CPU usage percentage...")
    cpuStress.Deallocate()
    Thread.Sleep(TimeSpan.FromSeconds(5))
    Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
    Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
End Using
```

![](/Images/CpuStress-Example.png)

#### Memory Stress

```vbnet
Using memStress As New MemoryStress()
    Dim memorySize As Long = 1073741824 1 GB

    Console.WriteLine("Allocating physical memory size...")
    memStress.Allocate(memorySize)
    Console.WriteLine("Instance Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.InstancePhysicalMemorySize, (memStress.InstancePhysicalMemorySize / 1024.0F ^ 3))
    Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 3))
    Console.WriteLine()
    Console.WriteLine("Deallocating physical memory size...")
    memStress.Deallocate()
    Console.WriteLine("Instance Physical Memory Size (in bytes): {0}", memStress.InstancePhysicalMemorySize)
    Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} MB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 2))
End Using
```

![](/Images/MemoryStress-Example.png)

#### Disk Stress

```vbnet
Using diskStress As New DiskStress()
    Console.WriteLine("Allocating disk I/O read and write operations...")
    diskStress.Allocate(fileSize:=1048576) 1 MB

    Thread.Sleep(TimeSpan.FromSeconds(10))

    Console.WriteLine("Stopping disk I/O read and write operations...")
    diskStress.Deallocate()

    Console.WriteLine()
    Console.WriteLine("Instance disk I/O read operations count: {0} (total of files read)", diskStress.InstanceReadCount)
    Console.WriteLine("Process  disk I/O read operations count: {0}", diskStress.ProcessReadCount)
    Console.WriteLine()
    Console.WriteLine("Instance disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceReadBytes, (diskStress.InstanceReadBytes / 1024.0F ^ 3))
    Console.WriteLine("Process  disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessReadBytes, (diskStress.ProcessReadBytes / 1024.0F ^ 3))
    Console.WriteLine()
    Console.WriteLine("Instance disk I/O write operations count: {0} (total of files written)", diskStress.InstanceWriteCount)
    Console.WriteLine("Process  disk I/O write operations count: {0}", diskStress.ProcessWriteCount)
    Console.WriteLine()
    Console.WriteLine("Instance disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceWriteBytes, (diskStress.InstanceWriteBytes / 1024.0F ^ 3))
    Console.WriteLine("Process  disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessWriteBytes, (diskStress.ProcessWriteBytes / 1024.0F ^ 3))
End Using
```

![](/Images/DiskStress-Example.png)

## ⚠️ Disclaimer:

This Work (the repository and the content provided in) is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Work or the use or other dealings in the Work.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/Hardware-Stress-API-for-.NET-developers/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This work is distributed for educational purposes and without any profit motive. However, if you find value in my efforts and wish to support and motivate my ongoing work, you may consider contributing financially through the following options:

 - ### Paypal:
    You can donate any amount you like via **Paypal** by clicking on this button:

    [![Donation Account](Images/Paypal_Donate.png)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY)

 - ### Envato Market:
   If you are a .NET developer, you may want to explore '**DevCase Class Library for .NET**', a huge set of APIs that I have on sale.
   Almost all reusable code that you can find across my works is condensed, refined and provided through DevCase Class Library.

    Check out the product:
    
   [![DevCase Class Library for .NET](Images/DevCase_Banner.png)](https://codecanyon.net/item/elektrokit-class-library-for-net/19260282)

<u>**Your support means the world to me! Thank you for considering it!**</u> 👍
