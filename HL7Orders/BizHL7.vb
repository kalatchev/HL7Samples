Public Class BizHL7

    Private Const SENDING_APP As String = "Joystick"
    Private Const RECEIVING_APP As String = "iLab"

#Region "Business Layer"

    Public Enum PIDType As Integer
        Anonymous = 0
        EGN = 1
        ENCh = 2
    End Enum

    Public Enum Action As Integer
        [New]
        Update
        Cancel
    End Enum

    Public Enum Priority
        Normal
        Emergency
    End Enum

    Public Class Patient
        Public Property PIDType As PIDType
        Public Property PID As String
        Public Property GivenName As String
        Public Property MiddleName As String
        Public Property FamilyName As String
        Public Property DateOfBirth As Date?
        Public Property Gender As String
        Public Property MedicalCase As String
        Public Property AmbulatoryNumber As String
        Public Property EntityId As String
    End Class

    Public Class Doctor
        Public Property UIN As String
        Public Property Title As String
        Public Property GivenName As String
        Public Property MiddleName As String
        Public Property FamilyName As String
    End Class

    Public Class Location
        Public Property WardCode As String
        Public Property WardName As String
        Public Property Room As String
        Public Property Bed As String
    End Class

    Public Class Examination
        Public Property LoincCode As String
        Public Property Name As String
    End Class

    Public Class Order
        Public Property OrderId As String = "1"
        Public Property MessageId As String = Guid.NewGuid.ToString
        Public Property Action As Action = Action.New
        Public Property Priority As Priority = Priority.Normal
        Public Property Patient As Patient
        Public Property Doctor As Doctor
        Public Property Location As Location
        Public Property ToVisit As Date?
        Public Property Examinations As List(Of Examination)
        Public Property Note As String
    End Class

#End Region

    Public Property HospitalName As String = ""
    Public Property LabName As String = ""

    Dim mParser As New NHapi.Base.Parser.PipeParser

    Private Sub FillMSH(Msg As NHapi.Model.V251.Message.OML_O21, BizOrder As Order)
        Msg.MSH.SendingApplication.NamespaceID.Value = SENDING_APP
        Msg.MSH.SendingFacility.NamespaceID.Value = Me.HospitalName
        Msg.MSH.ReceivingApplication.NamespaceID.Value = RECEIVING_APP
        Msg.MSH.ReceivingFacility.NamespaceID.Value = Me.LabName
        Msg.MSH.MessageControlID.Value = BizOrder.MessageId
        Msg.MSH.ProcessingID.ProcessingID.Value = "P" 'P=Production mode
    End Sub

    Private Sub FillPID(ModelPatient As NHapi.Model.V251.Group.OML_O21_PATIENT, BizPatient As Patient)
        Dim Ind As Integer = 0

        'National identifier (ЕГН или ЕНЧ)
        If Not String.IsNullOrWhiteSpace(BizPatient.PID) AndAlso (BizPatient.PIDType = PIDType.EGN OrElse BizPatient.PIDType = PIDType.ENCh) Then
            ModelPatient.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.PID.Trim
            'GRAO=ЕГН, MVR=ЕНЧ
            If BizPatient.PIDType = PIDType.EGN Then
                ModelPatient.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "GRAO"
            Else
                ModelPatient.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "MVR"
            End If
            ModelPatient.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "NI"
            Ind += 1
        End If

        'Medical record (ИЗ)
        If Not String.IsNullOrWhiteSpace(BizPatient.MedicalCase) Then
            ModelPatient.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.MedicalCase.Trim
            ModelPatient.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "Hospital"
            ModelPatient.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
            Ind += 1
        End If

        'Medical record (Амбулаторен номер)
        If Not String.IsNullOrWhiteSpace(BizPatient.AmbulatoryNumber) Then
            ModelPatient.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.AmbulatoryNumber.Trim
            ModelPatient.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "Ambulatory"
            ModelPatient.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
            Ind += 1
        End If

        'Medical record (ID на пациента в БИС)
        If Not String.IsNullOrWhiteSpace(BizPatient.EntityId) Then
            ModelPatient.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.EntityId.Trim
            ModelPatient.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "HIS"
            ModelPatient.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
            Ind += 1
        End If

        'Patient names - given, middle and family
        ModelPatient.PID.GetPatientName(0).GivenName.Value = BizPatient.GivenName
        ModelPatient.PID.GetPatientName(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value = BizPatient.MiddleName
        ModelPatient.PID.GetPatientName(0).FamilyName.Surname.Value = BizPatient.FamilyName

        If BizPatient.DateOfBirth.HasValue Then
            'Patient date of birth
            ModelPatient.PID.DateTimeOfBirth.Time.Value = BizPatient.DateOfBirth.Value.ToString("yyyyMMdd")
        End If

        'Patient gender M=Male, F=Female, U=Unknown
        If BizPatient.Gender = "M" Then
            ModelPatient.PID.AdministrativeSex.Value = "M"
        ElseIf BizPatient.Gender = "F" Then
            ModelPatient.PID.AdministrativeSex.Value = "M"
        Else
            ModelPatient.PID.AdministrativeSex.Value = "U"
        End If

    End Sub

    Private Sub FillORC(Ord As NHapi.Model.V251.Group.OML_O21_ORDER, BizOrder As Order)
        'NW=New, XO=Modification, CA=Cancel
        Select Case BizOrder.Action
            Case Action.Update
                Ord.ORC.OrderControl.Value = "XO"
            Case Action.Cancel
                Ord.ORC.OrderControl.Value = "CA"
            Case Else
                'New by default
                Ord.ORC.OrderControl.Value = "NW"
        End Select

        Ord.ORC.PlacerOrderNumber.EntityIdentifier.Value = BizOrder.OrderId.Trim

        'Ordering doctor
        Ord.ORC.GetOrderingProvider(0).IDNumber.Value = BizOrder.Doctor.UIN
        Ord.ORC.GetOrderingProvider(0).GivenName.Value = BizOrder.Doctor.GivenName
        Ord.ORC.GetOrderingProvider(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value = BizOrder.Doctor.MiddleName
        Ord.ORC.GetOrderingProvider(0).FamilyName.Surname.Value = BizOrder.Doctor.FamilyName
        Ord.ORC.GetOrderingProvider(0).DegreeEgMD.Value = BizOrder.Doctor.Title

        'Date and time to visit (in case of future order, do not set in case of supplemental order)
        If BizOrder.ToVisit.HasValue Then
            Ord.ORC.GetQuantityTiming(0).StartDateTime.Time.Value = BizOrder.ToVisit.Value.ToString("yyyyMMddHHmm")
        End If

        Ord.AddTIMING()
        Ord.TIMINGs(0).TQ1.SetIDTQ1.Value = "1"
        Select Case BizOrder.Priority
            Case Priority.Normal
                Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "R"
                Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "Routine"
            Case Priority.Emergency
                Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "S"
                Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "STAT"
            Case Else
                'Default priority
                Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "R"
                Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "Routine"
        End Select


        'NTE --------------------------------------------
        'Note
        Ord.OBSERVATION_REQUEST().AddNTE()
        Ord.OBSERVATION_REQUEST().NTEs(0).SetIDNTE.Value = "1"
        Ord.OBSERVATION_REQUEST().NTEs(0).GetComment(0).Value = "Пациента има калцирали вени!"

    End Sub

    Private Sub FillPV1(Visit As NHapi.Model.V251.Group.OML_O21_PATIENT_VISIT, BizOrder As Order)
        'Patient location
        Visit.PV1.AssignedPatientLocation.PointOfCare.Value = BizOrder.Location.WardName
        Visit.PV1.AssignedPatientLocation.Room.Value = BizOrder.Location.Room
        Visit.PV1.AssignedPatientLocation.Bed.Value = BizOrder.Location.Bed
        Visit.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = BizOrder.Location.WardCode
    End Sub

    Public Function GenerateOrder(BizOrder As Order) As NHapi.Base.Model.IMessage

        Dim Msg As New NHapi.Model.V251.Message.OML_O21

        'MSH 
        Me.FillMSH(Msg, BizOrder)

        'PID 
        Msg.PATIENT.PID.SetIDPID.Value = "1" 'First and only one Patient
        Me.FillPID(Msg.PATIENT, BizOrder.Patient)

        'PV1 
        Msg.PATIENT.PATIENT_VISIT.PV1.SetIDPV1.Value = "1" 'First and only one Visit
        Me.FillPV1(Msg.PATIENT.PATIENT_VISIT, BizOrder)

        'ORC 
        Msg.AddORDER()
        Me.FillORC(Msg.ORDERs(0), BizOrder)

        'OBR
        Dim ExamInd As Integer = 0
        For Each CurExam As Examination In BizOrder.Examinations
            If Msg.ORDERs(ExamInd) Is Nothing Then Msg.AddORDER()
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.SetIDOBR.Value = (ExamInd + 1).ToString
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.PlacerOrderNumber.EntityIdentifier.Value = BizOrder.OrderId.Trim
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Identifier.Value = CurExam.LoincCode.Trim
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Text.Value = CurExam.Name.ToString
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = "LN" 'Loinc


            ExamInd += 1
        Next

        Return Msg
    End Function


End Class
