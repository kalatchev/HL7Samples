Imports NHapi.Base.Model

Public Class HL7Utils

    Private Shared Sub SetMSH(SegmentMSH As NHapi.Model.V251.Segment.MSH, Conf As ConfigData, MsgId As String)
        If SegmentMSH Is Nothing OrElse Conf Is Nothing Then Exit Sub
        With SegmentMSH
            .SendingApplication.NamespaceID.Value = Conf.OriginApp
            .SendingFacility.NamespaceID.Value = Conf.OriginFacility
            .ReceivingApplication.NamespaceID.Value = Conf.TargetApp
            .ReceivingFacility.NamespaceID.Value = Conf.TargetFacility
            .DateTimeOfMessage.Time.Value = Now().ToString("yyyyMMddHHmmss")
            .MessageType.MessageStructure.Value = "OML_O21"
            .MessageControlID.Value = MsgId
            .ProcessingID.ProcessingID.Value = "P" 'P=Production mode
        End With
    End Sub

    Private Shared Sub SetSFT(SegmentSFT As NHapi.Model.V251.Segment.SFT)
        If SegmentSFT Is Nothing Then Exit Sub
        With SegmentSFT
            .SoftwareProductName.Value = My.Application.Info.ProductName
            .SoftwareVendorOrganization.OrganizationName.Value = My.Application.Info.CompanyName
            .SoftwareCertifiedVersionOrReleaseNumber.Value = My.Application.Info.Version.ToString
            .SoftwareBinaryID.Value = My.Application.Info.Version.ToString
        End With
    End Sub

    Private Shared Sub SetNTE(SegmentNTE As NHapi.Model.V251.Segment.NTE, Note As String)
        With SegmentNTE
            .SetIDNTE.Value = "1" 'One and only one note
            .GetComment(0).Value = Note
        End With
    End Sub

    Private Shared Sub SetPID(SegmentPID As NHapi.Model.V251.Segment.PID, Ord As Order)
        If SegmentPID Is Nothing OrElse Ord Is Nothing Then Exit Sub

        SegmentPID.SetIDPID.Value = "1" 'First and only one PID

        If Ord.MedicalCase Is Nothing OrElse Ord.MedicalCase.Patient Is Nothing Then Exit Sub

        Dim Ind As Integer = 0

        'National identifier (ЕГН и ЕНЧ)
        If Ord.MedicalCase.Patient.PIDType = Constants.PIDTypes.EGN Then
            With SegmentPID.GetPatientIdentifierList(Ind)
                .IDNumber.Value = Ord.MedicalCase.Patient.PID.Trim
                .AssigningAuthority.NamespaceID.Value = "GRAO"
                .IdentifierTypeCode.Value = "NI"
            End With
            Ind += 1
        ElseIf Ord.MedicalCase.Patient.PIDType = Constants.PIDTypes.ENCh Then
            With SegmentPID.GetPatientIdentifierList(Ind)
                .IDNumber.Value = Ord.MedicalCase.Patient.PID.Trim
                .AssigningAuthority.NamespaceID.Value = "MVR"
                .IdentifierTypeCode.Value = "NI"
            End With
            Ind += 1
        End If

        'TODO: Set ambulatory/hospital MR

        'Medical record (ИЗ)
        With SegmentPID.GetPatientIdentifierList(Ind)
            .IDNumber.Value = Ord.MedicalCase.CaseNumber
            .AssigningAuthority.NamespaceID.Value = "Hospital"
            .IdentifierTypeCode.Value = "MR"
        End With

        Ind += 1


        ''Medical record (Амбулаторен номер)
        'If Not String.IsNullOrWhiteSpace(BizPatient.AmbulatoryNumber) Then
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.AmbulatoryNumber.Trim
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).AssigningAuthority.NamespaceID.Value = "Ambulatory"
        '    Msg.PATIENT.PID.GetPatientIdentifierList(Ind).IdentifierTypeCode.Value = "MR"
        '    Ind += 1
        'End If

        'Medical record (ID на пациента в БИС, може и да е буквено-цифров)
        With SegmentPID.GetPatientIdentifierList(Ind)
            .IDNumber.Value = Ord.MedicalCase.Id.ToString
            .AssigningAuthority.NamespaceID.Value = "HIS"
            .IdentifierTypeCode.Value = "XX"
        End With
        Ind += 1


        'Patient names - given, middle and family
        With SegmentPID.GetPatientName(0)
            .GivenName.Value = Ord.MedicalCase.Patient.GivenName
            .SecondAndFurtherGivenNamesOrInitialsThereof.Value = Ord.MedicalCase.Patient.MiddleName
            .FamilyName.Surname.Value = Ord.MedicalCase.Patient.FamilyName
        End With

        'Patient date of birth
        If Ord.MedicalCase.Patient.DateOfBirth.HasValue Then
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
        If SegmentPV1 Is Nothing Then Exit Sub

        'PV.1 Set ID
        SegmentPV1.SetIDPV1.Value = "1" 'First and only one PV1

        If Ord.MedicalCase Is Nothing OrElse Ord.MedicalCase.Patient Is Nothing Then Exit Sub

        'PV1.2 Patient Class
        'TODO: PV1.2 (inpatient/outpatient)!
        SegmentPV1.PatientClass.Value = "I" 'Inpatient

        With SegmentPV1.AssignedPatientLocation
            'PV1.3.1 Point Of Care
            .PointOfCare.Value = Ord.MedicalCase.Ward.Name

            'PV1.3.2 Room
            .Room.Value = Ord.MedicalCase.RoomCode

            'PV1.3.3 Bed
            .Bed.Value = Ord.MedicalCase.BedCode

            'PV1.3.4 Facility
            .Facility.NamespaceID.Value = Ord.MedicalCase.Ward.Code
        End With

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

        'ORC.2.1 Order ID - required
        SegmentORC.PlacerOrderNumber.EntityIdentifier.Value = Ord.OrderId

        'ORC.7.4 Quantity/Timing - Start Date/Time
        If Ord.FutureVistTime.HasValue Then
            SegmentORC.GetQuantityTiming(0).StartDateTime.Time.Value = Ord.FutureVistTime.Value.ToString("yyyyMMddHHmm")
        End If

        'ORC.12. Ordering Provide - Ordering doctor
        With SegmentORC.GetOrderingProvider(0)
            .IDNumber.Value = Ord.OrderingDoctor.UIN
            .GivenName.Value = Ord.OrderingDoctor.GivenName
            .SecondAndFurtherGivenNamesOrInitialsThereof.Value = Ord.OrderingDoctor.MiddleName
            .FamilyName.Surname.Value = Ord.OrderingDoctor.FamilyName
            .PrefixEgDR.Value = Ord.OrderingDoctor.Title
            .AssigningAuthority.NamespaceID.Value = "BLS"
            .IdentifierTypeCode.Value = "DN"
        End With



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
        If SegmentOBR Is Nothing OrElse Exm Is Nothing Then Exit Sub

        'OBR.1 - Set ID
        SegmentOBR.SetIDOBR.Value = Num.ToString

        'OBR.2 -Placer Order Number
        SegmentOBR.PlacerOrderNumber.EntityIdentifier.Value = OrderId
        If Not String.IsNullOrEmpty(OrdNamespace) Then
            SegmentOBR.PlacerOrderNumber.NamespaceID.Value = OrdNamespace
        End If

        'OBR.4 - Universal Service Identifier
        With SegmentOBR.UniversalServiceIdentifier
            .Identifier.Value = Exm.LoincCode.Trim
            .Text.Value = Exm.Name.ToString
            .NameOfCodingSystem.Value = "LN" 'Loinc
            .AlternateIdentifier.Value = Exm.HisId
            .NameOfAlternateCodingSystem.Value = "HCPT" 'HIS code
        End With

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
        SetSFT(Msg.AddSFT)

        'NTE (optional)
        If Not String.IsNullOrWhiteSpace(Ord.Note) Then
            SetNTE(Msg.AddNTE(), Ord.Note)
        End If

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

    Public Shared Function GetStringMsg(Msg As IMessage) As String
        If Msg Is Nothing Then Return ""
        Dim p As New NHapi.Base.Parser.PipeParser
        Return p.Encode(Msg)
    End Function

    Public Shared Function GetWinStringMsg(Msg As IMessage) As String
        Return GetStringMsg(Msg).Replace(vbCr, vbCrLf)
    End Function

End Class
