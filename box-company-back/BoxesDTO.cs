namespace box_company_back
{
    public class BoxesDTO
    {
        public class BoxCreateDto
        {
            public string BoxName { get; set; }
            public string Material { get; set; }
            public decimal Width { get; set; } 
            public decimal Height { get; set; } 
            public decimal Depth { get; set; } 
        }

        public class BoxUpdateDto
        {
            public int BoxId { get; set; } 
            public string BoxName { get; set; }
            public string Material { get; set; }
            public decimal Width { get; set; } 
            public decimal Height { get; set; } 
            public decimal Depth { get; set; } 
        }
    }
}