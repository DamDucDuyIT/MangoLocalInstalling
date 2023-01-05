using System;

namespace MangoLocal.Models.ENRICHRCPModels
{
    public class Store
    {
        public int StoreId { get; set; }
        public string RoyaltyFee { get; set; }
        public string StoreName { get; set; }
        public string Logo { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactAddress { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContractId { get; set; }
        public string Comments { get; set; }
        public string DatabaseName { get; set; }
        public string Dbuser { get; set; }
        public string Dbpassword { get; set; }
        public string ServerId { get; set; }
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastChange { get; set; }
        public bool? StatusContract { get; set; }
        public string MangoCheckIn { get; set; }
        public string MangoTech { get; set; }
        public string MagonPos { get; set; }
        public string Website { get; set; }
        public string TotalEmail { get; set; }
        public string TotalMessenger { get; set; }
        public string MangoClient { get; set; }
        public string MangoManger { get; set; }
        public string CurrentSms { get; set; }
        public string CurrentEmail { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? NumPossystem { get; set; }
        public int? NumCheckInApp { get; set; }
        public bool? IsPlus { get; set; }
    }
}
