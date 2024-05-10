namespace MyMvcProject.Models
{
    public class PartyGuest
    {
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public Guid UserId { get; set; }
    }

}
