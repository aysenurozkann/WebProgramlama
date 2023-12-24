namespace HastaneRandevu_Y225012153.Models.Domain
{
    public class CalismaSaati
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public DateTime CalismaZamani { get; set; }

        public string DoctorAdi { get; set; }
    }
}
