Imports System.Xml.Serialization

<Serializable>
Public Class MedicalCase

    Public Property Id As Integer
    Public Property PatientId As Integer 'Just storage
    <XmlIgnore()>
    Public Property Patient As Patient
    Public Property WardCode As String
    <XmlIgnore()>
    Public Property Ward As Ward
    Public Property CaseNumber As String 'Useed as entity identifier
    Public Property RoomCode As String
    Public Property BedCode As String
    Public Property AttendingDoctorUIN As String
    Public Property ContractNum As String
    <XmlIgnore()>
    Public Property Contract As Contract

    <XmlArray("Diagnoses"), XmlArrayItem(GetType(String), ElementName:="Diagnosis")>
    Public Property DiagCodes As List(Of String) 'just storage

    <XmlIgnore>
    Public Property Diagnoses As List(Of Diagnosis)

End Class
