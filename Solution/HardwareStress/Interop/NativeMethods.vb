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

#Region " Imports "

Imports System.Security

Imports DevCase.Interop.Unmanaged.Win32.Structures

#End Region

#Region " NativeMethods "

Namespace DevCase.Interop.Unmanaged.Win32

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Platform Invocation methods (P/Invoke), access unmanaged code.
    ''' <para></para>
    ''' This class does not suppress stack walks for unmanaged code permission.
    ''' <see cref="SuppressUnmanagedCodeSecurityAttribute"/> must not be applied to this class.
    ''' <para></para>
    ''' This class is for methods that can be used anywhere because a stack walk will be performed.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="https://msdn.microsoft.com/en-us/library/ms182161.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class NativeMethods

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="NativeMethods"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Kernel32.dll "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves accounting information for all I/O operations performed by the specified process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getprocessiocounters"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hProcess">
        ''' A handle to the process. 
        ''' <para></para>
        ''' The handle must have the PROCESS_QUERY_INFORMATION or PROCESS_QUERY_LIMITED_INFORMATION access right.
        ''' </param>
        ''' 
        ''' <param name="refIoCounters">
        ''' A pointer to an <see cref="IoCounters"/> structure that receives the I/O accounting information for the process.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is <see langword="True"/>.
        ''' <para></para>
        ''' If the function fails, the return value is <see langword="False"/>.
        ''' <para></para>
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error()"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("Kernel32.dll", SetLastError:=False)>
        Public Shared Function GetProcessIoCounters(ByVal hProcess As IntPtr,
                                                    ByRef refIoCounters As IoCounters
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Fills a block of memory with a specified value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa366561(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="destination">
        ''' A pointer to the starting address of the block of memory to fill.
        ''' </param>
        ''' 
        ''' <param name="length">
        ''' The size of the block of memory to fill, in bytes. 
        ''' This value must be less than the size of the <paramref name="destination"/> buffer.
        ''' </param>
        ''' 
        ''' <param name="fillValue">
        ''' The byte value with which to fill the memory block.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("kernel32.dll", EntryPoint:="RtlFillMemory", SetLastError:=True)>
        Public Shared Sub FillMemory(ByVal destination As IntPtr,
                                     ByVal length As IntPtr,
                                     ByVal fillValue As Byte)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Fills a block of memory with zeros.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa366920(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="address">
        ''' A pointer to the starting address of the block of memory to fill with zeros.
        ''' </param>
        ''' 
        ''' <param name="size">
        ''' The size of the block of memory to fill with zeros, in bytes.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Sub ZeroMemory(ByVal address As IntPtr,
                                     ByVal size As IntPtr)
        End Sub

#End Region

    End Class

End Namespace

#End Region
