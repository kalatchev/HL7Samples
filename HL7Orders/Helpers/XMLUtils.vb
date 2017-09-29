Imports System.IO
Imports System.Text
Imports System.Xml.Serialization

Public Class XMLUtils

    Public Shared Function ReadObject(Of T)(Path As String) As T
        If String.IsNullOrWhiteSpace(Path) Then Throw New ArgumentNullException(NameOf(Path), "Path is null or empty.")
        If Not My.Computer.FileSystem.FileExists(Path) Then Throw New ArgumentOutOfRangeException(NameOf(Path), "Missing file.")
        Dim ser As XmlSerializer = New XmlSerializer(GetType(T))
        Using fs As FileStream = System.IO.File.OpenRead(Path)
            Return DirectCast(ser.Deserialize(fs), T)
        End Using
    End Function

    Public Shared Sub WriteObject(Obj As Object, Path As String)
        If String.IsNullOrWhiteSpace(Path) Then Throw New ArgumentNullException(NameOf(Path), "Path is null or empty.")
        If My.Computer.FileSystem.FileExists(Path) Then My.Computer.FileSystem.DeleteFile(Path)
        Dim ser As XmlSerializer = New XmlSerializer(Obj.GetType)
        Using fs As FileStream = System.IO.File.Create(Path)
            ser.Serialize(fs, Obj)
        End Using
    End Sub

    Public Shared Function GetXML(Obj As Object) As String
        If Obj Is Nothing Then Return Nothing
        Dim ser As XmlSerializer = New XmlSerializer(Obj.GetType)
        Using ms As New MemoryStream
            ser.Serialize(ms, Obj)
            Return Encoding.UTF8.GetString(ms.ToArray)
            ms.Close()
        End Using
    End Function

    Public Shared Sub WriteObject(Obj As Object, Stream As Stream)
        If Stream Is Nothing Then Throw New ArgumentNullException(NameOf(Stream), $"{NameOf(Stream)} can't be null.")
        If Obj Is Nothing Then Throw New ArgumentNullException(NameOf(Obj), $"{NameOf(Obj)} can't be null.")
        Dim buf() As Byte = Encoding.UTF8.GetBytes(GetXML(Obj))
        Stream.Write(buf, 0, buf.Length)
    End Sub

    Public Shared Function GetObject(Of T)(Stream As Stream) As T
        If Stream Is Nothing Then Return Nothing
        Dim ser As New XmlSerializer(GetType(T))
        Return DirectCast(ser.Deserialize(Stream), T)
    End Function

    Public Shared Function GetObject(Of T)(XML As String) As T
        If String.IsNullOrWhiteSpace(XML) Then Return Nothing
        Dim ser As New XmlSerializer(GetType(T))
        Using sr As New MemoryStream(Encoding.UTF8.GetBytes(XML))
            Return DirectCast(ser.Deserialize(sr), T)
            sr.Close()
        End Using
    End Function

End Class
