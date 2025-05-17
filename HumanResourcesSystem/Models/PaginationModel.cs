namespace HumanResourcesSystem.Models
{
    public class PaginationModel<T,V> where T : class where V : class
    {
        public PartialPaginationModel? PartialPaginationModel { get; set; }
        public List<T>? Dataset { get; set; }
        public V? Data { get; set; }
       
    }
}
