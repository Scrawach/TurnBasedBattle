namespace CodeBase.View.Characters.Services
{
    public interface IGameObjectProvider
    {
        Character this[string id] { get; set; }
        bool Has(string id);
        void Remove(string id);
        void Destroy(string id);
    }
}