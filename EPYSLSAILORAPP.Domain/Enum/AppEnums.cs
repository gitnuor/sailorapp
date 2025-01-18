namespace EPYSLSAILORAPP.Domain
{
    public enum Status
    {
        All = 1,
        Pending = 2,
        Completed = 3,
        Proposed = 4,
        Approved = 5,
        Acknowledge = 6,
        PartiallyCompleted = 7,
        AllStatus = 8,
        Reject = 9,
        Revise = 10,
        Additional = 11,
        ReturnProposedPrice = 12,
        ReTest = 13,
        UnAcknowledge = 14,
        PendingReceiveCI = 15,
        PendingReceivePO = 16,
        UnApproved = 17,
        AwaitingPropose = 18,
        PendingBatch = 19,
        CPR = 20,
        FPR = 21,
        Active = 22,
        InActive = 23,
        Executed = 24,
        YDPPending = 25,
        YDPComplete = 26,
        YDQCPending = 27,
        YDQCComplete = 28,
        YDQCFail = 29,
        New = 30,
        Report = 31,
        Edit = 32,
        Check = 33,
        ProposedForApproval = 34,
        CheckReject = 35,
        ProposedForAcknowledge = 36,
        ProposedForAcknowledgeAcceptence = 37,
        AcknowledgeAcceptence = 38,
        Indent_Pending = 39,
        RejectReview = 40,
        Pre_Pending = 41,
        Post_Pending = 42,
        ReviseForAcknowledge = 43,
        Pass = 44,
        Fail = 45,
        Confirm = 46,
        Rework = 47,
        Hold = 48,
        PendingConfirmation = 49,
        PendingGroup = 50
    }

    public enum RepeatAfterEnum
    {
        /// <summary>
        /// No Repeat
        /// </summary>
        NoRepeat = 1,

        /// <summary>
        /// Repeat Every Year
        /// </summary>
        EveryYear = 2,

        /// <summary>
        /// Every Month
        /// </summary>
        EveryMonth = 3,

        /// <summary>
        /// Every Day
        /// </summary>
        EveryDay = 4
    }

    public enum Team_Category
    {

        Designer = 10,
        Merchandiser = 31,
        Marketing_Merchandising=28

    }

    public enum Enum_Role
    {

        Admin = 1,
        Designer = 2,
        BP = 3,
        Merchandiser = 4,
        SCM = 5,
        MCD = 6,

    }

    public enum Image_Position_Type
    {
        Front = 1,
        Back = 2,
        TechSheet = 3,
        Embellishment = 4,
        Measurement_Chart = 5
    }

    public enum Enum_gen_item_structure_group
    {
        Fabric = 1,
        Sewig_Accessories = 2,
        Finishing_Accessories = 3
    }

    public enum Enum_gen_item_structure_group_sub
    {
        Fabric = 1,
        Collar = 2,
        Cuff = 3,
        Sewing_Thread = 4,
        Button = 5,
        Zipper = 6,
        Hang_Tag = 7

    }

    public enum Enum_measurement_unit
    {
        Quantitative = 1,
        Length = 5,
        Liquid = 6,
        Weight = 7
    }

    public enum Enum_gen_segment
    {
        Construction = 3,
        Composition = 2,
        GSM = 4,

    }

    public enum Enum_PreCostingReview_Status
    {
        NewPreCosting = 1,
        MerchantRequestedForReview = 2,
        MerchantAckPreCosting = 3,
        ModifiedByDesigner = 4,

    }

    public enum Enum_PreCostingReviewApproval_Status
    {
        AckByDesigner = 1,
        ModifiedRequestByMerchant = 2,
        RejectByDesigner = 3,
        AckByMerchant = 4,

    }

    public enum Enum_Approval_Status
    {
        Pending = 1,
        Draft = 2,
        SendForApproval = 3,
        Approved = 4,
        Rejected = 5,

    }

    public enum Enum_Email_Template
    {
        Account_Created = 1,
        Password_Reset=2,

    }

}
