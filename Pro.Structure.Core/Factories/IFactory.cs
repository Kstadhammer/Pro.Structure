namespace Pro.Structure.Core.Factories;

public interface IFactory<TEntity, TModel>
{
    TEntity CreateEntity(TModel model);
    TModel CreateModel(TEntity entity);
}
