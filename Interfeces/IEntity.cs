namespace ConsoleBank_1.Interfeces
{
    /// <summary>
    /// Интерфейс моделей с идентификатором
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
