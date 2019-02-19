' This source-code is freely distributed as part of "DevCase for .NET Framework".
' 
' Maybe you would like to consider to buy this powerful set of libraries to support me. 
' You can do loads of things with my apis for a big amount of diverse thematics.
' 
' Here is a link to the purchase page:
' https://codecanyon.net/item/elektrokit-class-library-for-net/19260282
' 
' Thank you.

#Region " Public Members Summary "

#Region " Properties "

' InstanceCpuPercentage As Single
' ProcessCpuPercentage As Single

#End Region

#Region " Constructors "

' New()

#End Region

#Region " Methods "

' Allocate(Single)
' Deallocate()
' Dispose()

#End Region

#End Region

#Region " Usage Examples "

'Using cpuStress As New CpuStress()
'    Dim percentage As Single = 20.5F '20.50%
'
'    Console.WriteLine("Allocating CPU usage percentage...")
'    cpuStress.Allocate(percentage)
'    Thread.Sleep(TimeSpan.FromSeconds(5))
'    Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
'    Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
'    Console.WriteLine()
'
'    Console.WriteLine("Deallocating CPU usage percentage...")
'    cpuStress.Deallocate()
'    Thread.Sleep(TimeSpan.FromSeconds(5))
'    Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
'    Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
'End Using

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.ConstrainedExecution
Imports System.Security

#End Region

#Region " CpuStress "

Namespace DevCase.IO

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Provides a mechanism to stress CPU usage on the current computer.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Using cpuStress As New CpuStress()
    '''     Dim percentage As Single = 20.5F '20.50%
    ''' 
    '''     Console.WriteLine("Allocating CPU usage percentage...")
    '''     cpuStress.Allocate(percentage)
    '''     Thread.Sleep(TimeSpan.FromSeconds(5))
    '''     Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
    '''     Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
    '''     Console.WriteLine()
    ''' 
    '''     Console.WriteLine("Deallocating CPU usage percentage...")
    '''     cpuStress.Deallocate()
    '''     Thread.Sleep(TimeSpan.FromSeconds(5))
    '''     Console.WriteLine("Instance CPU average usage percentage: {0:F2}%", cpuStress.InstanceCpuPercentage)
    '''     Console.WriteLine("Process  CPU average usage percentage: {0:F2}%", cpuStress.ProcessCpuPercentage)
    ''' End Using
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class CpuStress : Implements IDisposable

#Region " Private Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The underlying <see cref="Thread"/> objects used to allocate CPU usage percentage.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected ReadOnly threads As Collection(Of Thread)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The method to be invoked when the threads in <see cref="CpuStress.threads"/> begins executing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected ReadOnly threadStart As ThreadStart

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the average percentage of CPU usage that is allocated by this instance.
        ''' <para></para>
        ''' Value is in range of 0.01% to 100%.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The average percentage of CPU usage that is allocated by this instance.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstanceCpuPercentage As Single
            Get
                Return Me.instanceCpuPercentage_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field ) The average percentage of CPU usage that is allocated by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instanceCpuPercentage_ As Single

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the average percentage of CPU usage that is allocated by the current process.
        ''' <para></para>
        ''' Value is in range of 0% to 100%.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The average percentage of CPU usage that is allocated by the current process.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessCpuPercentage As Single
            <DebuggerStepThrough>
            Get
                Return Me.GetCurrentProcessCpuPercentage()
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="CpuStress"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()
            Me.threads = New Collection(Of Thread)

            Me.threadStart = New ThreadStart(
            Sub() ' https://stackoverflow.com/questions/2514544/simulate-steady-cpu-load-and-spikes
                Dim watch As New Stopwatch()
                watch.Start()
                Do
                    ' Make the loop go on for "percentage" milliseconds then sleep the remaining percentage milliseconds. 
                    ' So 40% CPU utilization means work 40ms And sleep 60ms.
                    If (watch.ElapsedMilliseconds > Me.instanceCpuPercentage_) Then
                        Thread.Sleep(TimeSpan.FromMilliseconds(100 - Me.instanceCpuPercentage_))
                        watch.Restart()
                    End If
                Loop
            End Sub)
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Allocates the specified average percentage of CPU usage for this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="percentage">
        ''' The average percentage of CPU usage to allocate, from 0.01% to 100%.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' Value greater than 0 is required.
        ''' </exception>
        ''' 
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Value must be in range of 0.01% to 100%.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)>
        <SecuritySafeCritical>
        <DebuggerStepThrough>
        Public Overridable Sub Allocate(ByVal percentage As Single)
            If (percentage <= 0) Then
                Throw New ArgumentException(paramName:=NameOf(percentage), message:="Value greater than 0 is required.")
            End If
            If (percentage > 100) Then
                Throw New ArgumentOutOfRangeException(paramName:=NameOf(percentage), message:="Value must be in range of 0.01% to 100%.")
            End If

            If (Me.threads.Any()) Then ' Running threads will allocate new CPU usage percentage values themself.
                Me.instanceCpuPercentage_ = percentage
                Exit Sub

            Else ' Allocate desired CPU usage percentage by starting new threads.
                For i As Integer = 0 To (Environment.ProcessorCount - 1)
                    Dim tr As New Thread(Me.threadStart, 0) With {
                    .Name = String.Format("{0} {{{1}}}", NameOf(CpuStress), Guid.NewGuid().ToString().ToUpper()),
                    .Priority = ThreadPriority.Normal
                }
                    Me.threads.Add(tr)
                Next i

                Me.instanceCpuPercentage_ = percentage
                For Each tr As Thread In Me.threads
                    tr.Start()
                Next tr
            End If
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Frees any allocated CPU usage by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overridable Sub Deallocate()
            If (Me.threads.Any()) Then
                For Each tr As Thread In Me.threads
                    tr.Abort()
                Next tr
                Me.threads.Clear()
            End If
            Me.instanceCpuPercentage_ = Nothing
            GC.Collect()
        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the average percentage of CPU usage that is allocated by the current process.
        ''' <para></para>
        ''' Value is in range of 0% to 100%.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The average percentage of CPU usage that is allocated by the current process.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Function GetCurrentProcessCpuPercentage() As Single
            Dim pr As Process = Process.GetCurrentProcess()
            Dim pid As Integer = pr.Id
            Dim prName As String = Path.GetFileNameWithoutExtension(pr.ProcessName)

            Dim perfCat As New PerformanceCounterCategory("Process")
            Dim instanceNames As IEnumerable(Of String) =
            From instanceName As String In perfCat.GetInstanceNames()
            Where instanceName.StartsWith(prName)

            For Each instanceName As String In instanceNames
                Using pidCounter As New PerformanceCounter("Process", "ID Process", instanceName, True)
                    If (CInt(pidCounter.RawValue) = pid) Then
                        Using prTimeCounter As New PerformanceCounter("Process", "% Processor Time", instanceName)
                            Dim sample1 As CounterSample = prTimeCounter.NextSample()
                            Thread.Sleep(1000)
                            Dim sample2 As CounterSample = prTimeCounter.NextSample()
                            Dim average As Single = CounterSample.Calculate(sample1, sample2)
                            Return (average / Environment.ProcessorCount)
                        End Using
                    End If
                End Using
            Next instanceName

            Return 0
        End Function

#End Region

#Region " IDisposable Implementation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flag to detect redundant calls when disposing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isDisposed As Boolean = False

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases all the resources used by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(isDisposing:=True)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="isDisposing">
        ''' <see langword="True"/>  to release both managed and unmanaged resources; 
        ''' <see langword="False"/> to release only unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub Dispose(ByVal isDisposing As Boolean)
            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                Me.Deallocate()
            End If
            Me.isDisposed = True
        End Sub

#End Region

    End Class

End Namespace

#End Region
