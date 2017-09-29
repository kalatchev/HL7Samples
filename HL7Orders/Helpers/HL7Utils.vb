Imports NHapi.Base.Model

Public Class HL7Utils

    Private Shared Sub SetMSH(SegmentMSH As NHapi.Model.V251.Segment.MSH, Conf As ConfigData, MsgId As String)
        SegmentMSH.SendingApplication.NamespaceID.Value = Conf.OriginApp
        SegmentMSH.SendingFacility.NamespaceID.Value = Conf.OriginFacility
        SegmentMSH.ReceivingApplication.NamespaceID.Value = Conf.TargetApp
        SegmentMSH.ReceivingFacility.NamespaceID.Value = Conf.TargetFacility
        SegmentMSH.DateTimeOfMessage.Time.Value = Now().ToString("yyyyMMddHHmmss")
        SegmentMSH.MessageType.MessageStructure.Value = "OML_O21"
        SegmentMSH.MessageControlID.Value = MsgId
        SegmentMSH.ProcessingID.ProcessingID.Value = "P" 'P=Production mode
    End Sub

    Private Shared Sub AddSFT(Msg As NHapi.Model.V251.Message.OML_O21)
        Dim s As NHapi.Model.V251.Segment.SFT = Msg.AddSFT()
        With s
            .SoftwareProductName.Value = My.Application.Info.ProductName
            .SoftwareVendorOrganization.OrganizationName.Value = My.Application.Info.CompanyName
            .SoftwareCertifiedVersionOrReleaseNumber.Value = My.Application.Info.Version.ToString
            .SoftwareBinaryID.Value = My.Application.Info.Version.ToString
        End With
    End Sub

    Private Shared Sub AddNTE(Msg As NHapi.Model.V251.Message.OML_O21, Note As String)
        If Not String.IsNullOrWhiteSpace(Note) Then
            Dim nt As NHapi.Model.V251.Segment.NTE = Msg.AddNTE()
            With nt
                .SetIDNTE.Value = "1" 'One and only one note
                .GetComment(0).Value = Note
            End With
        End If
    End Sub

    Private Shared Sub SetPID(SegmentPID As NHapi.Model.V251.Segment.PID, Ord As Order)
        SegmentPID.SetIDPID.Value = "1" 'First and only one PID

        If Ord.MedicalCase Is Nothing OrElse Ord.MedicalCase.Patient Is Nothing Then Exit Sub

        Dim Ind As Integer = 0

        'National identifier (ЕГН и ЕНЧ)
        If Ord.MedicalCase.Patient.PIDType = Constants.PIDTypes.EGN Then
            SegmentPID.GetPatientIdentifierList(Ind).IDNumber.Value = Ord.MedicalCase.Patient.PID.Trim
            SegmentPID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "GRAO"
            SegmentPID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "NI"
            Ind += 1
        ElseIf Ord.MedicalCase.Patient.PIDType = Constants.PIDTypes.ENCh Then
            SegmentPID.GetPatientIdentifierList(Ind).IDNumber.Value = Ord.MedicalCase.Patient.PID.Trim
            SegmentPID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "MVR"
            SegmentPID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "NI"
            Ind += 1
        End If

        'Medical record (ИЗ)
        SegmentPID.GetPatientIdentifierList(Ind).IDNumber.Value = Ord.MedicalCase.CaseNumber
        SegmentPID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "Hospital"
        SegmentPID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
        Ind += 1


        ''Medical record (Амбулаторен номер)
        'If Not String.IsNullOrWhiteSpace(BizPatient.AmbulatoryNumber) Then
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.AmbulatoryNumber.Trim
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "Ambulatory"
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
        '    Ind += 1
        'End If

        'Medical record (ID на пациента в БИС, може и да е буквено-цифров)
        SegmentPID.GetPatientIdentifierList(Ind).IDNumber.Value = Ord.MedicalCase.Id.ToString
        SegmentPID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "HIS"
        SegmentPID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
        Ind += 1


        'Patient names - given, middle and family
        SegmentPID.GetPatientName(0).GivenName.Value = Ord.MedicalCase.Patient.GivenName
        SegmentPID.GetPatientName(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value = Ord.MedicalCase.Patient.MiddleName
        SegmentPID.GetPatientName(0).FamilyName.Surname.Value = Ord.MedicalCase.Patient.FamilyName

        If Ord.MedicalCase.Patient.DateOfBirth.HasValue Then
            'Patient date of birth
            SegmentPID.DateTimeOfBirth.Time.Value = Ord.MedicalCase.Patient.DateOfBirth.Value.ToString("yyyyMMdd")
        End If

        'Patient gender M=Male, F=Female, U=Unknown
        If Ord.MedicalCase.Patient.Gender = "M" Then
            SegmentPID.AdministrativeSex.Value = "M"
        ElseIf Ord.MedicalCase.Patient.Gender = "F" Then
            SegmentPID.AdministrativeSex.Value = "M"
        Else
            SegmentPID.AdministrativeSex.Value = "U"
        End If
    End Sub

    Private Shared Sub SetPV1(SegmentPV1 As NHapi.Model.V251.Segment.PV1, Ord As Order)
        'PV.1 Set ID
        SegmentPV1.SetIDPV1.Value = "1" 'First and only one PV1

        If Ord.MedicalCase Is Nothing OrElse Ord.MedicalCase.Patient Is Nothing Then Exit Sub

        'PV1.2 Patient Class
        'TODO: PV1.2!
        SegmentPV1.PatientClass.Value = "I" 'Inpatient

        'PV1.3.1 Point Of Care
        SegmentPV1.AssignedPatientLocation.PointOfCare.Value = Ord.MedicalCase.Ward.Name

        'PV1.3.2 Room
        SegmentPV1.AssignedPatientLocation.Room.Value = Ord.MedicalCase.RoomCode

        'PV1.3.3 Bed
        SegmentPV1.AssignedPatientLocation.Bed.Value = Ord.MedicalCase.BedCode

        'PV1.3.4 Facility
        SegmentPV1.AssignedPatientLocation.Facility.NamespaceID.Value = Ord.MedicalCase.Ward.Code

    End Sub

    Private Shared Sub SetORC(SegmentORC As NHapi.Model.V251.Segment.ORC, Ord As Order)
        'ORC.1.1 Order Action
        Select Case Ord.Action
            Case Constants.Actions.Update
                SegmentORC.OrderControl.Value = "XO"
            Case Constants.Actions.Cancel
                SegmentORC.OrderControl.Value = "CA"
            Case Else
                'New by default
                SegmentORC.OrderControl.Value = "NW"
        End Select

        'Ordering doctor
        SegmentORC.GetOrderingProvider(0).IDNumber.Value = Ord.OrderingDoctor.UIN
        SegmentORC.GetOrderingProvider(0).GivenName.Value = Ord.OrderingDoctor.GivenName
        SegmentORC.GetOrderingProvider(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value = Ord.OrderingDoctor.MiddleName
        SegmentORC.GetOrderingProvider(0).FamilyName.Surname.Value = Ord.OrderingDoctor.FamilyName
        SegmentORC.GetOrderingProvider(0).PrefixEgDR.Value = Ord.OrderingDoctor.Title
        SegmentORC.GetOrderingProvider(0).AssigningAuthority.NamespaceID.Value = "BLS"
        SegmentORC.GetOrderingProvider(0).IdentifierTypeCode.Value = "DN"

        If Ord.FutureVistTime.HasValue Then
            SegmentORC.GetQuantityTiming(0).StartDateTime.Time.Value = Ord.FutureVistTime.Value.ToString("yyyyMMddHHmm")
        End If



    End Sub

    Private Shared Sub SetTQ1(SegmentTQ1 As NHapi.Model.V251.Segment.TQ1, Ord As Order)
        SegmentTQ1.SetIDTQ1.Value = "1" 'First and only one timing

        Select Case Ord.Priority
            Case Constants.Priorities.Normal
                SegmentTQ1.GetPriority(0).Identifier.Value = "R"
                SegmentTQ1.GetPriority(0).Text.Value = "Routine"
            Case Constants.Priorities.Emergency
                SegmentTQ1.GetPriority(0).Identifier.Value = "S"
                SegmentTQ1.GetPriority(0).Text.Value = "STAT"
            Case Else
                'Default priority
                SegmentTQ1.GetPriority(0).Identifier.Value = "R"
                SegmentTQ1.GetPriority(0).Text.Value = "Routine"
        End Select

    End Sub

    Private Shared Sub SetOBR(SegmentOBR As NHapi.Model.V251.Segment.OBR, Exm As Examination, Num As Integer, OrderId As String, OrdNamespace As String)
        SegmentOBR.SetIDOBR.Value = Num.ToString
        SegmentOBR.PlacerOrderNumber.EntityIdentifier.Value = OrderId
        SegmentOBR.PlacerOrderNumber.NamespaceID.Value = OrdNamespace
        SegmentOBR.UniversalServiceIdentifier.Identifier.Value = Exm.LoincCode.Trim
        SegmentOBR.UniversalServiceIdentifier.Text.Value = Exm.Name.ToString
        SegmentOBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = "LN" 'Loinc
        SegmentOBR.UniversalServiceIdentifier.AlternateIdentifier.Value = Exm.HisId
        SegmentOBR.UniversalServiceIdentifier.NameOfAlternateCodingSystem.Value = "HCPT" 'HIS code
    End Sub

    Private Shared Sub AddDG1s(GroupOBSERVATIONREQUEST As NHapi.Model.V251.Group.OML_O21_OBSERVATION_REQUEST, MedCase As MedicalCase)
        Dim DiagId As Integer = 1
        For Each dg In MedCase.Diagnoses
            Dim d As NHapi.Model.V251.Segment.DG1 = GroupOBSERVATIONREQUEST.AddDG1()
            With d
                .SetIDDG1.Value = DiagId.ToString
                .DiagnosisCodeDG1.Identifier.Value = dg.Code
                .DiagnosisCodeDG1.NameOfCodingSystem.Value = "I10" 'ICD-10
                .DiagnosisCodeDG1.Text.Value = dg.Name
                .DiagnosisType.Value = "W" 'Working
            End With
            DiagId += 1
        Next
    End Sub

    Private Shared Sub SetBLG(SegmentBLG As NHapi.Model.V251.Segment.BLG, MedCase As MedicalCase)
        SegmentBLG.AccountID.IDNumber.Value = MedCase.Contract.ContractNum
        SegmentBLG.AccountID.AssigningAuthority.NamespaceID.Value = "Hospital"
        SegmentBLG.AccountID.IdentifierTypeCode.Value = "XX"
    End Sub

    Public Shared Function Order2Message(Conf As ConfigData, Ord As Order) As IMessage
        If Ord Is Nothing Then Throw New ArgumentNullException(NameOf(Ord))
        If Conf Is Nothing Then Throw New ArgumentNullException(NameOf(Conf))
        Dim Msg As New NHapi.Model.V251.Message.OML_O21

        'MSH segment (required)
        SetMSH(Msg.MSH, Conf, Guid.NewGuid.ToString)

        'SFT (optional, debug info, feel free to modify how the app data is retrieved)
        AddSFT(Msg)

        'NTE (optional)
        AddNTE(Msg, Ord.Note)

        'PATIENT group (optinal by stadard, requied by current integracion)
        Dim GroupPATIENT As NHapi.Model.V251.Group.OML_O21_PATIENT = Msg.PATIENT 'creates group if it is necesary

        '  PID
        SetPID(GroupPATIENT.PID, Ord)

        '  PATIENT VISIT group (optinal by stadard, requied by current integracion)
        Dim GroupPATIENTVISIT As NHapi.Model.V251.Group.OML_O21_PATIENT_VISIT = Msg.PATIENT.PATIENT_VISIT

        '    PV1
        SetPV1(GroupPATIENTVISIT.PV1, Ord)

        'ORDER gruop (required, repeating)
        Dim ExamNum As Integer = 1 'TODO: Is tahat correct?
        For Each exm As Examination In Ord.Examinations
            Dim GroupORDER As NHapi.Model.V251.Group.OML_O21_ORDER = Msg.AddORDER

            '  ORC
            SetORC(GroupORDER.ORC, Ord)

            '  TIMING group
            Dim GroupTIMING As NHapi.Model.V251.Group.OML_O21_TIMING = GroupORDER.AddTIMING

            '    TQ1
            SetTQ1(GroupTIMING.TQ1, Ord)

            '  OBSERVATION REQUEST group (optinal by stadard, requied by current integracion)
            Dim GroupOBSERVATIONREQUEST As NHapi.Model.V251.Group.OML_O21_OBSERVATION_REQUEST = GroupORDER.OBSERVATION_REQUEST

            '    OBR
            SetOBR(GroupOBSERVATIONREQUEST.OBR, exm, ExamNum, Ord.OrderId, ConfigData.Instance.OrderNumberNamespace)

            '    DG1s
            AddDG1s(GroupOBSERVATIONREQUEST, Ord.MedicalCase)

            '  BLG
            SetBLG(GroupORDER.BLG, Ord.MedicalCase)

            ExamNum += 1

        Next





        Return Msg
    End Function

    Public Shared Function GetWinStringMsg(Msg As IMessage) As String
        If Msg Is Nothing Then Return ""
        Dim p As New NHapi.Base.Parser.PipeParser
        Return p.Encode(Msg).Replace(vbCr, vbCrLf)
    End Function

End Class
