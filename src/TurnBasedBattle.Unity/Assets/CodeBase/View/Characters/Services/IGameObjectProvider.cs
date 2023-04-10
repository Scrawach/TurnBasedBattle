namespace CodeBase.View.Characters.Services
{
    public interface IGameObjectProvider
    {
        Character this[string id] { get; set; }
        void Remove(string id);
    }
}