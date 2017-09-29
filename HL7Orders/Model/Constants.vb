Imports System.Xml.Serialization

Public Class Constants

    Public Enum PIDTypes As Integer
        <XmlEnum("Anonymous")> Anonymous = 0
        <XmlEnum("EGN")> EGN = 1
        <XmlEnum("ENCh")> ENCh = 2
    End Enum

    Public Enum Actions As Integer
        [New]
        Update
        Cancel
    End Enum

    Public Enum Priorities
        <XmlEnum("Normal")> Normal = 0
        <XmlEnum("Emergency")> Emergency = 1
    End Enum

End Class
