' This source-code is freely distributed as part of "DevCase for .NET Framework".
' 
' Maybe you would like to consider to buy this powerful set of libraries to support me. 
' You can do loads of things with my apis for a big amount of diverse thematics.
' 
' Here is a link to the purchase page:
' https://codecanyon.net/item/elektrokit-class-library-for-net/19260282
' 
' Thank you.

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " IoCounters "

Namespace DevCase.Interop.Unmanaged.Win32.Structures

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains I/O accounting information for a process or a job object. 
    ''' <para></para>
    ''' For a job object, the counters include all operations performed by 
    ''' all processes that have ever been associated with the job, 
    ''' in addition to all processes currently associated with the job.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_io_counters"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure IoCounters

        ''' <summary>
        ''' The number of read operations performed.
        ''' </summary>
        Public ReadOperationCount As Long

        ''' <summary>
        ''' The number of write operations performed.
        ''' </summary>
        Public WriteOperationCount As Long

        ''' <summary>
        ''' The number of I/O operations performed, other than read and write operations.
        ''' </summary>
        Public OtherOperationCount As Long

        ''' <summary>
        ''' The number of bytes read.
        ''' </summary>
        Public ReadTransferCount As Long

        ''' <summary>
        ''' The number of bytes written.
        ''' </summary>
        Public WriteTransferCount As Long

        ''' <summary>
        ''' The number of bytes transferred during operations other than read and write operations.
        ''' </summary>
        Public OtherTransferCount As Long

    End Structure

End Namespace

#End Region
