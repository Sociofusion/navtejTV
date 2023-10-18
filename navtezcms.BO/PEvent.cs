using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace navtezcms.BO
{
    [Table("Event")]
    public class PEvent
    {
        [ExplicitKey]
        public Int64 ID { get; set; }

        public Int64 TitleContentID { get; set; }
        
        public Decimal Price { get; set; }

        public Decimal GST { get; set; }

        public Int32 ISFree { get; set; }

        public Int32 ISOnlineEvent { get; set; }

        public Int32 ISActive { get; set; }

        public DateTime? EventDate { get; set; }
        
        [Write(false)]
        public string EventDate_ddMMyyyy
        {
            get
            {
                if (EventDate.HasValue)
                {
                    return EventDate.Value.ToString("dd/MM/yyyy");
                }
                else
                { return ""; }
            }
        }

        public DateTime? RegistrationOpenTill { get; set; }
        [Write(false)]
        public string RegistrationOpenTill_ddMMyyyy
        {
            get
            {
                if (RegistrationOpenTill.HasValue)
                {
                    return RegistrationOpenTill.Value.ToString("dd/MM/yyyy");
                }
                else
                { return ""; }
            }
        }

        public DateTime? StartTime { get; set; }
        [Write(false)]
        public string StartTime_hhmm
        {
            get
            {
                if (StartTime.HasValue)
                {
                    return StartTime.Value.ToString("hh:mm tt");
                }
                else
                { return ""; }
            }
        }

        public DateTime? EndTime { get; set; }
        [Write(false)]
        public string EndTime_hhmm
        {
            get
            {
                if (EndTime.HasValue)
                {
                    return EndTime.Value.ToString("hh:mm tt");
                }
                else
                { return ""; }
            }
        }

        public String GoogleMapUrl { get; set; }

        public String Slug { get; set; }

        public Int64 ContactPNameContentID { get; set; }
        
        public Int64 DescriptionContentID { get; set; }

        public Int64 EventAddressContentID { get; set; }

        public String ContactPPhone { get; set; }

        public String ContactPEmail { get; set; }

        public String AttachmentLink { get; set; }


        [Write(false)]
        public List<PTranslation> TitleData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultTitleToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> DescriptionData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultDescriptionToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> EventAddressData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultEventAddressToDisplay { get; set; }

        [Write(false)]
        public List<PTranslation> ContactPNameData { get; set; } = new List<PTranslation>();

        [Write(false)]
        public string DefaultContactPNameToDisplay { get; set; }

        [Write(false)]
        public string Language { get; set; }

        [Write(false)]
        public int DefaultLanguageID { get; set; }

        [Write(false)]
        public PAsset pAsset { get; set; }

        [Write(false)]
        public string AssetFullUrl { get; set; }

    }
}
