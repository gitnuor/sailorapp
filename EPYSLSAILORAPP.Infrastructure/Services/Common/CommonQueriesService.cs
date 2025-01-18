using EPYSLSAILORAPP.Domain.Statics;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public static class CommonQueriesService
    {
        public static string GetContactsByCatetoryId(int categoryId)
        {
            return GetContactsByCatetoryId(categoryId, ContactsName_ShortName.Name);
        }
        public static string GetContactsByCatetoryId(int categoryId, string name)
        {
            return $@"SELECT CAST(C.ContactID AS VARCHAR) AS id, C.{name} AS text
                FROM Contacts C
                INNER JOIN ContactCategoryChild CCC ON C.ContactID = CCC.ContactID
                INNER JOIN ContactCategoryHK CHK ON CCC.ContactCategoryID = CHK.ContactCategoryID
                WHERE CHK.ContactCategoryID = {categoryId}
                ORDER BY C.{name}";
        }
        public static string GetContactsByCategoryType(string contactCategory)
        {
            return GetContactsByCategoryType(contactCategory, ContactsName_ShortName.Name);
        }

        public static string GetContactsByCategoryType(string contactCategory, string name)
        {
            return
                $@"Select Cast(C.ContactID As varchar) [id], C.{name} [text]
                From Contacts C
                Inner Join ContactCategoryChild CCC On C.ContactID = CCC.ContactID
                Inner Join ContactCategoryHK CC ON CC.ContactCategoryID = CCC.ContactCategoryID
                Where CC.ContactCategoryName = '{contactCategory}'
                Order By C.{name}";
        }

        public static string GetEntityTypesByEntityTypeId(int entityTypeId)
        {
            return GetEntityTypesByEntityTypeId(entityTypeId, "");
        }
        public static string GetEntityTypesByEntityTypeId(int entityTypeId, string exceptValue)
        {
            return
                $@"SELECT CAST(ValueID AS VARCHAR) id, ValueName text
                        FROM EntityTypeValue
                        WHERE EntityTypeID = {entityTypeId} AND ValueName <> '{exceptValue}'
                        ORDER BY ValueName";
        }

        public static string GetEntityTypesByEntityTypeName(string entityTypeName)
        {
            return GetEntityTypesByEntityTypeName(entityTypeName, "");
        }
        public static string GetEntityTypesByEntityTypeName(string entityTypeName, string exceptValue)
        {
            return
                $@"SELECT CAST(ValueID AS VARCHAR) id, ValueName text
                FROM EntityTypeValue EV
                Inner Join EntityType ET On EV.EntityTypeID = ET.EntityTypeID
                WHERE ET.EntityTypeName = '{entityTypeName}' AND ValueName <> 'Select'
                ORDER BY ValueName";
        }

        /// <summary>
        /// Get entity type values by type name.
        /// </summary>
        /// <param name="entityTypeName"></param>
        /// <returns>Returns array of entity type values.</returns>
        public static string GetEntityTypeByTypeNameOnlyValues(string entityTypeName)
        {
            return $@"
                Select EV.ValueName
                From EntityTypeValue EV
                Inner Join EntityType ET On EV.EntityTypeID = ET.EntityTypeID
                Where ET.EntityTypeName = '{entityTypeName}'
                Order By EV.ValueName";
        }

        public static string GetAllLocation()
        {
            return
                $@"Select CAST(L.LocationID AS VARCHAR) id, L.LocationName [text]
                From Location L";
        }

        public static string GetAllUnit()
        {
            return $@"SELECT CAST(UnitID AS VARCHAR) AS id, DisplayUnitDesc AS text
                  FROM Unit";
        }

        /// <summary>
        /// Get all item segment values.
        /// </summary>
        /// <returns></returns>
        public static string GetItemSegments()
        {
            return $@"SELECT CAST(ISV.SegmentValueID As varchar) [id], ISV.SegmentValue [text], CAST(ISN.SegmentNameID As varchar) [desc]
                FROM ItemSegmentName ISN
                INNER JOIN ItemSegmentValue ISV ON ISN.SegmentNameID = ISV.SegmentNameID
                WHERE ISNULL(ISV.SegmentValue, '') <> ''
                ORDER BY ISV.SegmentValue";
        }

        /// <summary>
        /// Get all item segment values with segment name as description.
        /// </summary>
        /// <returns></returns>
        public static string GetItemSegmentValuesWithSegmentName()
        {
            return $@"SELECT CAST(ISV.SegmentValueID As varchar) [id], ISV.SegmentValue [text], ISN.SegmentName [desc]
                FROM ItemSegmentName ISN
                INNER JOIN ItemSegmentValue ISV ON ISN.SegmentNameID = ISV.SegmentNameID
                WHERE ISNULL(ISV.SegmentValue, '') <> ''
                ORDER BY ISN.SegmentName";
        }

        /// <summary>
        /// Get item segment values with segment name as description, by segment names passed as an array as query parameter.
        /// For example in below query we have parameter '@SegmentNames. Parameter must be same as query parameter without '@'.
        /// That's why our params object should be like params = { SegmentNames = ['', ''] }
        /// </summary>
        /// <returns></returns>
        public static string GetItemSegmentValuesBySegmentNamesWithSegmentName()
        {
            return $@"SELECT CAST(ISV.SegmentValueID As varchar) [id], ISV.SegmentValue [text], ISN.SegmentName [desc]
                FROM ItemSegmentName ISN
                INNER JOIN ItemSegmentValue ISV ON ISN.SegmentNameID = ISV.SegmentNameID
                WHERE ISN.SegmentName In @SegmentNames And ISNULL(ISV.SegmentValue, '') <> ''
                ORDER BY ISN.SegmentName";
        }

        /// <summary>
        /// Get item segment values by segment name ids with segment name id as description.
        /// </summary>
        /// <param name="segmentNameIds">Segment name ids should be passed as "1, 2, 3" etc</param>
        /// <returns></returns>
        public static string GetItemSegmentsBySegmentNameIds(string segmentNameIds)
        {
            return $@"SELECT CAST(ISV.SegmentValueID As varchar) [id], ISV.SegmentValue [text], CAST(ISN.SegmentNameID As varchar) [desc]
                FROM ItemSegmentName ISN
                INNER JOIN ItemSegmentValue ISV ON ISN.SegmentNameID = ISV.SegmentNameID
                WHERE ISN.SegmentNameID In({segmentNameIds}) AND ISNULL(ISV.SegmentValue, '') <> ''
                ORDER BY ISV.SegmentValue";
        }

        /// <summary>
        /// Get item segment values of an Item Segment.
        /// </summary>
        /// <param name="segmentName">Segment name.</param>
        /// <returns>Sql query.</returns>
        public static string GetItemSegmentValuesBySegmentName(string segmentName)
        {
            return $@"Select CAST(ISV.SegmentValueID AS nvarchar) id, ISV.SegmentValue [text]
                From ItemSegmentValue ISV
                Inner Join ItemSegmentName ISN On ISV.SegmentNameID = ISN.SegmentNameID
                Where ISN.SegmentName = '{segmentName}'";
        }
        public static string GetItemSegmentValuesBySubGroupId(int itemSubGroupId)
        {
            return $@"Select Cast(ISV.SegmentValueID as varchar) id, ISV.SegmentValue [text], ISN.SegmentName [desc], Cast(ISN.SegmentNameId as varchar) [additionalValue]
            From ItemStructure IST
            Inner Join ItemSegmentName ISN On IST.SegmentNameID = ISN.SegmentNameID
            Inner Join ItemSegmentValue ISV On ISV.SegmentNameID = ISN.SegmentNameID
            Where IST.ItemSubGroupID = {itemSubGroupId}
            Order By IST.SegmentSeqNo";
        }

        /// <summary>
        /// Get Item Structure to display table.
        /// </summary>
        /// <param name="itemSubGroupId"></param>
        /// <returns>Data of type <see cref="ItemStructureDTO"/></returns>
        public static string GetItemStructureBySubGroupId(int itemSubGroupId)
        {
            return $@"Select ISN.SegmentName SegmentDisplayName
	                , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueDesc' SegmentValueDescName
	                , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueId' SegmentValueIdName
                From ItemStructure IST
                Left Join ItemSegmentName ISN On IST.SegmentNameID = ISN.SegmentNameID
                Where IST.ItemSubGroupID = {itemSubGroupId}
                Order By IST.SegmentSeqNo";
        }

        /// <summary>
        /// Get Item Structure to display table.
        /// </summary>
        /// <param name="itemSubGroupId">Item SubGroup Id</param>
        /// <returns>Data of type <see cref="ItemStructureDTO"/></returns>
        public static string GetFullItemStructureBySubGroupId(int itemSubGroupId)
        {
            return $@"Select IST.SegmentNameID, IST.AllowAdd, IST.IsNumericValue, ISN.SegmentName SegmentDisplayName
                    , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueDesc' SegmentValueDescName
                    , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueId' SegmentValueIdName
                    , IST.HasDefaultValue, IST.SegmentValueID
                From ItemStructure IST
                Left Join ItemSegmentName ISN On IST.SegmentNameID = ISN.SegmentNameID
                Where IST.ItemSubGroupID = {itemSubGroupId}
                Order By IST.SegmentSeqNo";
        }

        /// <summary>
        /// Get Item Structure to display table.
        /// </summary>
        /// <param name="subGroupName">Item Sub Group Name</param>
        /// <returns>Data of type <see cref="ItemStructureDTO"/></returns>
        public static string GetFullItemStructureBySubGroupName(string subGroupName)
        {
            return $@"Select IST.SegmentNameID, IST.AllowAdd, IST.IsNumericValue, ISN.SegmentName SegmentDisplayName
                    , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueDesc' SegmentValueDescName
                    , 'Segment' + Cast(ROW_NUMBER() Over (Order By IST.SegmentSeqNo) As varchar) + 'ValueId' SegmentValueIdName
                    , IST.HasDefaultValue, IST.SegmentValueID
                From ItemStructure IST
                Inner Join ItemSubGroup ISG On ISG.ItemSubGroupID = IST.ItemSubGroupID
                Left Join ItemSegmentName ISN On IST.SegmentNameID = ISN.SegmentNameID
                Where ISG.SubGroupName = '{subGroupName}'
                Order By IST.SegmentSeqNo";
        }

        public static string GetAllSuppliersBySubGroupName(string subGroupName)
        {
            return $@"Select Cast(C.ContactID as varchar) [id], C.ShortName [text]
                From SupplierItemGroupStatus ST
                Inner Join ContactBusinessType CBT On ST.ContactID = CBT.ContactID
                Inner Join BusinessType BT On BT.TypeID = CBT.TypeID
                Inner Join Contacts C On C.ContactID = ST.ContactID
                Inner Join ContactCategoryChild CCC On CCC.ContactID = ST.ContactID
                Inner Join ContactCategoryHK CHK On CHK.ContactCategoryID = CCC.ContactCategoryID
                INNER JOIN ItemSubGroup I ON I.ItemSubGroupID = ST.ItemSubGroupID
                Where BT.TypeName = 'Manufacturer' And CHK.ContactCategoryName = '{ContactCategoryNames.SUPPLIER}' AND  I.SubGroupName = '{subGroupName}'";
        }

        public static string GetSupplierDetailsBySubGroupName(string subGroupName)
        {
            return $@"Select
                    C.Name Supplier, C.ShortName, C.AgentName, C.Officeaddress, C.FactoryAddress, C.PhoneNo, C.FaxNo, C.EmailNo, C.ContactPerson, CAI.VATRegNo, CAI.TradeLicenceNo, CAI.BOIRegNo, CAI.BondedWareHouse,
                    CAI.BWHExpiryDate, CAI.InHouse, CAI.InLand, CAI.EPZ, CAI.TransportFacility, CAI.HasNotifyParty, CAI.DiscountPercent, CAI.PaymentType, CAI.CreditDays, CAI.CreditLimit, CAI.Commission, CAI.LCStatus,
                    ETV.ValueName PortOfLoading, POD.ValueName PortOfDischarge, SM.ValueName ShipmentMode, PaymentTermsName = dbo.[fnPaymentTermsNameByContactID] (C.ContactID), IncoTermsName = dbo.fnIncoTermsNameByContactID(C.ContactID),
                    Cty.CountryName Country 
                    From SupplierItemGroupStatus ST
                    Inner Join ContactBusinessType CBT On ST.ContactID = CBT.ContactID
                    Inner Join BusinessType BT On BT.TypeID = CBT.TypeID
                    Inner Join Contacts C On C.ContactID = ST.ContactID
                    Inner Join ContactAdditionalInfo CAI On CAI.ContactID = C.ContactID
                    Inner Join ContactCategoryChild CCC On CCC.ContactID = ST.ContactID
                    Inner Join ContactCategoryHK CHK On CHK.ContactCategoryID = CCC.ContactCategoryID
                    INNER JOIN ItemSubGroup I ON I.ItemSubGroupID = ST.ItemSubGroupID
                    Left Join EntityTypeValue etv On etv.ValueID = CAI.PortOfLoadingID
                    Left Join EntityTypeValue POD On POD.ValueID = CAI.PortOfDischargeID
                    Left Join EntityTypeValue SM On SM.ValueID = CAI.ShipmentModeID
                    Left Join Country cty On cty.CountryID = C.CountryID
                    Where BT.TypeName = 'Manufacturer' And CHK.ContactCategoryName = '{ContactCategoryNames.SUPPLIER}' AND  I.SubGroupName = '{subGroupName}'";
        }
        public static string GetRackListByLocationID(int locationId)
        {
            return $@"SELECT  cast(RackID as varchar) id,RackNo [text]
                    FROM Rack
                    ----WHERE LocationID={locationId}
                    ORDER BY Sequence";
        }
        public static string GetRackListByCompanyID(int companyId)
        {
            return $@"SELECT  cast(R.RackID as varchar) id,R.RackNo [text]
                    From Rack R
                    Inner Join Location L On L.LocationID = R.LocationID
                    Inner Join WareHouse W On W.WareHouseID = L.WareHouseID
                    Where W.CompanyID = {companyId}
                    ORDER BY R.Sequence";
        }
        public static string GetLocationListByCompanyID(int companyId)
        {
            return $@"SELECT  cast(L.LocationID as varchar) id, L.ShortName [text]
                    From Location L 
                    Inner Join WareHouse W On W.WareHouseID = L.WareHouseID
                    Where W.CompanyID = {companyId}";
        }
        public static string GetEmployeeByDepartmentAndSectionList(string deptName, string sectionName)
        {
            return $@"SELECT e.DisplayEmployeeCode [id]
                        ,e.EmployeeName + ' (' + e.DisplayEmployeeCode + ')' [text]
                    FROM Employee e
                    Inner Join EmployeeDepartment d on d.DepertmentID = e.DepertmentID
                    Inner Join EmployeeSection s on s.SectionID = e.SectionID
                    where d.DepertmentDescription = '{deptName}' And s.SectionName in ('{sectionName}')";
        }
        public static string GetEmployeeList()
        {
            return $@"SELECT [DisplayEmployeeCode] [id]
                        ,[EmployeeName] + ' (' + [DisplayEmployeeCode] + ')' [text]
                    FROM [Employee]";
        }
        public static string GetCompany()
        {
            return $@"SELECT CAST(CompanyID AS VARCHAR) AS id, CompanyName AS text
            FROM CompanyEntity
			ORDER BY CompanyName";
        }
        public static string GetApplication()
        {
            return $@"SELECT CAST(ApplicationID AS VARCHAR) AS id, ApplicationName AS text
            FROM Application
			ORDER BY ApplicationName";
        }
        public static string GetUserType()
        {
            return $@"SELECT CAST(UserTypeID AS VARCHAR) AS id, UserType AS text
            FROM owin_userType_HK
			ORDER BY UserType";
        }
        public static string GetPaymentMethods(int contactId)
        {
            return $@"SELECT CAST(PM.PaymentMethodID AS VARCHAR) AS id, PM.PaymentMethodName AS text
                FROM PaymentMethod PM
                Inner Join ContactPaymentMethod CPM On PM.PaymentMethodID = CPM.PaymentMethodID
                WHERE ContactID = {contactId} AND PM.PaymentMethodID > 0
                GROUP BY PM.PaymentMethodID, PM.PaymentMethodName";
        }
        public static string GetIncoTermsBySupplier(int supplierId)
        {
            return $@"SELECT CAST(CIT.IncoTermsID AS VARCHAR) AS id, IT.IncoTermsName AS text
                FROM ContactIncoTerms CIT
                INNER JOIN ContactAdditionalInfo CAI ON CAI.ContactID = CIT.ContactID
                INNER JOIN IncoTerms IT ON IT.IncoTermsID = CIT.IncoTermsID
                WHERE CAI.ContactID = {supplierId}";
        }
        public static string GetPaymentTermsBySupplier(int supplierId)
        {
            return $@"SELECT CAST(CPT.PaymentTermsID AS VARCHAR) AS id, PT.PaymentTermsName AS text
                FROM ContactPaymentTerms CPT
                INNER JOIN PaymentTrems PT ON PT.PaymentTermsID = CPT.PaymentTermsID
                WHERE CPT.ContactID = {supplierId}";
        }
        public static string GetItemSubGroups()
        {
            return $@"Select Cast(ItemSubGroupID As varchar) id, SubGroupName [text]
                From ItemSubGroup";
        }
        public static string GetSupplierCountryOfOrigins(int supplierId)
        {
            return $@"
                SELECT CAST(CT.CountryID AS VARCHAR) AS id, C.CountryName AS text
                FROM Contacts CT
                INNER JOIN Country C ON C.CountryID = CT.CountryID
                WHERE ContactID = {supplierId}";
        }
        public static string GetItemSubGroupMailConfiguration(string subGroupName, string mailfor)
        {
            return $@"Select ISGMS.*
                    From ItemSubGroupMailSetup ISGMS
                    Inner Join ItemSubGroup ISG On ISG.ItemSubGroupID = ISGMS.ItemSubGroupID
                    Inner Join MailSetupFor MSF On MSF.SetupForID = ISGMS.SetupForID
                    Where ISG.SubGroupName='{subGroupName}' And MSF.SetupForName in (Select _ID From dbo.fnReturnStringArray('{mailfor}',','))";
        }
        public static string GetPortOfDischargeByCompany(int companyId)
        {
            return $@"Select CAST(ContactAddressID as varchar) [id], CA.ExternalID [text]
                From ContactAddress CA
                Inner Join Contacts C On C.ContactID = CA.ContactID
                Inner Join CompanyEntity CE On C.MappingCompanyID = CE.CompanyID
                Where CompanyID = {companyId}";
        }
        public static string GetFinancialYear()
        {
            return $@"Select CAST(Min(FinancialYearID) as varchar) [id], YearName [text]
                From FinancialYear
				Group By YearName
                Order By Min(FinancialYearID) Desc;
                ";
        }
        public static string GetCountry()
        {
            return $@"Select CAST(Min(CountryID) as varchar) [id], CountryName [text]
                From Country
				Group By CountryName;
                ";
        }
        public static string GetContactCategoryHK()
        {
            return $@"Select CAST(Min(ContactCategoryID) as varchar) [id], ContactCategoryName [text]
                    From ContactCategoryHK
                    Group By ContactCategoryName;
                    ";
        }
        public static string GetSeasonMaster()
        {
            return $@"Select CAST(SeasonID as varchar) [id], ShortName [text]
                From Season
                Order By ShortName;
                ";
        }
        public static string GetStyleGender()
        {
            return $@"Select CAST(StyleGenderID as varchar) [id], StyleGenderName [text]
                From StyleGender
                Order By StyleGenderName;
                ";
        }
        public static string GetStyleLabel()
        {
            return $@"Select CAST(StyleLabelID as varchar) [id], ShortName + '[' + ISNULL(LabelCode,'') + ']' [text]
                From StyleLabel
                Order By StyleLabelName;
                ";
        }
        public static string GetStyleMasterCategory()
        {
            return $@"Select CAST(StyleMasterCategoryID as varchar) [id], MasterCategoryName [text]
                From StyleMasterCategory
                Order By MasterCategoryName;
                ";
        }
        public static string GetStyleCategory()
        {
            return $@"Select CAST(StyleCategoryID as varchar) [id], StyleCategoryName [text]
                From StyleCategory
                Order By StyleCategoryName;
                ";
        }
    }

}
