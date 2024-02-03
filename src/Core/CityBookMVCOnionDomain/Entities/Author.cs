namespace CityBookMVCOnionDomain.Entities
{
    public class Author : BaseNameableEntity
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string Tvit { get; set; }
        public string Inst { get; set; }
        public string VK { get; set; }
        public string Wp { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public string WebSite { get; set; }
        //Relational props
        public List<Blog>? Blogs { get; set; }



    }
}
