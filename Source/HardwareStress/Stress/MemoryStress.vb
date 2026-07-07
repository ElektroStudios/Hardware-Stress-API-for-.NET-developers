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

' InstancePhysicalMemorySize As Long
' ProcessPhysicalMemorySize As Long

#End Region

#Region " Constructors "

' New()

#End Region

#Region " Methods "

' Allocate(Long)
' Allocate(Long, Byte)
' Deallocate()
' Dispose()

#End Region

#End Region

#Region " Usage Examples "

'Using memStress As New MemoryStress()
'    Dim memorySize As Long = 1073741824 '1 GB
'
'    Console.WriteLine("Allocating physical memory size...")
'    memStress.Allocate(memorySize)
'    Console.WriteLine("Instance Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.InstancePhysicalMemorySize, (memStress.InstancePhysicalMemorySize / 1024.0F ^ 3))
'    Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 3))
'    Console.WriteLine()
'    Console.WriteLine("Deallocating physical memory size...")
'    memStress.Deallocate()
'    Console.WriteLine("Instance Physical Memory Size (in bytes): {0}", memStress.InstancePhysicalMemorySize)
'    Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} MB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 2))
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

#End Region

#Region " MemoryStress "

Namespace DevCase.IO

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Provides a mechanism to stress physical memory RAM on the current computer.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Using memStress As New MemoryStress()
    '''     Dim memorySize As Long = 1073741824 '1 GB
    ''' 
    '''     Console.WriteLine("Allocating physical memory size...")
    '''     memStress.Allocate(memorySize)
    '''     Console.WriteLine("Instance Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.InstancePhysicalMemorySize, (memStress.InstancePhysicalMemorySize / 1024.0F ^ 3))
    '''     Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} GB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 3))
    '''     Console.WriteLine()
    '''     Console.WriteLine("Deallocating physical memory size...")
    '''     memStress.Deallocate()
    '''     Console.WriteLine("Instance Physical Memory Size (in bytes): {0}", memStress.InstancePhysicalMemorySize)
    '''     Console.WriteLine("Process  Physical Memory Size (in bytes): {0} ({1:F2} MB)", memStress.ProcessPhysicalMemorySize, (memStress.ProcessPhysicalMemorySize / 1024.0F ^ 2))
    ''' End Using
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class MemoryStress : Implements IDisposable

#Region " Private Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The pointer to a unmanaged memory block of specific size.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private memoryHandle As IntPtr

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of physical memory, in bytes, that is allocated by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of physical memory, in bytes, that is allocated by this instance.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property InstancePhysicalMemorySize As Long
            Get
                Return Me.instancePhysicalMemorySize_
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field ) The amount of physical memory, in bytes, that is allocated by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private instancePhysicalMemorySize_ As Long

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of physical memory, in bytes, that is allocated by the current process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of physical memory, in bytes, that is allocated by the current process.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ProcessPhysicalMemorySize As Long
            Get
                Return Process.GetCurrentProcess().WorkingSet64
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="MemoryStress"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Allocates the specified amount of physical memory for this instance and fills the memory with zeros.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="size">
        ''' The amount of physical memory to allocate, in bytes.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' Value greater than 0 is required.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overridable Sub Allocate(ByVal size As Long)
            Me.Allocate(size, fillValue:=0)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Allocates the specified amount of physical memory for this instance and fills the memory with the specified value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="size">
        ''' The amount of physical memory to allocate, in bytes.
        ''' </param>
        ''' 
        ''' <param name="fillValue">
        ''' The value that will be used to fill the memory.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' Value greater than 0 is required.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)>
        <SecurityCritical>
        <DebuggerStepThrough>
        Public Overridable Sub Allocate(ByVal size As Long, ByVal fillValue As Byte)
            If (size <= 0) Then
                Throw New ArgumentException(paramName:=NameOf(size), message:="Value greater than 0 is required.")
            End If

            ' Allocate new memory.
            Dim ptrSize As New IntPtr(size)
            If (Me.memoryHandle = IntPtr.Zero) Then
                Me.memoryHandle = Marshal.AllocHGlobal(ptrSize)
            Else
                Me.memoryHandle = Marshal.ReAllocHGlobal(Me.memoryHandle, ptrSize)
            End If

            ' Zero-fill memory block.
            If (fillValue = 0) Then
                NativeMethods.ZeroMemory(Me.memoryHandle, ptrSize)
            Else
                NativeMethods.FillMemory(Me.memoryHandle, ptrSize, fillValue)
            End If

            Me.instancePhysicalMemorySize_ = size
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Frees any allocated memory for this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overridable Sub Deallocate()
            If (Me.memoryHandle <> IntPtr.Zero) Then
                Marshal.FreeHGlobal(Me.memoryHandle)
                Me.memoryHandle = IntPtr.Zero
                Me.instancePhysicalMemorySize_ = Nothing
            End If
        End Sub

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
            GC.SuppressFinalize(Me)
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

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub Finalize()
            Me.Dispose(False)
            MyBase.Finalize()
        End Sub

#End Region

    End Class

End Namespace

#End Region
