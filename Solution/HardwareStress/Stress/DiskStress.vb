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

' Filesize As Integer
' InstanceReadCount As Long
' InstanceReadBytes As Long
' InstanceWriteCount As Long
' InstanceWriteBytes As Long
' ProcessReadCount As Long
' ProcessReadBytes As Long
' ProcessWriteCount As Long
' ProcessWriteBytes As Long

#End Region

#Region " Constructors "

' New()

#End Region

#Region " Methods "

' Allocate(Optional: Integer)
' Deallocate()
' Dispose()

#End Region

#End Region

#Region " Usage Examples "

'Using diskStress As New DiskStress()
'    Console.WriteLine("Allocating disk I/O read and write operations...")
'    diskStress.Allocate(fileSize:=1048576) '1 MB
'
'    Thread.Sleep(TimeSpan.FromSeconds(10))
'
'    Console.WriteLine("Stopping disk I/O read and write operations...")
'    diskStress.Deallocate()
'
'    Console.WriteLine()
'    Console.WriteLine("Instance disk I/O read operations count: {0} (total of files read)", diskStress.InstanceReadCount)
'    Console.WriteLine("Process  disk I/O read operations count: {0}", diskStress.ProcessReadCount)
'    Console.WriteLine()
'    Console.WriteLine("Instance disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceReadBytes, (diskStress.InstanceReadBytes / 1024.0F ^ 3))
'    Console.WriteLine("Process  disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessReadBytes, (diskStress.ProcessReadBytes / 1024.0F ^ 3))
'    Console.WriteLine()
'    Console.WriteLine("Instance disk I/O write operations count: {0} (total of files written)", diskStress.InstanceWriteCount)
'    Console.WriteLine("Process  disk I/O write operations count: {0}", diskStress.ProcessWriteCount)
'    Console.WriteLine()
'    Console.WriteLine("Instance disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceWriteBytes, (diskStress.InstanceWriteBytes / 1024.0F ^ 3))
'    Console.WriteLine("Process  disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessWriteBytes, (diskStress.ProcessWriteBytes / 1024.0F ^ 3))
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

Imports DevCase.Interop.Unmanaged.Win32
Imports DevCase.Interop.Unmanaged.Win32.Structures

#End Region

#Region " DiskStress "

Namespace DevCase.IO

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Provides a mechanism to stress disk through I/O read and write operations on the current computer.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Using diskStress As New DiskStress()
    '''     Console.WriteLine("Allocating disk I/O read and write operations...")
    '''     diskStress.Allocate(fileSize:=1048576) '1 MB
    ''' 
    '''     Thread.Sleep(TimeSpan.FromSeconds(10))
    ''' 
    '''     Console.WriteLine("Stopping disk I/O read and write operations...")
    '''     diskStress.Deallocate()
    ''' 
    '''     Console.WriteLine()
    '''     Console.WriteLine("Instance disk I/O read operations count: {0} (total of files read)", diskStress.InstanceReadCount)
    '''     Console.WriteLine("Process  disk I/O read operations count: {0}", diskStress.ProcessReadCount)
    '''     Console.WriteLine()
    '''     Console.WriteLine("Instance disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceReadBytes, (diskStress.InstanceReadBytes / 1024.0F ^ 3))
    '''     Console.WriteLine("Process  disk I/O read data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessReadBytes, (diskStress.ProcessReadBytes / 1024.0F ^ 3))
    '''     Console.WriteLine()
    '''     Console.WriteLine("Instance disk I/O write operations count: {0} (total of files written)", diskStress.InstanceWriteCount)
    '''     Console.WriteLine("Process  disk I/O write operations count: {0}", diskStress.ProcessWriteCount)
    '''     Console.WriteLine()
    '''     Console.WriteLine("Instance disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.InstanceWriteBytes, (diskStress.InstanceWriteBytes / 1024.0F ^ 3))
    '''     Console.WriteLine("Process  disk I/O written data (in bytes): {0} ({1:F2} GB)", diskStress.ProcessWriteBytes, (diskStress.ProcessWriteBytes / 1024.0F ^ 3))
    ''' End Using
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class DiskStress : Implements IDisposable

#Region " Private Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The underlying <see cref="Thread"/> objects used to allocate disk I/O read and write operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected ReadOnly threads As Collection(Of Thread)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The method to be invoked when the threads in <see cref="CpuStress.threads"/> begins executing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected ReadOnly threadStart As ThreadStart

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Object used for thread-synchronization.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly syncObject As New Object()

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The bytes used to write files.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected fileBytes As MemoryStream

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the size of the files being read and written.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The size of the files being read and written.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Filesize As Integer
            Get
                Return Me.filesize_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' (Backing Field) The size of the files being read and written.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private filesize_ As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of I/O read operations performed by this instance. This value equals to the amount of files read.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of I/O read operations performed by this instance.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstanceReadCount As Long
            Get
                Return Me.instanceReadCount_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' (Backing Field) The amount of I/O read operations performed by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instanceReadCount_ As Long

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of data read by this instance through I/O read operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of data read by this instance through I/O read operations.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstanceReadBytes As Long
            Get
                Return Me.instanceReadBytes_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' (Backing Field) The amount of data read by this instance through I/O read operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instanceReadBytes_ As Long

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of I/O write operations performed by this instance. This value equals to the amount of files written.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of I/O write operations performed by this instance.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstanceWriteCount As Long
            Get
                Return Me.instanceWriteCount_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' (Backing Field) The amount of I/O write operations performed by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instanceWriteCount_ As Long

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of data written by this instance through I/O write operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of data written by this instance through I/O write operations.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstanceWriteBytes As Long
            Get
                Return Me.instanceWriteBytes_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' (Backing Field) The amount of data written by this instance through I/O read operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instanceWriteBytes_ As Long

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of I/O read operations performed by the current process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of I/O read operations performed by the current process.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessReadCount As Long
            Get
                Return Me.GetIoCounters.ReadOperationCount
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of data read by the current process through I/O read operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of data read by the current process through I/O read operations.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessReadBytes As Long
            Get
                Return Me.GetIoCounters.ReadTransferCount
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of I/O write operations performed by the current process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of I/O write operations performed by the current process.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessWriteCount As Long
            Get
                Return Me.GetIoCounters.WriteOperationCount
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of data written by the current process through I/O write operations.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of data written by the current process through I/O write operations.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessWriteBytes As Long
            Get
                Return Me.GetIoCounters.WriteTransferCount
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="DiskStress"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()
            Me.syncObject = New Object()
            Me.threads = New Collection(Of Thread)

            Me.threadStart = New ThreadStart(
            Sub()
                Do
                    Dim tmpFilePath As String = Path.GetTempFileName()
                    Dim bytes As Byte() = Me.fileBytes.GetBuffer()
                    Dim bytesLen As Integer = CInt(bytes.Length)

                    ' Write file.
                    Using writter As New FileStream(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.None)
                        writter.SetLength(bytesLen)
                        writter.Write(bytes, 0, (bytesLen - 1))
                    End Using
                    SyncLock Me.syncObject
                        Me.instanceWriteCount_ += 1
                        Me.instanceWriteBytes_ += bytesLen
                    End SyncLock

                    ' Read file.
                    Dim readBytes As Byte() = File.ReadAllBytes(tmpFilePath)
                    SyncLock Me.syncObject
                        Me.instanceReadCount_ += 1
                        Me.instanceReadBytes_ += readBytes.Length
                    End SyncLock
                    GC.Collect(GC.GetGeneration(readBytes))

                    ' Delete file.
                    File.Delete(tmpFilePath)
                Loop
            End Sub)
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Starts allocating I/O read and write operations for the current process running this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filesize">
        ''' The size of the files to read and write. Default value is 1048576 (1 MegaByte).
        ''' <para></para>
        ''' Specifying a value greater than 104857600 (100 MB) may produce operating system hangs. Use with caution.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' Value greater than 0 is required.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)>
        <SecuritySafeCritical>
        <DebuggerStepThrough>
        Public Overridable Sub Allocate(Optional ByVal fileSize As Integer = 1048576)
            If (fileSize <= 0) Then
                Throw New ArgumentException(paramName:=NameOf(fileSize), message:="Value greater than 0 is required.")
            End If

            Me.filesize_ = fileSize
            Me.fileBytes = New MemoryStream(fileSize)
            Me.fileBytes.SetLength(fileSize)

            If (Me.threads.Any()) Then ' Running threads will allocate new I/O read and write operations themself.
                Exit Sub

            Else ' Start allocating I/O read and write operations by running new threads.
                For i As Integer = 0 To (Environment.ProcessorCount - 1)
                    Dim tr As New Thread(Me.threadStart, 0) With {
                .Name = String.Format("{0} {{{1}}}", NameOf(DiskStress), Guid.NewGuid().ToString().ToUpper()),
                .Priority = ThreadPriority.Normal
            }
                    Me.threads.Add(tr)
                Next i

                For Each tr As Thread In Me.threads
                    tr.Start()
                Next tr

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stops allocating I/O read and write operations for the current process running this instance.
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

            Me.fileBytes = Nothing
            Me.filesize_ = Nothing
            If (Me.fileBytes IsNot Nothing) Then
                Me.fileBytes.Close()
                Me.fileBytes = Nothing
            End If
            GC.Collect()
        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the I/O counters for the current process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The I/O counters for the current process..
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Function GetIoCounters() As IoCounters
            Dim ioCounters As IoCounters
            Dim win32Err As Integer
            If Not NativeMethods.GetProcessIoCounters(Process.GetCurrentProcess().Handle, ioCounters) Then
                win32Err = Marshal.GetLastWin32Error()
                Throw New Win32Exception(win32Err)
            End If
            Return ioCounters
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
