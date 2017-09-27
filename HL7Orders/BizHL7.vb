
Public Class BizHL7

    Dim mParser As New NHapi.Base.Parser.PipeParser

    Private Sub FillMSH(Msg As NHapi.Model.V251.Message.OML_O21, BizOrder As Order)
        Msg.MSH.SendingApplication.NamespaceID.Value = ConfigData.Instance.OriginApp
        Msg.MSH.SendingFacility.NamespaceID.Value = ConfigData.Instance.OriginFacility
        Msg.MSH.ReceivingApplication.NamespaceID.Value = ConfigData.Instance.TargetApp
        Msg.MSH.ReceivingFacility.NamespaceID.Value = ConfigData.Instance.TargetFacility
        Msg.MSH.DateTimeOfMessage.Time.Value = Now().ToString("yyyyMMddHHmmss")
        Msg.MSH.MessageType.MessageStructure.Value = "OML_O21"
        Msg.MSH.MessageControlID.Value = BizOrder.MessageId
        Msg.MSH.ProcessingID.ProcessingID.Value = "P" 'P=Production mode
    End Sub

    Private Sub FillPID(ModelPatient As NHapi.Model.V251.Group.OML_O21_PATIENT, BizPatient As Patient)
        Dim Ind As Integer = 0

        'National identifier (ЕГН или ЕНЧ)
        If Not String.IsNullOrWhiteSpace(BizPatient.PID) AndAlso (BizPatient.PIDType = Constants.PIDTypes.EGN OrElse BizPatient.PIDType = Constants.PIDTypes.ENCh) Then
            ModelPatient.PID.GetPatientIdentifierList(Ind).IDNumber.Value = BizPatient.PID.Trim
            'GRAO=ЕГН, MVR=ЕНЧ
            If BizPatient.PIDType = Constants.PIDTypes.EGN Then
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
            Case Constants.Actions.Update
                Ord.ORC.OrderControl.Value = "XO"
            Case Constants.Actions.Cancel
                Ord.ORC.OrderControl.Value = "CA"
            Case Else
                'New by default
                Ord.ORC.OrderControl.Value = "NW"
        End Select

        Ord.ORC.PlacerOrderNumber.EntityIdentifier.Value = BizOrder.OrderId.Trim

        'Ordering doctor
        Ord.ORC.GetOrderingProvider(0).IDNumber.Value = BizOrder.OrderingDoctor.UIN
        Ord.ORC.GetOrderingProvider(0).GivenName.Value = BizOrder.OrderingDoctor.GivenName
        Ord.ORC.GetOrderingProvider(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value = BizOrder.OrderingDoctor.MiddleName
        Ord.ORC.GetOrderingProvider(0).FamilyName.Surname.Value = BizOrder.OrderingDoctor.FamilyName
        Ord.ORC.GetOrderingProvider(0).PrefixEgDR.Value = BizOrder.OrderingDoctor.Title
        Ord.ORC.GetOrderingProvider(0).AssigningAuthority.NamespaceID.Value = "BLS"
        Ord.ORC.GetOrderingProvider(0).IdentifierTypeCode.Value = "DN"
        ' Ord.ORC.GetOrderingProvider(0).DegreeEgMD.Value = "MD" 'Only MD is applicable for doctors

        'Date and time to visit (in case of future order, do not set in case of supplemental order)
        If BizOrder.ToVisit.HasValue Then
            Ord.ORC.GetQuantityTiming(0).StartDateTime.Time.Value = BizOrder.ToVisit.Value.ToString("yyyyMMddHHmm")
        End If

        'Ord.AddTIMING()
        'Ord.TIMINGs(0).TQ1.SetIDTQ1.Value = "1"
        'Select Case BizOrder.Priority
        '    Case Constants.Priorities.Normal
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "R"
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "Routine"
        '    Case Constants.Priorities.Emergency
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "S"
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "STAT"
        '    Case Else
        '        'Default priority
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Identifier.Value = "R"
        '        Ord.TIMINGs(0).TQ1.GetPriority(0).Text.Value = "Routine"
        'End Select


        'NTE --------------------------------------------
        'Note
        'Ord.OBSERVATION_REQUEST().AddNTE()
        'Ord.OBSERVATION_REQUEST().NTEs(0).SetIDNTE.Value = "1"
        'Ord.OBSERVATION_REQUEST().NTEs(0).GetComment(0).Value = "Пациента има калцирали вени!"

    End Sub

    Private Sub FillPV1(Visit As NHapi.Model.V251.Group.OML_O21_PATIENT_VISIT, BizOrder As Order)
        'Patient location
        'TODO: Fix patient class value
        Visit.PV1.PatientClass.Value = "I" 'Inpatient
        Visit.PV1.AssignedPatientLocation.PointOfCare.Value = BizOrder.Location.WardName
        Visit.PV1.AssignedPatientLocation.Room.Value = BizOrder.Location.Room
        Visit.PV1.AssignedPatientLocation.Bed.Value = BizOrder.Location.Bed
        Visit.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = BizOrder.Location.WardCode

    End Sub

    Public Function GenerateOrder(BizOrder As Order) As NHapi.Base.Model.IMessage

        Dim Msg As New NHapi.Model.V251.Message.OML_O21

        'MSH (required)
        Me.FillMSH(Msg, BizOrder)

        'SFT (optional, debug info)
        Dim s As NHapi.Model.V251.Segment.SFT = Msg.AddSFT()
        With s
            .SoftwareProductName.Value = My.Application.Info.ProductName
            .SoftwareVendorOrganization.OrganizationName.Value = My.Application.Info.CompanyName
            .SoftwareCertifiedVersionOrReleaseNumber.Value = My.Application.Info.Version.ToString
            .SoftwareBinaryID.Value = My.Application.Info.Version.ToString
        End With

        'NTE (optional)
        If Not String.IsNullOrWhiteSpace(BizOrder.Note) Then
            Dim nt As NHapi.Model.V251.Segment.NTE = Msg.AddNTE()
            With nt
                .SetIDNTE.Value = "1" 'One and only one note
                .GetComment(0).Value = BizOrder.Note
            End With
        End If

        'PATIENT Group (required)
        Msg.PATIENT.PID.SetIDPID.Value = "1" 'First and only one Patient
        'PID 
        Me.FillPID(Msg.PATIENT, BizOrder.Patient)

        'PV1 
        Msg.PATIENT.PATIENT_VISIT.PV1.SetIDPV1.Value = "1" 'First and only one Visit
        Me.FillPV1(Msg.PATIENT.PATIENT_VISIT, BizOrder)


        Dim ExamInd As Integer = 0
        For Each CurExam As Examination In BizOrder.Examinations
            If Msg.ORDERs(ExamInd) Is Nothing Then Msg.AddORDER()
            Me.FillORC(Msg.ORDERs(ExamInd), BizOrder)
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.SetIDOBR.Value = (ExamInd + 1).ToString
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.PlacerOrderNumber.EntityIdentifier.Value = BizOrder.OrderId.Trim
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.PlacerOrderNumber.NamespaceID.Value = ConfigData.Instance.OrderNumberNamespace
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Identifier.Value = CurExam.LoincCode.Trim
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Text.Value = CurExam.Name.ToString
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = "LN" 'Loinc
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.AlternateIdentifier.Value = CurExam.HisId
            Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.NameOfAlternateCodingSystem.Value = "HCPT" 'HIS code

            Dim DiagId As Integer = 1
            For Each dg In BizOrder.Diagnosis
                Dim d As NHapi.Model.V251.Segment.DG1 = Msg.ORDERs(ExamInd).OBSERVATION_REQUEST.AddDG1()
                With d
                    .SetIDDG1.Value = DiagId.ToString
                    .DiagnosisCodeDG1.Identifier.Value = dg.Key
                    .DiagnosisCodeDG1.NameOfCodingSystem.Value = "I10" 'ICD-10
                    .DiagnosisCodeDG1.Text.Value = dg.Value
                    .DiagnosisType.Value = "W" 'Working
                End With
                DiagId += 1
            Next

            Msg.ORDERs(ExamInd).BLG.AccountID.IDNumber.Value = "201601"
            Msg.ORDERs(ExamInd).BLG.AccountID.AssigningAuthority.NamespaceID.Value = "Hospital"
            Msg.ORDERs(ExamInd).BLG.AccountID.IdentifierTypeCode.Value = "XX" 'Organization identifier

            ExamInd += 1
        Next



        Return Msg
    End Function


End Class
